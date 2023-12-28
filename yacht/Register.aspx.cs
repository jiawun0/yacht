using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LoginId"] != null)
                {
                    string loginId = Session["LoginId"].ToString();
                    bool isManger = (Session["isManger"] != null) ? (bool)Session["isManger"] : false;

                    if (loginId != null && isManger)
                    {
                        //顯示登入者
                        string name = Showusername(loginId);
                        Literal_name.Text = "歡迎, " + name + "!";
                        ShowDB();
                    }
                    else
                    {
                        //非IsManger，請重新登入
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    //尚未登入，請登入
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void Button_Register_Click(object sender, EventArgs e)
        {
            bool haveSameAccount = false;

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtLogin2"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            string sqlCheck = "SELECT * FROM Login WHERE account = @account";

            //查詢跟參數的部分很難寫成方法，需自行研究
            string sql = $"insert into Login (name, account, password, salt) values (@name, @account, @password, @salt)";

            //發送SQL語法，取得結果
            SqlCommand commandCheck = new SqlCommand(sqlCheck, connection);
            SqlCommand commandAdd = new SqlCommand(sql, connection);

            //檢查有無重複帳號
            commandCheck.Parameters.AddWithValue("@account", TextBox_account.Text.Trim());

            SqlDataReader readerCountry = commandCheck.ExecuteReader();
            if (readerCountry.Read())
            {
                haveSameAccount = true;
                //帳號重複通知
                Response.Write("<script>alert('帳號已經存在');</script>");
            }
            connection.Close();

            //無重複帳號才執行加入
            if (!haveSameAccount)
            {
                //Hash 加鹽加密
                string password = TextBox_password.Text;
                var salt = CreateSalt();
                string saltStr = Convert.ToBase64String(salt); //將 byte 改回字串存回資料表
                var hash = HashPassword(password, salt);
                string hashPassword = Convert.ToBase64String(hash);

                commandAdd.Parameters.AddWithValue("@name", TextBox_name.Text);
                commandAdd.Parameters.AddWithValue("@account", TextBox_account.Text);
                commandAdd.Parameters.AddWithValue("@password", hashPassword);
                commandAdd.Parameters.AddWithValue("@salt", saltStr);

                string Password = TextBox_password.Text.Trim();
                string pwCheck = TextBox_pwCheck.Text.Trim();

                if (TextBox_password.Text.Length < 6)
                {
                    Response.Write("<script>alert('密碼必須至少包含 6 個字元');</script>");
                    connection.Close();
                    return;
                }

                if (Password != pwCheck)
                {
                    Response.Write("<script>alert('密碼和確認密碼不相符');</script>");
                    connection.Close();
                    return;
                }

                connection.Open();
                commandAdd.ExecuteNonQuery();
                connection.Close();
                //畫面渲染
                GridView_Register.DataBind();
                //清空輸入欄位
                TextBox_name.Text = "";
                TextBox_account.Text = "";
                TextBox_password.Text = "";
                LabelAdd.Visible = false;

                Response.Redirect("Register.aspx");
            }
        }

        void ShowDB()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtLogin2"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = "select * from Login ";

            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            GridView_Register.DataSource = reader;

            GridView_Register.DataBind();

            connection.Close();
        }

        string Showusername(string LoginId)
        {
            string name = "";

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtLogin2"].ConnectionString);
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string sql = "select name from Login where Id = @LoginId";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.Parameters.AddWithValue("@LoginId", LoginId);

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    name = reader["name"].ToString();
                    break;
                }
            }

            connection.Close();

            return name;
        }

        // Argon2 加密
        //產生 Salt 功能
        private byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }
        // Hash 處理加鹽的密碼功能
        private byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            //底下這些數字會影響運算時間，而且驗證時要用一樣的值
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // 4 核心就設成 8
            argon2.Iterations = 4; // 迭代運算次數
            argon2.MemorySize = 1024 * 1024; // 1 GB

            return argon2.GetBytes(16);
        }

        protected void GridView_Register_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView_Register.EditIndex = e.NewEditIndex;
            ShowDB();
        }

        protected void GridView_Register_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView_Register.Rows[e.RowIndex];

            int boardId = Convert.ToInt32(GridView_Register.DataKeys[e.RowIndex].Value);

            TextBox textBox_accountT = row.FindControl("TextBox_accountT") as TextBox;
            string changeText_accountT = textBox_accountT.Text;

            //TextBox textBox_passwordT = row.FindControl("TextBox_passwordT") as TextBox;
            //string changeText_passwordT = textBox_passwordT.Text;

            TextBox textBox_nameT = row.FindControl("TextBox_nameT") as TextBox;
            string changeText_nameT = textBox_nameT.Text;

            CheckBox checkBox_isManger = row.FindControl("CheckBox_isManger") as CheckBox;
            bool isManger = Convert.ToBoolean(checkBox_isManger.Checked);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtLogin2"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = $"update Login set account = @account, name = @name, isManger = @isManger where Id = @BoardId ";

            sqlCommand.Parameters.AddWithValue("@account", changeText_accountT);
            //sqlCommand.Parameters.AddWithValue("@password", changeText_passwordT);
            sqlCommand.Parameters.AddWithValue("@name", changeText_nameT);
            sqlCommand.Parameters.AddWithValue("@isManger", isManger);
            sqlCommand.Parameters.AddWithValue("@BoardId", boardId);
            sqlCommand.CommandText = sql;

            sqlCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('更新成功');</script>");
            GridView_Register.EditIndex = -1;
            ShowDB();
        }

        protected void GridView_Register_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int boardId = Convert.ToInt32(GridView_Register.DataKeys[e.RowIndex].Value);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtLogin2"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string deleteSql = $"delete from Login where Id = @boardId ";
            SqlCommand deleteCommand = new SqlCommand(deleteSql, connection);
            deleteCommand.Parameters.AddWithValue("@boardId", boardId);
            deleteCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('刪除成功');</script>");

            ShowDB();
        }

        protected void GridView_Register_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_Register.EditIndex = -1;
            ShowDB();
        }
    }
}