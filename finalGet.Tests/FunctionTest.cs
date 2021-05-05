using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using finalGet;

namespace finalGet.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestItem()
        {

            Item item = new Item();
            item.id = "BTC";
            Assert.True(item.id.Length <= 4);
        }
    }
}
