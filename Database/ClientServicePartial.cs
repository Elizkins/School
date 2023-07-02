using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Database
{
    public partial class ClientService
    {
        public string TimeLeft
        {
            get
            {
                TimeSpan time = StartTime - DateTime.Now;
                int hour = (int)Math.Truncate(time.TotalHours);
                int minutes = time.Minutes;
                string finalytime = $"{hour} {Helper.GetDeclension(hour, "час", "часа", "часов")} {minutes} {Helper.GetDeclension(minutes, "минута", "минуты", "минут")}";
                return finalytime;
            }
        }
    }
}
