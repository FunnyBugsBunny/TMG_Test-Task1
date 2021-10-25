using Newtonsoft.Json;
using System;
using System.Net;
using System.Windows;

namespace TMG_Test1
{
    public class Repository
    {
        private int _index;
        public TextString[] textStrings;
        public Repository()
        {
            this._index = 0;
            this.textStrings = new TextString[1];
        }

        /// <summary>
        /// Fills the repository with data
        /// </summary>
        /// <param name="stringsId"></param>
        public void Load(params int[] stringsId)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("TMG-Api-Key", "0J/RgNC40LLQtdGC0LjQutC4IQ==");
                try
                {
                    foreach (var _stringId in stringsId)
                    {
                        string uri = string.Format("http://tmgwebtest.azurewebsites.net/api/textstrings/{0}", _stringId.ToString());
                        var response = client.DownloadString(uri);

                        var _text = JsonConvert.DeserializeObject<TextString>(response);
                        Add(_text);
                    }
                }
                catch(Exception ex)
                {
                    var mes = string.Format("Произошла ошибка: {0}\nSource: {1}", ex.Message, ex.Source);
                    MessageBox.Show(mes);
                }
            }
        }

        /// <summary>
        /// Doubles the array if it is full
        /// </summary>
        /// <param name="Flag"></param>
        private void Resize(bool Flag)
        {
            if (Flag)
                Array.Resize(ref this.textStrings, this.textStrings.Length * 2);
        }

        /// <summary>
        /// Add an element to the array
        /// </summary>
        /// <param name="currentTextString"></param>
        public void Add(TextString currentTextString)
        {
            this.Resize(_index >= this.textStrings.Length);
            this.textStrings[_index] = currentTextString;
            this._index++;
        }
    }
}
