using ConsoleApp.Common;
using NUnit.Framework;
using System;

namespace ConsoleApp.UnitTest
{
    [TestFixture]
    public class HelperUnitTest
    {
        [Test]
        public void ReverseWords_Success()
        {
            // Arrange
            var words = "I like to go for a swim";

            // Act
            var actual = Utility.ReverseWords(words);

            // Assert
            Assert.AreEqual("swim a for go to like I", actual);
        }

        [Test]
        public void ReverseWords_ThrowsException_InputIsNullOrEmpty()
        {
            string words = null;

            // Act and Assert
            Assert.That(() => Utility.ReverseWords(words), 
                Throws.Exception.TypeOf<ArgumentNullException>().
                And.Message.EqualTo("Words input is required"));
        }
    }
}
