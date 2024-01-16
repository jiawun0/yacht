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
    public partial class Newoverview : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //將 GUID 存入 Session 供上方列表共用
            string guidStr = (string)Session["guid"];

            if (!IsPostBack)
            {
                loadGallery(guidStr); //讀取並渲染上方相簿輪播
                loadLeftMenu(); //讀取並渲染左側型號邊欄
                loadTopMenu(); //讀取並渲染型號內容上方標題及分頁列
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //將型號對應 guid 存入 Session 與子頁共用
                getGuid(); //要放在 Init 不然 Content 頁會去先去抓 Session 而抓不到
            }
        }

        //從資料庫先撈guid
        private void getGuid()
        {
            //取得網址傳值的型號對應 GUID
            string guidStr = Request.QueryString["id"];
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtall"].ConnectionString);
            string sql = "SELECT TOP 1 guid FROM Yachts";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                //如果無網址傳值就用第一筆遊艇型號的 GUID
                if (String.IsNullOrEmpty(guidStr))
                {
                    guidStr = reader["guid"].ToString().Trim();
                }
            }
            connection.Close();
            //將 GUID 存入 Session 供上方列表共用
            Session["guid"] = guidStr;
        }

        //輪播
        private void loadGallery(string guidStr)
        {
            
            //依 GUID 取得遊艇輪播圖片資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtall"].ConnectionString);

            connection.Open();

            string sql = "select Yachts.Id, Yachts.guid, YachtsPhoto.PhotoPath from Yachts " +
               " left join YachtsPhoto on Yachts.Id = YachtsPhoto.YachtsId " +
               " where guid = @guid ";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@guid", guidStr);
            ////將準備的SQL指令給操作物件
            command.CommandText = sql;

            SqlDataReader reader = command.ExecuteReader();

            // Create a list to store the PhotoPaths
            List<string> photoPaths = new List<string>();
            while (reader.Read())
            {
                string photoPath = reader["PhotoPath"].ToString().Trim();
                photoPaths.Add(photoPath);
            }

            ////設定用 Eval 綁定的輪播圖片路徑資料
            Repeater_photo.DataSource = photoPaths; // 設定資料來源
            Repeater_photo.DataBind(); // 刷新圖片資料

            connection.Close();
        }

        //相對路徑
        protected string GetRelativeImagePath(string albumPath)
        {
            if (!string.IsNullOrEmpty(albumPath))
            {
                string relativePath = albumPath.Replace(Server.MapPath("~"), "").Replace(Server.MapPath("\\"), "/");
                return relativePath;
            }
            return string.Empty;
        }

        //左側欄位
        private void loadLeftMenu()
        {
            string urlPathStr = System.IO.Path.GetFileName(Request.PhysicalPath);
            
            //取得遊艇型號資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtall"].ConnectionString);
            string sql = "SELECT * FROM Yachts";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            StringBuilder leftMenuHtml = new StringBuilder();

            while (reader.Read())
            {
                string yachtModelStr = reader["yachtModel"].ToString();
                string isNewDesignStr = reader["isNewDesign"].ToString();
                string isNewBuildingStr = reader["isNewBuilding"].ToString();
                string guidStr = reader["guid"].ToString();
                string isNewStr = "";
                //依是否為新建或新設計加入標註
                if (isNewDesignStr.Equals("True"))
                {
                    isNewStr = "新設計";
                }
                else if (isNewBuildingStr.Equals("True"))
                {
                    isNewStr = "新建製";
                }
                leftMenuHtml.Append($"<li><a href='{urlPathStr}?id={guidStr}'>{yachtModelStr} {isNewStr}</a></li>");
            }
            connection.Close();

            //渲染左側遊艇型號選單
            LeftMenuHtml.Text = leftMenuHtml.ToString();
        }

        //上方欄位
        private void loadTopMenu()
        {
            //取得 Session 共用 GUID，Session 物件需轉回字串
            string guidStr = Session["guid"].ToString();
            //依 GUID 取得遊艇資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectYachtall"].ConnectionString);
            string sql = "SELECT * FROM Yachts WHERE guid = @guid";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@guid", guidStr);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            StringBuilder topMenuHtmlStr = new StringBuilder();
            //StringBuilder dimensionsTableHtmlStr = new StringBuilder();

            if (reader.Read())
            {
                string yachtModelStr = reader["yachtModel"].ToString();
                //string contentHtmlStr = HttpUtility.HtmlDecode(reader["overviewContentHtml"].ToString());
                //string loadJson = HttpUtility.HtmlDecode(reader["overviewDimensionsJSON"].ToString());
                //string dimensionsImgPathStr = reader["overviewDimensionsImgPath"].ToString();
                //string downloadsFilePathStr = reader["overviewDownloadsFilePath"].ToString();

                //加入渲染型號內容上方分類連結列表
                topMenuHtmlStr.Append($"<li><a class='menu_yli01' href='yachtOverviewFront.aspx?id={guidStr}' >OverView</a></li>");
                topMenuHtmlStr.Append($"<li><a class='menu_yli02' href='yachtLayoutFront.aspx?id={guidStr}' >Layout & deck plan</a></li>");
                topMenuHtmlStr.Append($"<li><a class='menu_yli03' href='yachtSpecificationFront.aspx?id={guidStr}' >Specification</a></li>");

                //渲染畫面
                //渲染上方小連結
                LabLink.InnerText = yachtModelStr;
                //渲染標題
                LabTitle.InnerText = yachtModelStr;
                //渲染型號內容上方分類連結列表
                TopMenuHtml.Text = topMenuHtmlStr.ToString();
            }
            connection.Close();
        }
    }
}
