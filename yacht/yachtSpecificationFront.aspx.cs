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
    public partial class yachtSpecificationFront : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //會先跑 Content 頁的 Page_Load 才跑 Master 頁的 Page_Load
            if (!IsPostBack)
            {
                loadSpecification();
            }
        }

        private void loadSpecification()
        {
            //取得網址傳值的 id 內容
            string guidStr = Session["guid"].ToString();

            //從資料庫取資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtall"].ConnectionString);
            //string sql = "select overviewContentHtml from Yachts where guid = @guid ";
            string sql = "select Y.Id, Y.yachtModel, Y.guid, Y.overviewContentHtml, Y.overviewDimensionsImgPath, Y.overviewDownloadsFilePath, Y.specificationContentHtml, YD.Specification, YD.size, YD.YachtsId from Yachts Y" +
               " left join YachtsDimension YD on Y.Id = YD.YachtsId " +
               " where guid = @guid ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@guid", guidStr);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                //渲染CKeditor
                Literal_specificationContentHtml.Text = HttpUtility.HtmlDecode(reader["specificationContentHtml"].ToString());
            }

            connection.Close();
        }
    }
}
