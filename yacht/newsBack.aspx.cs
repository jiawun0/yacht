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
                //// 頁面首次加載時執行
                //string selectedDate = GetSelectedDate();
                //// 將選擇的日期設置回TextBox_Date
                //TextBox_Date.Text = selectedDate;
                //loadDayNewsHeadline(selectedDate);

                //先綁定取得選取預設值:日期
                //DropDownList_Headline.DataBind();

                loadDayNewsHeadline();
            }
        }
        //判斷是否有日期~結果無法成功，先隱藏不使用
        private string GetSelectedDate()
        {
            // 如果TextBox中有選擇日期，則返回選擇的日期
            if (DateTime.TryParse(TextBox_Date.Text, out DateTime selectedDate))
            {
                return selectedDate.ToString();
            }
            // 如果TextBox中沒有選擇日期，返回今天的日期
            return DateTime.Now.ToString();
        }

        private void loadDayNewsHeadline()
        {
            // 取得日曆選取日期，如果未選擇日期，則預設為今天日期
            //DateTime selectedDate = DateTime.Now;
            string selNewsDate  = TextBox_Date.Text.ToString();

            //// 檢查TextBox中是否有選擇日期
            //if (DateTime.TryParse(TextBox_Date.Text, out DateTime _))
            //{
            //    selNewsDate = selectedDate.ToString("yyyy-M-dd");
            //}

            // 從 Session 變量中檢索選擇的日期
            //Session["SelectedDate"] = TextBox_Date.Text;
            //selectedDate = Session["SelectedDate"] as string;


            //依下拉選單選取國家的值 (id) 取得地區分類
            //string selHeadline_id = "1";
            string selHeadline_id = DropDownList_Headline.SelectedValue;

            //依選取日期取得資料庫新聞內容
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectnews2"].ConnectionString);
            string sql = "SELECT * FROM news WHERE dateTitle = @dateTitle ORDER BY Id asc ";
            //string sql = "SELECT * FROM news WHERE Id = @Id ORDER BY Id asc ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@dateTitle", selNewsDate);
            //command.Parameters.AddWithValue("@Id", selHeadline_id);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                DetailsView_news.DataSource = reader;
                DetailsView_news.DataBind();
            }
            else
            {
                // 如果沒有匹配的記錄，清空 ListView
                DetailsView_news.DataSource = null;
                DetailsView_news.DataBind();
            }

            //ListView_news.DataSource = reader;
            //ListView_news.DataBind();

            //SqlDataAdapter adapter = new SqlDataAdapter(command);
            //DataTable newsDataTable = new DataTable();
            //adapter.Fill(newsDataTable);
            //ListView_news.DataSource = newsDataTable;

            // 重新繫結 DropDownList
            //DropDownList_Headline.DataBind();

            connection.Close();
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

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectnews2"].ConnectionString);
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

        //當DDL選擇日期改變時刷新畫面資料(本次最重要功能)
        protected void DropDownList_Headline_SelectedIndexChanged(object sender, EventArgs e)
        {
            DetailsView_news.DataBind();
            loadDayNewsHeadline();
        }

        protected void TextBox_Date_TextChanged(object sender, EventArgs e)
        {
            loadDayNewsHeadline();
        }
    }
}