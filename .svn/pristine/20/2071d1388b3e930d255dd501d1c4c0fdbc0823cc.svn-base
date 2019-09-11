using _Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _BLL
{
    public class RequestInfo
    {
        private readonly _DAL.RequestInfo dal = new _DAL.RequestInfo();
        public RequestInfo()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(_Model.RequestInfo model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }


        /// <summary>
        /// 获得数据 第二
        /// </summary>
        public IList<_Model.RequestInfo> GetRequestInfo(string LineNo)
        {
            var _dt = dal.GetRequestInfo(LineNo);
            if (_dt != null)
                return ModelConvertHelper<_Model.RequestInfo>.ConvertToModel(_dt);
            else
                return null;
        }

        /// <summary>
        /// 获得数据 第一
        /// </summary>
        public IList<_Model.RequestInfo> GetRequest()
        {
            var _dt = dal.GetRequest();
            if (_dt != null)
                return ModelConvertHelper<_Model.RequestInfo>.ConvertToModel(_dt);
            else
                return null;
        }





        public bool IsExist(string typeNo, string timeStamp)
        {
            var _ds = dal.Exist(typeNo, timeStamp);
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



        public IList<_Model.RequestInfo> GetNewRequest(string lineNo)
        {
            var _dt = dal.GetNewRequest(lineNo);
            if (_dt != null)
                return ModelConvertHelper<_Model.RequestInfo>.ConvertToModel(_dt);
            else
                return null;
        }

        public bool IsExist(string typeNo, string timeStamp, string fileTime, string _LineNo)
        {
            var _ds = dal.Exist(typeNo, timeStamp, fileTime, _LineNo);
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
