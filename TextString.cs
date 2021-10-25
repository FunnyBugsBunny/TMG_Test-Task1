using System.Collections.Generic;

namespace TMG_Test1
{
    public class TextString
    {
        public string text { get; }
        public int wordsQt { get; }
        public int vowelsQt { get; }
        private List<string> _lang;
        private string _onlyLetter;
        public TextString(string text)
        {
            langInit();
            this.text = text;
            _onlyLetter = text.Trim(new char[] { ',', '.', }).ToLower();
            this.wordsQt = SumWords();
            this.vowelsQt = SumVolwes();
        }

        /// <summary>
        /// Initialization array of languages
        /// Here you can add a new language
        /// </summary>
        private void langInit()
        {
            _lang = new List<string>();
            _lang.Add("aеiоu");             // Spanish + French
            _lang.Add("aeiouy");           // English
            _lang.Add("aeiouäöü");        // German
            _lang.Add("aeioóuyąę");      // Polish
            _lang.Add("ауоыиэяюёе");   // Russian
        }

        /// <summary>
        /// Calculation the number of words
        /// </summary>
        /// <returns></returns>
        private int SumWords()
        {
            int wordsQt = _onlyLetter.Split(new char[] { ' ', '\u2014' }).Length;
            return wordsQt;
        }

        /// <summary>
        /// Calculation the number of volwes
        /// </summary>
        /// <returns></returns>
        private int SumVolwes()
        {
            int vowelsQt = 0;
            for (int i = 0; i < _onlyLetter.Length; i++)
            {
                foreach (var item in _lang)
                {
                    if (item.Contains(_onlyLetter[i]))
                    {
                        vowelsQt += 1;
                        break;
                    }
                }
            }
            return vowelsQt;
        }
    }
}
