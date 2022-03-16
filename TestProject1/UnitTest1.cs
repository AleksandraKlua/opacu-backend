using System;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(5, 5);
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, 5);
        }
    }
}
