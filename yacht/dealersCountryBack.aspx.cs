using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht
{
    public partial class dealersCountryBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LoginId"] != null)
                {
                    string loginId = Session["LoginId"].ToString();
                    bool isManger = (Session["isManger"] != null) ? (bool)Session["isManger"] : false;

                    if (loginId != null && isManger)
                    {
                        //顯示登入者
                        string name = Showusername(loginId);
                        Literal_name.Text = "歡迎, " + name + "!";
                        ShowDB();
                    }
                    else
                    {
                        //非IsManger，請重新登入
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    //尚未登入，請登入
                    Response.Redirect("Login.aspx");
                }

                //先綁定取得選取預設值:國家的
                DropDownList1.DataBind(); 
                showDealerList();
            }
        }

        protected void Button_add_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectcountry"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            //發送SQL語法
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            //查詢跟參數的部分很難寫成方法，需自行研究
            string sql = $"insert into countrySort (countrySort) values (@countrySort)";

            sqlCommand.Parameters.AddWithValue("@countrySort", TextBox_country.Text.Trim());

            sqlCommand.CommandText = sql;

            sqlCommand.ExecuteNonQuery();

            connection.Close();

            //清空輸入欄位
            TextBox_country.Text = "";

            //Response.Redirect("dealersCountryBack.aspx");
            ShowDB();
        }

        void ShowDB()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectcountry"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = "select * from countrySort ";

            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            GridView_country.DataSource = reader;

            GridView_country.DataBind();

            connection.Close();
        }

        string Showusername(string LoginId)
        {
            string name = "";

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectcountry"].ConnectionString);
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string sql = "select name from Login where Id = @LoginId";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.Parameters.AddWithValue("@LoginId", LoginId);

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    name = reader["name"].ToString();
                    break;
                }
            }

            connection.Close();

            return name;
        }

        protected void GridView_country_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView_country.EditIndex = e.NewEditIndex;
            ShowDB();
        }

        protected void GridView_country_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView_country.Rows[e.RowIndex];

            int boardId = Convert.ToInt32(GridView_country.DataKeys[e.RowIndex].Value);

            TextBox textBox_countrySortT = row.FindControl("TextBox_countrySortT") as TextBox;
            string changeText_countrySortT = textBox_countrySortT.Text;

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectcountry"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = $"update countrySort set countrySort = @countrySort where Id = @BoardId ";

            sqlCommand.Parameters.AddWithValue("@countrySort", changeText_countrySortT);
            sqlCommand.Parameters.AddWithValue("@BoardId", boardId);
            sqlCommand.CommandText = sql;

            sqlCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('更新成功');</script>");
            GridView_country.EditIndex = -1;
            ShowDB();
        }

        protected void GridView_country_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int boardId = Convert.ToInt32(GridView_country.DataKeys[e.RowIndex].Value);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectcountry"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string deleteSql = $"delete from [dealers] where [country_ID] = @boardId; delete from countrySort where Id = @boardId ";
            SqlCommand deleteCommand = new SqlCommand(deleteSql, connection);
            deleteCommand.Parameters.AddWithValue("@boardId", boardId);
            deleteCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('刪除成功');</script>");

            ShowDB();
        }

        protected void GridView_country_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_country.EditIndex = -1;
            ShowDB();
        }

        //如果gridview國家刪除時可以同步刷新DDL
        protected void DeltedCountry(object sender, EventArgs e)
        {
            //刷新國家下拉列表資料
            DropDownList1.DataBind(); 
        }

        ////國家區域GV
        //void ShowDB2()
        //{
        //    //依下拉選單選取國家的值 (id) 取得地區分類
        //    string selCountry_id = DropDownList1.SelectedValue;

        //    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectarea"].ConnectionString);

        //    if (connection.State != System.Data.ConnectionState.Open)
        //    {
        //        connection.Open();
        //    }

        //    string sql = "SELECT area FROM Dealers WHERE country_ID = @selCountry_id ";
        //    SqlCommand sqlCommand = new SqlCommand(sql, connection);
        //    sqlCommand.Parameters.AddWithValue("@selCountry_id", selCountry_id);

        //    sqlCommand.CommandText = sql;

        //    SqlDataReader reader = sqlCommand.ExecuteReader();

        //    GridView_area.DataSource = reader;

        //    GridView_area.DataBind();

        //    connection.Close();
        //}

        //顯示國家區域RBL
        private void showDealerList()
        {
            //依下拉選單選取國家的值 (id) 取得地區分類
            string selCountry_id = DropDownList1.SelectedValue;
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectarea"].ConnectionString);
            string sql = "SELECT area FROM Dealers WHERE country_ID = @selCountry_id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@selCountry_id", selCountry_id);

            //取得地區分類
            connection.Open();
            SqlDataReader readerCountry = command.ExecuteReader();
            while (readerCountry.Read())
            {
                string typeStr = readerCountry["area"].ToString();
                // RadioButtonList 增加方式
                ListItem listItem = new ListItem();
                listItem.Text = typeStr;
                listItem.Value = typeStr;
                RadioButtonList1.Items.Add(listItem);
            }
            connection.Close();
        }

        //增加國家區域
        protected void BtnAddArea_Click(object sender, EventArgs e)
        {
            //取得下拉選單國家的值 (id)
            string selCountry_id = DropDownList1.SelectedValue;
            //取得輸入欄內的文字
            string areaStr = TextBox_area.Text;
            //判斷是否重複用
            bool isRepeat = false;

            //取得地區分類
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectarea"].ConnectionString);
            string sql = $"SELECT area FROM Dealers WHERE country_ID = @selCountry_id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@selCountry_id", selCountry_id);
            connection.Open();
            SqlDataReader readerCountry = command.ExecuteReader();
            while (readerCountry.Read())
            {
                string typeStr = readerCountry["area"].ToString();
                //判斷有無重複名稱
                if (areaStr.Equals(typeStr))
                {
                    isRepeat = true;
                    //重複警告
                    TextBox_area.ForeColor = Color.Red;
                    TextBox_area.Text = "已重複使用";
                }
            }
            connection.Close();

            //輸入的區域名稱不重複才執行
            if (!isRepeat)
            {
                TextBox_area.ForeColor = Color.Black;
                //新增區域
                string sql2 = "INSERT INTO Dealers (country_ID, area) VALUES(@selCountry_id, @areaStr)";
                SqlCommand command2 = new SqlCommand(sql2, connection);
                command2.Parameters.AddWithValue("@selCountry_id", selCountry_id);
                command2.Parameters.AddWithValue("@areaStr", areaStr);
                connection.Open();
                command2.ExecuteNonQuery();
                connection.Close();

                //畫面渲染
                RadioButtonList1.Items.Clear(); //清掉舊的
                //BtnDelArea.Visible = false;
                //DealerList.Visible = false;
                //LabUploadImg.Visible = false;
                //UpdateDealerListLab.Visible = false;
                showDealerList(); //讀取新的

                //清空輸入欄位
                TextBox_area.Text = "";

                showDealerList();
            }
        }

        //刪除國家區域
        protected void BtnDelArea_Click(object sender, EventArgs e)
        {
            //取得選取資料的值
            string selAreaStr = RadioButtonList1.SelectedValue;

            //刪除實際圖檔檔案
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectarea"].ConnectionString);
            string sql = "SELECT dealerImgPath FROM Dealers WHERE area = @selAreaStr";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@selAreaStr", selAreaStr);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string imgPath = reader["dealerImgPath"].ToString();
                if (!imgPath.Equals(""))
                {
                    string savePath = Server.MapPath($"~/Tayanahtml/upload/Images/{imgPath}");
                    //File.Delete(savePath);
                }
            }
            connection.Close();

            //刪除資料庫該筆資料
            string sqlDel = "DELETE FROM Dealers WHERE area = @selAreaStr";
            SqlCommand commandDel = new SqlCommand(sqlDel, connection);
            commandDel.Parameters.AddWithValue("@selAreaStr", selAreaStr);
            connection.Open();
            commandDel.ExecuteNonQuery();
            connection.Close();

            //畫面渲染
            RadioButtonList1.Items.Clear(); //清掉舊的
            //BtnDelArea.Visible = false;
            //DealerList.Visible = false;
            //LabUploadImg.Visible = false;
            //UpdateDealerListLab.Visible = false;
            showDealerList(); //讀取新的
            TextBox_area.ForeColor = Color.Black;
            TextBox_area.Text = "";
        }

        //當選擇國家改變時刷新畫面資料
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList1.Items.Clear();
            //BtnDelArea.Visible = false;
            //DealerList.Visible = false;
            //LabUploadImg.Visible = false;
            //UpdateDealerListLab.Visible = false;
            showDealerList();
            GridView_area.DataBind();
        }
    }
}