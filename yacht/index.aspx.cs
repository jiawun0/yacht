using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
                loadBannerNum();
            }
        }

        //輪播大圖
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

            //string sql = "WITH gggg AS ( SELECT Id, PhotoPath, YachtsId, ROW_NUMBER() OVER (PARTITION BY YachtsId ORDER BY Id ASC) AS RowNum FROM  YachtsPhoto) SELECT Id, PhotoPath, YachtsId FROM gggg WHERE　RowNum= 1 ";
            string sql = "WITH gggg AS ( SELECT YP.Id, YP.PhotoPath, YP.YachtsId, Y.yachtModel, ROW_NUMBER() OVER (PARTITION BY YP.YachtsId ORDER BY YP.Id ASC) AS RowNum FROM YachtsPhoto YP JOIN Yachts Y ON YP.YachtsId = Y.Id) SELECT Id, PhotoPath, YachtsId, yachtModel FROM gggg WHERE RowNum = 1 ";
            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            StringBuilder bannerHtml = new StringBuilder();

            while (reader.Read())
            {
                string PhotoPath = reader["PhotoPath"].ToString();
                string yachtModel = reader["yachtModel"].ToString();

                bannerHtml.Append("<li class='info'>");
                bannerHtml.Append("<a href='#'>");
                bannerHtml.Append("<img src='" + PhotoPath + "' alt='' /></a><div class='wordtitle'>");
                bannerHtml.Append("<span>" + yachtModel + "</span><br/>");
                bannerHtml.Append("<p>SPECIFICATION SHEET</p>");
                bannerHtml.Append("</div>");
                bannerHtml.Append("</li>");
            }

            reader.Close();
            connection.Close();

            // 將 HTML 字串設置到 Literal 控制項中
            //不顯示但影響輪播圖片數量計算
            Literal_banner.Text = bannerHtml.ToString();

        }

        //輪播小圖
        private void loadBannerNum()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtsPhoto"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            //string sql = "WITH gggg AS ( SELECT Id, PhotoPath, YachtsId, ROW_NUMBER() OVER (PARTITION BY YachtsId ORDER BY Id ASC) AS RowNum FROM  YachtsPhoto) SELECT Id, PhotoPath, YachtsId FROM gggg WHERE　RowNum= 1 ";
            string sql = "WITH gggg AS ( SELECT YP.Id, YP.PhotoPath, YP.YachtsId, Y.yachtModel, ROW_NUMBER() OVER (PARTITION BY YP.YachtsId ORDER BY YP.Id ASC) AS RowNum FROM YachtsPhoto YP JOIN Yachts Y ON YP.YachtsId = Y.Id) SELECT Id, PhotoPath, YachtsId, yachtModel FROM gggg WHERE RowNum = 1 ";
            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            StringBuilder bannerNumHtml = new StringBuilder();

            while (reader.Read())
            {
                string PhotoPath = reader["PhotoPath"].ToString();

                bannerNumHtml.Append("<li>");
                bannerNumHtml.Append("<div>");
                bannerNumHtml.Append("<p class='bannerimg_p'>");
                bannerNumHtml.Append("<img src='" + PhotoPath + "' alt='' />");
                bannerNumHtml.Append("</p>");
                bannerNumHtml.Append("</div>");
                bannerNumHtml.Append("</li>");
            }

            reader.Close();
            connection.Close();

            // 將 HTML 字串設置到 Literal 控制項中
            //不顯示但影響輪播圖片數量計算
            Literal_bannerNum.Text = bannerNumHtml.ToString();

        }
    }
}