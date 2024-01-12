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
                loadNews();
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

                // Assuming your web application is in the root directory
                string relativePath = GetRelativeImagePath(PhotoPath);

                // Map the virtual path to a physical path
                string physicalPath = Server.MapPath("~/" + relativePath);

                bannerHtml.Append("<li class='info'>");
                bannerHtml.Append("<a href='#'>");
                bannerHtml.Append("<img src='" + relativePath + "' alt='' /></a><div class='wordtitle'>");
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

                // Assuming your web application is in the root directory
                string relativePath = GetRelativeImagePath(PhotoPath);

                // Map the virtual path to a physical path
                string physicalPath = Server.MapPath("~/" + relativePath);

                bannerNumHtml.Append("<li>");
                bannerNumHtml.Append("<div>");
                bannerNumHtml.Append("<p class='bannerimg_p'>");
                bannerNumHtml.Append("<img width='100px' src='" + relativePath + "' alt='' />");
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

        //相對路徑
        protected string GetRelativeImagePath(string Path) //相對路徑
        {
            if (!string.IsNullOrEmpty(Path))
            {
                string relativePath = Path.Replace(Server.MapPath("~"), "").Replace(Server.MapPath("\\"), "/");
                return relativePath;
            }
            return string.Empty;
        }

        //抓取新聞
        private void loadNews()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtsPhoto"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法，取得結果
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = "SELECT TOP 3 dateTitle, headline, guid, isTop, thumbnailPath FROM news ORDER BY Id DESC ";
            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();
            StringBuilder NewsHtml = new StringBuilder();

            while (reader.Read())
            {
                string dateTitle = reader["dateTitle"].ToString();
                string headline = reader["headline"].ToString();
                string guid = reader["guid"].ToString();
                string thumbnailPath = reader["thumbnailPath"].ToString();

                // Assuming your web application is in the root directory
                string relativePath = GetRelativeImagePath(thumbnailPath);

                NewsHtml.Append("<li>");
                NewsHtml.Append("<div class='news01'>");

                // Check if the news has TOP label
                bool isTop = Convert.ToBoolean(reader["isTop"]);
                if (isTop)
                {
                    NewsHtml.Append("<div class='newstop'>");
                    NewsHtml.Append("<img src='images/new_top01.png' alt='' />");
                    NewsHtml.Append("</div>");
                }

                NewsHtml.Append("<div class='news02p1'>");
                NewsHtml.Append("<p class='news02p1img'>");
                NewsHtml.Append("<img src='" + relativePath + "' alt='' />");
                NewsHtml.Append("</p>");
                NewsHtml.Append("</div>");

                NewsHtml.Append("<p class='news02p2'>");
                NewsHtml.Append("<span>" + dateTitle + "</span>");
                NewsHtml.Append("<a href='newsFrontdetail.aspx?guid=" + guid + "'>" + headline + "</a>");
                NewsHtml.Append("</p>");

                NewsHtml.Append("</div>");
                NewsHtml.Append("</li>");
            }


            reader.Close();
            connection.Close();

            Literal_News.Text = NewsHtml.ToString();
        }
    }
}