using System.IO;
using System.IO.Packaging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDo_Anwendung_WPF
{
	public partial class MainWindow : Window
	{
		private void NewToDo(object s, RoutedEventArgs e)
		{
			ViewBox.Visibility = Visibility.Collapsed;
			NewBox.Visibility = Visibility.Visible;
		}
		private void SavaToDo(object s, RoutedEventArgs e)
		{
            string[] toDo_str = NewButtonTextBox.Text.ToString().Split('\n');
			if (toDo_str == null)
			{
				MessageBox.Show("You need to write a Todo.");
				return;
			}
			int amountOfToDos = 0;
			using (var sr = new StreamReader("ToDo.txt"))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					if (line.Split(':')[0] == "todo")
						++amountOfToDos;
				}
			}
			using (var sw = new StreamWriter("ToDo.txt"))
			{
				sw.WriteLine($"todo:{amountOfToDos + 1}");
				foreach (string str in toDo_str)
				{
					sw.WriteLine(str);
				}
			}
			NewBox.Visibility = Visibility.Collapsed;
		}
		private void ViewToDo(object s, RoutedEventArgs e)
        {
            NewBox.Visibility = Visibility.Visible;
            ViewBox.Visibility = Visibility.Collapsed;

            List<ListBoxItem> todos = new List<ListBoxItem>();
			using (var sr = new StreamReader("ToDo.txt"))
			{
				string todo = "";
				string line = sr.ReadLine();
				do
				{
					todo += line;
					line = sr.ReadLine();
					if (line.Split(':')[0] == "todo")
					{
						todos.Add(new ListBoxItem { Content = todo });
						todo = "";
					}
				}
				while (line != null);
			}
            ViewListBox.ItemsSource = todos;
        }
		public MainWindow()
		{
			InitializeComponent();
		}
	}
}