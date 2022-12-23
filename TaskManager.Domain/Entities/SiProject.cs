using System.Collections.Generic;
using TaskManager.Domain.Commons;

namespace TaskManager.Domain.Entities

{
    //Class that represents Project model
    public class SiProject : SiBaseEntity
    {
        private SiEnumProjectStatus _status;
        
        private DateTime _completionDate;
        private DateTime _startDate;
        private List<SiTask> _tasks = new List<SiTask>();


        public SiProject() : base()
        { }
        public SiProject(int id) : base(id)
        { }
        public DateTime StartDate {
            get
            {
                return _startDate;
            }
            set
            {
                if (_startDate == value) return;
                //some logic to check start date if needed
                _startDate = value;
            }
        }
        public DateTime CompletionDate { 
            get 
            {
                return _completionDate; 
            }
            set
            {
                if (_completionDate == value) return;
                if (value < _startDate) throw new Exception("Completion Date can not be earlier than Start Date");
                _completionDate = value;
            }
        }

        public int Priority { get; set; }

        
        public SiEnumProjectStatus Status
        {
            get
            {
                return _status;
            } 
            set
            {
                if (_status == value) return;
                // Some logic here to check whether status can be changed:
                _status = value;
            }
        }
        
        public IReadOnlyCollection<SiTask> Tasks //IReadOnlyCollection&&!! - Json Serializer???
        {
            get
            {
                return _tasks;
            }
            set { _tasks = (List<SiTask>)value; }
        }
        //public void TaskAdd(SiTask task)
        //{
        //    //Some business logic to check:
        //    _tasks.Add(task);
        //}
        //public void TaskRemove(SiTask task)
        //{
        //    //Some business logic to check:
        //    _tasks.Remove(task);
        //}

    }
}
