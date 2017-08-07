using System;
using Moq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using XamarinAppV1.Models;

namespace UnitTest
{
    [TestClass]
    public class UnitTest_GuessWords
    {
        [TestMethod]
        public void ReadFile_Method()
        {
            GuessWords words = new GuessWords();

            var list = words.ReadFile();
            list.Count.Should().Be(5757);

        }
    }
}
