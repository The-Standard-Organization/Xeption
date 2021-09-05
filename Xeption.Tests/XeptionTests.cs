using Tynamix.ObjectFiller;
using Xunit;

namespace Xeption.Tests
{
    public class XeptionTests
    {
        private readonly Xeption xeption;

        public XeptionTests() =>
            this.xeption = new Xeption();

        [Fact]
        public void ShouldAppendListOfKeyValues()
        {
            // given
            Dictionary<string, List<string>> randomDictionary = 
                CreateRandomDictionary();

            Dictionary<string, List<string>> expectedDictionary =
                randomDictionary;

            var xeption = new Xeption();

            // when
            foreach(string key in randomDictionary.Keys)
            {
                randomDictionary[key].ForEach(value =>
                {
                    this.xeption.
                });
            }

            // then

        }

        private static Dictionary<string, List<string>> CreateRandomDictionary() =>
            new Filler<Dictionary<string, List<string>>>().Create();
    }
}