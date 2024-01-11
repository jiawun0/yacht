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
    public partial class yachtBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadyachtModel();
            }
        }

        //新增yacht
        protected void Button_addyachtModel_Click(object sender, EventArgs e)
        {
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
            DropDownList_YachtsAlbum.SelectedValue = TextBox_yachtModel.Text.Trim();

            //清空之前的RBL照片
            RadioButtonList_PhotoPath.Items.Clear();
        }

        //yachtModel綁定gridview
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

            connection.Close();
        }

        //新增yachtPhoto
        protected void Button_AddPhotoPath_Click(object sender, EventArgs e)
        {

        }

        //刪除yachtPhoto
        protected void Button_DelPhotoPath_Click(object sender, EventArgs e)
        {

        }

        //DDL連動yachtModel，同時連動YachtsAlbum
        protected void DropDownList_YachtsAlbum_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //RBL連動YachtsAlbum，同時連動YachtsPhoto
        protected void RadioButtonList_PhotoPath_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView_Yachts_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GridView_Yachts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GridView_Yachts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GridView_Yachts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
    }
}