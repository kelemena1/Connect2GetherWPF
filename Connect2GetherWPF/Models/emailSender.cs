using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect2GetherWPF.Models
{
    internal class emailSender
    {
       public string Email { get; set; }
       public string subject { get; set; }
       public string body { get; set; }

        public emailSender(int id, string subject, string body)
        {
            this.subject = subject;
            this.body = body;
        }

        public emailSender()
        {
        }
    }
}
