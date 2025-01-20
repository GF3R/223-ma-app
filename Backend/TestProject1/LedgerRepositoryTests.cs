using Xunit.Abstractions;

namespace LedgerTest
{
    public class LedgerRepositoryTests
    {
        private readonly ITestOutputHelper output;

        public LedgerRepositoryTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GetAllLedgers_ReturnsAllLedgers()
        {
            Assert.True(true, "Test not implemented");
        }
    }
}