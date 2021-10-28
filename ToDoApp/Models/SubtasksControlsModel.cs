using System.Windows.Controls;

namespace ToDoApp.Models
{
    public class SubtasksControlsModel
    {
        public DatePicker SomeDatePicker { get; set; } = new DatePicker();
        public TextBox SomeTextBox { get; set; } = new TextBox();

        public TextBox SomeStartHoursTextBox { get; set; } = new TextBox();
        public TextBlock SomeStartTextBlock1 { get; set; } = new TextBlock();
        public TextBox SomeStartMinutesTextBox { get; set; } = new TextBox();
        public TextBlock SomeStartTextBlock2 { get; set; } = new TextBlock();

        public TextBlock SomeTimeDash { get; set; } = new TextBlock();

        public TextBox SomeEndHoursTextBox { get; set; } = new TextBox();
        public TextBlock SomeEndTextBlock1 { get; set; } = new TextBlock();
        public TextBox SomeEndMinutesTextBox { get; set; } = new TextBox();
        public TextBlock SomeEndTextBlock2 { get; set; } = new TextBlock();
    }
}
