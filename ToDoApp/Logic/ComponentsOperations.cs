using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using ToDoApp.Models;

namespace ToDoApp.Logic
{
    public class ComponentsOperations
    {
        private readonly TaskWindow _taskWindow;
        
        public ComponentsOperations(CreateTaskWindow createTaskWindow)
        {
            _taskWindow = createTaskWindow;
        }

        public ComponentsOperations(EditTaskWindow editTaskWindow)
        {
            _taskWindow = editTaskWindow;
        }

        public void CreateSubtasksControlsModels(Grid subtasksGrid, int number)
        {
            for (int i = 0; i < number; i++)
            {
                subtasksGrid.RowDefinitions.Add(new RowDefinition());
                subtasksGrid.RowDefinitions.Add(new RowDefinition());
                int rowCount = subtasksGrid.RowDefinitions.Count;

                TextBlock newTextBlock = new TextBlock
                {
                    HorizontalAlignment = _taskWindow.SubtasksCountTextBlock.HorizontalAlignment,
                    Margin = _taskWindow.SubtasksCountTextBlock.Margin,
                    Text = (rowCount / 2).ToString() + ".",
                    Style = _taskWindow.SubtasksCountTextBlock.Style
                };
                Grid.SetRow(newTextBlock, rowCount - 2);

                DatePicker newDatePicker = new DatePicker
                {
                    Margin = _taskWindow.SubtasksDatePicker.Margin,
                    Width = _taskWindow.SubtasksDatePicker.Width,
                    HorizontalAlignment = _taskWindow.SubtasksDatePicker.HorizontalAlignment,
                    IsTodayHighlighted = _taskWindow.SubtasksDatePicker.IsTodayHighlighted,
                    FontSize = _taskWindow.SubtasksDatePicker.FontSize
                };
                Grid.SetRow(newDatePicker, rowCount - 2);

                TextBox newTextBox = new TextBox
                {
                    Margin = _taskWindow.SubtasksTextBox.Margin,
                    FontSize = _taskWindow.SubtasksTextBox.FontSize
                };
                Grid.SetRow(newTextBox, rowCount - 2);

                TextBox newStartHoursTextBox = new TextBox
                {
                    HorizontalAlignment = _taskWindow.SubtasksStartHoursTextBox.HorizontalAlignment,
                    FontSize = _taskWindow.SubtasksStartHoursTextBox.FontSize,
                    Visibility = _taskWindow.SubtasksStartHoursTextBox.Visibility,
                    Width = _taskWindow.SubtasksStartHoursTextBox.Width,
                    Margin = _taskWindow.SubtasksStartHoursTextBox.Margin,
                    MaxLength = _taskWindow.SubtasksStartHoursTextBox.MaxLength
                };
                Grid.SetRow(newStartHoursTextBox, rowCount - 1);

                TextBlock newStartTextBlock1 = new TextBlock
                {
                    HorizontalAlignment = _taskWindow.SubtasksStartTextBlock1.HorizontalAlignment,
                    Text = _taskWindow.SubtasksStartTextBlock1.Text,
                    Margin = _taskWindow.SubtasksStartTextBlock1.Margin,
                    Visibility = _taskWindow.SubtasksStartTextBlock1.Visibility,
                    Style = _taskWindow.SubtasksStartTextBlock1.Style
                };
                Grid.SetRow(newStartTextBlock1, rowCount - 1);

                TextBox newStartMinutesTextBox = new TextBox
                {
                    HorizontalAlignment = _taskWindow.SubtasksStartMinutesTextBox.HorizontalAlignment,
                    FontSize = _taskWindow.SubtasksStartMinutesTextBox.FontSize,
                    Visibility = _taskWindow.SubtasksStartMinutesTextBox.Visibility,
                    Width = _taskWindow.SubtasksStartMinutesTextBox.Width,
                    Margin = _taskWindow.SubtasksStartMinutesTextBox.Margin,
                    MaxLength = _taskWindow.SubtasksStartMinutesTextBox.MaxLength
                };
                Grid.SetRow(newStartMinutesTextBox, rowCount - 1);

                TextBlock newStartTextBlock2 = new TextBlock
                {
                    HorizontalAlignment = _taskWindow.SubtasksStartTextBlock2.HorizontalAlignment,
                    Text = _taskWindow.SubtasksStartTextBlock2.Text,
                    Margin = _taskWindow.SubtasksStartTextBlock2.Margin,
                    Visibility = _taskWindow.SubtasksStartTextBlock2.Visibility,
                    Style = _taskWindow.SubtasksStartTextBlock2.Style
                };
                Grid.SetRow(newStartTextBlock2, rowCount - 1);

                TextBlock newTimeDash = new TextBlock
                {
                    HorizontalAlignment = _taskWindow.SubtasksTimeDash.HorizontalAlignment,
                    Text = _taskWindow.SubtasksTimeDash.Text,
                    Margin = _taskWindow.SubtasksTimeDash.Margin,
                    Visibility = _taskWindow.SubtasksTimeDash.Visibility,
                    Style = _taskWindow.SubtasksTimeDash.Style
                };
                Grid.SetRow(newTimeDash, rowCount - 1);

                TextBox newEndHoursTextBox = new TextBox
                {
                    HorizontalAlignment = _taskWindow.SubtasksEndHoursTextBox.HorizontalAlignment,
                    FontSize = _taskWindow.SubtasksEndHoursTextBox.FontSize,
                    Visibility = _taskWindow.SubtasksEndHoursTextBox.Visibility,
                    Width = _taskWindow.SubtasksEndHoursTextBox.Width,
                    Margin = _taskWindow.SubtasksEndHoursTextBox.Margin,
                    MaxLength = _taskWindow.SubtasksEndHoursTextBox.MaxLength
                };
                Grid.SetRow(newEndHoursTextBox, rowCount - 1);

                TextBlock newEndTextBlock1 = new TextBlock
                {
                    HorizontalAlignment = _taskWindow.SubtasksEndTextBlock1.HorizontalAlignment,
                    Text = _taskWindow.SubtasksEndTextBlock1.Text,
                    Margin = _taskWindow.SubtasksEndTextBlock1.Margin,
                    Visibility = _taskWindow.SubtasksEndTextBlock1.Visibility,
                    Style = _taskWindow.SubtasksEndTextBlock1.Style
                };
                Grid.SetRow(newEndTextBlock1, rowCount - 1);

                TextBox newEndMinutesTextBox = new TextBox
                {
                    HorizontalAlignment = _taskWindow.SubtasksEndMinutesTextBox.HorizontalAlignment,
                    FontSize = _taskWindow.SubtasksEndMinutesTextBox.FontSize,
                    Visibility = _taskWindow.SubtasksEndMinutesTextBox.Visibility,
                    Width = _taskWindow.SubtasksEndMinutesTextBox.Width,
                    Margin = _taskWindow.SubtasksEndMinutesTextBox.Margin,
                    MaxLength = _taskWindow.SubtasksEndMinutesTextBox.MaxLength
                };
                Grid.SetRow(newEndMinutesTextBox, rowCount - 1);

                TextBlock newEndTextBlock2 = new TextBlock
                {
                    HorizontalAlignment = _taskWindow.SubtasksEndTextBlock2.HorizontalAlignment,
                    Text = _taskWindow.SubtasksEndTextBlock2.Text,
                    Margin = _taskWindow.SubtasksEndTextBlock2.Margin,
                    Visibility = _taskWindow.SubtasksEndTextBlock2.Visibility,
                    Style = _taskWindow.SubtasksEndTextBlock2.Style
                };
                Grid.SetRow(newEndTextBlock2, rowCount - 1);

                subtasksGrid.Children.Add(newDatePicker);
                subtasksGrid.Children.Add(newTextBox);
                subtasksGrid.Children.Add(newTextBlock);

                subtasksGrid.Children.Add(newStartHoursTextBox);
                subtasksGrid.Children.Add(newStartTextBlock1);
                subtasksGrid.Children.Add(newStartMinutesTextBox);
                subtasksGrid.Children.Add(newStartTextBlock2);

                subtasksGrid.Children.Add(newTimeDash);

                subtasksGrid.Children.Add(newEndHoursTextBox);
                subtasksGrid.Children.Add(newEndTextBlock1);
                subtasksGrid.Children.Add(newEndMinutesTextBox);
                subtasksGrid.Children.Add(newEndTextBlock2);

                newDatePicker.SelectedDateChanged += _taskWindow.SubtasksDatePicker_SelectedDateChanged;

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
                _taskWindow.SubtasksControlsList.Add(newSubtasksControls);

                SubtasksModel newSubtask = new SubtasksModel
                {
                    SequentialNumber = _taskWindow.SubtasksControlsList.IndexOf(newSubtasksControls) + 1,
                    Date = "-",
                    Subtask = _taskWindow.SubtasksControlsList[_taskWindow.SubtasksControlsList.IndexOf(newSubtasksControls)].SomeTextBox.Text,
                    TimeRange = "-"
                };
                _taskWindow.SubtasksList.Add(newSubtask);
            }
        }
    }
}