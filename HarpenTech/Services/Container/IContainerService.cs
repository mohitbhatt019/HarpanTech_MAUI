using HarpenTech.Models.Container;

namespace HarpenTech.Services.Container
{
    public interface IContainerService
    {
        Task<ContainerInfo> GetContainerInfoAsync(string authToken);
    }
}