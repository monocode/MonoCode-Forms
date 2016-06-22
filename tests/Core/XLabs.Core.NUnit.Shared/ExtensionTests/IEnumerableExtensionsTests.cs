using System;
using NUnit.Framework;
using XLabs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Xlabs.Core.ExtensionsTests
{
	[TestFixture]
	public class IEnumerableExtensionsTests
	{
		[Test]
		public void ConvertToReadonly()
		{
			IEnumerable<Color> myList = new List<Color>(){ Color.Blue, Color.White };
			IReadOnlyCollection<Color> myReadOnly = IEnumerableExtensions.ToReadOnlyCollection(myList);
			Assert.AreEqual (((List<Color>)myList).Count, myReadOnly.Count);
		}
		class dataClass
		{
			public string Name{ get; set; }
		}
		[Test]
		public void FluentForEach()
		{

			IEnumerable<dataClass> theOriginalData=new List<dataClass>{new dataClass(){Name="a"},new dataClass(){Name="b"},new dataClass(){Name="c"}};
			IEnumerable<dataClass> thechangedData= IEnumerableExtensions.ForEach<dataClass>(theOriginalData,delegate(dataClass obj) {obj.Name="New Name";});
			foreach(dataClass dc in thechangedData)
			{
				Assert.AreEqual("New Name",dc.Name);
			}
		}
	}
}

