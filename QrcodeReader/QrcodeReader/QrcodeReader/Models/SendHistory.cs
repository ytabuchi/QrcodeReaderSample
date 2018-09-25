using System;
using SQLite;

namespace QrcodeReader.Models
{
    public class SendHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int DataId { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public int IsSent { get; set; }
    }

}
