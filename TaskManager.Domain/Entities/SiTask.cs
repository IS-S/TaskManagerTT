using TaskManager.Domain.Commons;

namespace TaskManager.Domain.Entities
{
    // Class that represents Task model
    public class SiTask : SiBaseEntity
    {
        private SiProject? _project;
        private int _projectId;
        private SiEnumTaskStatus _status;

        public SiTask() : base()
        { }
        public SiTask(int id) : base(id)
        { }

        public int ProjectId
        {
            get => _projectId; set
            {
                if (_projectId == value) return;
                _projectId = value;
                if (_project?.Id != _projectId) _project = null;
            }
        }
        public SiProject? Project
        {
            get => _project; set
            {
                if (_project == value) return;
                if (value?.Id != _projectId) _projectId = value == null ? _projectId = default : value.Id;
                _project = value;
            }
        }
        public string? Description { get; set; }
        public int Priority { get; set; }


        public SiEnumTaskStatus Status
        {
            get => _status; set
            {
                if(_status == value) return;
                _status = value;
            }
        }

    }
}
