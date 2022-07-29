using KlickitTask.Data;
using KlickitTask.DTO;
using KlickitTask.Models;

namespace KlickitTask.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model, UserType role);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);//login
    }
}
