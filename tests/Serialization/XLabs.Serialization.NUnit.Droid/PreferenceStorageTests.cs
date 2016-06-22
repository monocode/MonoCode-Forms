using XLabs.Serialization.JsonNET;
using NUnit.Framework;
using SecureStorageTests;
using XLabs.Platform.Services;
using XLabs.Serialization;

namespace Labs.Tests.Droid.Tests
{

    [TestFixture]
    public class PreferenceStorageTests : global::SecureStorageTests.SecureStorageTests
    {
        #region Overrides of SecureStorageTests

        protected override ISecureStorage Storage
        {
            get { return new SharedPreferencesStorage("PassW0rd"); }
        }

        protected override IByteSerializer Serializer
        {
            get { return new JsonSerializer(); }
        }

        #endregion
    }
}