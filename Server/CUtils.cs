using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class CUtils
    {

        public static List<Grupe> getGrupeFromFlux(int flux)
        {
            using (var context = new FacultateEntities())
            {
                var results = (from c in context.Grupes
                               where c.Flux.Equals(flux)
                               orderby c.ID
                               select c).ToList();
                return results;
            }
        }
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
            {
                for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                    yield return day;
            }
            public static DateTime getFirstDayOfModul()
            {
                var date = DateTime.Today;


                switch (date.DayOfWeek.ToString())
                {
                    case "Monday":
                        break;
                    case "Tuesday":
                        date = date.AddDays(-1);
                        break;
                    case "Wednesday":
                        date = date.AddDays(-2);
                        break;
                    case "Thursday":
                        date = date.AddDays(-3);
                        break;
                    case "Friday":
                        date = date.AddDays(-4);
                        break;
                    case "Saturday":
                        date = date.AddDays(-5);
                        break;
                    case "Sunday":
                        date = date.AddDays(-6);
                        break;
                }

                return date;
            }
            public static DateTime getLastDayOfModul()
            {
                var date = DateTime.Today;
                switch (date.DayOfWeek.ToString())
                {
                    case "Monday":
                        date = date.AddDays(11);
                        break;
                    case "Tuesday":
                        date = date.AddDays(10);
                        break;
                    case "Wednesday":
                        date = date.AddDays(9);
                        break;
                    case "Thursday":
                        date = date.AddDays(8);
                        break;
                    case "Friday":
                        date = date.AddDays(7);
                        break;
                    case "Saturday":
                        date = date.AddDays(6);
                        break;
                    case "Sunday":
                        date = date.AddDays(5);
                        break;
                }
                return date;
            }
            public static bool isCurs(in String x)
            {
                if (x.Contains(CVars.curs))
                    return true;
                return false;
            }
            public static bool isCurs(in StringBuilder x)
            {
                if (x.ToString().Contains(CVars.curs))
                    return true;
                return false;
            }
      
    }
}
