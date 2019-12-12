using OnlineShop.Domain;
using OnlineShop.DTO;
using System.Threading.Tasks;

namespace OnlineShop.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentServiceResponseDTO> CreateInvoice(Order order);
    }
}
