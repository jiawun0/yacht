using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;

namespace yacht
{
    public partial class newsFront2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadList();
                showPageControls();
            }
        }

        //參考大抄
        public int RecordCount { get; set; }//總共幾筆資料totalItems
        public int PageSize { get; set; }//一頁幾筆資料limit
        public string targetPage { get; set; } //作用頁面完整網頁名稱

        public void showPageControls()
        {
            litPage.Text = ""; //清空分頁控制項
            int page = 1; //預設第1頁

            if (RecordCount == 0)
            {
                return;
            }
            if (PageSize == 0)
            {
                return;
            }
            //確認當前頁面檔案名稱非 null 在 ?? 左側非 null 則不變，左側是 null 則傳回右側結果 
            targetPage = targetPage ?? System.IO.Path.GetFileName(Request.PhysicalPath);

            //渲染分頁控制項 //鄰近頁 adjacents 參數不建議設太大，可能導致換行
            //litPage.Text = getPaginationString(page, RecordCount, PageSize, 2, targetPage);
        }
        ////前台呈現所有新聞格式
        //private void loadList()
        //{
        //    //預設為第1頁
        //    int page = 1;
        //    //判斷網址後有無參數
        //    //也可用String.IsNullOrWhiteSpace
        //    if (!String.IsNullOrEmpty(Request.QueryString["page"]))
        //    {
        //        page = Convert.ToInt32(Request.QueryString["page"]);
        //    }

        //    //設定頁面參數屬性
        //    //設定控制項參數: 一頁幾筆資料
        //    WebUserControl_Page.limit = 5;
        //    //設定控制項參數: 作用頁面完整網頁名稱
        //    WebUserControl_Page.targetPage = "WebForm1.aspx";

        //    //建立計算分頁資料顯示邏輯 (每一頁是從第幾筆開始到第幾筆結束)
        //    //計算每個分頁的第幾筆到第幾筆
        //    var floor = (page - 1) * WebUserControl_Page.limit + 1; //每頁的第一筆
        //    var ceiling = page * WebUserControl_Page.limit; //每頁的最末筆

        //    //將取得的資料數設定給參數 count
        //    int count = 36; //總資料數，可修改數字測試分頁功能是否正常

        //    //設定控制項參數: 總共幾筆資料
        //    WebUserControl_Page.totalItems = count;

        //    //渲染分頁控制項
        //    WebUserControl_Page.showPageControls();

        //    //設定模擬資料內容
        //    StringBuilder listHtml = new StringBuilder();
        //    for (int i = floor; i <= ceiling; i++)
        //    {
        //        if (i <= count)
        //        {
        //            listHtml.Append($"<a href=''> --------- 第 {i} 筆資料 --------- </a></li><br /><br />");
        //        }
        //    }
        //    LiteralTest.Text = listHtml.ToString();
        //}

        public class NewsItem0
        {
            public string ImageUrl { get; set; }
            public string Date { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }

        public class NewsItem
        {
            public string ImageUrl { get; set; }
            public string Date { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }

        //前台呈現所有新聞格式
        private void loadList()
        {
            //將news資料呈現
            StringBuilder htmlBuilder = new StringBuilder();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectnews2"].ConnectionString);
            connection.Open();
            string sql = "select Id, dateTitle, headline, summary, thumbnailPath from news order by Id asc ";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string thumbnailPath = reader["thumbnailPath"].ToString();
                string dateTitle = reader["dateTitle"].ToString();
                string headline = reader["headline"].ToString();
                string summary = reader["summary"].ToString();

                htmlBuilder.Append("<li>");
                htmlBuilder.Append("<div class='list01'>");
                htmlBuilder.Append("<ul>");
                htmlBuilder.Append("<li><div><p><img src='" + thumbnailPath + "' alt='' /></p></div></li>");
                htmlBuilder.Append("<li><span>" + dateTitle + "</span><br />" + headline + "</li>");
                htmlBuilder.Append("<li>" + summary + "</li>");
                htmlBuilder.Append("</ul>");
                htmlBuilder.Append("</div>");
                htmlBuilder.Append("</li>");
            }
            reader.Close();
            connection.Close();

            // 將 HTML 字串設置到 Literal 控制項中
            newList.Text = "<ul>" + htmlBuilder.ToString() + "</ul>";
            connection.Close();

            //for (int i = floor; i <= ceiling; i++)
            //{
            //    if (i <= count)
            //    {
            //        listHtml.Append($"<a href=''> --------- 第 {i} 筆資料 --------- </a></li><br /><br />");
            //    }
            //}

            //if (newList != null)
            //{
            //    newList.Text = listHtml.ToString();
            //}
        }

        //下方分頁欄位設定~參考2000的dataset程式碼
        public void dataset()
        {
            // 創建一個 StringBuilder 來儲存全部內容+下方分頁欄位的 HTML 內容
            StringBuilder sb = new StringBuilder();

            //p 目前第幾頁
            int p = Convert.ToInt32(Request["p"]);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectnews2"].ConnectionString);
            connection.Open();
            string sql = "select * from news order by Id asc ";
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);

            DataSet ds = new DataSet();

            //將SQL指令填入DataSet
            da.Fill(ds);

            DataTable dt = ds.Tables[0];

            //PageSize 每頁顯示5筆
            int PageSize = 5;

            //SQL指令總筆數
            int RecordCount = dt.Rows.Count;

            //如果撈不到資料就結束
            if (RecordCount == 0)
            {
                Response.Write("<script>alert('沒有資料');</script>");
                connection.Close();
                Response.End();
            }

            //Pages 資料總頁數，使用除法取得商
            int Pages = ((RecordCount + PageSize) - 1) / PageSize;

            int StartIndex = (p - 1) * PageSize;
            int EndIndex = Math.Min(RecordCount, p * PageSize);

            //NowPageCount 目前這頁要從DataSet第幾筆資料開始撈取
            int NowPageCount = 0;

            if (p > 0)
            {
                NowPageCount = (p - 1) * PageSize;
            }
            //Response.Write("共計" + RecordCount + "筆 / 共需" + Pages + "頁");
            sb.Append("共計" + RecordCount + "筆 / 共需" + Pages + "頁");

            //rowNo 目前這頁要撈出幾筆
            int rowNo = 0;
            int index = NowPageCount;

            //Response.Write("<table border = '0' width = '95%'>");
            //Response.Write("<tr><th>ID</th><th>Title</th><th>Content</th></tr>");
            sb.Append("<table border='0' width='95%'>");
            sb.Append("<tr><th>dateTitle</th><th>Title</th><th>Content</th></tr>");

            //頁面呈現內容在這
            while ((rowNo < PageSize) && (NowPageCount < RecordCount))
            {
                DataRow dr = dt.Rows[index]; // Get the current row from the dataset
                int Id = Convert.ToInt32(dr["Id"]); // Assuming 'Id' is an integer column
                string headline = dr["headline"].ToString(); // Assuming 'Title' is a string column
                string summary = dr["summary"].ToString(); // Assuming 'Content' is a string column

                // Creating HTML table rows for each record
                Response.Write("<tr>");
                Response.Write("<td>" + Id + "</td>");
                Response.Write("<td>" + headline + "</td>");
                Response.Write("<td>" + summary + "</td>");
                Response.Write("</tr>");

                rowNo++; // Increment row number for pagination
                index++; // Move to the next record in the dataset
            }

            //Response.Write("</table>");
            sb.Append("</table>");

            //顯示上一頁下一頁
            if (Pages > 0)
            {
                if (p > 1)
                {
                    //Response.Write("<a href = 'newsFront.aspx?p=" + (p - 1) + "'>[<<<上一頁]</a>");
                    sb.Append("<a href = 'newsFront.aspx?p=" + (p - 1) + "'>[<<<上一頁]</a>");
                }
                //Response.Write("首頁");
                if (p < Pages)
                {
                    //Response.Write("<a href = 'newsFront.aspx?p=" + (p + 1) + "'>[下一頁>>>]</a>");
                    sb.Append("<a href = 'newsFront.aspx?p=" + (p + 1) + "'>[下一頁>>>]</a>");
                }
            }

            //顯示所有頁數
            //Response.Write("<hr width = '97%' size = 1>");
            sb.Append("<hr width='97%' size='1'>");

            //Pages 總頁數
            for (int i = 1; i <= Pages; i++)
            {
                if (p == i)
                {
                    //列出所在頁數
                    //Response.Write("[" + p + "]&nbsp;&nbsp;");
                    sb.Append("[" + p + "]&nbsp;&nbsp;");
                }
                else
                {
                    //列出每頁頁數
                    //Response.Write("<a href = 'newsFront.aspx?p=" + i + "'>" + i + "</a> &nbsp; &nbsp;");
                    sb.Append("<a href='newsFront.aspx?p=" + i + "'>" + i + "</a>&nbsp;&nbsp;");
                }
            }
            // 取得 ASP.NET Literal 控制項
            Literal litPage = (Literal)FindControl("litPage");

            // 檢查 Literal 控制項是否存在
            if (litPage != null)
            {
                // 將 StringBuilder 中的動態生成 HTML 內容指定給 Literal 控制項的 Text 屬性
                litPage.Text = sb.ToString();
            }
        }

        protected void lnkFirstPage_Click(object sender, EventArgs e)
        {
            dl_currentPage.SelectedValue = "1";//到第一頁
            this.btnToPage_Click(null, null);//跳頁事件
        }

        protected void lnkPrePage_Click(object sender, EventArgs e)
        {
            dl_currentPage.SelectedValue = Convert.ToInt32(dl_currentPage.SelectedValue) - 1 + "";
            this.btnToPage_Click(null, null);//跳頁事件
        }

        protected void lnkNextPage_Click(object sender, EventArgs e)
        {
            dl_currentPage.SelectedValue = Convert.ToInt32(dl_currentPage.SelectedValue) + 1 + "";
            this.btnToPage_Click(null, null);//跳頁事件
        }

        protected void lnkLastPage_Click(object sender, EventArgs e)
        {
            dl_currentPage.SelectedValue = dl_currentPage.Items[dl_currentPage.Items.Count - 1].Value;
            this.btnToPage_Click(null, null);//跳頁事件
        }

        protected void btnToPage_Click(object sender, EventArgs e)
        {
            //防呆
            int currentPage = 1;
            if (dl_currentPage.SelectedValue == "")
            {
                currentPage = 1;
            }
            else
            {
                currentPage = Convert.ToInt32(dl_currentPage.SelectedValue);
            }
        }
    }
}
