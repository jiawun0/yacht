using CKFinder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht
{
    public partial class conpanyBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FileBrowser fileBrowser = new FileBrowser();
                fileBrowser.BasePath = "/ckfinder";
                fileBrowser.SetupCKEditor(CKEditorControl_aboutUs);
                fileBrowser.SetupCKEditor(CKEditorControl_Certificat);

                //loadCkeditorContent();
                //loadCertificatContent();

                if (Session["LoginId"] != null)
                {
                    string loginId = Session["LoginId"].ToString();
                    bool isManger = (Session["isManger"] != null) ? (bool)Session["isManger"] : false;

                    if (loginId != null && isManger)
                    {
                        //顯示登入者
                        string name = Showusername(loginId);
                        Literal_name.Text = "歡迎, " + name + "!";
                        loadCkeditorContent();
                        loadCertificatContent();
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

        //讀取Ckeditor_aboutus
        private void loadCkeditorContent()
        {
            //取得 About Us 頁面 HTML 資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtConnectionString"].ConnectionString);
            string sql = "select aboutUsHtml from company where Id = 1";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                //渲染畫面
                CKEditorControl_aboutUs.Text = HttpUtility.HtmlDecode(reader["aboutUsHtml"].ToString());
            }
            connection.Close();
        }

        //上傳Ckeditor_aboutus
        protected void UploadAboutUsBtn_Click(object sender, EventArgs e)
        {
            //取得 CKEditorControl 的 HTML 內容
            string aboutUsHtmlStr = HttpUtility.HtmlEncode(CKEditorControl_aboutUs.Text);
            //更新 About Us 頁面 HTML 資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtConnectionString"].ConnectionString);
            string sql = "update company set aboutUsHtml = @aboutUsHtml where Id = 1";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@aboutUsHtml", aboutUsHtmlStr);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            //渲染畫面提示
            DateTime nowtime = DateTime.Now;
            UploadAboutUsLab.Visible = true;
            UploadAboutUsLab.Text = "上傳成功" + nowtime.ToString("G");
        }

        //讀取Ckeditor_certificat
        private void loadCertificatContent()
        {
            //取得 Certificat 頁文字說明資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtConnectionString"].ConnectionString);
            string sql = "SELECT certificatHtml FROM company WHERE Id = 1";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                //渲染畫面
                //CKEditorControl_Certificat.Text = HttpUtility.HtmlDecode(reader["certificatHtml"].ToString());
                // 直接將數據賦值給 CKEditor 控件
                CKEditorControl_Certificat.Text = HttpUtility.HtmlDecode(reader["certificatHtml"].ToString());
            }
            connection.Close();
        }

        //上傳Ckeditor_certificat
        protected void uploadCertificatBtn_Click(object sender, EventArgs e)
        {
            //取得 CKEditorControl 的 HTML 內容
            string certificatHtmlStr = HttpUtility.HtmlEncode(CKEditorControl_Certificat.Text);
            //更新 certificat 頁面 HTML 資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtConnectionString"].ConnectionString);
            string sql = "UPDATE company SET certificatHtml = @certificatHtml WHERE Id = 1";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@certificatHtml", certificatHtmlStr);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            //渲染畫面提示
            DateTime nowtime = DateTime.Now;
            uploadCertificatLab.Visible = true;
            uploadCertificatLab.Text = "上傳成功" + nowtime.ToString("G");
        }

        //顯示登入者
        string Showusername(string LoginId)
        {
            string name = "";

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectcountry"].ConnectionString);
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

    }
}