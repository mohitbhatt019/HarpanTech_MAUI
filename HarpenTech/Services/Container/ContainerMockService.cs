using HarpenTech.Models.Container;

namespace HarpenTech.Services.Container;

public class ContainerMockService : IContainerService
{
    public Task<ContainerInfo> GetContainerInfoAsync(string authToken)
    {
        throw new NotImplementedException();
    }
}