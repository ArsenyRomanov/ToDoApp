using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToDoApp.Models;
using ToDoApp.Logic;

namespace ToDoApp
{
    /// <summary>
    /// Логика взаимодействия для CreateTaskWindow.xaml
    /// </summary>
    public partial class CreateTaskWindow : TaskWindow
    {
        public ToDoModel ToDo { get; private set; } = new ToDoModel();

        public CreateTaskWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreationDateTextBlock.Text = ToDo.CreationDate.ToString();

            SubtasksGrid.Children.Add(SubtasksCountTextBlock);
            SubtasksGrid.Children.Add(SubtasksDatePicker);
            SubtasksGrid.Children.Add(SubtasksTextBox);

            SubtasksGrid.Children.Add(SubtasksStartHoursTextBox);
            SubtasksGrid.Children.Add(SubtasksStartTextBlock1);
            SubtasksGrid.Children.Add(SubtasksStartMinutesTextBox);
            SubtasksGrid.Children.Add(SubtasksStartTextBlock2);

            SubtasksGrid.Children.Add(SubtasksTimeDash);

            SubtasksGrid.Children.Add(SubtasksEndHoursTextBox);
            SubtasksGrid.Children.Add(SubtasksEndTextBlock1);
            SubtasksGrid.Children.Add(SubtasksEndMinutesTextBox);
            SubtasksGrid.Children.Add(SubtasksEndTextBlock2);

            SubtasksControlsModel firstControls = new SubtasksControlsModel
            {
                SomeDatePicker = SubtasksDatePicker,
                SomeTextBox = SubtasksTextBox,
                SomeStartHoursTextBox = SubtasksStartHoursTextBox,
                SomeStartTextBlock1 = SubtasksStartTextBlock1,
                SomeStartMinutesTextBox = SubtasksStartMinutesTextBox,
                SomeStartTextBlock2 = SubtasksStartTextBlock2,
                SomeTimeDash = SubtasksTimeDash,
                SomeEndHoursTextBox = SubtasksEndHoursTextBox,
                SomeEndTextBlock1 = SubtasksEndTextBlock1,
                SomeEndMinutesTextBox = SubtasksEndMinutesTextBox,
                SomeEndTextBlock2 = SubtasksEndTextBlock2
            };
            SubtasksControlsList.Add(firstControls);

