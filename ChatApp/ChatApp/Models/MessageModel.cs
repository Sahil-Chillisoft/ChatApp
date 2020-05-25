using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class MessageModel
    {
        [DisplayName("Id")] 
        public int id { get; set; }

        [DisplayName("Recipient")] 
        public string recipient { get; set; }

        [DisplayName("Message")] 
        public string message { get; set; }

        [DisplayName("Author")] 
        public string author { get; set; }

        [DisplayName("Date")] 
        public DateTime date { get; set; }
    }
}
