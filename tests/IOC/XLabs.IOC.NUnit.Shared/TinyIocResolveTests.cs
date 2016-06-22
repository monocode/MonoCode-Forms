#if WINDOWS_PHONE
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
#else
using NUnit.Framework;
#endif

namespace IocTests
{
#if !WINDOWS_PHONE && !WINDOWS_PHONE_APP
    using TinyIoC;
    using XLabs.Ioc.TinyIOC;
#endif
    using XLabs.Ioc;

    [TestFixture()]
    public class TinyIocResolveTests : ResolveTests
    {
        protected override IResolver GetEmptyResolver()
        {
#if !WINDOWS_PHONE && !WINDOWS_PHONE_APP
            return new TinyResolver(new TinyIoCContainer());
#else
            return null;
#endif
        }

        protected override IDependencyContainer GetEmptyContainer()
        {
#if !WINDOWS_PHONE && !WINDOWS_PHONE_APP
            return new TinyContainer(new TinyIoCContainer());
#else
            return null;
#endif
        }
    }
}
