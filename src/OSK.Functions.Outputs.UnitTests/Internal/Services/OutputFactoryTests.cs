using OSK.Functions.Outputs.Abstractions;
using OSK.Functions.Outputs.Internal.Services;
using Xunit;

namespace OSK.Functions.Outputs.UnitTests.Internal.Services
{
    public class OutputFactoryTests
    {
        #region Variables

        private readonly OutputFactory _factory;

        #endregion

        #region Constructors

        public OutputFactoryTests()
        {
            _factory = new OutputFactory();
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

        #region Create(TValue)

        [Fact]
        public void Create_TValue_SuccessfulOutput_HasErrorInformation_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(1, OutputSpecificityCode.Success,
                new ErrorInformation(new Exception()), OutputStatusCode.DefaultSource, null));
        }

        [Fact]
        public void Create_TValue_FailureOutput_HasNoErrorInformation_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(1, OutputSpecificityCode.InvalidParameter,
                null, OutputStatusCode.DefaultSource, null));
        }

        [Fact]
        public void Create_TValue_FailureOutput_HasErrorInformationWithoutValidValues_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(1, OutputSpecificityCode.InvalidParameter,
                new ErrorInformation(null), OutputStatusCode.DefaultSource, null));
        }

        [Fact]
        public void Create_TValue_FailureOutput_ValidError_ReturnsSuccessfully()
        {
            // Arrange
            var error = new ErrorInformation(new Error("Hello World"));
            var specificityCode = OutputSpecificityCode.Locked;
            var originationSource = "NoWay";

            // Act
            var output = _factory.Create(specificityCode, error, originationSource, null);

            // Assert
            Assert.False(output.IsSuccessful);
            Assert.Equal(specificityCode, output.StatusCode.SpecificityCode);
            Assert.Equal(originationSource, output.StatusCode.OriginationSource);
            Assert.NotNull(output.ErrorInformation?.Error);
            Assert.Equal(error.Error!.Value.Message, output.ErrorInformation.Value.Error.Value.Message);
        }

        [Fact]
        public void Create_TValue_FailureOutput_ValidException_ReturnsSuccessfully()
        {
            // Arrange
            var error = new ErrorInformation(new Exception("Hello world"));
            var specificityCode = OutputSpecificityCode.BadGateway;
            var originationSource = "NoWay";

            // Act
            var output = _factory.Create(specificityCode, error, originationSource, null);

            // Assert
            Assert.False(output.IsSuccessful);
            Assert.Equal(specificityCode, output.StatusCode.SpecificityCode);
            Assert.Equal(originationSource, output.StatusCode.OriginationSource);
            Assert.NotNull(output.ErrorInformation?.Exception);
            Assert.Equal(error.Exception!.Message, output.ErrorInformation.Value.Exception.Message);
        }

        [Fact]
        public void Create_TValue_FailureOutput_ValidSuccess_ReturnsSuccessfully()
        {
            // Arrange
            var specificityCode = OutputSpecificityCode.Success;
            var originationSource = "NoWay";

            // Act
            var output = _factory.Create(specificityCode, null, originationSource, null);

            // Assert
            Assert.True(output.IsSuccessful);
            Assert.Equal(specificityCode, output.StatusCode.SpecificityCode);
            Assert.Equal(originationSource, output.StatusCode.OriginationSource);
            Assert.Null(output.ErrorInformation?.Exception);
        }

        #endregion

        #region Create

        [Fact]
        public void Create_SuccessfulOutput_HasErrorInformation_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(OutputSpecificityCode.Success,
                new ErrorInformation(new Exception()), OutputStatusCode.DefaultSource, null));
        }

        [Fact]
        public void Create_FailureOutput_HasNoErrorInformation_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(OutputSpecificityCode.InvalidParameter,
                null, OutputStatusCode.DefaultSource, null));
        }

        [Fact]
        public void Create_FailureOutput_HasErrorInformationWithoutValidValues_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() => _factory.Create(OutputSpecificityCode.InvalidParameter,
                new ErrorInformation(null), OutputStatusCode.DefaultSource, null));
        }

        [Fact]
        public void Create_FailureOutput_ValidError_ReturnsSuccessfully()
        {
            // Arrange
            var error = new ErrorInformation(new Error("Hello World"));
            var specificityCode = OutputSpecificityCode.Locked;
            var originationSource = "NoWay";

            // Act
            var output = _factory.Create(specificityCode, error, originationSource, null);

            // Assert
            Assert.False(output.IsSuccessful);
            Assert.Equal(specificityCode, output.StatusCode.SpecificityCode);
            Assert.Equal(originationSource, output.StatusCode.OriginationSource);
            Assert.NotNull(output.ErrorInformation?.Error);
            Assert.Equal(error.Error!.Value.Message, output.ErrorInformation.Value.Error.Value.Message);
        }

        [Fact]
        public void Create_FailureOutput_ValidException_ReturnsSuccessfully()
        {
            // Arrange
            var error = new ErrorInformation(new Exception("Hello world"));
            var specificityCode = OutputSpecificityCode.BadGateway;
            var originationSource = "NoWay";

            // Act
            var output = _factory.Create(specificityCode, error, originationSource, null);

            // Assert
            Assert.False(output.IsSuccessful);
            Assert.Equal(specificityCode, output.StatusCode.SpecificityCode);
            Assert.Equal(originationSource, output.StatusCode.OriginationSource);
            Assert.NotNull(output.ErrorInformation?.Exception);
            Assert.Equal(error.Exception!.Message, output.ErrorInformation.Value.Exception.Message);
        }

        [Fact]
        public void Create_FailureOutput_ValidSuccess_ReturnsSuccessfully()
        {
            // Arrange
            var specificityCode = OutputSpecificityCode.Success;
            var originationSource = "NoWay";

            // Act
            var output = _factory.Create(specificityCode, null, originationSource, null);

            // Assert
            Assert.True(output.IsSuccessful);
            Assert.Equal(specificityCode, output.StatusCode.SpecificityCode);
            Assert.Equal(originationSource, output.StatusCode.OriginationSource);
            Assert.Null(output.ErrorInformation?.Exception);
        }

        #endregion

    }
}