            SubtasksModel firstSubtask = new SubtasksModel
            {
                SequentialNumber = SubtasksControlsList.IndexOf(firstControls) + 1,
                Date = "-",
                Subtask = SubtasksControlsList[0].SomeTextBox.Text,
                TimeRange = "-"
            };
            SubtasksList.Add(firstSubtask);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void СreateButton_Click(object sender, RoutedEventArgs e)
        {
            ToDo.ShortTask = ShortTaskTextBox.Text;

            if (FinishToDatePicker.SelectedDate != null)
            {
                DateTime finishToDatePickerSelectedDate = (DateTime)FinishToDatePicker.SelectedDate;
                if (FinishToHoursTextBox.Text != "")
                {
                    finishToDatePickerSelectedDate = finishToDatePickerSelectedDate.AddHours(
                        double.Parse(FinishToHoursTextBox.Text));
                }
                if (FinishToMinutesTextBox.Text != "")
                {
                    finishToDatePickerSelectedDate = finishToDatePickerSelectedDate.AddMinutes(
                        double.Parse(FinishToMinutesTextBox.Text));
                }

                string finalDate;
                if (FinishToHoursTextBox.Text == "" && FinishToMinutesTextBox.Text == "")
                {
                    finalDate = finishToDatePickerSelectedDate.ToShortDateString();
                }
                else
                {
                    finalDate = finishToDatePickerSelectedDate.ToShortDateString() +
                        " " + finishToDatePickerSelectedDate.ToShortTimeString();
                }
                ToDo.FinishTo = finalDate;
            }

            if ((FinishToHoursTextBox.Text != "" && FinishToDatePicker.SelectedDate == null) || 
                (FinishToMinutesTextBox.Text != "" && FinishToDatePicker.SelectedDate == null) ||
                (FinishToHoursTextBox.Text != "" && 
                FinishToMinutesTextBox.Text != "" && 
                FinishToDatePicker.SelectedDate == null))
            {
                DateTime today = DateTime.Now;

                if (FinishToHoursTextBox.Text != "")
                {
                    ToDo.FinishTo = today.ToShortDateString() + " " + FinishToHoursTextBox.Text + ":00";
                }

                if (FinishToMinutesTextBox.Text != "")
                {
                    ToDo.FinishTo = today.AddDays(1).ToShortDateString() + " " + "0:" + FinishToMinutesTextBox.Text;
                }

                if (FinishToHoursTextBox.Text != "" && 
                    FinishToMinutesTextBox.Text != "")
                {
                    ToDo.FinishTo = today.ToShortDateString() + " " + 
                        FinishToHoursTextBox.Text + ":" + 
                        FinishToMinutesTextBox.Text;
                }
            }

            if (FinishToDatePicker.SelectedDate == null && 
                FinishToHoursTextBox.Text == "" && 
                FinishToMinutesTextBox.Text == "")
            {
                ToDo.FinishTo = "-";
            }

            if (PriorityComboBox.SelectedItem != null)
            {
                ToDo.Priority = int.Parse((PriorityComboBox.SelectedItem as TextBlock).Text);
            }

            if (TypeOfTaskComboBox.SelectedItem != null)
            {
                ToDo.TypeOfTask = (TypeOfTaskComboBox.SelectedItem as TextBlock).Text;
            }
            else
            {
                ToDo.TypeOfTask = "-";
            }

            ToDo.DetailedTask = !string.IsNullOrEmpty(DetailedTaskTextBox.Text) ? DetailedTaskTextBox.Text : "-";

            if (SubtasksDatePicker.SelectedDate != null && SubtasksTextBox.Text != "")
            {
                for (int i = 0; i < SubtasksList.Count; i++)
                {
                    if (SubtasksControlsList[i].SomeDatePicker.SelectedDate != null)
                        SubtasksList[i].Date = SubtasksControlsList[i].SomeDatePicker.SelectedDate.Value.ToShortDateString();
                    SubtasksList[i].Subtask = SubtasksControlsList[i].SomeTextBox.Text;
                    
                    //1111
                    if (SubtasksControlsList[i].SomeStartHoursTextBox.Text != "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text != "" &&
                        SubtasksControlsList[i].SomeEndHoursTextBox.Text != "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text != "")
                    {
                        SubtasksList[i].TimeRange = $"{SubtasksControlsList[i].SomeStartHoursTextBox.Text}:{SubtasksControlsList[i].SomeStartMinutesTextBox.Text} - {SubtasksControlsList[i].SomeEndHoursTextBox.Text}:{SubtasksControlsList[i].SomeEndMinutesTextBox.Text}";
                    }
                    //1110
                    else if (SubtasksControlsList[i].SomeStartHoursTextBox.Text != "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text != "" &&
                             SubtasksControlsList[i].SomeEndHoursTextBox.Text != "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text == "")
                    {
                        SubtasksList[i].TimeRange = $"{SubtasksControlsList[i].SomeStartHoursTextBox.Text}:{SubtasksControlsList[i].SomeStartMinutesTextBox.Text} - {SubtasksControlsList[i].SomeEndHoursTextBox.Text}:00";
                    }
                    //1101
                    else if (SubtasksControlsList[i].SomeStartHoursTextBox.Text != "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text != "" &&
                             SubtasksControlsList[i].SomeEndHoursTextBox.Text == "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text != "")
                    {
                        SubtasksList[i].TimeRange = $"{SubtasksControlsList[i].SomeStartHoursTextBox.Text}:{SubtasksControlsList[i].SomeStartMinutesTextBox.Text} - 0:{SubtasksControlsList[i].SomeEndMinutesTextBox.Text}";
                    }
                    //1100
                    else if (SubtasksControlsList[i].SomeStartHoursTextBox.Text != "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text != "" &&
                             SubtasksControlsList[i].SomeEndHoursTextBox.Text == "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text == "")
                    {
                        SubtasksList[i].TimeRange = $"{SubtasksControlsList[i].SomeStartHoursTextBox.Text}:{SubtasksControlsList[i].SomeStartMinutesTextBox.Text} - 0:00";
                    }
                    //1011
                    else if (SubtasksControlsList[i].SomeStartHoursTextBox.Text != "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text == "" &&
                             SubtasksControlsList[i].SomeEndHoursTextBox.Text != "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text != "")
                    {
                        SubtasksList[i].TimeRange = $"{SubtasksControlsList[i].SomeStartHoursTextBox.Text}:00 - {SubtasksControlsList[i].SomeEndHoursTextBox.Text}:{SubtasksControlsList[i].SomeEndMinutesTextBox.Text}";
                    }
                    //1010
                    else if (SubtasksControlsList[i].SomeStartHoursTextBox.Text != "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text == "" &&
                             SubtasksControlsList[i].SomeEndHoursTextBox.Text != "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text == "")
                    {
                        SubtasksList[i].TimeRange = $"{SubtasksControlsList[i].SomeStartHoursTextBox.Text}:00 - {SubtasksControlsList[i].SomeEndHoursTextBox.Text}:00";
                    }
                    //1001
                    else if (SubtasksControlsList[i].SomeStartHoursTextBox.Text != "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text == "" &&
                             SubtasksControlsList[i].SomeEndHoursTextBox.Text == "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text != "")
                    {
                        SubtasksList[i].TimeRange = $"{SubtasksControlsList[i].SomeStartHoursTextBox.Text}:00 - 0:{SubtasksControlsList[i].SomeEndMinutesTextBox.Text}";
                    }
                    //1000
                    else if (SubtasksControlsList[i].SomeStartHoursTextBox.Text != "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text == "" &&
                             SubtasksControlsList[i].SomeEndHoursTextBox.Text == "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text == "")
                    {
                        SubtasksList[i].TimeRange = $"{SubtasksControlsList[i].SomeStartHoursTextBox.Text}:00 - 0:00";
                    }
                    //0111
                    else if (SubtasksControlsList[i].SomeStartHoursTextBox.Text == "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text != "" &&
                             SubtasksControlsList[i].SomeEndHoursTextBox.Text != "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text != "")
                    {
                        SubtasksList[i].TimeRange = $"0:{SubtasksControlsList[i].SomeStartMinutesTextBox.Text} - {SubtasksControlsList[i].SomeEndHoursTextBox.Text}:{SubtasksControlsList[i].SomeEndMinutesTextBox.Text}";
                    }
                    //0110
                    else if (SubtasksControlsList[i].SomeStartHoursTextBox.Text == "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text != "" &&
                             SubtasksControlsList[i].SomeEndHoursTextBox.Text != "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text == "")
                    {
                        SubtasksList[i].TimeRange = $"0:{SubtasksControlsList[i].SomeStartMinutesTextBox.Text} - {SubtasksControlsList[i].SomeEndHoursTextBox.Text}:00";
                    }
                    //0101
                    else if (SubtasksControlsList[i].SomeStartHoursTextBox.Text == "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text != "" &&
                             SubtasksControlsList[i].SomeEndHoursTextBox.Text == "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text != "")
                    {
                        SubtasksList[i].TimeRange = $"0:{SubtasksControlsList[i].SomeStartMinutesTextBox.Text} - 0:{SubtasksControlsList[i].SomeEndMinutesTextBox.Text}";
                    }
                    //0100
                    else if (SubtasksControlsList[i].SomeStartHoursTextBox.Text == "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text != "" &&
                             SubtasksControlsList[i].SomeEndHoursTextBox.Text == "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text == "")
                    {
                        SubtasksList[i].TimeRange = $"0:{SubtasksControlsList[i].SomeStartMinutesTextBox.Text} - 0:00";
                    }
                    //0011
                    else if (SubtasksControlsList[i].SomeStartHoursTextBox.Text == "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text == "" &&
                             SubtasksControlsList[i].SomeEndHoursTextBox.Text != "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text != "")
                    {
                        SubtasksList[i].TimeRange = $"0:00 - {SubtasksControlsList[i].SomeEndHoursTextBox.Text}:{SubtasksControlsList[i].SomeEndMinutesTextBox.Text}";
                    }
                    //0010
                    else if (SubtasksControlsList[i].SomeStartHoursTextBox.Text == "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text == "" &&
                             SubtasksControlsList[i].SomeEndHoursTextBox.Text != "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text == "")
                    {
                        SubtasksList[i].TimeRange = $"0:00 - {SubtasksControlsList[i].SomeEndHoursTextBox.Text}:00";
                    }
                    //0001
                    else if (SubtasksControlsList[i].SomeStartHoursTextBox.Text == "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text == "" &&
                             SubtasksControlsList[i].SomeEndHoursTextBox.Text == "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text != "")
                    {
                        SubtasksList[i].TimeRange = $"0:00 - 0:{SubtasksControlsList[i].SomeEndMinutesTextBox.Text}";
                    }
                    //0000
                    else if (SubtasksControlsList[i].SomeStartHoursTextBox.Text == "" && SubtasksControlsList[i].SomeStartMinutesTextBox.Text == "" &&
                             SubtasksControlsList[i].SomeEndHoursTextBox.Text == "" && SubtasksControlsList[i].SomeEndMinutesTextBox.Text == "")
                    {
                        SubtasksList[i].TimeRange = "-";
                    }

                    if (SubtasksList[i].Date == "-")
                        SubtasksList.RemoveAt(i);
                }
                ToDo.Subtasks = SubtasksList;
            }

            if ((DateRangeFirstDateDatePicker.SelectedDate != null &&
                DateRangeLastDateDatePicker.SelectedDate == null) ||
                (DateRangeFirstDateDatePicker.SelectedDate == null &&
                DateRangeLastDateDatePicker.SelectedDate != null))
            {
                MessageBox.Show("If the first field in Date range is filled in, " +
                    "then the second field must also be filled in");
            }
            else
            {

                if (DateRangeFirstDateDatePicker.SelectedDate != null &&
                    DateRangeLastDateDatePicker.SelectedDate != null)
                {
                    ToDo.SetDateRange(DateRangeFirstDateDatePicker.SelectedDate.Value,
                        DateRangeLastDateDatePicker.SelectedDate.Value);

                    if (ToDo.FinishTo == "-")
                        ToDo.FinishTo = DateRangeLastDateDatePicker.SelectedDate.Value.ToShortDateString();
                }
                else
                {
                    ToDo.DateRange = "-";
                }

                if (ToDo.ShortTask == "")
                    MessageBox.Show("Short task value cannot be empty");
                else if (DateRangeFirstDateDatePicker.SelectedDate > DateRangeLastDateDatePicker.SelectedDate)
                    MessageBox.Show("First date in range cannot be later than the last date");
                else if (ToDo.FinishTo != "-" && ToDo.DateRange != "-" &&
                    DateRangeLastDateDatePicker.SelectedDate > DateTime.Parse(ToDo.FinishTo))
                    MessageBox.Show("Last date in range cannot be later than the finish to date");
                else
                    Close();
            }
        }

        private void FinishToHoursTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox iFinishToHoursTextBox = sender as TextBox;

            if (int.TryParse(iFinishToHoursTextBox.Text, out _) == false)
            {
                TextChange textChangeToDelete = e.Changes.ElementAt(0);
                int iAddedLength = textChangeToDelete.AddedLength;
                int iOffset = textChangeToDelete.Offset;

                iFinishToHoursTextBox.Text = iFinishToHoursTextBox.Text.Remove(iOffset, iAddedLength);
            }

            if (iFinishToHoursTextBox.Text.Length == 2)
            {
                FinishToMinutesTextBox.Focus();
                FinishToMinutesTextBox.Select(0, 0);
            }
        }

        private void FinishToMinutesTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox iFinishToMinutesTextBox = sender as TextBox;

            if (int.TryParse(iFinishToMinutesTextBox.Text, out _) == false)
            {
                TextChange textChange = e.Changes.ElementAt(0);
                int iAddedLength = textChange.AddedLength;
                int iOffset = textChange.Offset;

                iFinishToMinutesTextBox.Text = iFinishToMinutesTextBox.Text.Remove(iOffset, iAddedLength);
            }

            if (iFinishToMinutesTextBox.Text.Length == 2)
            {
                Keyboard.ClearFocus();
            }
        }

        private void AdditionalInfoExpander_Expanded(object sender, RoutedEventArgs e)
        {
            Width += 422;
        }

        private void AdditionalInfoExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            Width -= 422;
        }

        public override void SubtasksDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ((DatePicker)sender).SelectedDateChanged -= SubtasksDatePicker_SelectedDateChanged;

            ComponentsOperations componentsOperations = new ComponentsOperations(this);
            componentsOperations.CreateSubtasksControlsModels(SubtasksGrid, 1);
        }

        private void SubtasksMoreInfoCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < SubtasksControlsList.Count; i++)
            {
                SubtasksControlsList[i].SomeStartHoursTextBox.Visibility = Visibility.Visible;
                SubtasksControlsList[i].SomeStartTextBlock1.Visibility = Visibility.Visible;
                SubtasksControlsList[i].SomeStartMinutesTextBox.Visibility = Visibility.Visible;
                SubtasksControlsList[i].SomeStartTextBlock2.Visibility = Visibility.Visible;

                SubtasksControlsList[i].SomeTimeDash.Visibility = Visibility.Visible;

                SubtasksControlsList[i].SomeEndHoursTextBox.Visibility = Visibility.Visible;
                SubtasksControlsList[i].SomeEndTextBlock1.Visibility = Visibility.Visible;
                SubtasksControlsList[i].SomeEndMinutesTextBox.Visibility = Visibility.Visible;
                SubtasksControlsList[i].SomeEndTextBlock2.Visibility = Visibility.Visible;
            }
        }

        private void SubtasksMoreInfoCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < SubtasksControlsList.Count; i++)
            {
                SubtasksControlsList[i].SomeStartHoursTextBox.Visibility = Visibility.Collapsed;
                SubtasksControlsList[i].SomeStartTextBlock1.Visibility = Visibility.Collapsed;
                SubtasksControlsList[i].SomeStartMinutesTextBox.Visibility = Visibility.Collapsed;
                SubtasksControlsList[i].SomeStartTextBlock2.Visibility = Visibility.Collapsed;

                SubtasksControlsList[i].SomeTimeDash.Visibility = Visibility.Collapsed;

                SubtasksControlsList[i].SomeEndHoursTextBox.Visibility = Visibility.Collapsed;
                SubtasksControlsList[i].SomeEndTextBlock1.Visibility = Visibility.Collapsed;
                SubtasksControlsList[i].SomeEndMinutesTextBox.Visibility = Visibility.Collapsed;
                SubtasksControlsList[i].SomeEndTextBlock2.Visibility = Visibility.Collapsed;
            }
        }
    }
}
