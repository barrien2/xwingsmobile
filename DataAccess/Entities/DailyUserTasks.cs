using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class DailyUserTasks
    {
        public int idTask { get; set; }
        public int idreference { get; set; }
        public int idUser { get; set; }
        public DateTime Taskdate { get; set; }
    }
}
