using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Model
{
    public class RequestInfo
    {
        public int Id { get; set; }
        public string TypeNo { get; set; }
        public string TimeStamp { get; set; }
        public string FileTime { get; set; }
        public string LineName { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
