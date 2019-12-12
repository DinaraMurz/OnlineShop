using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendInformationMessage(List<string> ids, string message);
    }
}
