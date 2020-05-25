using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChatApp.Models
{
    public class UserModel
    {
        public int id { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }
    }
}
