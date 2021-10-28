using System.ComponentModel;

namespace ToDoApp.Models
{
    public class SubtasksModel : INotifyPropertyChanged
    {
        private int _sequentialNumber;
        public int SequentialNumber
        {
            get { return _sequentialNumber; }
            set
            {
                if (_sequentialNumber == value)
                    return;
                _sequentialNumber = value;
                OnPropertyChanged("SequentialNumber");
            }
        }

        private string _date;
        public string Date
        {
            get { return _date; }
            set
            {
                if (_date == value)
                    return;
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        private string _subtask;
        public string Subtask
        {
            get { return _subtask; }
            set
            {
                if (_subtask == value)
                    return;
                _subtask = value;
                OnPropertyChanged("Subtask");
            }
        }

        private string _startHours;
        public string StartHours
        {
            get { return _startHours; }
            set
            {
                if (_startHours == value)
                    return;
                _startHours = value;
                OnPropertyChanged("StartHours");
            }
        }

        private string _startMinutes;
        public string StartMinutes
        {
            get { return _startMinutes; }
            set
            {
                if (_startMinutes == value)
                    return;
                _startMinutes = value;
                OnPropertyChanged("StartMinutes");
            }
        }

        private string _endHours;
        public string EndHours
        {
            get { return _endHours; }
            set
            {
                if (_endHours == value)
                    return;
                _endHours = value;
                OnPropertyChanged("EndHours");
            }
        }

        private string _endMinutes;
        public string EndMinutes
        {
            get { return _endMinutes; }
            set
            {
                if (_endMinutes == value)
                    return;
                _endMinutes = value;
                OnPropertyChanged("EndMinutes");
            }
        }

        private string _timeRange;
        public string TimeRange
        {
            get { return _timeRange; }
            set
            {
                if (_timeRange == value)
                    return;
                _timeRange = value;
                OnPropertyChanged("TimeRange");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
