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
    public partial class dealersFront : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getCountryID();
                loadLeftMenu();
                loadDealerList();

                //if (Request.QueryString["Id"] != null)
                //{
                //    string countryIDStr = Request.QueryString["Id"];

                //    loadDealerList();
                //}
            }
        }

        //取得國家 id
        private void getCountryID()
        {
            //取得網址傳值的 id 內容
            string urlIDStr = Request.QueryString["Id"];

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectcountry"].ConnectionString);

            //設為第一筆國家名稱 Id
            string sql = "SELECT TOP 1 Id FROM CountrySort";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                urlIDStr = reader["Id"].ToString();
            }
            connection.Close();

            //將 id 存入 Session 使用
            //Session["Id"] = urlIDStr;
        }

        //讀取側邊欄
        private void loadLeftMenu()
        {
            //反覆變更字串的值建議用 StringBuilder 效能較好
            //處理大量字串操作，允許在追加字串時更有效率地處理內部的記憶體管理
            StringBuilder leftMenuHtml = new StringBuilder();

            //取得國家分類
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectcountry"].ConnectionString);
            string sqlCountry = "SELECT * FROM CountrySort";
            SqlCommand commandCountry = new SqlCommand(sqlCountry, connection);
            connection.Open();
            SqlDataReader readerCountry = commandCountry.ExecuteReader();

            while (readerCountry.Read())
            {
                string idStr = readerCountry["Id"].ToString();
                string countryStr = readerCountry["countrySort"].ToString();
                // StringBuilder 用 Append 加入字串內容
                leftMenuHtml.Append($"<li><a href='dealersFront.aspx?Id={idStr}'> {countryStr} </a></li>");
            }
            connection.Close();

            //渲染畫面
            LeftMenu.Text = leftMenuHtml.ToString();
        }

        //讀取主要內容
        private void loadDealerList()
        {
            //取得 Session 儲存 id，Session 物件需轉回字串
            //string countryIDStr = Session["Id"].ToString();

            string countryIDStr = "8";
            if (Request.QueryString["Id"] != null)
            {
                countryIDStr = Request.QueryString["Id"];
            }

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectarealist"].ConnectionString);
            string sql = "SELECT countrySort FROM CountrySort WHERE Id = @countryIDStr";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@countryIDStr", countryIDStr);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string countryStr = reader["countrySort"].ToString();
                LabLink.InnerText = countryStr;
                LitTitle.InnerText = countryStr;
            }
            connection.Close();

            //依 country id 取得代理商資料
            StringBuilder dealerListHtml = new StringBuilder();
            string sqlArea = "select * FROM dealers where country_ID = @countryIDStr";
            SqlCommand commandArea = new SqlCommand(sqlArea, connection);
            commandArea.Parameters.AddWithValue("@countryIDStr", countryIDStr);
            connection.Open();
            SqlDataReader readerArea = commandArea.ExecuteReader();

            while (readerArea.Read())
            {
                string idStr = readerArea["Id"].ToString();
                string areaStr = readerArea["area"].ToString();
                string imgPathStr = readerArea["dealerImgPath"].ToString();
                imgPathStr = GetRelativeImagePath(imgPathStr); // 將路徑轉換為相對路徑
                string nameStr = readerArea["name"].ToString();
                string contactStr = readerArea["contact"].ToString();
                string addressStr = readerArea["address"].ToString();
                string telStr = readerArea["tel"].ToString();
                string faxStr = readerArea["fax"].ToString();
                string emailStr = readerArea["email"].ToString();
                string linkStr = readerArea["link"].ToString();

                dealerListHtml.Append("<li><div class='list02'><ul><li class='list02li'><div>" +
                                     $"<p><img id='Image{idStr}' src='{imgPathStr}'" +
                                     $"style='border-width:0px;' Width='209px' /> </p></div></li>" +
                                     $"<li class='list02li02'> <span>{areaStr}</span><br />");

                if (!string.IsNullOrEmpty(nameStr))
                {
                    dealerListHtml.Append($"{nameStr}<br />");
                }
                if (!string.IsNullOrEmpty(contactStr))
                {
                    dealerListHtml.Append($"Contact：{contactStr}<br />");
                }
                if (!string.IsNullOrEmpty(addressStr))
                {
                    dealerListHtml.Append($"Address：{addressStr}<br />");
                }
                if (!string.IsNullOrEmpty(telStr))
                {
                    dealerListHtml.Append($"TEL：{telStr}<br />");
                }
                if (!string.IsNullOrEmpty(faxStr))
                {
                    dealerListHtml.Append($"FAX：{faxStr}<br />");
                }
                if (!string.IsNullOrEmpty(emailStr))
                {
                    dealerListHtml.Append($"E-Mail：{emailStr}<br />");
                }
                if (!string.IsNullOrEmpty(linkStr))
                {
                    dealerListHtml.Append($"<a href='{linkStr}' target='_blank'>{linkStr}</a>");
                }
                dealerListHtml.Append("</li></ul></div></li>");
            }
            connection.Close();

            //渲染畫面
            DealerList.Text = dealerListHtml.ToString();
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
