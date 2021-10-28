using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<ToDoModel> _todoListData;
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoListData.json";
        private FileIOUtils _fileIOUtils;
        
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(10)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOUtils = new FileIOUtils(PATH);

            try
            {
                _todoListData = _fileIOUtils.LoadListData();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Close();
            }

            dgToDoList.ItemsSource = _todoListData;
            _todoListData.ListChanged += TodoListData_ListChanged;
        }

        private void TodoListData_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || 
                e.ListChangedType == ListChangedType.ItemDeleted ||
                e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOUtils.SaveListData(sender);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    Close();
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (dgToDoList.SelectedItem != null)
            {
                ToDoModel someToDo = dgToDoList.SelectedItem as ToDoModel;

                ShortTaskTextBlock.Text = someToDo.ShortTask;
                CreationDateTextBlock.Text = someToDo.CreationDate.ToString();
                FinishToTextBlock.Text = someToDo.FinishTo;
                DoneCheckBox.IsChecked = someToDo.IsDone;
                PriorityTextBlock.Text = someToDo.Priority.ToString();
                TypeOfTaskTextBlock.Text = someToDo.TypeOfTask;
                DetailedTaskTextBlock.Text = someToDo.DetailedTask;
                DateRangeTextBlock.Text = someToDo.DateRange;

                if (DateTime.TryParse(someToDo.FinishTo, out DateTime someToDoFinishTo) != false)
                {
                    ToDoListCalendar.SelectedDates.Clear();
                    someToDoFinishTo = someToDoFinishTo.Date;
                    ToDoListCalendar.SelectedDate = someToDoFinishTo;
                }

                if (someToDo.DateRange != "-" && someToDo.DateRange != null)
                {
                    ToDoListCalendar.SelectedDates.Clear();

                    if (someToDo.DateRange.Length > 10)
                    {
                        DateTime firstDate = DateTime.Parse(someToDo.DateRange.Substring(0, 10));
                        DateTime lastDate = DateTime.Parse(someToDo.DateRange.Substring(13, 10));

                        ToDoListCalendar.SelectedDates.AddRange(firstDate, lastDate);
                    }
                    else
                    {
                        DateTime onlyDate = DateTime.Parse(someToDo.DateRange);
                        ToDoListCalendar.SelectedDates.Add(onlyDate);
                    }

                    if (DateTime.TryParse(someToDo.FinishTo, out DateTime someToDoFinishToWithDateRange) != false)
                    {
                        someToDoFinishToWithDateRange = someToDoFinishToWithDateRange.Date;
                        ToDoListCalendar.SelectedDates.Add(someToDoFinishToWithDateRange);
                    }
                }

                if (someToDo.DateRange == "-" && someToDo.FinishTo == "-")
                    ToDoListCalendar.SelectedDates.Clear();

                dgSubtasks.ItemsSource = someToDo.Subtasks;
            }
            else
            {
                ToDoListCalendar.SelectedDates.Clear();
                dgSubtasks.ItemsSource = null;
            }
        }

        private void CreateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTaskWindow createTaskWindow = new CreateTaskWindow();
            createTaskWindow.ShowDialog();

            ToDoModel newToDo = new ToDoModel
            {
                CreationDate = createTaskWindow.ToDo.CreationDate,
                IsDone = false,
                ShortTask = createTaskWindow.ToDo.ShortTask,
                FinishTo = createTaskWindow.ToDo.FinishTo,
                Priority = createTaskWindow.ToDo.Priority,
                TypeOfTask = createTaskWindow.ToDo.TypeOfTask,
                DetailedTask = createTaskWindow.ToDo.DetailedTask,
                DateRange =  createTaskWindow.ToDo.DateRange,
                Subtasks = createTaskWindow.ToDo.Subtasks
            };
            if (!string.IsNullOrEmpty(newToDo.ShortTask))
            {
                _todoListData.Add(newToDo);
            }
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(dgToDoList.SelectedItem is ToDoModel todoToDelete))
                return;
            if (_todoListData.Count > 1)
                dgToDoList.SelectedItem = dgToDoList.Items[_todoListData.IndexOf(todoToDelete) - 1];
            _todoListData.Remove(todoToDelete);
        }

        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(dgToDoList.SelectedItem is ToDoModel todoToEdit))
                return;
            
            EditTaskWindow editTaskWindow = new EditTaskWindow(todoToEdit);
            editTaskWindow.ShowDialog();

            if (todoToEdit.Coequals(editTaskWindow.ToDoToEdit))
            {
                return;
            }
            else
            {
                if (editTaskWindow.ToDoToEdit.ShortTask != null && editTaskWindow.ToDoToEdit.ShortTask != "")
                    todoToEdit.AssignValue(editTaskWindow.ToDoToEdit);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Height >= (MinHeight + FiltersCreationDateDatePicker.Height) || WindowState == WindowState.Maximized)
            {
                FiltersCreationDateDatePicker.Visibility = Visibility.Visible;
            }
            else
            {
                FiltersCreationDateDatePicker.Visibility = Visibility.Hidden;
            }
        }
    }
}
