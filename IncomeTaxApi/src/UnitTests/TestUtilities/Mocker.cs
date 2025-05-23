using Moq;
using Moq.AutoMock;
using NUnit.Framework;

namespace IncomeTaxApi.UnitTests.TestUtilities
{
    /// <summary>
    /// Base class for tests that adds auto-mocking services.
    /// </summary>
    /// <typeparam name="TSubject">The type of the system under test</typeparam>
    public abstract class Mocker<TSubject> where TSubject : class
    {
        protected AutoMocker AutoMocker { get; private set; } = null!;

        /// <summary>
        /// The automatically generated system under test.
        /// </summary>
        protected TSubject Subject { get; private set; } = null!;

        [SetUp]
        public void BaseSetUp()
        {
            AutoMocker = new AutoMocker();

            SetUp();

            CreateSubject();
        }

        [TearDown]
        public void BaseTearDown()
        {
            if (Subject is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// Obtain an instance of an automatically generated mock dependency.
        /// Typically, this is for setup/verification purposes.
        /// </summary>
        /// <typeparam name="TMock">The type of the dependency being mocked</typeparam>
        protected Mock<TMock> GetMock<TMock>() where TMock : class
        {
            return AutoMocker.GetMock<TMock>();
        }

        /// <summary>
        /// Register specific instances for the subjects dependencies
        /// before subject itself is mocked
        /// </summary>
        protected virtual void SetUp()
        {
        }

        /// <summary>
        /// Allows derived test classes to recreate the subject, which is useful for situations where we want to
        /// configure the subject's dependencies as part of the Arrange section of the test.
        /// </summary>
        private void CreateSubject()
        {
            Subject = AutoMocker.CreateInstance<TSubject>();
        }
    }
}