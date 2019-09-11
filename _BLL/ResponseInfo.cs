using _Common;
using _Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _BLL
{
    public class ResponseInfo
    {

        private readonly _DAL.ResponseInfo dal = new _DAL.ResponseInfo();
        public ResponseInfo()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(_Model.ResponseInfo model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<_Model.ResponseInfo> GetResponseInfoList(string strWhere)
        {
            var _dt = dal.GetList(strWhere);
            if (_dt != null && _dt.Tables[0] != null)
                return ModelConvertHelper<_Model.ResponseInfo>.ConvertToModel(_dt.Tables[0]);
            else
                return null;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<_Model.ResponseInfo> GetResponseInfo()
        {
            var _dt = dal.GetResponseInfo();
            if (_dt != null)
                return ModelConvertHelper<_Model.ResponseInfo>.ConvertToModel(_dt);
            else
                return null;
        }

        public IList<_Model.ResponseInfo> GetResponse(string partNo)
        {
            var table = dal.GetResponse(partNo);
            return ModelConvertHelper<_Model.ResponseInfo>.ConvertToModel(table); ;
        }

        public void UpdateMatIDByPartNo(string partNo, string matId)
        {
            dal.UpdateMatIDByPartNo(partNo, matId);
        }



        public bool IsExist(string partNo, string timeStamp, string LineNo, string matId)
        {
            var _ds = dal.Exist(partNo, timeStamp, LineNo, matId);
            if (_ds != null && _ds.Tables.Count > 0)
            {
                if (_ds.Tables[0] != null && _ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
