using NUnit.Framework;

namespace Infrastructure.Tests
{
    public class SequenceRecognitionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NotFound()
        {
            var sequence = "BBCBCD";
            var key = "BCA";

            var recognition = new SequenceRecognition<char>(sequence);

            var expected = RecognitionResult<string>.NotFound;
            var actual = recognition.Recognize(key);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindAtStart()
        {
            var sequence = "BBCBCD";
            var key = "BBC";
            var expectedIndex = 0;

            TestOneKey(sequence, key, expectedIndex);
        }

        [Test]
        public void FindInTheMiddle()
        {
            var sequence = "XCBCBDD";
            var key = "CBD";
            var expectedIndex = 3;

            TestOneKey(sequence, key, expectedIndex);
        }

        [Test]
        public void FindInTheEnd()
        {
            var sequence = "BBCBCD";
            var key = "BCD";
            var expectedIndex = 3;

            TestOneKey(sequence, key, expectedIndex);
        }

        [Test]
        public void FindFromSeveralKeys()
        {
            var sequence = "ABCDEFGH";
            var expectedKey = "CD";
            var keys = new[]
            {
                "BBC", 
                "ABD",
                "CDEFG",
                expectedKey
            };
            var expectedIndex = 2;

            TestSeveralKeys(sequence, expectedIndex, expectedKey, keys);
        }

        private void TestOneKey(string sequence, string key, int expectedIndex)
        {
            TestSeveralKeys(sequence, expectedIndex, key, key);
        }

        private void TestSeveralKeys(string sequence, int expectedIndex, string expectedKey, params string[] keys)
        {
            var recognition = new SequenceRecognition<char>(sequence);

            var expected = new RecognitionResult<string>(expectedIndex, expectedKey);
            var actual = recognition.Recognize(keys);

            Assert.AreEqual(expected, actual);
        }
    }
}