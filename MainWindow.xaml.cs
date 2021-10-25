using System.Windows;
using System.Windows.Documents;

namespace TMG_Test1
{
    public partial class MainWindow : Window
    {
        private Repository rep;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var range = new TextRange(inputBox.Document.ContentStart, inputBox.Document.ContentEnd);
            InputString input = new InputString(range);
            var _stringsId = input.Load();

            rep = new Repository();
            rep.Load(_stringsId);
            listView.ItemsSource = rep.textStrings;
        }
    }
}
