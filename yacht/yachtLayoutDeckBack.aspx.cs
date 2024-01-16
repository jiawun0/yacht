using CKFinder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht
{
    public partial class yachtLayoutDeckBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FileBrowser fileBrowser = new FileBrowser();
                fileBrowser.BasePath = "/ckfinder";
                fileBrowser.SetupCKEditor(CKEditorControl_specificationContent);
                loadCkeditorContent();
                loaddimensionsImg();

                //if (Session["LoginId"] != null)
                //{
                //    string loginId = Session["LoginId"].ToString();
                //    bool isManger = (Session["isManger"] != null) ? (bool)Session["isManger"] : false;

                //    if (loginId != null && isManger)
                //    {
                //        //顯示登入者
                //        string name = Showusername(loginId);
                //        Literal_name.Text = "歡迎, " + name + "!";
                //        loadCkeditorContent();
                //        loaddimensionsImg();
                //    }
                //    else
                //    {
                //        //非IsManger，請重新登入
                //        Response.Redirect("Login.aspx");
                //    }
                //}
                //else
                //{
                //    //尚未登入，請登入
                //    Response.Redirect("Login.aspx");
                //}
            }
        }

        protected void DropDownList_yachtModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //選擇後想出現的東西
            //選取DDL後連到Ckeditor
            loadCkeditorContent();
            loaddimensionsImg();
        }

        //讀取dimensionsImg
        private void loaddimensionsImg()
        {
            string selectedValue = DropDownList_yachtModel.SelectedValue;

            //從資料庫取資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectLayoutDeck"].ConnectionString);
            string sql = "select overviewDimensionsImgPath from Yachts where Id = @Id ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", selectedValue);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string dimensionsImgPathStr = reader["overviewDimensionsImgPath"].ToString();
                //相對路徑
                string relativePath = GetRelativeImagePath(dimensionsImgPathStr);

                //尺寸表格圖片
                Literal_img.Text = "<img src = '" + relativePath + "' alt = '' />";
            }

            connection.Close();
        }
        //新增圖片
        protected void Button_upLayoutDeckImgPath_Click(object sender, EventArgs e)
        {
            string selectedValue = DropDownList_yachtModel.SelectedValue;

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectLayoutDeck"].ConnectionString);
            //有檔案才能上傳
            if (FileUpload_LayoutDeck.HasFile) //FileUpload1.PostedFile != null
            {
                try
                {
                    string FileName = Path.GetFileName(FileUpload_LayoutDeck.FileName);
                    string saveDirectory = Server.MapPath("~/Album/");
                    string savePath = Path.Combine(saveDirectory, FileName);
                    FileUpload_LayoutDeck.SaveAs(savePath);

                    connection.Open();

                    //新增
                    //string sql = "Insert into Yachts (overviewDimensionsImgPath, Id) values(@overviewDimensionsImgPath, @Id)";
                    string sql = "UPDATE Yachts SET layoutDeckPlanImgPath = @layoutDeckPlanImgPath WHERE Id = @Id ";
                    SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    sqlCommand.Parameters.AddWithValue("@layoutDeckPlanImgPath", savePath);
                    sqlCommand.Parameters.AddWithValue("@Id", selectedValue);

                    //將準備的SQL指令給操作物件
                    sqlCommand.CommandText = sql;

                    sqlCommand.ExecuteNonQuery();

                    Response.Write("<script>alert('相片新增成功');</script>");
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('相片新增失敗');</script>");
                }
            }
            else
            {
                //沒有檔案
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('請選擇檔案');", true);
                return;
            }
        }

        //讀取Ckeditor
        private void loadCkeditorContent()
        {
            string selectedValue = DropDownList_yachtModel.SelectedValue;

            //取得頁面 HTML 資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectLayoutDeck"].ConnectionString);
            string sql = "select specificationContentHtml from Yachts where Id = @Id ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", selectedValue);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                // 渲染畫面
                if (reader["specificationContentHtml"] != DBNull.Value)
                {
                    CKEditorControl_specificationContent.Text = HttpUtility.HtmlDecode(reader["specificationContentHtml"].ToString());
                }
                else
                {
                    //沒有內容走這步
                    CKEditorControl_specificationContent.Text = "";
                }
            }
            connection.Close();
        }
        //上傳ContentHTML
        protected void UploadspecificationContentBtn_Click(object sender, EventArgs e)
        {
            string selectedValue = DropDownList_yachtModel.SelectedValue;

            //取得 CKEditorControl 的 HTML 內容
            string HtmlStr = HttpUtility.HtmlEncode(CKEditorControl_specificationContent.Text);
            //更新 About Us 頁面 HTML 資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectLayoutDeck"].ConnectionString);
            string sql = "update Yachts set specificationContentHtml = @specificationContentHtml where Id = @Id ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@specificationContentHtml", HtmlStr);
            command.Parameters.AddWithValue("@Id", selectedValue);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            //渲染畫面提示
            DateTime nowtime = DateTime.Now;
            UploadspecificationContent.Visible = true;
            UploadspecificationContent.Text = "上傳成功" + nowtime.ToString("G");
        }

        //顯示登入者
        string Showusername(string LoginId)
        {
            string name = "";

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectLayoutDeck"].ConnectionString);
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

        //使用相對路徑顯示photo
        protected string GetRelativeImagePath(string albumPath) //相對路徑
        {
            if (!string.IsNullOrEmpty(albumPath))
            {
                string relativePath = albumPath.Replace(Server.MapPath("~"), "").Replace(Server.MapPath("\\"), "/");
                return relativePath;
            }
            return string.Empty;
        }
    }
}