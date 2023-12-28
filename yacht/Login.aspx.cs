using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace yacht
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 檢查是否為首次加載頁面，避免在每次 postback 時重新隱藏 GridView
                GridView1.Visible = false;
            }
        }
        protected void Button_login_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtLogin2"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            //查詢跟參數的部分很難寫成方法，需自行研究
            string sql = $"select Id, IsManger from Login where account = @account and password = @password";

            //增加參數並設定值，記得用.叫出來
            sqlCommand.Parameters.AddWithValue("@account", TextBox_account.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@password", TextBox_password.Text.Trim());

            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            //執行該SQL查詢，用reader接資料
            SqlDataReader reader = sqlCommand.ExecuteReader();

            //int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
            //object result = sqlCommand.ExecuteScalar();

            //用read先叫出來，while也可以
            if (reader.Read())
            {
                //將是否為管理者設為boolean
                bool IsManger = Convert.ToBoolean(reader["IsManger"]);

                // 登入成功，取得使用者 ID，並存儲到 Session 中
                Session["LoginId"] = reader["Id"];

                Session["IsManger"] = IsManger;

                Response.Redirect("Register.aspx");
            }
            else
            {
                // 登入失敗
                Response.Write("<script>alert('帳號或密碼錯誤，請重新輸入');</script>");
            }
            connection.Close();
        }
    }
}