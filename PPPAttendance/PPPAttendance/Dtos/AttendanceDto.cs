using System;

namespace PPPAttendance.Dtos
{
    public class AttendanceDto
    {
        public DateTime Date { get; set; }
        public int Men { get; set; }
        public int Women { get; set; }
        public int Children { get; set; }
        public string Service { get; set; }
    }
}