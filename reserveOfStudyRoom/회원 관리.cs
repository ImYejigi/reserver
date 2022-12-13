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
    public partial class 회원_관리 : Form
    {
        private OracleConnection odpConn = new OracleConnection();
        관리자_메인 _parent; 
        string sqlstr;    //쿼리문 저장 변수
        DBClass dbc = new DBClass();  //*****DBClass 객체 생성
        private int intID; //ID 필드 설정
        private string strCommand;
        //데이터 삭제, 추가, 업데이트 인지를 설정할 문자열 필드
        public int getintID
        { get { return intID; } }
        public string getstrCommand
        { get { return strCommand; } }
        public 회원_관리(관리자_메인 inform1)
        {
            InitializeComponent();
            _parent = inform1;
        }

        private void 회원_관리_Load(object sender, EventArgs e)
        {
            if (_parent.getstrCommand == "삭제")
            {
                txtUserNo.Enabled = false;
                initialTextBoxes();
            }
            try
            {
                dbc.DB_Open();//*****
                sqlstr = "Select * From reserver ORDER BY id ASC";
                dbc.DCom.CommandText = sqlstr;
                dbc.DA.SelectCommand = dbc.DCom;
                dbc.DA.Fill(dbc.DS, "reserver");
                dbc.DS.Tables["reserver"].Clear();
                dbc.DA.Fill(dbc.DS, "reserver");
                DBGrid.DataSource = dbc.DS.Tables["reserver"].DefaultView;
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
        private void initialTextBoxes()//사용자 함수 정의
        {
            odpConn.ConnectionString = "User Id = moonu1; Password = 1111; Data Source = (DESCRIPTION = (ADDRESS =(PROTOCOL = TCP) (HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe))); ";
            odpConn.Open();
            string strqry = "select * from reserver where id=:id";
            OracleCommand OraCmd = new OracleCommand(strqry,
            odpConn);
            OraCmd.Parameters.Add("id",
            OracleDbType.Int32).Value = _parent.getintID;
            OracleDataReader odr = OraCmd.ExecuteReader();
            while (odr.Read())
            {
                txtUserNo.Text = Convert.ToString(odr.GetValue(0));
                txtUserName.Text = Convert.ToString(odr.GetValue(1));
            }
            odr.Close();
            odpConn.Close();
        }
        private int INSERTRow()//사용자 함수 정의
        {
            odpConn.ConnectionString = "User Id = moonu1; Password = 1111; Data Source = (DESCRIPTION = (ADDRESS =(PROTOCOL = TCP) (HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe))); ";
            odpConn.Open();
            string strqry = "INSERT INTO reserver VALUES (:id, :name)";
            OracleCommand OraCmd = new OracleCommand(strqry, odpConn);

            OraCmd.Parameters.Add("id", OracleDbType.Int32).Value =
            txtUserNo.Text.Trim();
            OraCmd.Parameters.Add("name", OracleDbType.Varchar2,
            20).Value = txtUserName.Text.Trim();
            return OraCmd.ExecuteNonQuery(); //추가되는 행수 반환
        }
        private int DELETERow() //사용자 함수 정의
        {
            odpConn.ConnectionString = "User Id = moonu1; Password = 1111; Data Source = (DESCRIPTION = (ADDRESS =(PROTOCOL = TCP) (HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe))); ";
            odpConn.Open();
            string strqry = "DELETE FROM reserver WHERE id=:id";
            OracleCommand OraCmd = new OracleCommand(strqry, odpConn);
            OraCmd.Parameters.Add("id", OracleDbType.Int32).Value
            = _parent.getintID;
            return OraCmd.ExecuteNonQuery();
        }

    }
}
