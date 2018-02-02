using IRAP.Global;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IRAP.DataMigration {
    class DataMigirationDraw : DataMigrationBase {
        private static string className = MethodBase.GetCurrentMethod().DeclaringType.FullName; 

        public override void Query(out int errCode, out string errText) {
            string strProcedureName = string.Format("{0}.{1}", className, MethodBase.GetCurrentMethod().Name);
            WriteLog.Instance.WriteBeginSplitter(strProcedureName);
            var sourceData = GetSourceData("uvw_todo_draw", out errCode, out errText);
            if (errCode != 0) {
                return;
            }
            var list = ModelConvertHelper<DrawEntity>.ConvertToModel(sourceData).ToList<DrawEntity>();
            if (list == null || list.Count == 0) {
                return;
            }
            foreach (DrawEntity item in list) {
                if (item == null) {
                    continue;
                }
                InsertData(item, out errCode, out errText);
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
            var item = obj as DrawEntity;
            if (item == null) {
                return;
            }
            try {
                WriteLog.Instance.Write(
                    string.Format("执行存储过程 " +
                        "usp_wt_draw，参数：" +
                        "i_ywno={0}|i_gracntno={1}|i_gflxid={2}|i_contractno={3}|i_bankcode={4}|i_cardno={5}"+
                        "|i_grname={6}|i_mobileno={7}|i_bjamount={8}|i_lxamount={9}|i_remark={10}",
                       item.TRANSACTNO, item.GRACNTNO, item.GFLXID, item.CONTRACTNO, item.BANKCODE,item.CARDNO,
                       item.GRNAME,item.MOBILENO,item.BJAMOUNT,item.LXAMOUNT,item.REMARK),
                    strProcedureName);

                #region 执行数据库函数或存储过程
                using (OracleConnection conn = new OracleConnection(CON_STR_TARGET)) {
                    conn.Open();
                    OracleCommand ocm = conn.CreateCommand();
                    ocm.CommandType = CommandType.StoredProcedure;
                    ocm.CommandText = "usp_wt_draw";
                    ocm.Parameters.Add("i_ywno", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_ywno"].Value = item.TRANSACTNO;
                    ocm.Parameters.Add("i_gracntno", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_gracntno"].Value = item.GRACNTNO;
                    ocm.Parameters.Add("i_gflxid", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_gflxid"].Value = item.GFLXID;
                    ocm.Parameters.Add("i_contractno", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_contractno"].Value = item.CONTRACTNO;
                    ocm.Parameters.Add("i_bankcode", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_bankcode"].Value = item.BANKCODE;
                    ocm.Parameters.Add("i_cardno", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_cardno"].Value = item.CARDNO;
                    ocm.Parameters.Add("i_grname", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_grname"].Value = item.GRNAME;
                    ocm.Parameters.Add("i_mobileno", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_mobileno"].Value = item.MOBILENO;
                    ocm.Parameters.Add("i_bjamount", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_bjamount"].Value = item.BJAMOUNT;
                    ocm.Parameters.Add("i_lxamount", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_lxamount"].Value = item.LXAMOUNT; 
                    ocm.Parameters.Add("i_remark", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_remark"].Value = item.REMARK;
                    ocm.Parameters.Add("o_errCode", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    ocm.Parameters.Add("o_errtext", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    ocm.ExecuteNonQuery();
                    errCode = ocm.Parameters["o_errCode"] == null ? 0 : Convert.ToInt32(ocm.Parameters["o_errCode"].Value.ToString());
                    errText = ocm.Parameters["o_errtext"] == null || ocm.Parameters["o_errtext"].Value == null ? null : ocm.Parameters["o_errtext"].Value.ToString();
                }
                var errInfo = errText;
                this.SaveResult(item.TRANSACTNO, errCode, errText);
                errInfo += errText;
                ShowResult(item.REMARK, errInfo);
                #endregion
            } catch (Exception error) {
                errCode = 99000;
                errText = string.Format("获取视图usp_wt_draw内容时发生异常：{0}", error.Message);
                ShowResult(item.REMARK, errText);
                WriteLog.Instance.Write(errText, strProcedureName);
            } finally {
                WriteLog.Instance.WriteEndSplitter(strProcedureName);
            }
        }
    }
}
