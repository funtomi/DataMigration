using IRAP.Global;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IRAP.DataMigration {
    class DataMigirationLoan :DataMigrationBase{
        private static string className = MethodBase.GetCurrentMethod().DeclaringType.FullName;

        public override void Query(out int errCode, out string errText) {
            string strProcedureName = string.Format("{0}.{1}", className, MethodBase.GetCurrentMethod().Name);
            WriteLog.Instance.WriteBeginSplitter(strProcedureName);
            var sourceData = GetSourceData("uvw_todo_dksl", out errCode, out errText);
            if (errCode != 0) {
                return;
            }
            var list = ModelConvertHelper<LoanEntity>.ConvertToModel(sourceData).ToList<LoanEntity>();
            if (list == null || list.Count == 0) {
                return;
            }
            foreach (LoanEntity item in list) {
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
            var item = obj as LoanEntity;
            if (item == null) {
                return;
            }
            try {
                WriteLog.Instance.Write(
                    string.Format("执行存储过程 " +
                        "usp_wt_dksl，参数：" +
                        "i_ywno={0}|i_gracntno={1}|i_grname={2}|i_citizenidno={3}|i_hyid={4}|i_hk={5}|i_dwname={6}"
                        + "|i_dept={7}|i_address={8}|i_postcode={9}|i_jobid={10}|i_ZCID={11}|i_HomeAddress={12}"
                        + "|i_homepostcode={13}|i_Salary={14}|i_PeopleOfFamily={15}|i_Debit={16}|i_MobileNo={17}|i_GRAcntNo2={18}"
                        + "|i_GRName2={19}|i_CitizenIDNo2={20}|i_HK2={21}|i_DWName2={22}|i_Dept2={23}|i_Address2={24}"
                        + "|i_PostCode2={25}|i_JobID2={26}|i_ZCID2={27}|i_MobileNo2={28}|i_GFLXID={29}|i_ContractNo={30}"
                        + "|i_Seller={31}|i_HouseAddress={32}|i_BuildArea={33}|i_PayMoney={34}|i_HouseAmount={35}|i_HousePrice={36}"
                        + "|i_CertNo={37}|i_ContractDate={38}|i_DKType={39}|i_LoanAmount={40}|i_LoanMonths={41}|i_PayMethod={42}"
                        + "|i_IsBizLoan={43}|i_BizLoanAmount={44}|i_BizLoanMonth={45}|i_IsMerge={46}|i_MergeAmount={47}|i_BuyHouseType={48}"
                        + "|i_GJJLoanInfo={49}|i_DYType={50}|i_BDCQZ={51}|i_DYHouseAddress={52}|i_DYBuildArea={53}|i_DYBelong={54}"
                        + "|i_PayBankCode={55}|i_PayAcntName={56}|i_PayAcntNo={57}|i_remark={58}",
                       item.TRANSACTNO, item.GRACNTNO, item.GRNAME, item.CITIZENIDNO, item.HYID, item.HK,item.DWNAME,
                       item.DEPT, item.ADDRESS, item.POSTCODE, item.JOBID, item.ZCID, item.HOMEADDRESS,
                       item.HOMEPOSTCODE, item.SALARY, item.PEOPLEOFFAMILY, item.DEBIT, item.MOBILENO, item.GRACNTNO2,
                       item.GRNAME2, item.CITIZENIDNO2, item.HK2, item.DWNAME2, item.DEPT2, item.ADDRESS2,
                       item.POSTCODE2, item.JOBID2, item.ZCID2, item.MOBILENO2, item.GFLXID, item.CONTRACTNO,
                       item.SELLER, item.HOUSEADDRESS, item.BUILDAREA, item.PAYMONEY, item.HOUSEAMOUNT, item.HOUSEPRICE,
                       item.CERTNO, item.CONTRACTDATE, item.DKTYPE, item.LOANAMOUNT, item.LOANMONTHS, item.PAYMETHOD,
                       item.ISBIZLOAN, item.BIZLOANAMOUNT, item.BIZLOANMONTH, item.ISMERGE, item.MERGEAMOUNT, item.BUYHOUSETYPE,
                       item.GJJLOANINFO, item.DYTYPE, item.BDCQZ, item.DYHOUSEADDRESS, item.DYBUILDAREA, item.DYBELONG,
                       item.PAYBANKCODE, item.PAYACNTNAME, item.PAYACNTNO ,item.REMARK),
                    strProcedureName);

                #region 执行数据库函数或存储过程
                using (OracleConnection conn = new OracleConnection(CON_STR_TARGET)) {
                    conn.Open();
                    OracleCommand ocm = conn.CreateCommand();
                    ocm.CommandType = CommandType.StoredProcedure;
                    ocm.CommandText = "usp_wt_dksl";
                    #region 添加参数 
                    ocm.Parameters.Add("i_ywno", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_ywno"].Value = item.TRANSACTNO;
                    ocm.Parameters.Add("i_gracntno", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_gracntno"].Value = item.GRACNTNO;
                    ocm.Parameters.Add("i_citizenidno", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_citizenidno"].Value = item.CITIZENIDNO;
                    ocm.Parameters.Add("i_hyid", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_hyid"].Value = item.HYID;
                    ocm.Parameters.Add("i_hk", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_hk"].Value = item.HK;
                     ocm.Parameters.Add("i_dwname", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_dwname"].Value = item.DWNAME; 
                    ocm.Parameters.Add("i_dept", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_dept"].Value = item.DEPT; 
                    ocm.Parameters.Add("i_address", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_address"].Value = item.ADDRESS; 
                    ocm.Parameters.Add("i_postcode", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_postcode"].Value = item.POSTCODE; 
                    ocm.Parameters.Add("i_jobid", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_jobid"].Value = item.JOBID; 
                    ocm.Parameters.Add("i_ZCID", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_ZCID"].Value = item.ZCID; 
                    ocm.Parameters.Add("i_HomeAddress", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_HomeAddress"].Value = item.HOMEADDRESS; 
                    ocm.Parameters.Add("i_homepostcode", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_homepostcode"].Value = item.HOMEPOSTCODE; 
                    ocm.Parameters.Add("i_Salary", OracleDbType.Double).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_Salary"].Value = item.SALARY; 
                    ocm.Parameters.Add("i_PeopleOfFamily", OracleDbType.Int64).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_PeopleOfFamily"].Value = item.PEOPLEOFFAMILY; 
                    ocm.Parameters.Add("i_Debit", OracleDbType.Double).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_Debit"].Value = item.DEBIT; 
                    ocm.Parameters.Add("i_MobileNo", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_MobileNo"].Value = item.MOBILENO; 
                    ocm.Parameters.Add("i_GRAcntNo2", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_GRAcntNo2"].Value = item.GRACNTNO2; 
                    ocm.Parameters.Add("i_GRName2", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_GRName2"].Value = item.GRNAME2;
                    ocm.Parameters.Add("i_CitizenIDNo2", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_CitizenIDNo2"].Value = item.CITIZENIDNO2;
                    ocm.Parameters.Add("i_HK2", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_HK2"].Value = item.HK2;
                    ocm.Parameters.Add("i_DWName2", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_DWName2"].Value = item.DWNAME2;
                    ocm.Parameters.Add("i_Dept2", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_Dept2"].Value = item.DEPT2;
                    ocm.Parameters.Add("i_Address2", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_Address2"].Value = item.ADDRESS2;
                    ocm.Parameters.Add("i_PostCode2", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_PostCode2"].Value = item.POSTCODE2;
                    ocm.Parameters.Add("i_JobID2", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_JobID2"].Value = item.JOBID2;
                    ocm.Parameters.Add("i_ZCID2", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_ZCID2"].Value = item.ZCID2;
                    ocm.Parameters.Add("i_MobileNo2", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_MobileNo2"].Value = item.MOBILENO2;
                    ocm.Parameters.Add("i_GFLXID", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_GFLXID"].Value = item.GFLXID;
                    ocm.Parameters.Add("i_ContractNo", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_ContractNo"].Value = item.CONTRACTNO;
                    ocm.Parameters.Add("i_Seller", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_Seller"].Value = item.SELLER;
                    ocm.Parameters.Add("i_HouseAddress", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_HouseAddress"].Value = item.HOUSEADDRESS;
                    ocm.Parameters.Add("i_BuildArea", OracleDbType.Double).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_BuildArea"].Value = item.BUILDAREA; 
                    ocm.Parameters.Add("i_PayMoney", OracleDbType.Double).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_PayMoney"].Value = item.PAYMONEY;
                    ocm.Parameters.Add("i_HouseAmount", OracleDbType.Double).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_HouseAmount"].Value = item.HOUSEAMOUNT;
                    ocm.Parameters.Add("i_HousePrice", OracleDbType.Double).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_HousePrice"].Value = item.HOUSEPRICE;
                    ocm.Parameters.Add("i_CertNo", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_CertNo"].Value = item.CERTNO;
                    ocm.Parameters.Add("i_ContractDate", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_ContractDate"].Value = item.CONTRACTNO;
                    ocm.Parameters.Add("i_DKType", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_DKType"].Value = item.DKTYPE;
                    ocm.Parameters.Add("i_LoanAmount", OracleDbType.Double).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_LoanAmount"].Value = item.LOANAMOUNT;
                    ocm.Parameters.Add("i_LoanMonths", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_LoanMonths"].Value = item.LOANMONTHS;
                    ocm.Parameters.Add("i_PayMethod", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_PayMethod"].Value = item.PAYMETHOD;
                    ocm.Parameters.Add("i_IsBizLoan", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_IsBizLoan"].Value = item.ISBIZLOAN;
                    ocm.Parameters.Add("i_BizLoanAmount", OracleDbType.Double).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_BizLoanAmount"].Value = item.BIZLOANAMOUNT;
                    ocm.Parameters.Add("i_BizLoanMonth", OracleDbType.Double).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_BizLoanMonth"].Value = item.BIZLOANMONTH;
                    ocm.Parameters.Add("i_IsMerge", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_IsMerge"].Value = item.ISMERGE;
                    ocm.Parameters.Add("i_MergeAmount", OracleDbType.Double).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_MergeAmount"].Value = item.MERGEAMOUNT;
                    ocm.Parameters.Add("i_BuyHouseType", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_BuyHouseType"].Value = item.BUYHOUSETYPE; 
                    ocm.Parameters.Add("i_GJJLoanInfo", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_GJJLoanInfo"].Value = item.GJJLOANINFO;
                    ocm.Parameters.Add("i_DYType", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_DYType"].Value = item.DYTYPE;
                    ocm.Parameters.Add("i_BDCQZ", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_BDCQZ"].Value = item.BDCQZ;
                    ocm.Parameters.Add("i_DYHouseAddress", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_DYHouseAddress"].Value = item.DYHOUSEADDRESS;
                    ocm.Parameters.Add("i_DYBuildArea", OracleDbType.Double).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_DYBuildArea"].Value = item.DYBUILDAREA;
                    ocm.Parameters.Add("i_DYBelong", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_DYBelong"].Value = item.DYBELONG;
                    ocm.Parameters.Add("i_PayBankCode", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_PayBankCode"].Value = item.PAYBANKCODE;
                    ocm.Parameters.Add("i_PayAcntName", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_PayAcntName"].Value = item.PAYACNTNAME;
                    ocm.Parameters.Add("i_PayAcntNo", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_PayAcntNo"].Value = item.PAYACNTNO;
                    ocm.Parameters.Add("i_DYBelong", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_DYBelong"].Value = item.DYBELONG; 
                    ocm.Parameters.Add("i_remark", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    ocm.Parameters["i_remark"].Value = item.REMARK;
                    ocm.Parameters.Add("o_errCode", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    ocm.Parameters.Add("o_errtext", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    #endregion
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
                errText = string.Format("获取视图usp_wt_dksl内容时发生异常：{0}", error.Message);
                ShowResult(item.REMARK, errText);
                WriteLog.Instance.Write(errText, strProcedureName);
            } finally {
                WriteLog.Instance.WriteEndSplitter(strProcedureName);
            }
        }
    }
}
