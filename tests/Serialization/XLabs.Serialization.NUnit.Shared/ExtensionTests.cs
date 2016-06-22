using System;
using System.Collections.Generic;
using System.Text;
using XLabs.Serialization;
#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
using Xamarin.Forms.Labs.Services.Serialization;
using TextSerializationTests;
#else
using NUnit.Framework;
#endif

namespace SerializationTests
{
    [TestFixture()]
    public abstract class ExtensionTests
    {
        protected abstract ISerializer Serializer { get; }
    }
}
