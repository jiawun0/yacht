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
    public partial class Newoverview : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //將 GUID 存入 Session 供上方列表共用
            string guidStr = (string)Session["guid"];

            if (!IsPostBack)
            {
                loadGallery(guidStr); //讀取並渲染上方相簿輪播
                //loadLeftMenu(); //讀取並渲染左側型號邊欄
                //loadTopMenu(); //讀取並渲染型號內容上方標題及分頁列
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

        private void getGuid()
        {
            //取得網址傳值的型號對應 GUID
            string guidStr = Request.QueryString["guidStr"];
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

        protected string GetRelativeImagePath(string albumPath)
        {
            if (!string.IsNullOrEmpty(albumPath))
            {
                string relativePath = albumPath.Replace(Server.MapPath("~"), "").Replace(Server.MapPath("\\"), "/");
                return relativePath;
            }
            return string.Empty;
        }
    }
}
