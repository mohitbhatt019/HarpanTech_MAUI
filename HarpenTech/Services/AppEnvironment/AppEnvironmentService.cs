using HarpenTech.Services.Container;

namespace HarpenTech.Services.AppEnvironment
{
    // AppEnvironmentService class implements the IAppEnvironmentService interface for managing application environment settings
    public class AppEnvironmentService : IAppEnvironmentService
    {
        // Mock container service for testing purposes
        private readonly IContainerService _mockContainerService;

        // Actual container service used in the production environment
        private readonly IContainerService _containerService;

        // Gets the current container service based on the environment
        public IContainerService ContainerService { get; private set; }

        // Constructor for the AppEnvironmentService class
        public AppEnvironmentService(
            IContainerService mockContainerService, IContainerService containerService)
        {
            _mockContainerService = mockContainerService;
            _containerService = containerService;
        }

        // Updates dependencies based on the specified flag indicating whether to use mock services
        public void UpdateDependencies(bool useMockServices)
        {
            // Set the container service based on the specified environment
            ContainerService = useMockServices ? _mockContainerService : _containerService;
        }
    }
}
