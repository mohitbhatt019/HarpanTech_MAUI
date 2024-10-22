using HarpenTech.Services.Container;

namespace HarpenTech.Services.AppEnvironment
{
    // IAppEnvironmentService interface defines the contract for managing application environment settings
    public interface IAppEnvironmentService
    {
        // Gets the container service for handling container-related operations
        IContainerService ContainerService { get; }

        // Updates dependencies based on the specified flag indicating whether to use mock services
        void UpdateDependencies(bool useMockServices);
    }
}
