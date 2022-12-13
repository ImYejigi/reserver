using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reserveOfStudyRoom
{

    public partial class 관리자_메인 : Form
    {
        string sqlstr;    //쿼리문 저장 변수
        DBClass dbc = new DBClass();  //*****DBClass 객체 생성
        private int intID; //ID 필드 설정
        private string strCommand;
        //데이터 삭제, 추가, 업데이트 인지를 설정할 문자열 필드
        private OracleConnection odpConn = new
         OracleConnection();
        public int getintID
        { get { return intID; } }
        public string getstrCommand
        { get { return strCommand; } }
        private void showDataGridView() //사용자 정의 함수
        {
            try
            {
                odpConn.ConnectionString = "User Id = moonu1; Password = 1111; Data Source = (DESCRIPTION = (ADDRESS =(PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = xe))); ";
                odpConn.Open();
                OracleDataAdapter oda = new OracleDataAdapter();
                oda.SelectCommand = new OracleCommand("SELECT * from phone", odpConn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                odpConn.Close();
                DBGrid.DataSource = dt;
                DBGrid.AutoResizeColumns();
                DBGrid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
                DBGrid.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
                DBGrid.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 발생 : " + ex.ToString());
                odpConn.Close();
            }
        }
        public 관리자_메인()
        {
            InitializeComponent();
        }

        private void 선택한행업데이트ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            strCommand = "업데이트";
            intID = Convert.ToInt32(DBGrid.SelectedCells[0].Value);
            회원_관리 form2 = new 회원_관리(this);
            form2.ShowDialog();
            form2.Dispose();
            showDataGridView();
        }

        private void 새로운데이터추가ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            strCommand = "추가";
            회원_관리 form2 = new 회원_관리(this);
            form2.ShowDialog();
            form2.Dispose();
            showDataGridView();
        }

        private void 선택한행삭제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            strCommand = "삭제";
            intID = Convert.ToInt32(DBGrid.SelectedCells[0].Value);
            회원_관리 form2 = new 회원_관리(this);
            form2.ShowDialog();
            form2.Dispose();
            showDataGridView();
        }

        private void 관리자_메인_Load(object sender, EventArgs e)
        {
            try
            {
                dbc.DS.Clear();
                dbc.DBAdapter.Fill(dbc.DS, "phone");
                DBGrid.DataSource = dbc.DS.Tables["phone"].DefaultView;
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
            catch (Exception DE)
            {
                MessageBox.Show(DE.Message);
            }
        }
    }
}
