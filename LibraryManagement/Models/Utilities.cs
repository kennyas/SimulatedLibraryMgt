using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimulatedLibraryMgt.Models
{
    public static class Utilities
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static bool IsHoliday(DateTime dateTime)
        {
            var dateValue = db.BusinessHolidays.Where(x => x.FromDate >= dateTime && x.ToDate <= dateTime).ToList();
            var result = dateValue.Any(); //db.BusinessHolidays.Any(x=>x.FromDate == dateTime || x.ToDate == dateTime);
            if (!result)
            {
                result = Constants.Weekends.Contains(dateTime.DayOfWeek);
            }
           
            return result; //Constants.Weekends.Contains(dateTime.DayOfWeek);
        }
        public static DateTime AddBusinessDays(DateTime originalDateTime, int noOfDays)
        {
            var result = originalDateTime;

            for(var iCntr = 0; iCntr < noOfDays; iCntr++)
            {
                do
                {
                  result = result.AddDays(1);
                } while (IsHoliday(result));
            }

            return result;
        }

        public static int CountBusinessDays(DateTime dateTime1, DateTime dateTime2)
        {
            int result = 0;
            //var res = db.BusinessHolidays.Where(x => x.FromDate == dateTime1 && x.ToDate == dateTime2).ToList();
            int dayCount = (dateTime2 - dateTime1).Days;
            //var res = AddBusinessDays(dateTime1, dayCount );

            var dateRes = dateTime1;


            for (var iCntr = 0; iCntr < dayCount; iCntr++)
            {
                
                if (!IsHoliday(dateRes))
                {
                    result = result + 1;
                    
                }
                dateRes = dateRes.AddDays(1);
                //do
                //{
                //    result = result + 1;
                //    dateRes = dateRes.AddDays(1);
                //} while (IsHoliday(dateRes));
            }

            //result = res.Count();
            return result;
        }
    }
}