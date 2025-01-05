using Microsoft.Extensions.Logging;
using Moq;
using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Logging.Internal.Services;
using Xunit;

namespace OSK.Functions.Outputs.Logging.UnitTests.Internal.Services
{
    public class OutputFactoryTests
    {
        #region Variables

        private readonly Mock<ILogger<OutputFactoryTests>> _mockLogger;

        private readonly OutputFactory<OutputFactoryTests> _factory;

        #endregion

        #region Constructors

        public OutputFactoryTests()
        {
            _mockLogger = new Mock<ILogger<OutputFactoryTests>>();

            _factory = new OutputFactory<OutputFactoryTests>(_mockLogger.Object);
        }

        #endregion
    }
}
