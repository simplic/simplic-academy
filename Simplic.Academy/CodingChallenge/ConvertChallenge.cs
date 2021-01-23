using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace CodingChallenge
{
    public class ConvertChallenge
    {
        [Fact]
        public void FixBug_Convert_01()
        {
            var value = "24 1";

            var intValue = int.Parse(value);

            Assert.Equal(241, intValue);
        }

        [Fact]
        public void FixBug_Convert_02()
        {
            var value = "24​1";

            var intValue = int.Parse(value);

            Assert.Equal(241, intValue);
        }

        [Fact]
        public void FixBug_Convert_03()
        {
            var value = "24.1";
            var culture = new CultureInfo("de-DE");

            var doubleValue = double.Parse(value, culture);

            Assert.Equal(24.1, doubleValue);
        }
    }
}
