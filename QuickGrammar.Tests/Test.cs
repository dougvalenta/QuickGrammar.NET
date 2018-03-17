// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
using NUnit.Framework;
namespace QuickGrammar.Tests
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestCase()
        {
            Grammar g = new Grammar();
            Assert.IsNull(g);
        }
    }
}
