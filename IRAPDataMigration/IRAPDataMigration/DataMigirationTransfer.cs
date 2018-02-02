using IRAP.Global;
using Oracle.DataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IRAP.DataMigration {
    class DataMigirationTransfer:DataMigrationBase {
        public DataMigirationTransfer() { 
        }
        private static string className = MethodBase.GetCurrentMethod().DeclaringType.FullName;

        public override System.Data.DataTable GetSourceData(out int errCode,out string errText) {
            string strProcedureName = string.Format( "{0}.{1}", className, MethodBase.GetCurrentMethod().Name);
            WriteLog.Instance.WriteBeginSplitter(strProcedureName);
            errCode = 0;
            errText = "";
            try { 
                WriteLog.Instance.Write("获取视图uvw_todo_grzy内容：", strProcedureName);

                #region 执行数据库函数或存储过程
                using (OracleConnection conn = new OracleConnection(CON_STR_SOURCE)) {
                    conn.Open();
                    string sql = "select * from UVW_TODO_GRZY";
                    OracleDataAdapter ada = new OracleDataAdapter(sql, conn);
                    //OracleCommand cmd = new OracleCommand(sql, conn);
                    //var result = cmd.ExecuteScalar();
                    DataTable dt = new DataTable();
                    ada.Fill(dt); 
                    return dt;
                }
                #endregion
            } catch (Exception error) {
                errCode = 99000;
                errText = string.Format("获取视图uvw_todo_grzy内容时发生异常：{0}", error.Message);
                return null;
            } finally {
                WriteLog.Instance.WriteEndSplitter(strProcedureName);
            }
        }

        public override void Query(out int errCode, out string errText) {
            string strProcedureName = string.Format( "{0}.{1}", className, MethodBase.GetCurrentMethod().Name);
            WriteLog.Instance.WriteBeginSplitter(strProcedureName); 
            var sourceData = GetSourceData(out errCode, out errText);
            if (errCode !=0) {
                return;
            }
            var list = ModelConvertHelper<TransferEntity>.ConvertToModel(sourceData).ToList<TransferEntity>();
            if (list == null||list.Count == 0) {
                return;
            }
            foreach (TransferEntity item in list) {
                if (item == null) {
                    continue;
                }
                InsertData(item,out errCode,out errText);
            }
        }

        public override void InsertData(object obj, out int errCode, out string errText) {
            string strProcedureName = string.Format("{0}.{1}", className, MethodBase.GetCurrentMethod().Name);
            WriteLog.Instance.WriteBeginSplitter(strProcedureName);
            errCode = 0;
            errText = "";
            if (obj == null) {
                return;
            }
            var item = obj as TransferEntity;
            if (item == null) {
                return;
            }
            try {
                WriteLog.Instance.Write(
                    string.Format("执行存储过程 " +
                        "usp_wt_grzy，参数：" +
                        "i_ywno={0}|i_gracntno={1}|i_dwacntno={2}|i_dstdwacntno={3}|i_remark={4}",
                       item.TRANSACTNO, item.GRACNTNO, item.SRCDWACNTNO, item.DSTDWACNTNO, item.REMARK),
                    strProcedureName);

                #region 执行数据库函数或存储过程
                using (OracleConnection conn = new OracleConnection(CON_STR_TARGET)) {
                    conn.Open();
                    OracleCommand ocm = conn.CreateCommand();
                    ocm.CommandType = CommandType.StoredProcedure;
                    ocm.CommandText = "usp_wt_grzy";
                    ocm.Parameters.Add("i_ywno", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_ywno"].Value = item.TRANSACTNO;
                    ocm.Parameters.Add("i_gracntno", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_gracntno"].Value = item.GRACNTNO;
                    ocm.Parameters.Add("i_dwacntno", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_dwacntno"].Value = item.SRCDWACNTNO;
                    ocm.Parameters.Add("i_dstdwacntno", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_dstdwacntno"].Value = item.DSTDWACNTNO;
                    ocm.Parameters.Add("i_remark", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_remark"].Value = item.REMARK;
                    ocm.Parameters.Add("i_amount", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_remark"].Value = 0;//todo:现在没有这个参数，后面添上
                    ocm.Parameters.Add("o_errCode", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    ocm.Parameters.Add("o_errtext", OracleDbType.Varchar2,100).Direction = ParameterDirection.Output;
                    ocm.ExecuteNonQuery();
                    errCode = ocm.Parameters["o_errCode"] == null ? 0 : Convert.ToInt32(ocm.Parameters["o_errCode"].Value.ToString());
                    errText = ocm.Parameters["o_errtext"] == null || ocm.Parameters["o_errtext"].Value == null ? null : ocm.Parameters["o_errtext"].Value.ToString();
                }
                ShowResult(item.REMARK, errText);
                this.SaveResult(item.TRANSACTNO, errCode, errText);
                #endregion
            } catch (Exception error) {
                errCode = 99000;
                errText = string.Format("获取视图uvw_todo_grzy内容时发生异常：{0}", error.Message);
                ShowResult(item.REMARK, errText); 
                WriteLog.Instance.Write(errText, strProcedureName);
            } finally {
                WriteLog.Instance.WriteEndSplitter(strProcedureName); 
            } 
        }
         
    }
}
