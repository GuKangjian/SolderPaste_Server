using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Model
{
    public class ResponseInfo
    {

        public int Id { get; set; }
        public string PartNo { get; set; }
        public string MatID { get; set; }
        public string TimeStamp { get; set; }
        public string LineName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
