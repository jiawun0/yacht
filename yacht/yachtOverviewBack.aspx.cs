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
    public partial class yachtOverviewBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FileBrowser fileBrowser = new FileBrowser();
                fileBrowser.BasePath = "/ckfinder";
                fileBrowser.SetupCKEditor(CKEditorControl_overviewContent);

                loadDimension();
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
                //        loadDimension();
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
            //選取DDL後連到DetailsView_news
            loadDimension();

            //選取DDL後連到Ckeditor
            loadCkeditorContent();
            loaddimensionsImg();
        }

        //讀取DimensionImg
        private void loaddimensionsImg()
        {
            string selectedValue = DropDownList_yachtModel.SelectedValue;

            //從資料庫取資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectwithOverview"].ConnectionString);
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
        protected void Button_upDimensionsImgPath_Click(object sender, EventArgs e)
        {
            string selectedValue = DropDownList_yachtModel.SelectedValue;

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectwithOverview"].ConnectionString);
            //有檔案才能上傳
            if (FileUpload_DimensionsImgPath.HasFile) //FileUpload1.PostedFile != null
            {
                try
                {
                    string FileName = Path.GetFileName(FileUpload_DimensionsImgPath.FileName);
                    string saveDirectory = Server.MapPath("~/Album/");
                    string savePath = Path.Combine(saveDirectory, FileName);
                    FileUpload_DimensionsImgPath.SaveAs(savePath);

                    connection.Open();

                    //新增
                    //string sql = "Insert into Yachts (overviewDimensionsImgPath, Id) values(@overviewDimensionsImgPath, @Id)";
                    string sql = "UPDATE Yachts SET overviewDimensionsImgPath = @overviewDimensionsImgPath WHERE Id = @Id ";
                    SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    sqlCommand.Parameters.AddWithValue("@overviewDimensionsImgPath", savePath);
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

        //新增附件
        protected void Button_download_Click(object sender, EventArgs e)
        {
            string selectedValue = DropDownList_yachtModel.SelectedValue;

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectwithOverview"].ConnectionString);
            //有檔案才能上傳
            if (FileUpload_downloadPDF.HasFile) //FileUpload1.PostedFile != null
            {
                try
                {
                    string FileName = Path.GetFileName(FileUpload_downloadPDF.FileName);
                    string saveDirectory = Server.MapPath("~/Album/");
                    string savePath = Path.Combine(saveDirectory, FileName);
                    FileUpload_downloadPDF.SaveAs(savePath);

                    connection.Open();

                    //新增
                    //string sql = "Insert into Yachts (overviewDimensionsImgPath, Id) values(@overviewDimensionsImgPath, @Id)";
                    string sql = "UPDATE Yachts SET overviewDownloadsFilePath = @overviewDownloadsFilePath WHERE Id = @Id ";
                    SqlCommand sqlCommand = new SqlCommand(sql, connection);
                    sqlCommand.Parameters.AddWithValue("@overviewDownloadsFilePath", savePath);
                    sqlCommand.Parameters.AddWithValue("@Id", selectedValue);

                    //將準備的SQL指令給操作物件
                    sqlCommand.CommandText = sql;

                    sqlCommand.ExecuteNonQuery();

                    Response.Write("<script>alert('檔案新增成功');</script>");
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('檔案新增失敗');</script>");
                }
            }
            else
            {
                //沒有檔案
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('請選擇檔案');", true);
                return;
            }
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

        //新增Dimension欄位
        protected void Button_addDimension_Click(object sender, EventArgs e)
        {
            string selectedValue = DropDownList_yachtModel.SelectedValue;

            //檢查欄位不可為空
            string SpecificationValue = TextBox_Specification.Text;
            string sizeValue = TextBox_size.Text;

            if (string.IsNullOrEmpty(SpecificationValue) || string.IsNullOrEmpty(sizeValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('規格、尺寸不可為空，請檢查確認');", true);
                return;
            }

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectDimension"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            //查詢跟參數的部分很難寫成方法，需自行研究
            string sql = $"insert into YachtsDimension (Specification, size, YachtsId) values (@Specification, @size, @YachtsId)";

            sqlCommand.Parameters.AddWithValue("@Specification", TextBox_Specification.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@size", TextBox_size.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@YachtsId", selectedValue);

            sqlCommand.CommandText = sql;

            sqlCommand.ExecuteNonQuery();

            connection.Close();

            //清空輸入欄位
            TextBox_Specification.Text = "";
            TextBox_size.Text = "";

            Response.Write("<script>alert('新增成功');</script>");
            loadDimension();
        }

        protected void GridView_Dimension_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView_Dimension.EditIndex = e.NewEditIndex;
            loadDimension();
        }

        protected void GridView_Dimension_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView_Dimension.Rows[e.RowIndex];

            int boardId = Convert.ToInt32(GridView_Dimension.DataKeys[e.RowIndex].Value);

            TextBox textBox_SpecificationT = row.FindControl("TextBox_SpecificationT") as TextBox;
            string changeText_SpecificationT = textBox_SpecificationT.Text;

            TextBox textBox_sizeT = row.FindControl("TextBox_sizeT") as TextBox;
            string changeText_sizeT = textBox_sizeT.Text;

            //檢查欄位不可為空
            string SpecificationTValue = changeText_SpecificationT;
            string sizeTValue = changeText_sizeT;

            if (string.IsNullOrEmpty(SpecificationTValue) || string.IsNullOrEmpty(sizeTValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('規格、尺寸不可為空，請檢查確認');", true);
                return;
            }

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectDimension"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = $"update YachtsDimension set Specification = @Specification, size = @size where Id = @BoardId ";

            sqlCommand.Parameters.AddWithValue("@Specification", changeText_SpecificationT);
            sqlCommand.Parameters.AddWithValue("@size", changeText_sizeT);
            sqlCommand.CommandText = sql;

            sqlCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('更新成功');</script>");
            GridView_Dimension.EditIndex = -1;
            loadDimension();
        }

        protected void GridView_Dimension_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int boardId = Convert.ToInt32(GridView_Dimension.DataKeys[e.RowIndex].Value);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectDimension"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string deleteSql = $"delete from YachtsDimension where Id = @boardId ";
            SqlCommand deleteCommand = new SqlCommand(deleteSql, connection);
            deleteCommand.Parameters.AddWithValue("@boardId", boardId);
            deleteCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('刪除成功');</script>");

            loadDimension();
        }

        protected void GridView_Dimension_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_Dimension.EditIndex = -1;
            loadDimension();
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