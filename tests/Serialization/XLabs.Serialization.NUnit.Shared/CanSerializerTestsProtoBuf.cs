#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
#else
using NUnit.Framework;
#endif

namespace SerializationTests
{
    using XLabs.Serialization;

#if !WINDOWS_PHONE && !WINDOWS_PHONE_APP
    using XLabs.Serialization.ProtoBuf;
#endif

    [TestFixture()]
    public class CanSerializerTestsProtoBuf : CanSerializerTests
    {
        protected override ISerializer Serializer
        {
#if WINDOWS_PHONE && !WINDOWS_PHONE_APP
            get { return new ProtoBufSerializer(); }
#else
            get { return null; }
#endif
        }
    }
}
