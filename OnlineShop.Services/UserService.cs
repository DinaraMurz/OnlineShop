using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.DataAccess;
using OnlineShop.Domain;
using OnlineShop.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services
{
    public class UserService
    {
        private readonly OnlineShopContext context;
        private readonly string jwtSecret;

        public UserService(OnlineShopContext context, IOptions<SecretOptions> secretOptions)
        {
            this.context = context;
            this.jwtSecret = secretOptions.Value.JWTSecret;
        }

        public async Task<string> Authenticate(string phoneNumber, string code)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);

            if (user is null || String.IsNullOrEmpty(user.VerificationCode) || user.VerificationCode != code)
            {
                return null;
            }

            //https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, phoneNumber)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            //return tokenHandler.WriteToken(securityToken);
        }

        public async Task SaveUserCode(string phoneNumber, string code)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);

            if (user is null)
            {
                //user = new User
                //{
                //    PhoneNumber = phoneNumber,
                //    VerificationCode = code,
                //    Cart = new Cart(),
                //    FullName = "User-" + Guid.NewGuid().ToString(),
                //    FavoriteProducts = new List<FavoriteProduct>()
                //};
                //
                //await context.Users.AddAsync(user);

            }
            else
            {
                user.VerificationCode = code;
            }

            await context.SaveChangesAsync();
        }

        public async Task<bool> ConfirmUserCode(string phoneNumber, string code)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);

            if (user.VerificationCode == code)
            {
                user.IsConfirmed = true;
                return true;
            }

            user.IsConfirmed = false;
            return false;
        }
        public async Task Registation(string fullname, string phoneNumber, ICollection<FavoriteProduct> favoriteProduct, Cart cart)
        {
            var user = new User
            {
                FullName = fullname,
                PhoneNumber = phoneNumber,
                FavoriteProducts = favoriteProduct,
                Cart = cart
            };
        }
    }
}
