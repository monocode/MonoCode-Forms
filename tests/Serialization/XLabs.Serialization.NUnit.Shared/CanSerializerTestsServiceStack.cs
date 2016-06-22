#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
#else
using NUnit.Framework;
using XLabs.Serialization.JsonNET;

#endif

namespace SerializationTests
{
    using XLabs.Serialization;

    [TestFixture()]
    public class CanSerializerTestsServiceStack : CanSerializerTests
    {
        protected override ISerializer Serializer
        {
            get { return new JsonSerializer(); }
        }
    }
}
