using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class ChannelModel
    {
        public int id { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }
    }
}
