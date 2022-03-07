using Newtonsoft.Json.Linq;
using Wordle.Interfaces;

namespace Wordle.Services
{
    public class WordleService : IWordleService
    {
        public HashSet<string> AvailableWords = new HashSet<string>();
        public WordleService()
        {
            this.AvailableWords = GetEnglishWords();
        }
        public bool IsWordValid(string word, int desiredLength)
        {
            if (word == null || word.Length !=  desiredLength)
            {
                return false;
            }

            if(!this.AvailableWords.Contains(word))
            {
                return false;
            }


            return true;
        }


        public char[,] SetupEmptyBoard(int x, int y)
        {
            char[,] board = new char[x,y];


            return board;
        }


        private HashSet<string> GetEnglishWords()
        {
            HashSet<string> words = new HashSet<string>();
            using (StreamReader r = new StreamReader("words_dictionary.json"))
            {
                string jsonstr = r.ReadToEnd();
                JObject json = JObject.Parse(jsonstr);
                foreach(var property in json.Properties())
                {
                    //For now, let's just add the 5 letter words
                    if (property.Name.Length == 5)
                    {
                        words.Add(property.Name);
                    }
                }
            }
            return words;
        }
    }
}
