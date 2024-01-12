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
    public partial class yachtOverviewBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FileBrowser fileBrowser = new FileBrowser();
                fileBrowser.BasePath = "/ckfinder";
                fileBrowser.SetupCKEditor(CKEditorControl_overviewContent);

                //loadDimension();

                if (Session["LoginId"] != null)
                {
                    string loginId = Session["LoginId"].ToString();
                    bool isManger = (Session["isManger"] != null) ? (bool)Session["isManger"] : false;

                    if (loginId != null && isManger)
                    {
                        //顯示登入者
                        string name = Showusername(loginId);
                        Literal_name.Text = "歡迎, " + name + "!";
                        loadDimension();
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

        protected void DropDownList_yachtModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //選擇後想出現的東西
            //選取DDL後連到DetailsView_news
            loadDimension();

            //選取DDL後連到Ckeditor
            loadCkeditorContent();
        }

        //新增圖片
        protected void Button_upDimensionsImgPath_Click(object sender, EventArgs e)
        {

        }

        //新增附件
        protected void Button_download_Click(object sender, EventArgs e)
        {

        }

        //顯示Dimension詳情，綁定gridview
        private void loadDimension()
        {
            string selectedValue = DropDownList_yachtModel.SelectedValue;

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectwithOverview"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = "select * from YachtsDimension where YachtsId = @YachtsId ";
            sqlCommand.Parameters.AddWithValue("@YachtsId", selectedValue);

            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            GridView_Dimension.DataSource = reader;

            GridView_Dimension.DataBind();

            connection.Close();
        }

        //新增Dimension
        protected void Button_addDimension_Click(object sender, EventArgs e)
        {

        }

        protected void GridView_Dimension_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GridView_Dimension_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GridView_Dimension_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GridView_Dimension_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        //讀取Ckeditor
        private void loadCkeditorContent()
        {
            string selectedValue = DropDownList_yachtModel.SelectedValue;

            //取得頁面 HTML 資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectwithOverview"].ConnectionString);
            string sql = "select overviewContentHtml from Yachts where Id = @Id ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", selectedValue);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            
            if (reader.Read())
            {
                // 渲染畫面
                if (reader["overviewContentHtml"] != DBNull.Value)
                {
                    CKEditorControl_overviewContent.Text = HttpUtility.HtmlDecode(reader["overviewContentHtml"].ToString());
                }
                else
                {
                    //沒有內容走這步
                    CKEditorControl_overviewContent.Text = "";
                }
            }
            connection.Close();
        }

        //上傳ContentHTML
        protected void UploadnewsoverviewContentBtn_Click(object sender, EventArgs e)
        {
            string selectedValue = DropDownList_yachtModel.SelectedValue;

            //取得 CKEditorControl 的 HTML 內容
            string HtmlStr = HttpUtility.HtmlEncode(CKEditorControl_overviewContent.Text);
            //更新 About Us 頁面 HTML 資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectwithOverview"].ConnectionString);
            string sql = "update Yachts set overviewContentHtml = @overviewContentHtml where Id = @Id ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@overviewContentHtml", HtmlStr);
            command.Parameters.AddWithValue("@Id", selectedValue);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            //渲染畫面提示
            DateTime nowtime = DateTime.Now;
            UploadnewsoverviewContent.Visible = true;
            UploadnewsoverviewContent.Text = "上傳成功" + nowtime.ToString("G");
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