using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Common
{

    public class TableToJson
    {

        /// <summary>
        /// 返回查询结果序列化（查）
        /// </summary>
        /// <param name="total"></param>
        /// <param name="rowString"></param>
        /// <returns></returns>
        public static string GetResultJson(int total, string rowString)
        {
            if (string.IsNullOrWhiteSpace(rowString) || (rowString.ToLower() == "null"))
            {
                return ("{\"total\":" + total + ",\"rows\":[]}");
            }
            return string.Concat(new object[] { "{\"total\":", total, ",\"rows\":", rowString, "}" });
        }
    }
}
