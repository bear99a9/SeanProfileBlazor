
namespace SeanProfileBlazor.Services
{
    public interface IBlogService
    {
        Task<IList<BlogModel>> SaveFile(MultipartFormDataContent content);
    }
}