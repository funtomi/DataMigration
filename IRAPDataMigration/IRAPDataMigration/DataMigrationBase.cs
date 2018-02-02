using IRAP.Global;
using Oracle.DataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace IRAP.DataMigration {
    public abstract class DataMigrationBase { 
        private static string _className = MethodBase.GetCurrentMethod().DeclaringType.FullName;
        protected static string CON_STR_SOURCE = ConfigurationManager.AppSettings["DBSource"].ToString();
        protected static string CON_STR_TARGET = ConfigurationManager.AppSettings["DBTarget"].ToString();
        protected object DataEntity {
            get { return _dataEntity; }
            set { _dataEntity = value; }
        }
        private object _dataEntity;

        public abstract DataTable GetSourceData(out int errCode, out string errText);
        public abstract void Query(out int errCode, out string errText);
        public abstract void InsertData(object obj, out int errCode, out string errText);
        public delegate void ShowResultDelegate(string businessType, string errText);
        public ShowResultDelegate ShowResult; 

        public void Start() { 
            Thread thread = new Thread(DoQuery);
            thread.IsBackground = true;
            thread.Start();
        }

        private void DoQuery() {
            //todo:定时启动
            int errCode = 0; string errText = "";
            Query(out errCode,out errText);
            //ShowResult("111", "222");
        }

        protected void SaveResult(Decimal transNo,int errCode,string errText) { 
            string strProcedureName = string.Format("{0}.{1}", _className, MethodBase.GetCurrentMethod().Name);
            WriteLog.Instance.WriteBeginSplitter(strProcedureName);
            try {
                WriteLog.Instance.Write(
                    string.Format("执行存储过程 " +
                        " usp_Back_TranResult，参数：" +
                        "i_ywno={0}|i_errCode={1}|i_errText={2} ",
                       transNo, errCode, errText),
                    strProcedureName);

                #region 执行数据库函数或存储过程
                using (OracleConnection conn = new OracleConnection(CON_STR_SOURCE)) {
                    conn.Open();
                    OracleCommand ocm = conn.CreateCommand();
                    ocm.CommandType = CommandType.StoredProcedure;
                    ocm.CommandText = "usp_Back_TranResult";
                    ocm.Parameters.Add("i_ywno", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_ywno"].Value = transNo;
                    ocm.Parameters.Add("i_errCode", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_errCode"].Value = errCode;
                    ocm.Parameters.Add("i_errText", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_errText"].Value = errText;
                    ocm.Parameters.Add("o_errCode", OracleDbType.Decimal).Direction = ParameterDirection.Output;
                    ocm.Parameters.Add("o_errtext", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    ocm.ExecuteNonQuery();
                    errCode = ocm.Parameters["o_errCode"] == null ? 0 : Convert.ToInt32(ocm.Parameters["o_errCode"].Value.ToString());
                    errText = ocm.Parameters["o_errtext"] == null || ocm.Parameters["o_errtext"].Value == null ? null : ocm.Parameters["o_errtext"].Value.ToString();
                    WriteLog.Instance.Write(errText, strProcedureName);
                }
                #endregion
            } catch (Exception error) {
                errCode = 99000;
                errText = string.Format("执行存储过程usp_Back_TranResult时发生异常：{0}", error.Message);
                WriteLog.Instance.Write(errText, strProcedureName);
            } finally {
                WriteLog.Instance.WriteEndSplitter(strProcedureName); 
            }  
        }

        
    }
}
