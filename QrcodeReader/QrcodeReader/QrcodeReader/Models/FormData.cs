using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace QrcodeReader.Models
{
    public class FormData
    {
        public long Id { get; set; }
        public string Category { get; set; }
        public string Machine { get; set; }
        public string SerialNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Comment { get; set; }
    }
}
