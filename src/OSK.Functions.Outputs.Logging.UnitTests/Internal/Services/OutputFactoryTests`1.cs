using Microsoft.Extensions.Logging;
using Moq;
using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Logging.Internal.Services;
using OSK.Functions.Outputs.Models;
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

        #region CreatePage

        [Fact]
        public void CreatePage_NullItems_ThrowsArgumentNullException()
        {
            // Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => _factory.CreatePage<int>(null, 0, 0, null));
        }

        [Fact]
        public void CreatePage_InvalidSkip_ThrowsArgumentOutOfRangeException()
        {
            // Arrange/Act/Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _factory.CreatePage([1, 2], -1, 1, null));
        }

        [Fact]
        public void CreatePage_InvalidTake_ThrowsArgumentOutOfRangeException()
        {
            // Arrange/Act/Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _factory.CreatePage([1, 2], 0, -1, null));
        }

        [Fact]
        public void CreatePage_Valid_ReturnsSuccessfully()
        {
            // Arrange
            int[] items = [1, 2, 3];
            var skip = 10;
            var take = 39;
            var total = 117;

            // Act
            var page = _factory.CreatePage(items, skip, take, total);

            // Assert
            Assert.Equal(items, page.Values);
            Assert.Equal(skip, page.Skip);
            Assert.Equal(take, page.Take);
            Assert.Equal(total, page.Total);
        }

        #endregion

        #region Validate

        [Fact]
        public void Validate_SuccessfulOutput_HasErrorInformation_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Validate(new Output(OutputStatusCode.Success,
                new ErrorInformation(new Exception()),
                null)));
        }

        [Fact]
        public void Validate_FailureOutput_HasNoErrorInformation_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Validate(new Output(
                new OutputStatusCode(OutputSpecificityCode.InvalidParameter),
                null,
                null)));
        }

        [Fact]
        public void Validate_FailureOutput_HasErrorInformationWithoutValidValues_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Validate(new Output(
                new OutputStatusCode(OutputSpecificityCode.InvalidParameter, OutputStatusCode.DefaultSource),
                new ErrorInformation(null),
                null)));
        }

        [Fact]
        public void Validate_FailureOutput_ValidError_ReturnsSuccessfully()
        {
            // Arrange/Act/Assert
            _factory.Validate(new Output(
                new OutputStatusCode(OutputSpecificityCode.Locked),
                new ErrorInformation(new Error("Hello World")),
                null));
        }

        [Fact]
        public void Validate_FailureOutput_ValidException_ReturnsSuccessfully()
        {
            // Arrange/Act/Assert
            _factory.Validate(new Output(
                new OutputStatusCode(OutputSpecificityCode.BadGateway),
                new ErrorInformation(new Exception("Hello world")),
                null));
        }

        [Fact]
        public void Validate_SuccessfulOutput_ValidSuccess_ReturnsSuccessfully()
        {
            // Arrange/Act/Assert
            _factory.Validate(new Output(OutputStatusCode.Success, null, null));
        }

        #endregion

        #region Validate(TValue)

        [Fact]
        public void Validate_TValue_SuccessfulOutput_HasErrorInformation_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Validate(new Output<int>(1,
                OutputStatusCode.Success,
                new ErrorInformation(new Exception()),
                null)));
        }

        [Fact]
        public void Validate_TValue_SuccessfulOutput_NullValue_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => _factory.Validate(new Output<int?>(null,
                OutputStatusCode.Success,
                null,
                null)));
        }

        [Fact]
        public void Validate_TValue_FailureOutput_HasNoErrorInformation_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Validate(new Output<int>(1,
                new OutputStatusCode(OutputSpecificityCode.InvalidParameter),
                null,
                null)));
        }

        [Fact]
        public void Validate_TValue_FailureOutput_HasErrorInformationWithoutValidValues_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Validate(new Output<int>(1,
                new OutputStatusCode(OutputSpecificityCode.InvalidParameter, OutputStatusCode.DefaultSource),
                new ErrorInformation(null),
                null)));
        }

        [Fact]
        public void Validate_TValue_FailureOutput_ValidError_ReturnsSuccessfully()
        {
            // Arrange/Act/Assert
            _factory.Validate(new Output<int?>(null,
                new OutputStatusCode(OutputSpecificityCode.Locked),
                new ErrorInformation(new Error("Hello World")),
                null));
        }

        [Fact]
        public void Validate_TValue_FailureOutput_ValidException_ReturnsSuccessfully()
        {
            // Arrange/Act/Assert
            _factory.Validate(new Output<int?>(null,
                new OutputStatusCode(OutputSpecificityCode.BadGateway),
                new ErrorInformation(new Exception("Hello world")),
                null));
        }

        [Fact]
        public void Validate_TValue_SuccessfulOutput_ValidSuccess_ReturnsSuccessfully()
        {
            // Arrange/Act/Assert
            _factory.Validate(new Output<int>(1, OutputStatusCode.Success, null, null));
        }

        #endregion
    }
}
