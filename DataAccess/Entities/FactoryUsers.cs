using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class FactoryUsers
    {
        public short IdUser { get; set; }
        public string UserName { get; set; }
        public short IdUserType { get; set; }
        public string Password { get; set; }
    }
}
