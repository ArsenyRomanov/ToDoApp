using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToDoApp.Logic;
using ToDoApp.Models;

namespace ToDoApp
{
    /// <summary>
    /// Логика взаимодействия для EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : TaskWindow
    {
        public ToDoModel ToDoToEdit { get; private set; } = new ToDoModel();

        public EditTaskWindow(ToDoModel todoToEdit)
        {
            InitializeComponent();

            CreationDateTextBlock.Text = todoToEdit.CreationDate;
            IsDoneCheckBox.IsChecked = todoToEdit.IsDone;
            ShortTaskTextBox.Text = todoToEdit.ShortTask;

            if (todoToEdit.FinishTo != "-")
            {
                FinishToDatePicker.SelectedDate = DateTime.Parse(todoToEdit.FinishTo).Date;

                if (todoToEdit.FinishTo.Length > 10)
                {
                    FinishToHoursTextBox.Text = DateTime.Parse(todoToEdit.FinishTo).Hour.ToString();
                    FinishToMinutesTextBox.Text = DateTime.Parse(todoToEdit.FinishTo).Minute.ToString();
                }
            }

            PriorityComboBox.Text = todoToEdit.Priority.ToString();
            TypeOfTaskComboBox.Text = todoToEdit.TypeOfTask;

            if (todoToEdit.DetailedTask != "-")
            {
                DetailedTaskTextBox.Text = todoToEdit.DetailedTask;
            }

            if (todoToEdit.DateRange != "-")
            {
                if (todoToEdit.DateRange.Length > 10)
                {
                    DateTime firstDate = DateTime.Parse(todoToEdit.DateRange.Substring(0, 10));
                    DateTime lastDate = DateTime.Parse(todoToEdit.DateRange.Substring(13, 10));

                    DateRangeFirstDateDatePicker.SelectedDate = firstDate;
                    DateRangeLastDateDatePicker.SelectedDate = lastDate;
                }
                else
                {
                    DateTime onlyDate = DateTime.Parse(todoToEdit.DateRange);

                    DateRangeFirstDateDatePicker.SelectedDate = onlyDate;
                    DateRangeLastDateDatePicker.SelectedDate = onlyDate;
                }
            }

            SubtasksList = todoToEdit.Subtasks;
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();
            ShortTaskTextBox.TabIndex = 0;
            FinishToDatePicker.TabIndex = 1;
            FinishToHoursTextBox.TabIndex = 2;
            FinishToMinutesTextBox.TabIndex = 3;
            IsDoneCheckBox.TabIndex = 4;
            PriorityComboBox.TabIndex = 5;
            TypeOfTaskComboBox.TabIndex = 6;
            DetailedTaskTextBox.TabIndex = 7;
            EditButton.TabIndex = 8;
            CancelButton.TabIndex = 9;

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
            
            //TODO: create file with methods of creating new SubtasksControlsModel objects and use it here and in CreateTaskWindow.xaml.cs
            //TODO: create new SubtasksControlsModel objects according to the number of subtasks in the ToDoToEdit object
            //TODO: fill in the necessary components info of the following subtasks
            
            ComponentsOperations componentsOperations = new ComponentsOperations(this);
            componentsOperations.CreateSubtasksControlsModels(SubtasksGrid, SubtasksList.Count);

            for (int i = 0; i < SubtasksList.Count-1; i++)
            {
                SubtasksControlsList[i].SomeDatePicker.SelectedDate = DateTime.Parse(SubtasksList[i].Date);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();
            Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ToDoToEdit.CreationDate = CreationDateTextBlock.Text;

            ToDoToEdit.IsDone = (bool)IsDoneCheckBox.IsChecked;

            ToDoToEdit.ShortTask = ShortTaskTextBox.Text;

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
                ToDoToEdit.FinishTo = finalDate;
            }
            else
            {
                ToDoToEdit.FinishTo = "-";
            }

            if (PriorityComboBox.SelectedItem != null)
            {
                TextBlock priorityComboBoxSelectedItem = PriorityComboBox.SelectedItem as TextBlock;
                ToDoToEdit.Priority = int.Parse(priorityComboBoxSelectedItem.Text);
            }

            if (TypeOfTaskComboBox.SelectedItem != null)
            {
                TextBlock typeOfTaskComboBoxSelectedItem = TypeOfTaskComboBox.SelectedItem as TextBlock;
                ToDoToEdit.TypeOfTask = typeOfTaskComboBoxSelectedItem.Text;
            }
            else
            {
                ToDoToEdit.TypeOfTask = "-";
            }

            if (DetailedTaskTextBox.Text != null && DetailedTaskTextBox.Text != "")
                ToDoToEdit.DetailedTask = DetailedTaskTextBox.Text;
            else
                ToDoToEdit.DetailedTask = "-";

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
                    ToDoToEdit.SetDateRange(DateRangeFirstDateDatePicker.SelectedDate.Value,
                        DateRangeLastDateDatePicker.SelectedDate.Value);

                    if (ToDoToEdit.FinishTo == "-")
                        ToDoToEdit.FinishTo = DateRangeLastDateDatePicker.SelectedDate.Value.ToShortDateString();
                }
                else
                {
                    ToDoToEdit.DateRange = "-";
                }

                if (ToDoToEdit.ShortTask == "")
                {
                    MessageBox.Show("Short task value cannot be empty");
                }
                else if (DateRangeFirstDateDatePicker.SelectedDate > DateRangeLastDateDatePicker.SelectedDate)
                {
                    MessageBox.Show("First date in range cannot be later than the last date");
                }
                else if (ToDoToEdit.FinishTo != "-" && ToDoToEdit.DateRange != "-" &&
                    DateRangeLastDateDatePicker.SelectedDate > DateTime.Parse(ToDoToEdit.FinishTo))
                {
                    MessageBox.Show("Last date in range cannot be later than the finish to date");
                }
                else
                {
                    Keyboard.ClearFocus();
                    Close();
                }
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

            SubtasksGrid.RowDefinitions.Add(new RowDefinition());
            SubtasksGrid.RowDefinitions.Add(new RowDefinition());
            int rowCount = SubtasksGrid.RowDefinitions.Count;

            DatePicker newDatePicker = new DatePicker
            {
                Margin = SubtasksDatePicker.Margin,
                Width = SubtasksDatePicker.Width,
                HorizontalAlignment = SubtasksDatePicker.HorizontalAlignment,
                IsTodayHighlighted = SubtasksDatePicker.IsTodayHighlighted,
                FontSize = SubtasksDatePicker.FontSize
            };
            Grid.SetRow(newDatePicker, rowCount - 2);

            TextBox newTextBox = new TextBox
            {
                Margin = SubtasksTextBox.Margin,
                FontSize = SubtasksTextBox.FontSize
            };
            Grid.SetRow(newTextBox, rowCount - 2);

            TextBlock newTextBlock = new TextBlock
            {
                HorizontalAlignment = SubtasksCountTextBlock.HorizontalAlignment,
                Margin = SubtasksCountTextBlock.Margin,
                Text = (rowCount / 2).ToString() + ".",
                Style = SubtasksCountTextBlock.Style
            };
            Grid.SetRow(newTextBlock, rowCount - 2);

            TextBox newStartHoursTextBox = new TextBox
            {
                HorizontalAlignment = SubtasksStartHoursTextBox.HorizontalAlignment,
                FontSize = SubtasksStartHoursTextBox.FontSize,
                Visibility = SubtasksStartHoursTextBox.Visibility,
                Width = SubtasksStartHoursTextBox.Width,
                Margin = SubtasksStartHoursTextBox.Margin,
                MaxLength = SubtasksStartHoursTextBox.MaxLength
            };
            Grid.SetRow(newStartHoursTextBox, rowCount - 1);

            TextBlock newStartTextBlock1 = new TextBlock
            {
                HorizontalAlignment = SubtasksStartTextBlock1.HorizontalAlignment,
                Text = SubtasksStartTextBlock1.Text,
                Margin = SubtasksStartTextBlock1.Margin,
                Visibility = SubtasksStartTextBlock1.Visibility,
                Style = SubtasksStartTextBlock1.Style
            };
            Grid.SetRow(newStartTextBlock1, rowCount - 1);

            TextBox newStartMinutesTextBox = new TextBox
            {
                HorizontalAlignment = SubtasksStartMinutesTextBox.HorizontalAlignment,
                FontSize = SubtasksStartMinutesTextBox.FontSize,
                Visibility = SubtasksStartMinutesTextBox.Visibility,
                Width = SubtasksStartMinutesTextBox.Width,
                Margin = SubtasksStartMinutesTextBox.Margin,
                MaxLength = SubtasksStartMinutesTextBox.MaxLength
            };
            Grid.SetRow(newStartMinutesTextBox, rowCount - 1);

            TextBlock newStartTextBlock2 = new TextBlock
            {
                HorizontalAlignment = SubtasksStartTextBlock2.HorizontalAlignment,
                Text = SubtasksStartTextBlock2.Text,
                Margin = SubtasksStartTextBlock2.Margin,
                Visibility = SubtasksStartTextBlock2.Visibility,
                Style = SubtasksStartTextBlock2.Style
            };
            Grid.SetRow(newStartTextBlock2, rowCount - 1);

            TextBlock newTimeDash = new TextBlock
            {
                HorizontalAlignment = SubtasksTimeDash.HorizontalAlignment,
                Text = SubtasksTimeDash.Text,
                Margin = SubtasksTimeDash.Margin,
                Visibility = SubtasksTimeDash.Visibility,
                Style = SubtasksTimeDash.Style
            };
            Grid.SetRow(newTimeDash, rowCount - 1);

            TextBox newEndHoursTextBox = new TextBox
            {
                HorizontalAlignment = SubtasksEndHoursTextBox.HorizontalAlignment,
                FontSize = SubtasksEndHoursTextBox.FontSize,
                Visibility = SubtasksEndHoursTextBox.Visibility,
                Width = SubtasksEndHoursTextBox.Width,
                Margin = SubtasksEndHoursTextBox.Margin,
                MaxLength = SubtasksEndHoursTextBox.MaxLength
            };
            Grid.SetRow(newEndHoursTextBox, rowCount - 1);

            TextBlock newEndTextBlock1 = new TextBlock
            {
                HorizontalAlignment = SubtasksEndTextBlock1.HorizontalAlignment,
                Text = SubtasksEndTextBlock1.Text,
                Margin = SubtasksEndTextBlock1.Margin,
                Visibility = SubtasksEndTextBlock1.Visibility,
                Style = SubtasksEndTextBlock1.Style
            };
            Grid.SetRow(newEndTextBlock1, rowCount - 1);

            TextBox newEndMinutesTextBox = new TextBox
            {
                HorizontalAlignment = SubtasksEndMinutesTextBox.HorizontalAlignment,
                FontSize = SubtasksEndMinutesTextBox.FontSize,
                Visibility = SubtasksEndMinutesTextBox.Visibility,
                Width = SubtasksEndMinutesTextBox.Width,
                Margin = SubtasksEndMinutesTextBox.Margin,
                MaxLength = SubtasksEndMinutesTextBox.MaxLength
            };
            Grid.SetRow(newEndMinutesTextBox, rowCount - 1);

            TextBlock newEndTextBlock2 = new TextBlock
            {
                HorizontalAlignment = SubtasksEndTextBlock2.HorizontalAlignment,
                Text = SubtasksEndTextBlock2.Text,
                Margin = SubtasksEndTextBlock2.Margin,
                Visibility = SubtasksEndTextBlock2.Visibility,
                Style = SubtasksEndTextBlock2.Style
            };
            Grid.SetRow(newEndTextBlock2, rowCount - 1);

            SubtasksGrid.Children.Add(newDatePicker);
            SubtasksGrid.Children.Add(newTextBox);
            SubtasksGrid.Children.Add(newTextBlock);

            SubtasksGrid.Children.Add(newStartHoursTextBox);
            SubtasksGrid.Children.Add(newStartTextBlock1);
            SubtasksGrid.Children.Add(newStartMinutesTextBox);
            SubtasksGrid.Children.Add(newStartTextBlock2);

            SubtasksGrid.Children.Add(newTimeDash);

            SubtasksGrid.Children.Add(newEndHoursTextBox);
            SubtasksGrid.Children.Add(newEndTextBlock1);
            SubtasksGrid.Children.Add(newEndMinutesTextBox);
            SubtasksGrid.Children.Add(newEndTextBlock2);

            newDatePicker.SelectedDateChanged += SubtasksDatePicker_SelectedDateChanged;
            newDatePicker.SelectedDateChanged += SubtasksDatePicker_SelectedDateChanged_SaveData;
            newTextBox.TextChanged += SubtasksTextBox_TextChanged;
            ((DatePicker)sender).SelectedDateChanged += SubtasksDatePicker_SelectedDateChanged_SaveData;

            SubtasksControlsModel newSubtasksControls = new SubtasksControlsModel
            {
                SomeDatePicker = newDatePicker,
                SomeTextBox = newTextBox,
                SomeStartHoursTextBox = newStartHoursTextBox,
                SomeStartTextBlock1 = newStartTextBlock1,
                SomeStartMinutesTextBox = newStartMinutesTextBox,
                SomeStartTextBlock2 = newStartTextBlock2,
                SomeTimeDash = newTimeDash,
                SomeEndHoursTextBox = newEndHoursTextBox,
                SomeEndTextBlock1 = newEndTextBlock1,
                SomeEndMinutesTextBox = newEndMinutesTextBox,
                SomeEndTextBlock2 = newEndTextBlock2
            };
            SubtasksControlsList.Add(newSubtasksControls);

            SubtasksModel newSubtask = new SubtasksModel
            {
                SequentialNumber = SubtasksControlsList.IndexOf(newSubtasksControls) + 1,
                Date = "-",
                Subtask = SubtasksControlsList[SubtasksControlsList.IndexOf(newSubtasksControls)].SomeTextBox.Text,
                TimeRange = "-"
            };
            SubtasksList.Add(newSubtask);
        }

        private void SubtasksDatePicker_SelectedDateChanged_SaveData(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 0; i < SubtasksControlsList.Count; i++)
            {
                if (((DatePicker)sender) == SubtasksControlsList[i].SomeDatePicker)
                {
                    SubtasksList[i].Date = ((DatePicker)sender).SelectedDate.Value.ToShortDateString();
                }
            }
        }

        private void SubtasksTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            for (int i = 0; i < SubtasksControlsList.Count; i++)
            {
                if (((TextBox)sender) == SubtasksControlsList[i].SomeTextBox)
                {
                    SubtasksList[i].Subtask = ((TextBox)sender).Text;
                }
            }
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
