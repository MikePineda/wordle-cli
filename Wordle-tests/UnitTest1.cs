using NUnit.Framework;
using System.Linq;
using Wordle.Services;

namespace Wordle_tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        [TestCase("wearxx")]
        [TestCase("asdasd")]
        [TestCase("wearxx")]
        [TestCase("xxvcdds")]
        [TestCase("wearxx")]
        [TestCase("csdcsdc")]
        [TestCase("zxczxc")]

        public void ItShouldFailIfUserWriteAWrongWord(string word)
        {
            //arrange
            var service = new WordleService();
            //act
            bool result = service.IsWordValid(word, word.Length);

            //assert
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase("weary")]
        [TestCase("buzzy")]
        [TestCase("pizza")]
        [TestCase("hello")]
        [TestCase("tizzy")]
        [TestCase("woman")]
        [TestCase("jammy")]

        public void ItShouldReturnTrueIfUserWriteACorrectWord(string word)
        {
            //arrange
            var service = new WordleService();
            //act
            bool result = service.IsWordValid(word, word.Length);

            //assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ItShouldReturnAnEmptyArray()
        {
            //arrange
            var service = new WordleService();
            var expected = new char[5, 5];
            //act
            var result = service.SetupEmptyBoard(5, 5);

            //assert
            Assert.IsTrue(expected.Rank == result.Rank && Enumerable.Range(0, expected.Rank).All(d => expected.GetLength(d) == result.GetLength(d)) && expected.Cast<char>().SequenceEqual(result.Cast<char>()));
        }
    }
}