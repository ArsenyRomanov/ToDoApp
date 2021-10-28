using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace ToDoApp.Models
{
    public class ToDoModel : INotifyPropertyChanged
    {
        [JsonProperty(PropertyName = "creationDate")]
        public string CreationDate { get; set; }

        private bool _isDone;
        [JsonProperty(PropertyName = "isDone")]
        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                if (_isDone == value)
                    return;
                this._isDone = value;
                OnPropertyChanged("IsDone");
            }
        }

        private string _shortTask;
        [JsonProperty(PropertyName = "shortTask")]
        public string ShortTask
        {
            get { return _shortTask; }
            set
            {
                if (_shortTask == value)
                    return;
                this._shortTask = value;
                OnPropertyChanged("ShortTask");
            }
        }

        private string _finishTo;
        [JsonProperty(PropertyName = "finishTo")]
        public string FinishTo
        {
            get { return _finishTo; }
            set
            {
                if (_finishTo == value)
                    return;
                this._finishTo = value;
                OnPropertyChanged("FinishTo");
            }
        }

        private int _priority;
        [JsonProperty(PropertyName = "priority")]
        public int Priority
        {
            get { return _priority; }
            set
            {
                if (_priority == value)
                    return;
                this._priority = value;
                OnPropertyChanged("Priority");
            }
        }

        private string _typeOfTask;
        [JsonProperty(PropertyName = "typeOfTask")]
        public string TypeOfTask
        {
            get { return _typeOfTask; }
            set
            {
                if (_typeOfTask == value)
                    return;
                this._typeOfTask = value;
                OnPropertyChanged("TypeOfTask");
            }
        }

        private string _dateRange;
        public string DateRange
        {
            get { return _dateRange; }
            set
            {
                if (_dateRange == value)
                    return;
                this._dateRange = value;
                OnPropertyChanged("DateRange");
            }
        }
        public void SetDateRange(DateTime firstDateInRange, DateTime lastDateInRange)
        {
            if (firstDateInRange.Date == lastDateInRange.Date)
                this.DateRange = firstDateInRange.ToShortDateString();
            else
                this.DateRange = firstDateInRange.ToShortDateString() + " - " + lastDateInRange.ToShortDateString();
        }

        private BindingList<SubtasksModel> _subtasks = new BindingList<SubtasksModel>();
        public BindingList<SubtasksModel> Subtasks
        {
            get { return _subtasks; }
            set
            {
                if (_subtasks == value)
                    return;
                this._subtasks = value;
                OnPropertyChanged("Subtasks");
            }
        }

        private string _detailedTask;
        [JsonProperty(PropertyName = "detailedTask")]
        public string DetailedTask
        {
            get { return _detailedTask; }
            set
            {
                if (_detailedTask == value)
                    return;
                this._detailedTask = value;
                OnPropertyChanged("DetailedTask");
            }
        }

        public ToDoModel()
        {
            DateTime tmp = DateTime.Now;
            CreationDate = tmp.ToShortDateString() + " " + tmp.ToShortTimeString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AssignValue(ToDoModel todoModel)
        {
            this.CreationDate = todoModel.CreationDate;
            this.IsDone = todoModel.IsDone;
            this.ShortTask = todoModel.ShortTask;
            this.FinishTo = todoModel.FinishTo;
            this.Priority = todoModel.Priority;
            this.TypeOfTask = todoModel.TypeOfTask;
            this.DetailedTask = todoModel.DetailedTask;
            this.DateRange = todoModel.DateRange;
        }

        public bool Coequals(ToDoModel todoModel)
        {
            if (this.CreationDate == todoModel.CreationDate &&
                this.IsDone == todoModel.IsDone &&
                this.ShortTask == todoModel.ShortTask &&
                this.FinishTo == todoModel.FinishTo &&
                this.Priority == todoModel.Priority &&
                this.TypeOfTask == todoModel.TypeOfTask &&
                this.DetailedTask == todoModel.DetailedTask && 
                this.DateRange == todoModel.DateRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
