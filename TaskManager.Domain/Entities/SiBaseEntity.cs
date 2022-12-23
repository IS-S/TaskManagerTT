using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain
{
    // Base Domain Entity class for Task and Project classes to be inherited from
    public abstract class SiBaseEntity 
    {
        private int _id;
        private string _name = "<Object Name>";
        private byte[] _objVersion = new byte[] { 0x0 };

        protected SiBaseEntity() { }
        protected SiBaseEntity(int id) { _id = id; }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value == _id) return;
                _id = value;
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        //Concurency control attribute:
        [Timestamp]
        public byte[] ObjVersion
        {
            get => _objVersion; set
            {
                _objVersion = value;
            }
        }
    }
}
