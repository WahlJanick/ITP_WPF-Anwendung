using System.IO;
using System.Windows;
using System.Windows.Controls;
namespace ToDo_Anwendung_WPF {
	public class Task {
		private String description;
		public Task(String description) {
			this.description = description;
		}
		public String getDescription() {
			return description;
		}
	}
	public class TaskList /*: IEnumerable<Task> this shit doesn't work, so dumb C Hashtag*/ {
		public List<Task> tasks = new List<Task>();
		public TaskList() {}
		public void addTask(Task task) {
			tasks.Add(task);
		}
		public void removeTask(Task task) {
			tasks.Remove(task);
		}
		public void save(String targetPath) {
			using (StreamWriter writer = new StreamWriter(targetPath)) {
				bool first = true;
				foreach (Task task in tasks) {
					if (first)
						first = false;
					else
						writer.Write('\n');
					writer.Write(task.getDescription());
				}
				writer.Close();
			}
		}
		public static TaskList load(String sourcePath) {
			TaskList taskList = new TaskList();
			if (!File.Exists(sourcePath))
				File.Create(sourcePath);
			using (StreamReader reader = new StreamReader(sourcePath)) {
				string line;
				while ((line = reader.ReadLine()) != null)
					taskList.addTask(new Task(line));
			}
			return taskList;
		}
	}
	public partial class MainWindow : Window {
		private TaskList taskList;
		private String fileName = "ToDo.txt";
		public MainWindow() {
			InitializeComponent();
			NewBox.Visibility = Visibility.Collapsed;
			ViewBox.Visibility = Visibility.Collapsed;
			loadTaskList();
			updateTaskList();
		}
		public void loadTaskList() {
			taskList = TaskList.load(fileName);
		}
		public void saveTaskList() {
			taskList.save(fileName);
		}
		public void updateTaskList() {
			ViewListBox.Items.Clear();
			List<ListBoxItem> items = new List<ListBoxItem>();
			foreach (Task task in taskList.tasks)
				ViewListBox.Items.Add(new ListBoxItem { Content = task.getDescription() });
		}

		private void NewToDo(object s, RoutedEventArgs eventArgument) {
			ViewBox.Visibility = Visibility.Collapsed;
			NewBox.Visibility = Visibility.Visible;
		}
		private void SaveToDo(object s, RoutedEventArgs eventArgument) {
			String text = NewButtonTextBox.Text.ToString();
			if (text == null) {
				MessageBox.Show("You need to write a Todo.");
				return;
			}
			taskList.addTask(new Task(text));
			taskList.save(fileName);
			NewBox.Visibility = Visibility.Collapsed;
			NewButtonTextBox.Text = "";
		}
		private void ViewToDo(object s, RoutedEventArgs eventArgument) {
			NewBox.Visibility = Visibility.Collapsed;
			ViewBox.Visibility = Visibility.Visible;
			updateTaskList();
		}
	}
}