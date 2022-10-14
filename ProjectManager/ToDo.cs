using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager
{
    public class ToDo
    {
        public int ToDoId { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public Worker Owner { get; set; }
    }
}
