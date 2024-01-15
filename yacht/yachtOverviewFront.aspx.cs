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
    public partial class yachtOverviewFront : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //會先跑 Content 頁的 Page_Load 才跑 Master 頁的 Page_Load
            if (!IsPostBack)
            {
                loadOverviewContent();
            }
        }

        private void loadOverviewContent()
        {
            //取得網址傳值的 id 內容
            string guidStr = Session["guid"].ToString();

            //從資料庫取資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtall"].ConnectionString);
            //string sql = "select overviewContentHtml from Yachts where guid = @guid ";
            string sql = "select Y.Id, Y.yachtModel, Y.guid, Y.overviewContentHtml, Y.overviewDimensionsImgPath, Y.overviewDownloadsFilePath, YD.Specification, YD.size, YD.YachtsId from Yachts Y" +
               " left join YachtsDimension YD on Y.Id = YD.YachtsId " +
               " where guid = @guid ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@guid", guidStr);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            StringBuilder dimensions = new StringBuilder();
            StringBuilder dimensionsImgPathHTML = new StringBuilder();

            while (reader.Read())
            {
                string yachtModelStr = reader["yachtModel"].ToString();
                string dimensionsImgPathStr = reader["overviewDimensionsImgPath"].ToString();
                //相對路徑
                string relativePath = GetRelativeImagePath(dimensionsImgPathStr);

                string downloadsFilePathStr = reader["overviewDownloadsFilePath"].ToString();
                //相對路徑
                string relativePathFile = GetRelativeImagePath(downloadsFilePathStr);

                string Specification = reader["Specification"].ToString();
                string size = reader["size"].ToString();

                dimensions.Append("<table class='table02'>");
                dimensions.Append("<tbody>");

                //注意長度
                dimensions.Append("<tr><td style='width:300px;'>" + Specification + "</td>");
                dimensions.Append("<td style='width:300px;'>" + size + "</td></tr>");

                dimensions.Append("</tbody>");
                dimensions.Append("</table>");

                //渲染CKeditor
                Literal_overviewContentHtml.Text = HttpUtility.HtmlDecode(reader["overviewContentHtml"].ToString());
                //遊艇名稱
                Literal_dimensionTitle.Text = yachtModelStr + " DIMENSIONS";
                //尺寸表格圖片(會重複??)
                dimensionsImgPathHTML.Append("<img src = '" + relativePath + "' alt = '' />");
                Literal_overviewDimensionsImgPath.Text = dimensionsImgPathHTML.ToString();
                //渲染尺寸表格文字內容
                Literal_Dimension.Text = dimensions.ToString();
                //下載檔案
                Literal_overviewDownloadsFilePath.Text = $"<a href='/{relativePathFile}' target='blank' >{yachtModelStr}</a>";

            }
            
            connection.Close();
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
    }
}
