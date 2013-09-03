using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace knockout.Models
{
    public class UserDetail
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Bdate { get; set; }
    }
}