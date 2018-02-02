using DevExpress.XtraGrid.Views.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRAP.DataMigration {
    public partial class Form1 : Form {
        private string _tempBusinessType = "";
        private string _tempErrText = "";
        delegate void AsynUpdateUI(string businessType, string errText);
        public Form1() {
            InitializeComponent(); 
        }

        private void ShowResultToGrid(string businessType, string errText) {
            this.Invoke(new Action<string, string>((m, n) => {
                _tempBusinessType = m;
                _tempErrText = n;
                this.gridView1.AddNewRow();
            }), businessType, errText); 
        }

        private DataTable CreateDataSource() {
            DataTable dt = new DataTable("data");
            dt.Columns.Add(new DataColumn("SeqNo", typeof(int)));
            dt.Columns.Add(new DataColumn("BusinessType", typeof(string)));
            dt.Columns.Add(new DataColumn("Message", typeof(string)));
            return dt;
        } 

        private void CreateTasks() {
            //转移
            DataMigirationTransfer trans = new DataMigirationTransfer();
            trans.ShowResult += ShowResultToGrid;
            trans.Start();
            //提取
            DataMigirationDraw draw = new DataMigirationDraw();
            draw.ShowResult += ShowResultToGrid;
            draw.Start();
            //提取还贷签约
            DataMigirationDrawLoanContract drawLC = new DataMigirationDrawLoanContract();
            drawLC.ShowResult += ShowResultToGrid;
            drawLC.Start();
            //提取还贷
            DataMigirationDrawLoan drawLoan = new DataMigirationDrawLoan();
            drawLoan.ShowResult += ShowResultToGrid;
            drawLoan.Start();
            //贷款
            DataMigirationLoan loan = new DataMigirationLoan();
            loan.ShowResult += ShowResultToGrid;
            loan.Start();
        }

        private void Form1_Load(object sender, EventArgs e) {
            this.gridControl1.DataSource = this.gridControl1.DataSource == null ? CreateDataSource() : this.gridControl1.DataSource;
            CreateTasks();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e) {
            lock (this.gridView1) {
                ColumnView view = sender as ColumnView;
                view.SetRowCellValue(e.RowHandle, "SeqNo", view.RowCount);
                view.SetRowCellValue(e.RowHandle, "BusinessType", _tempBusinessType);
                view.SetRowCellValue(e.RowHandle, "Message", _tempErrText);
            }
        }
    }
}
