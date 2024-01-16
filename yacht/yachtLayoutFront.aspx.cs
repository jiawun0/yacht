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
    public partial class yachtLayoutFront : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //會先跑 Content 頁的 Page_Load 才跑 Master 頁的 Page_Load
            if (!IsPostBack)
            {
                loadLayoutDeck();
            }
        }

        private void loadLayoutDeck()
        {
            //取得 Session 共用 Guid，Session 物件需轉回字串
            string guidStr = Session["guid"].ToString();

            //從資料庫取資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtall"].ConnectionString);
            //string sql = "select overviewContentHtml from Yachts where guid = @guid ";
            string sql = "select Y.Id, Y.yachtModel, Y.guid, Y.overviewContentHtml, Y.overviewDimensionsImgPath, Y.overviewDownloadsFilePath, Y.layoutDeckPlanImgPath, Y.specificationContentHtml, YD.Specification, YD.size, YD.YachtsId from Yachts Y" +
               " left join YachtsDimension YD on Y.Id = YD.YachtsId " +
               " where guid = @guid ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@guid", guidStr);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            StringBuilder layoutDeckPlanImgPathHTML = new StringBuilder();

            if (reader.Read())
            {
                string layoutDeckPlanImgPathStr = reader["layoutDeckPlanImgPath"].ToString();
                //相對路徑
                string relativePath = GetRelativeImagePath(layoutDeckPlanImgPathStr);

                layoutDeckPlanImgPathHTML.Append("<img src = '" + relativePath + "' alt = '' width='278' height='345' />");

                //圖片
                Literal_layoutDeckPlanImgPath.Text = layoutDeckPlanImgPathHTML.ToString();
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