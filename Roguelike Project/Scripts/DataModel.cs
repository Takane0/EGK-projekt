using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeProject.Scripts
{
    public class DataModel
    {
        public int Count { get; set; }
        public List<Guid> Guids { get; set; }= new List<Guid>();
        public bool Checked;
    }
}
