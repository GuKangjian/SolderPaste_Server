using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _Common
{
    public class Common
    {


        public static List<T> GetArrayObject<T>(DataTable table) where T : new()
        {

            List<T> list = new List<T>();

            PropertyInfo[] props = typeof(T).GetProperties();

            foreach (DataRow row in table.Rows)
            {

                T item = new T();

                foreach (PropertyInfo p in props)
                {
                    if (p.CanWrite && table.Columns.Contains(p.Name) && !(row[p.Name] is DBNull))
                    {
                        p.GetSetMethod().Invoke(
                            item
                            , new object[] { Convert.ChangeType(row[p.Name], p.PropertyType) }
                        );
                    }
                }

                list.Add(item);
            }

            return list;
        }

        public static string GetPropertyName<T>(Expression<Func<T, object>> expr)
        {
            string Name = string.Empty;

            if (expr.Body is UnaryExpression)
            {
                Name = ((MemberExpression)((UnaryExpression)expr.Body).Operand).Member.Name;
            }
            else if (expr.Body is MemberExpression)
            {
                Name = ((MemberExpression)expr.Body).Member.Name;
            }
            else if (expr.Body is ParameterExpression)
            {
                Name = ((ParameterExpression)expr.Body).Type.Name;
            }
            return Name;
        }

        /// <summary>
        /// 将DataTable转成单个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static T GetSingleObject<T>(DataTable table) where T : new()
        {

            T value = default(T);
            List<T> list = GetArrayObject<T>(table);
            if (list.Count > 0)
                value = list.FirstOrDefault();
            return value;
        }

        public static int GetProductSlot(int tableNo, int slotNum, int machineNo, int tableType, int feederType)
        {
            int stageNum = 0;
            int num = 0;
            if (tableType == 4)
            {

                stageNum = (machineNo - 1) * tableType + tableNo;
                num = slotNum;
                if (tableNo == 2)
                {
                    num = slotNum - 3 * feederType;
                }
                else if (tableNo == 3)
                {
                    num = slotNum - feederType;
                }
                else if (tableNo == 4)
                {
                    num = slotNum - 2 * feederType;
                }
            }
            else if (tableType == 2)
            {
                stageNum = (machineNo - 1) * tableType + tableNo;
                if (slotNum > feederType)
                {
                    num = slotNum - feederType;
                }
                else
                {
                    num = slotNum;
                }
            }

            int slot = stageNum * 10000 + num;
            return slot;
        }


        public static int GetSlotNo(int tableNo, int slotNum, int tableType, int feederType)
        {
            int num = 0;
            if (tableType == 4)
            {
                num = slotNum;
                if (tableNo == 2)
                {
                    num = slotNum - 3 * feederType;
                }
                else if (tableNo == 3)
                {
                    num = slotNum - feederType;
                }
                else if (tableNo == 4)
                {
                    num = slotNum - 2 * feederType;
                }
            }
            else if (tableType == 2)
            {

                if (slotNum > feederType)
                {
                    num = slotNum - feederType;
                }
                else
                {
                    num = slotNum;
                }
            }

            return num;
        }


        public static int GetSpliceSlot(string tableSlotNum, int machineNo, int tableType, int feederType)
        {

            int tableNo = int.Parse(tableSlotNum.Substring(0, tableSlotNum.Length - 4));
            int num = int.Parse(tableSlotNum.Substring(tableSlotNum.Length - 4));

            int stageNum = (machineNo - 1) * tableType + tableNo;

            int slot = stageNum * 10000 + num;
            return slot;
        }

       
        /// <summary>
        /// Get machineNo
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public static int GetMachineNo(string slot, int tableType)
        {
            int machineNo = 0;
            int tableside = int.Parse(slot.Substring(0, slot.Length - 4));
            if (tableside % tableType == 0)
            {
                machineNo = tableside / tableType;
            }
            else
            {
                machineNo = (tableside / tableType) + 1;
            }
            return machineNo;
        }
        /// <summary>
        /// Get StageNo
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public static int GetStageNo(string slot, int machineNo, int tableType)
        {
            int stageNo = 1;
            int tableside = int.Parse(slot.Substring(0, slot.Length - 4));

            int table = tableside - ((machineNo - 1) * tableType);

            if (table > 2)
            {
                stageNo = 2;
            }

            return stageNo;
        }

        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] StrToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

    }
}
