using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace CU.ApplicationIntegrationTests
{
    [CollectionDefinition(TestFixture.DbCollectionName)]
    public class DatabaseCollection : TestBed<TestFixture>
    {
        public DatabaseCollection(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base(testOutputHelper, fixture)
        {
        }

        protected override void Clear()
        {
        }

        protected override ValueTask DisposeAsyncCore()
        {
            return new ValueTask();
        }
    }
}
