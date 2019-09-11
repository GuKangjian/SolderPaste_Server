using _Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _BLL
{
    public class ResponseDic
    {
        private readonly _DAL.ResponseDic dal = new _DAL.ResponseDic();
        public ResponseDic()
        { }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(_Model.ResponseDic model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<_Model.ResponseDic> GetResponseDic(string strWhere)
        {
            var _dt = dal.GetResponseDic(strWhere);
            if (_dt != null && _dt.Tables.Count > 0)
                return ModelConvertHelper<_Model.ResponseDic>.ConvertToModel(_dt.Tables[0]);
            else
                return null;
        }



        public void UpdateMaterialsToResponseDicByPartNo(string productPartNo, string lineNo, string MaterialsPartNo)
        {
            dal.UpdateMatIDToResponseDicByPartNo(productPartNo, lineNo, MaterialsPartNo);
        }
    }
}
