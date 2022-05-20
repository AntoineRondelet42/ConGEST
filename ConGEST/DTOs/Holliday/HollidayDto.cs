using ConGEST.Models;
using System;

namespace ConGEST.DTOs.Holliday
{
    public class HollidayDto
    {
        public int HollidayId { get; set; }
        public DateTime DateAsk { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public int ValidStateId { get; set; }
        public int NumberOfWorkingDays { get; set; }

        public void CalculateWorkingDays()
        {
            int nbrJours = Convert.ToInt32((DateEnd - DateBegin).TotalDays);
            int dayofweek = GetDayOfWeekPosition(DateEnd);
            int reste = (nbrJours - dayofweek) / 7;
            NumberOfWorkingDays = nbrJours - (1 + reste) * 2;
        }

        private static int GetDayOfWeekPosition(DateTime DateEnd)
        {
            int result;
            switch (DateEnd.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    result = 5;
                    break;
                case DayOfWeek.Tuesday:
                    result = 4;
                    break;
                case DayOfWeek.Wednesday:
                    result = 3;
                    break;
                case DayOfWeek.Thursday:
                    result = 2;
                    break;
                case DayOfWeek.Friday:
                    result = 1;
                    break;
                default:
                    result = 0;
                    break;
            }
            return result;
        }
    }
}