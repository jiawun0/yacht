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
    public partial class yachtBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadyachtModel();
                loadPhotoList();

                //if (Session["LoginId"] != null)
                //{
                //    string loginId = Session["LoginId"].ToString();
                //    bool isManger = (Session["isManger"] != null) ? (bool)Session["isManger"] : false;

                //    if (loginId != null && isManger)
                //    {
                //        //顯示登入者
                //        string name = Showusername(loginId);
                //        Literal_name.Text = "歡迎, " + name + "!";
                //        loadyachtModel();
                //        loadPhotoList();
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

        //新增yacht
        protected void Button_addyachtModel_Click(object sender, EventArgs e)
        {
            //檢查欄位不可為空
            string textboxValue = TextBox_yachtModel.Text;

            if (string.IsNullOrEmpty(textboxValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('型號不可為空');", true);
                return;
            }

            //產生 GUID 隨機碼 + 時間2位秒數 (加強避免重複)
            DateTime nowTime = DateTime.Now;
            string nowSec = nowTime.ToString("ff");
            string guid = Guid.NewGuid().ToString().Trim() + nowSec;

            //取得是否勾選
            string isNewDesign = CheckBox_isNewDesign.Checked.ToString();
            string isNewBuilding = CheckBox_isNewBuilding.Checked.ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtall"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            //查詢跟參數的部分很難寫成方法，需自行研究
            string sql = $"insert into Yachts (yachtModel, isNewDesign, isNewBuilding, guid) values (@yachtModel, @isNewDesign, @isNewBuilding, @guid) ";

            sqlCommand.Parameters.AddWithValue("@yachtModel", TextBox_yachtModel.Text.Trim());
            sqlCommand.Parameters.AddWithValue("@isNewDesign", isNewDesign);
            sqlCommand.Parameters.AddWithValue("@isNewBuilding", isNewBuilding);
            sqlCommand.Parameters.AddWithValue("@guid", guid);

            sqlCommand.CommandText = sql;

            sqlCommand.ExecuteNonQuery();

            connection.Close();

            //清空輸入欄位
            TextBox_yachtModel.Text = "";
            CheckBox_isNewDesign.Checked = false;
            CheckBox_isNewBuilding.Checked = false;

            //新增進DDL
            //DropDownList_yachtModel.SelectedValue = TextBox_yachtModel.Text.Trim();

        }

        //顯示yachtModel，綁定gridview
        private void loadyachtModel()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtall"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = "select Id, yachtModel, isNewDesign, isNewBuilding, CreatDate from Yachts ";

            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            //使用這個reader物件的資料來取得內容
            GridView_Yachts.DataSource = reader;

            //進行資料連接
            GridView_Yachts.DataBind();

            //用SQL直接連了
            // Use this instead to bind the data to the DropDownList
            //DropDownList_yachtModel.DataSource = reader;
            //DropDownList_yachtModel.DataTextField = "yachtModel";
            //DropDownList_yachtModel.DataValueField = "Id";

            // 重新繫結 DropDownList，DDL上面也有連
            DropDownList_yachtModel.DataBind();

            reader.Close();
            connection.Close();
        }

        protected void GridView_Yachts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView_Yachts.EditIndex = e.NewEditIndex;
            loadyachtModel();
        }

        protected void GridView_Yachts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView_Yachts.Rows[e.RowIndex];

            int boardId = Convert.ToInt32(GridView_Yachts.DataKeys[e.RowIndex].Value);

            TextBox textBox_yachtModelT = row.FindControl("TextBox_yachtModelT") as TextBox;
            string changeText_yachtModelT = textBox_yachtModelT.Text;

            CheckBox checkBox_isNewDesignT = row.FindControl("CheckBox_isNewDesignT") as CheckBox;
            bool isNewDesignT = checkBox_isNewDesignT.Checked;

            CheckBox checkBox_isNewBuildingT = row.FindControl("CheckBox_isNewBuildingT") as CheckBox;
            bool isNewBuildingT = checkBox_isNewBuildingT.Checked;

            //檢查欄位不可為空
            string textboxValue = changeText_yachtModelT;

            if (string.IsNullOrEmpty(textboxValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('型號不可為空');", true);
                return;
            }

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtall"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = $"update Yachts set yachtModel = @yachtModel, isNewDesign = @isNewDesign, isNewBuilding = @isNewBuilding where Id = @BoardId ";

            sqlCommand.Parameters.AddWithValue("@yachtModel", changeText_yachtModelT);
            sqlCommand.Parameters.AddWithValue("@isNewDesign", isNewDesignT);
            sqlCommand.Parameters.AddWithValue("@isNewBuilding", isNewBuildingT);
            sqlCommand.Parameters.AddWithValue("@BoardId", boardId);
            sqlCommand.CommandText = sql;

            sqlCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('更新成功');</script>");
            GridView_Yachts.EditIndex = -1;
            loadyachtModel();
        }

        protected void GridView_Yachts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int boardId = Convert.ToInt32(GridView_Yachts.DataKeys[e.RowIndex].Value);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtall"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string deleteSql = $"delete from Yachts where Id = @boardId ";
            SqlCommand deleteCommand = new SqlCommand(deleteSql, connection);
            deleteCommand.Parameters.AddWithValue("@boardId", boardId);
            deleteCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('刪除成功');</script>");

            // 重新繫結 DropDownList1
            DropDownList_yachtModel.DataBind();

            loadyachtModel();
        }

        protected void GridView_Yachts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_Yachts.EditIndex = -1;
            loadyachtModel();
        }

        //DDL連動yachtModel，同時連動YachtsPhoto
        protected void DropDownList_yachtModel_SelectedIndexChanged(object sender, EventArgs e)
        { 
            //選擇後想出現的東西
            loadPhotoList();
        }

        //新增yachtPhoto
        protected void Button_AddYachtsPhoto_Click(object sender, EventArgs e)
        {
            //取得下拉選單選取值
            string sel_YachtId = DropDownList_yachtModel.SelectedValue;

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtsPhoto"].ConnectionString);
            //有檔案才能上傳
            if (FileUpload_YachtsPhoto.HasFile) //FileUpload1.PostedFile != null
            {
                //string FileName = FileUpload1.FileName;
                //savePath = savePath + FileName;
                //string saveDirectory = Server.MapPath("~ADO2/ADO/photo/");
                //FileUpload1.SaveAs(savePath);
                string FileName = Path.GetFileName(FileUpload_YachtsPhoto.FileName);
                //string saveDirectory = @"C:\Users\88691\source\repos\ADO2\ADO\photo\";
                string saveDirectory = Server.MapPath("~/Album/");// 相簿名稱路徑
                string savePath = Path.Combine(saveDirectory, FileName);
                FileUpload_YachtsPhoto.SaveAs(savePath);

                connection.Open();
                string sql = "Insert into YachtsPhoto (PhotoPath, YachtsId) values (@PhotoPath, @YachtsId) ";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.Parameters.AddWithValue("@PhotoPath", savePath);
                sqlCommand.Parameters.AddWithValue("@YachtsId", sel_YachtId);

                // 將相對路徑存入資料庫
                //string relativePath = "~/Album/" + saveDirectory;
                //sqlCommand.Parameters.AddWithValue("@PhotoPath", relativePath);

                //將準備的SQL指令給操作物件
                sqlCommand.CommandText = sql;

                sqlCommand.ExecuteNonQuery();

                Response.Write("<script>alert('相片新增成功');</script>");
            }
            else
            {
                //沒有檔案
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('請選擇檔案');", true);
                return;
                //Response.Write("<script>alert('請選擇檔案');</script>");
            }
            connection.Close();
        }

        //顯示PhotoList，綁定gridview
        private void loadPhotoList()
        {
            //取得下拉選單選取值
            string sel_YachtId = DropDownList_yachtModel.SelectedValue;

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtsPhoto"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;
   
            string sql = "select Id, PhotoPath, CreatTime, YachtsId from YachtsPhoto where YachtsId = @YachtsId ";
            sqlCommand.Parameters.AddWithValue("@YachtsId", sel_YachtId);
  
            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            //使用這個reader物件的資料來取得內容
            GridView_YachtsPhoto.DataSource = reader;

            //進行資料連接
            GridView_YachtsPhoto.DataBind();

            // 重新繫結 DropDownList，DDL上面也有連
            //DropDownList_yachtModel.DataBind();

            reader.Close();
            connection.Close();
        }

        protected void GridView_YachtsPhoto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int boardId = Convert.ToInt32(GridView_Yachts.DataKeys[e.RowIndex].Value);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtsPhoto"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string deleteSql = $"delete from YachtsPhoto where Id = @boardId ";
            SqlCommand deleteCommand = new SqlCommand(deleteSql, connection);
            deleteCommand.Parameters.AddWithValue("@boardId", boardId);
            deleteCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('刪除成功');</script>");

            // 重新繫結 DropDownList1
            //DropDownList_yachtModel.DataBind();

            loadPhotoList();
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