using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect2GetherWPF.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Permission(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public Permission() { }
    }
}
