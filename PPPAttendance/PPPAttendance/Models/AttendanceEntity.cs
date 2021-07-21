using System;
using Azure;
using Azure.Data.Tables;

namespace PPPAttendance.Models
{
    public class AttendanceEntity: ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; } 
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public DateTime Date { get; set; }
        public int Men { get; set; }
        public int Women { get; set; }
        public int Children { get; set; }
        public string Service { get; set; }

        public AttendanceEntity()
        {
            
        }
        
    }
}