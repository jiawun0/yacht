using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

namespace yacht
{
    public partial class newsFront : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadList();
                showPageControls();
            }
        }

        private void loadList()
        {
            //預設為第1頁
            int page = 1;
            //判斷網址後有無參數
            //也可用String.IsNullOrWhiteSpace
            if (!String.IsNullOrEmpty(Request.QueryString["page"]))
            {
                page = Convert.ToInt32(Request.QueryString["page"]);
            }

            //設定頁面參數屬性
            //設定控制項參數: 一頁幾筆資料
            WebUserControl_Page.limit = 5;
            //設定控制項參數: 作用頁面完整網頁名稱
            WebUserControl_Page.targetPage = "WebForm1.aspx";

            //建立計算分頁資料顯示邏輯 (每一頁是從第幾筆開始到第幾筆結束)
            //計算每個分頁的第幾筆到第幾筆
            var floor = (page - 1) * WebUserControl_Page.limit + 1; //每頁的第一筆
            var ceiling = page * WebUserControl_Page.limit; //每頁的最末筆

            //將取得的資料數設定給參數 count
            int count = 36; //總資料數，可修改數字測試分頁功能是否正常

            //設定控制項參數: 總共幾筆資料
            WebUserControl_Page.totalItems = count;

            //渲染分頁控制項
            WebUserControl_Page.showPageControls();

            //設定模擬資料內容
            StringBuilder listHtml = new StringBuilder();
            for (int i = floor; i <= ceiling; i++)
            {
                if (i <= count)
                {
                    listHtml.Append($"<a href=''> --------- 第 {i} 筆資料 --------- </a></li><br /><br />");
                }
            }
            LiteralTest.Text = listHtml.ToString();
        }

        public void showPageControls()
        {
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

            //NowPageCount 目前這頁要從DataSet第幾筆資料開始撈取
            int NowPageCount = 0;
            if (p > 0)
            {
                NowPageCount = (p - 1) * PageSize;
            }
            Response.Write("共計" + RecordCount + "筆 / 共需" + Pages + "頁");

            //rowNo 目前這頁要撈出幾筆
            int rowNo = 0;
            int index = NowPageCount;
            Response.Write("<table border = '0' width = '95%'>");
            Response.Write("<tr><th>ID</th><th>Title</th><th>Content</th></tr>");

            //while ((rowNo < PageSize) && (NowPageCount < RecordCount))
            //{
            //    DataRow dr = dt.Rows[index]; // Get the current row from the dataset
            //    int Id = Convert.ToInt32(dr["Id"]); // Assuming 'Id' is an integer column
            //    string headline = dr["headline"].ToString(); // Assuming 'Title' is a string column
            //    string summary = dr["summary"].ToString(); // Assuming 'Content' is a string column

            //    // Creating HTML table rows for each record
            //    Response.Write("<tr>");
            //    Response.Write("<td>" + Id + "</td>");
            //    Response.Write("<td>" + headline + "</td>");
            //    Response.Write("<td>" + summary + "</td>");
            //    Response.Write("</tr>");

            //    rowNo++; // Increment row number for pagination
            //    index++; // Move to the next record in the dataset
            //}

            //Response.Write("</table>");

            //顯示上一頁下一頁
            if (Pages > 0)
            {
                if (p > 1)
                {
                    Response.Write("<a href = 'newsFront.aspx?p=" + (p - 1) + "'>[<<<上一頁]</a>");
                }
                //Response.Write("首頁");
                if (p < Pages)
                {
                    Response.Write("<a href = 'newsFront.aspx?p=" + (p + 1) + "'>[下一頁>>>]</a>");
                }
            }

            //顯示所有頁數
            Response.Write("<hr width = '97%' size = 1>");
            //Pages 總頁數
            for (int i = 1; i <= Pages; i++)
            {
                if (p == i)
                {
                    //列出所在頁數
                    Response.Write("[" + p + "]&nbsp;&nbsp;");
                }
                else
                {
                    //列出每頁頁數
                    Response.Write("<a href = 'newsFront.aspx?p=" + i + "'>" + i + "</a> &nbsp; &nbsp;");
                }
            }
        }
    }
}