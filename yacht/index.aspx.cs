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
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadBanner();
            }
        }

        private void loadBanner()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtsPhoto"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = "WITH gggg AS ( SELECT Id, PhotoPath, YachtsId, ROW_NUMBER() OVER (PARTITION BY YachtsId ORDER BY Id ASC) AS RowNum FROM  YachtsPhoto) SELECT Id, PhotoPath, YachtsId FROM gggg WHERE　RowNum= 1 ";

            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            //使用這個reader物件的資料來取得內容
            //GridView_Yachts.DataSource = reader;

            //進行資料連接
            //GridView_Yachts.DataBind();

            reader.Close();
            connection.Close();
        }
    }
}