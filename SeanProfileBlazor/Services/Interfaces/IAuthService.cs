namespace SeanProfileBlazor.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> RegisterUser(UserRegister userRegister);
        Task<ServiceResponse<string>> Login(UserLogin userLogin);
    }
}
