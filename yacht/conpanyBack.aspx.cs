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

                loadCkeditorContent();
                loadCertificatContent();
            }
        }

        private void loadCkeditorContent()
        {
            //取得 About Us 頁面 HTML 資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtConnectionString"].ConnectionString);
            string sql = "SELECT aboutUsHtml FROM Company WHERE Id = 1";
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

        protected void UploadAboutUsBtn_Click(object sender, EventArgs e)
        {
            //取得 CKEditorControl 的 HTML 內容
            string aboutUsHtmlStr = HttpUtility.HtmlEncode(CKEditorControl_aboutUs.Text);
            //更新 About Us 頁面 HTML 資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtConnectionString"].ConnectionString);
            string sql = "UPDATE company SET aboutUsHtml = @aboutUsHtml WHERE Id = 1";
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

        private void loadCertificatContent()
        {
            //取得 Certificat 頁文字說明資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtConnectionString"].ConnectionString);
            string sql = "SELECT certificatHtml FROM Company WHERE Id = 1";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                //渲染畫面
                CKEditorControl_Certificat.Text = reader["certificatHtml"].ToString();
            }
            connection.Close();
        }

        protected void uploadCertificatBtn_Click(object sender, EventArgs e)
        {
            //取得 CKEditorControl 的 HTML 內容
            string certificatHtmlStr = HttpUtility.HtmlEncode(CKEditorControl_aboutUs.Text);
            //更新 About Us 頁面 HTML 資料
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

        
    }
}