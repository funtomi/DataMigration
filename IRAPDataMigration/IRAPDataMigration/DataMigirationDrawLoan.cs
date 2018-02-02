using IRAP.Global;
using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace IRAP.DataMigration {
    class DataMigirationDrawLoan:DataMigrationBase {
        private static string className = MethodBase.GetCurrentMethod().DeclaringType.FullName;

        public override void Query(out int errCode, out string errText) {
            string strProcedureName = string.Format("{0}.{1}", className, MethodBase.GetCurrentMethod().Name);
            WriteLog.Instance.WriteBeginSplitter(strProcedureName);
            var sourceData = GetSourceData("uvw_todo_tqhd", out errCode, out errText);
            if (errCode != 0) {
                return;
            }
            var list = ModelConvertHelper<DrawLoanEntity>.ConvertToModel(sourceData).ToList<DrawLoanEntity>();
            if (list == null || list.Count == 0) {
                return;
            }
            foreach (DrawLoanEntity item in list) {
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
            var item = obj as DrawLoanEntity;
            if (item == null) {
                return;
            }
            try {
                WriteLog.Instance.Write(
                    string.Format("执行存储过程 " +
                        "usp_wt_tqhd，参数：" +
                        "i_ywno={0}|i_gracntno={1}|i_loanacntno={2}|i_payamount={3}|i_changetype={4}|i_monthpay={5}"
                        + "i_resuidmonths={6}|i_remark={6}",
                       item.TRANSACTNO, item.GRACNTNO, item.LOANACNTNO,item.PAYAMOUNT,item.CHANGETYPE,item.MONTHPAY,
                       item.RESUIDMONTHS,item.REMARK),
                    strProcedureName);

                #region 执行数据库函数或存储过程
                using (OracleConnection conn = new OracleConnection(CON_STR_TARGET)) {
                    conn.Open();
                    OracleCommand ocm = conn.CreateCommand();
                    ocm.CommandType = CommandType.StoredProcedure;
                    ocm.CommandText = "usp_wt_tqhd";
                    ocm.Parameters.Add("i_ywno", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_ywno"].Value = item.TRANSACTNO;
                    ocm.Parameters.Add("i_gracntno", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_gracntno"].Value = item.GRACNTNO;
                    ocm.Parameters.Add("i_loanacntno", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_loanacntno"].Value = item.LOANACNTNO;
                    ocm.Parameters.Add("i_payamount", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_payamount"].Value = item.PAYAMOUNT;
                    ocm.Parameters.Add("i_changetype", OracleDbType.Int64).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_changetype"].Value = item.CHANGETYPE;
                    ocm.Parameters.Add("i_monthpay", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_monthpay"].Value = item.MONTHPAY;
                    ocm.Parameters.Add("i_resuidmonths", OracleDbType.Int64).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_resuidmonths"].Value = item.RESUIDMONTHS;
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
                errText = string.Format("获取视图usp_wt_tqhd内容时发生异常：{0}", error.Message);
                ShowResult(item.REMARK, errText);
                WriteLog.Instance.Write(errText, strProcedureName);
            } finally {
                WriteLog.Instance.WriteEndSplitter(strProcedureName);
            }
        }
    }
}
