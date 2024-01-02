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
using System.Web.Security;
using System.Data;

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
            string password = TextBox_password.Text;

            //連線資料庫
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtLogin2"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //查詢跟參數的部分很難寫成方法，需自行研究
            string sql = $"select * from Login where account = @account ";

            //創建 command 物件發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.Connection = connection;

            //增加參數並設定值，記得用.叫出來
            sqlCommand.Parameters.AddWithValue("@account", TextBox_account.Text.Trim());

            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            //執行該SQL查詢，用reader接資料
            SqlDataReader reader = sqlCommand.ExecuteReader();

            //int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
            //object result = sqlCommand.ExecuteScalar();

            //用read先叫出來，while也可以
            if (reader.Read())
            {
                byte[] hash = Convert.FromBase64String(reader["password"].ToString());
                byte[] salt = Convert.FromBase64String(reader["salt"].ToString());

                // 驗證密碼
                bool success = VerifyHash(password, salt, hash);

                if (success)
                {
                    // 取得需要的使用者資料
                    //string userData = reader["isManger"].ToString() + ";" + reader["account"].ToString() + ";" + reader["name"].ToString() + ";" + reader["email"].ToString();

                    // 設定驗證票
                    //SetAuthenTicket(userData, TextBox_account.Text);

                    // 登入成功，存儲到 Session 中
                    Session["LoginId"] = reader["Id"];
                    Session["IsManger"] = Convert.ToBoolean(reader["IsManger"]);

                    // 重定向到指定頁面
                    Response.Redirect("Register.aspx");
                }
                else
                {
                    // 密碼錯誤
                    Label4.Text = "密碼錯誤，請重新輸入。";
                    Label4.Visible = true;
                }
            }
            else
            {
                // 帳號錯誤
                Label4.Text = "帳號錯誤，請重新輸入。";
                Label4.Visible = true;
            }
            connection.Close();

            ////將是否為管理者設為boolean
            //bool IsManger = Convert.ToBoolean(reader["IsManger"]);

            //        // 登入成功，取得使用者 ID，並存儲到 Session 中
            //        Session["LoginId"] = reader["Id"];

            //        Session["IsManger"] = IsManger;

            //        Response.Redirect("Register.aspx");
            //    }
            //    else
            //    {
            //        // 登入失敗
            //        Response.Write("<script>alert('帳號或密碼錯誤，請重新輸入');</script>");
            //    }
            //    connection.Close();

            //// 5.資料庫用 Adapter 執行指令
            //SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            //// 6.建立一個空的 Table
            //DataTable dataTable = new DataTable();
            //// 7.將資料放入 Table
            //dataAdapter.Fill(dataTable);
            //// 登入流程管理 (Cookie)
            //if (dataTable.Rows.Count > 0)
            //{
            //    // SQL 有找到資料時執行

            //    //將字串轉回 byte
            //    byte[] hash = Convert.FromBase64String(dataTable.Rows[0]["password"].ToString());
            //    byte[] salt = Convert.FromBase64String(dataTable.Rows[0]["salt"].ToString());
            //    //驗證密碼
            //    bool success = VerifyHash(password, salt, hash);

            //    if (success)
            //    {
            //        //宣告驗證票要夾帶的資料 (用;區隔)
            //        string userData = dataTable.Rows[0]["isManger"].ToString() + ";" + dataTable.Rows[0]["account"].ToString() + ";" + dataTable.Rows[0]["name"].ToString() + ";" + dataTable.Rows[0]["email"].ToString();
            //        //設定驗證票(夾帶資料，cookie 命名) // 需額外引入using System.Web.Configuration;
            //        SetAuthenTicket(userData, TextBox_account.Text);
            //        //導頁至權限分流頁
            //        Response.Redirect("CheckAccount.ashx");
            //    }
            //    else
            //    {
            //        //資料庫裡找不到相同資料時，表示密碼有誤!
            //        Label4.Text = "password error, login failed!";
            //        Label4.Visible = true;
            //        connection.Close();
            //        return;
            //    }
            //}
            //else
            //{
            //    //資料庫裡找不到相同資料時，表示帳號有誤!
            //    Label4.Text = "Account error, login failed!";
            //    Label4.Visible = true;
            //    //終止程式
            //    //Response.End(); //會清空頁面
            //    return;
            //}
            //connection.Close();
        }

        // Argon2 驗證加密密碼
        // Hash 處理加鹽的密碼功能
        private byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            //底下這些數字會影響運算時間，而且驗證時要用一樣的值
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // 4 核心就設成 8
            argon2.Iterations = 4; //迭代運算次數
            argon2.MemorySize = 1024 * 1024; // 1 GB

            return argon2.GetBytes(16);
        }
        //驗證
        private bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            //根據輸入的密碼和鹽產生新的雜湊值 ( )    
            var newHash = HashPassword(password, salt);
            //提供的密碼與儲存的雜湊值匹配時，會回傳true
            return hash.SequenceEqual(newHash); // LINEQ
        }

        //設定驗證票
        //userData包含附加使用者特定資料的字串，這些資料將儲存在身份驗證票證中
        //userId表示使用者識別碼的字串（例如，使用者名稱、唯一使用者 ID）
        private void SetAuthenTicket(string userData, string userId)
        {
            //宣告一個驗證票 //需額外引入 using System.Web.Security; Ticket版本1, Ticket過期時間, false表示票證是否應在瀏覽器會話中保持不變(非持久)
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userId, DateTime.Now, DateTime.Now.AddHours(3), false, userData);
            //加密驗證票
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            //建立 Cookie，將建立的 cookie 新增到 HTTP 回應中，該回應將傳送回客戶端的瀏覽器
            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            //將 Cookie 寫入回應
            Response.Cookies.Add(authenticationCookie);
        }
    }
}