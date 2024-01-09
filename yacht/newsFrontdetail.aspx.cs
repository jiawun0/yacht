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
    public partial class newsFrontdetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadNews();
            }
        }

        //前台呈現新聞格式
        private void loadNews()
        {
            //取得網址傳值的 id 內容
            string guid = Request.QueryString["guid"];

            //從資料庫取資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectnews2"].ConnectionString);
            string sql = "select newsContentHtml from news where guid = @guid ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@guid", guid);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                //渲染畫面
                Literal_newsContent.Text = HttpUtility.HtmlDecode(reader["newsContentHtml"].ToString());
            }
            connection.Close();
        }
    }
}
