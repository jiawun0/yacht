using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht
{
    public partial class newsBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDayNewsHeadline();
            }
        }

        protected void Button_addHeadline_Click(object sender, EventArgs e)
        {
            //產生 GUID 隨機碼 + 時間2位秒數 (加強避免重複)
            DateTime nowTime = DateTime.Now;
            string nowSec = nowTime.ToString("ff");
            string guid = Guid.NewGuid().ToString().Trim() + nowSec;

            //取得日曆選取日期
            // 檢查TextBox中是否有選擇日期
            string selNewsDate = "";
            if (DateTime.TryParse(TextBox_Date.Text, out DateTime selectedDate))
            {
                selNewsDate = selectedDate.ToString("yyyy-M-dd");
            }

            //取得是否勾選
            string isTop = CheckBox_IsTop.Checked.ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectnews"].ConnectionString);
            string sql = "INSERT INTO news (dateTitle, headline, guid, isTop) VALUES (@dateTitle, @headline, @guid, @isTop)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@dateTitle", selNewsDate);
            command.Parameters.AddWithValue("@headline", TextBox_Headline.Text);
            command.Parameters.AddWithValue("@guid", guid);
            command.Parameters.AddWithValue("@isTop", isTop);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            //渲染畫面
            loadDayNewsHeadline();

            //清空輸入欄位
            TextBox_Headline.Text = "";
        }

        private void loadDayNewsHeadline()
        {
            // 取得日曆選取日期，如果未選擇日期，則預設為今天日期
            DateTime selectedDate = DateTime.Now;
            string selNewsDate = selectedDate.ToString("yyyy-M-dd");

            // 檢查TextBox中是否有選擇日期
            if (DateTime.TryParse(TextBox_Date.Text, out DateTime _))
            {
                selNewsDate = selectedDate.ToString("yyyy-M-dd");
            }

            //依選取日期取得資料庫新聞內容
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectnews"].ConnectionString);
            string sql = "SELECT * FROM news WHERE dateTitle = @dateTitle ORDER BY Id asc ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@dateTitle", selNewsDate);
            connection.Open();

            //SqlDataReader reader = command.ExecuteReader();

            //ListView_news.DataSource = reader;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable newsDataTable = new DataTable();
            adapter.Fill(newsDataTable);
            ListView_news.DataSource = newsDataTable;
            ListView_news.DataBind();

            connection.Close();
        }
    }
}