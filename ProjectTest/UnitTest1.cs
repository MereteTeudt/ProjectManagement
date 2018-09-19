using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidName()
        {
            Project 
        }
    }
}
