using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingGasulPDF.Common
{
    class Utility
    {
        public enum DateInterval
        {
            Year,
            Month,
            Weekday,
            Day,
            Hour,
            Minute,
            Second
        }

        public static long DateDiff(DateInterval interval, DateTime date1, DateTime date2)
        {

            TimeSpan ts = date2 - date1;

            switch (interval)
            {
                case DateInterval.Year:
                    return date2.Year - date1.Year;
                case DateInterval.Month:
                    return (date2.Month - date1.Month) + (12 * (date2.Year - date1.Year));
                case DateInterval.Weekday:
                    return Fix(ts.TotalDays) / 7;
                case DateInterval.Day:
                    return Fix(ts.TotalDays);
                case DateInterval.Hour:
                    return Fix(ts.TotalHours);
                case DateInterval.Minute:
                    return Fix(ts.TotalMinutes);
                default:
                    return Fix(ts.TotalSeconds);
            }
        }

        private static long Fix(double Number)
        {
            if (Number >= 0)
            {
                return (long)Math.Floor(Number);
            }
            return (long)Math.Ceiling(Number);
        }


        //입원비 천원단위 표기를 위한(ex)1.5만원) 커스텀 버림 메서드
        public static float getCustomFloorValueForIpWon(double amount)
        {
            double returnValue = amount * 10; //한자리수를 올려주고
            returnValue = Math.Floor(returnValue); //버림을 한다음
            return Convert.ToSingle(returnValue / 10); //다시 한자리수를 내려준다~
        }

        //윤년 문제해결...존재하지 않는 날짜를 받으면 다음달 1일로 넘겨서 줍니다.
        public static DateTime getSafeDate(Int32 year, DateTime criteriaDate)
        {
            DateTime returnDate = criteriaDate.AddYears(year - criteriaDate.Year);
            if (returnDate.Day != criteriaDate.Day)
            {
                returnDate.AddDays(1);
            }
            return returnDate;
        }
    }
}
