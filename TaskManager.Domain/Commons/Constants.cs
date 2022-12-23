using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Commons
{
    public enum SiEnumTaskStatus : byte
    {
        ToDo = 0,
        InProgress = 1,
        Done = 2
    }
    public enum SiEnumProjectStatus : byte
    {
        NotStarted = 0,
        Active = 1,
        Completed = 2
    }
}
