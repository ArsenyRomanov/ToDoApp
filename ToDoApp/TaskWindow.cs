using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ToDoApp.Models;

namespace ToDoApp
{
    public abstract class TaskWindow : Window
    {
        public TextBlock SubtasksCountTextBlock { get; set; } = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(5,5,0,0),
            Text = "1.",
            Style = (Style)Application.Current.FindResource("MaterialDesignBody1TextBlock")
        };

        public DatePicker SubtasksDatePicker { get; set; } = new DatePicker
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            Width = 100,
            IsTodayHighlighted = true,
            FontSize = 16,
            Margin = new Thickness(25,5,0,0)
        };
        public TextBox SubtasksTextBox { get; set; } = new TextBox
        {
            Margin = new Thickness(130,5,5,0),
            FontSize = 16
        };

        public TextBox SubtasksStartHoursTextBox { get; set; } = new TextBox
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            FontSize = 16,
            Visibility = Visibility.Collapsed,
            Width = 60,
            Margin = new Thickness(25,0,0,0),
            MaxLength = 2
        };
        public TextBlock SubtasksStartTextBlock1 { get; set; } = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            Text = "h : ",
            Margin = new Thickness(90,0,0,0),
            Visibility = Visibility.Collapsed,
            Style = (Style)Application.Current.FindResource("MaterialDesignBody1TextBlock")
        };
        public TextBox SubtasksStartMinutesTextBox { get; set; } = new TextBox
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            FontSize = 16,
            Visibility = Visibility.Collapsed,
            Width = 60,
            Margin = new Thickness(115, 0, 0, 0),
            MaxLength = 2
        };
        public TextBlock SubtasksStartTextBlock2 { get; set; } = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            Text = "m",
            Margin = new Thickness(180, 0, 0, 0),
            Visibility = Visibility.Collapsed,
            Style = (Style)Application.Current.FindResource("MaterialDesignBody1TextBlock")
        };

        public TextBlock SubtasksTimeDash { get; set; } = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            Text = "-",
            Margin = new Thickness(205, 0, 0, 0),
            Visibility = Visibility.Collapsed,
            Style = (Style)Application.Current.FindResource("MaterialDesignBody1TextBlock")
        };

        public TextBox SubtasksEndHoursTextBox { get; set; } = new TextBox
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            FontSize = 16,
            Visibility = Visibility.Collapsed,
            Width = 60,
            Margin = new Thickness(220, 0, 0, 0),
            MaxLength = 2
        };
        public TextBlock SubtasksEndTextBlock1 { get; set; } = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            Text = "h : ",
            Margin = new Thickness(285, 0, 0, 0),
            Visibility = Visibility.Collapsed,
            Style = (Style)Application.Current.FindResource("MaterialDesignBody1TextBlock")
        };
        public TextBox SubtasksEndMinutesTextBox { get; set; } = new TextBox
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            FontSize = 16,
            Visibility = Visibility.Collapsed,
            Width = 60,
            Margin = new Thickness(310, 0, 0, 0),
            MaxLength = 2
        };
        public TextBlock SubtasksEndTextBlock2 { get; set; } = new TextBlock
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            Text = "m",
            Margin = new Thickness(375, 0, 0, 0),
            Visibility = Visibility.Collapsed,
            Style = (Style)Application.Current.FindResource("MaterialDesignBody1TextBlock")
        };

        public BindingList<SubtasksModel> SubtasksList { get; set; } = new BindingList<SubtasksModel>();
        public List<SubtasksControlsModel> SubtasksControlsList { get; private set; } = new List<SubtasksControlsModel>();

        public TaskWindow()
        {
            Grid.SetRow(SubtasksCountTextBlock, 0);
            Grid.SetRow(SubtasksDatePicker, 0);
            Grid.SetRow(SubtasksTextBox, 0);
            Grid.SetRow(SubtasksStartHoursTextBox, 1);
            Grid.SetRow(SubtasksStartTextBlock1, 1);
            Grid.SetRow(SubtasksStartMinutesTextBox, 1);
            Grid.SetRow(SubtasksStartTextBlock2, 1);
            Grid.SetRow(SubtasksTimeDash, 1);
            Grid.SetRow(SubtasksEndHoursTextBox, 1);
            Grid.SetRow(SubtasksEndTextBlock1, 1);
            Grid.SetRow(SubtasksEndMinutesTextBox, 1);
            Grid.SetRow(SubtasksEndTextBlock2, 1);

            SubtasksDatePicker.SelectedDateChanged += SubtasksDatePicker_SelectedDateChanged;
        }

        public abstract void SubtasksDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e);
    }
}
