using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
                shownewsList();
            }
        }
        //判斷是否有日期~結果無法成功，先隱藏不使用
        //private string GetSelectedDate()
        //{
        //    // 如果TextBox中有選擇日期，則返回選擇的日期
        //    if (DateTime.TryParse(TextBox_Date.Text, out DateTime selectedDate))
        //    {
        //        return selectedDate.ToString();
        //    }
        //    // 如果TextBox中沒有選擇日期，返回今天的日期
        //    return DateTime.Now.ToString();
        //}

        //顯示DetailsView
        private void loadDayNewsHeadline()
        {
            // 取得日曆選取日期，如果未選擇日期，則預設為今天日期
            //DateTime selectedDate = DateTime.Now;
            //string selNewsDate = TextBox_Date.Text;

            //string selNewsDate = "";

            //// 檢查TextBox中是否有選擇日期
            //if (DateTime.TryParse(TextBox_Date.Text, out DateTime selectedDate))
            //{
            //    selNewsDate = selectedDate.ToString("yyyy-MM-dd");
            //}

            // 获取 RadioButtonList1 中选定的日期
            string selNewsDate = "";

            if (RadioButtonList1 != null && RadioButtonList1.SelectedItem != null)
            {
                selNewsDate = RadioButtonList1.SelectedItem.Text;
            }

            // 從 Session 變量中檢索選擇的日期
            //Session["SelectedDate"] = TextBox_Date.Text;
            //selectedDate = Session["SelectedDate"] as string;


            //依下拉選單選取國家的值 (id) 取得地區分類
            //string selHeadline_id = "1";
            //string selHeadline_id = DropDownList_Headline.SelectedValue;

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

        //顯示單日新聞篇章RBL(會全部顯示)
        private void shownewsList()
        {
            // 清空RadioButtonList
            RadioButtonList1.Items.Clear();

            //當textbox選擇日期時帶入
            string selNewsDate = "";

            // 檢查TextBox中是否有選擇日期
            if (DateTime.TryParse(TextBox_Date.Text, out DateTime selectedDate))
            {
                selNewsDate = selectedDate.ToString("yyyy-MM-dd");


                //依選取日期取得資料庫新聞內容
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectnews2"].ConnectionString);
                string sql = "SELECT * FROM news WHERE dateTitle = @dateTitle ORDER BY Id asc ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@dateTitle", selNewsDate);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string dateTitle = reader["dateTitle"].ToString();

                    // 添加符合選擇日期的 dateTitle 到 RadioButtonList1
                    ListItem listItem = new ListItem();
                    listItem.Text = dateTitle;
                    listItem.Value = dateTitle;
                    RadioButtonList1.Items.Add(listItem);
                }
                connection.Close();
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
            //string selNewsDate = TextBox_Date.Text.ToString();
            string selNewsDate = "";
            if (DateTime.TryParse(TextBox_Date.Text, out DateTime selectedDate))
            {
                selNewsDate = selectedDate.ToString("yyyy-MM-dd");
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
            RadioButtonList1.Items.Clear(); // 清除舊資料
            shownewsList(); // 重新讀取資料
            loadDayNewsHeadline();

            //清空輸入欄位
            TextBox_Date.Text = "";
            TextBox_Headline.Text = "";
            TextBox_summary.Text = "";
        }

        //當TextBox選擇日期改變時刷新畫面資料(本次最重要功能)
        protected void TextBox_Date_TextChanged(object sender, EventArgs e)
        {
            shownewsList();
        }

        //選擇日期，出現所有詳情
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            shownewsList();
            loadDayNewsHeadline();
        }

        //編輯模式
        protected void DetailsView_news_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            if (e.NewMode == DetailsViewMode.Edit)
            {
                DetailsView_news.ChangeMode(DetailsViewMode.Edit);
                //DetailsView_news.AllowPaging = false; // 禁用分页

                // 可能需要加载编辑数据的代码
                // loadEditData();
            }
            else if (e.NewMode == DetailsViewMode.ReadOnly)
            {
                DetailsView_news.ChangeMode(DetailsViewMode.ReadOnly);
                //DetailsView_news.AllowPaging = true; // 启用分页

                // 可能需要加载只读数据的代码
                // loadReadOnlyData();
            }

            loadDayNewsHeadline();
        }

        protected void DetailsView_news_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            // Get the index of the record being updated
            int boardId = Convert.ToInt32(e.Keys["Id"]);

            TextBox textBox_dateTitleT = DetailsView_news.FindControl("TextBox_dateTitleT") as TextBox;
            string changeText_dateTitleT = textBox_dateTitleT.Text;

            // 檢查TextBox中是否有選擇日期
            if (DateTime.TryParse(changeText_dateTitleT, out DateTime selectedDate))
            {
                changeText_dateTitleT = selectedDate.ToString("yyyy-MM-dd");
            }
            string changeText_dateTitleT123 = changeText_dateTitleT;

            TextBox textBox_headlineT = DetailsView_news.FindControl("TextBox_headlineT") as TextBox;
            string changeText_headlineT = textBox_headlineT.Text;

            CheckBox checkBox_isTopT = DetailsView_news.FindControl("CheckBox_isTopT") as CheckBox;
            bool isTopT = checkBox_isTopT.Checked;

            TextBox textBox_summaryT = DetailsView_news.FindControl("TextBox_summaryT") as TextBox;
            string changeText_summaryT = textBox_summaryT.Text;

            // Get the FileUpload control from the DetailsView
            FileUpload fileUpload_thumbnailPathT = DetailsView_news.FindControl("FileUpload_thumbnailPathT") as FileUpload;
            string fullFilePath = "";
            if (fileUpload_thumbnailPathT.HasFile)
            {
                string FileName = Path.GetFileName(fileUpload_thumbnailPathT.PostedFile.FileName);
                string saveDirectory = Server.MapPath("~/Album/");
                string savePath = Path.Combine(saveDirectory, FileName);
                fileUpload_thumbnailPathT.SaveAs(savePath);

                fullFilePath = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/Album/" + FileName);
            }

            string originalThumbnailPath = "";
            originalThumbnailPath = GetOriginalThumbnailPathFromDatabase(boardId);

            // Use the original thumbnail path if no new file was uploaded
            if (string.IsNullOrEmpty(fullFilePath))
            {
                fullFilePath = originalThumbnailPath;
            }
            string changeFileUpload_ImgT = fullFilePath;


            // Perform database update operation
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectnews2"].ConnectionString))
            {
                connection.Open();

                string sql = "UPDATE news SET dateTitle = @dateTitle, headline = @headline, isTop = @isTop, summary = @summary, thumbnailPath = @thumbnailPath WHERE Id = @Id";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);

                sqlCommand.Parameters.AddWithValue("@dateTitle", changeText_dateTitleT123);
                sqlCommand.Parameters.AddWithValue("@headline", changeText_headlineT);
                sqlCommand.Parameters.AddWithValue("@isTop", isTopT);
                sqlCommand.Parameters.AddWithValue("@summary", changeText_summaryT);
                sqlCommand.Parameters.AddWithValue("@thumbnailPath", changeFileUpload_ImgT);
                sqlCommand.Parameters.AddWithValue("@Id", boardId);

                sqlCommand.ExecuteNonQuery();
            }

            Response.Write("<script>alert('更新成功');</script>");
            DetailsView_news.ChangeMode(DetailsViewMode.ReadOnly);
            loadDayNewsHeadline();
        }

        // 示例方法，用于从数据库中检索原始缩略图路径
        private string GetOriginalThumbnailPathFromDatabase(int boardId)
        {
            string originalThumbnailPath = "";

            // 使用与您的数据库配置相匹配的连接字符串，执行查询以检索缩略图路径
            string connectionString = WebConfigurationManager.ConnectionStrings["Connectnews2"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT thumbnailPath FROM news WHERE Id = @Id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", boardId);

                // 执行查询并检索缩略图路径
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    originalThumbnailPath = result.ToString();
                }
            }

            return originalThumbnailPath;
        }

        protected void DetailsView_news_ItemDeleting(object sender, DetailsViewDeleteEventArgs e)
        {
            // Get the index of the record being deleted
            int boardId = Convert.ToInt32(e.Keys["Id"]);

            // Check if there's at least one record left in the data source before deletion
            if (DetailsView_news.DataSource != null && DetailsView_news.DataSource is ICollection collection && collection.Count <= 1)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform delete operation here
                using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectnews2"].ConnectionString))
                {
                    connection.Open();

                    string deleteSql = "DELETE FROM news WHERE Id = @boardId";
                    SqlCommand deleteCommand = new SqlCommand(deleteSql, connection);
                    deleteCommand.Parameters.AddWithValue("@boardId", boardId);
                    deleteCommand.ExecuteNonQuery();
                }

                loadDayNewsHeadline();
            }
        }

        //取消這樣寫
        protected void DetailsView_news_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                DetailsView_news.ChangeMode(DetailsViewMode.Edit);
                //ShowDVhistory(ViewState["HistoryNews"].ToString());
                loadDayNewsHeadline();
            }
            else if (e.CommandName == "Cancel")
            {
                DetailsView_news.ChangeMode(DetailsViewMode.ReadOnly);
                //ShowDVhistory(ViewState["HistoryNews"].ToString());
                loadDayNewsHeadline();
            }
        }

        //當DDL選擇日期改變時刷新畫面資料(本次最重要功能)
        protected void DropDownList_Headline_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string selectedValue = DropDownList_Headline.SelectedValue;

            string newsContent = GetnewsContent(selectedValue);

            CKEditorControl_newsContent.Text = newsContent;
        }

        //ckEditor取得資料
        private string GetnewsContent(string selectedValue)
        {
            string newsContent = "";

            //依選取日期取得資料庫新聞內容
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectnews2"].ConnectionString);
            string sql = "SELECT newsContentHtml FROM news WHERE Id = @selectedValue ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@selectedValue", selectedValue);
            connection.Open();

            //SqlDataReader reader = command.ExecuteReader();
            object result = command.ExecuteScalar();

            if (result != null)
            {
                newsContent = result.ToString();
            }
            else
            {
                newsContent = "";
            }
            connection.Close();

            return newsContent;
        }

        protected void UploadnewsContentBtn_Click(object sender, EventArgs e)
        {

        }

    }
}