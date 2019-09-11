using _Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _BLL
{
    public class LineNoConfig
    {
        private readonly _DAL.LineNoConfig dal = new _DAL.LineNoConfig();




        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<_Model.LineNoConfig> GetAllLineNoConfig()
        {
            var _dt = dal.GetAllLineNoConfig();
            if (_dt != null)
                return ModelConvertHelper<_Model.LineNoConfig>.ConvertToModel(_dt);
            else
                return null;
        }
    }
}
