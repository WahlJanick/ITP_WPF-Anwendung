using System.IO;
using System.IO.Packaging;
using System.Runtime.CompilerServices;
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
		private void SaveToDo(object s, RoutedEventArgs e)
		{
			string[] toDo_str = NewButtonTextBox.Text.ToString().Split("\r\n");
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
			StringBuilder todo = new StringBuilder();
			todo.Append($"todo:{amountOfToDos}");
            foreach (string str in toDo_str)
            {
                todo.Append('\n' + str);
            }
			var sw = new StreamWriter("ToDo.txt", append: true);
			sw.Write(todo.ToString());
			sw.Close();
            NewBox.Visibility = Visibility.Collapsed;
            NewButtonTextBox.Text = "";
        }
		private void ViewToDo(object s, RoutedEventArgs e)
		{
			NewBox.Visibility = Visibility.Collapsed;
			ViewBox.Visibility = Visibility.Visible;
			ViewListBox.Items.Clear();

            List<ListBoxItem> todos = new List<ListBoxItem>();
			string todoStr = "";
			using (var sr = new StreamReader("ToDo.txt"))
			{
				string line = "";
				while (true)
                {
                    line = sr.ReadLine();
					if (line == null)
						break;
                    todoStr += line + '\n';

                    /*
                    if (line.Split(':')[0] == "todo")
					{
						todos.Add(new ListBoxItem { Content = todoStr });
						todoStr = "";
					}
					*/
                }
			}
			todos.Add(new ListBoxItem { Content = todoStr });
			foreach (ListBoxItem todo in todos)
			{
				ViewListBox.Items.Add(todo);
            }
        }
		public MainWindow()
        {
            InitializeComponent();
            NewBox.Visibility = Visibility.Collapsed;
            ViewBox.Visibility = Visibility.Collapsed;
        }
	}
}