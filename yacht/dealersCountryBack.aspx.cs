using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht
{
    public partial class dealersCountryBack : System.Web.UI.Page
    {
        //先把管理者卡登入註解，等完成再開啟
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //先綁定取得選取預設值:國家
                DropDownList1.DataBind();
                showDealerList();
                loadCountry();
                loadDealers();

                //if (Session["LoginId"] != null)
                //{
                //    string loginId = Session["LoginId"].ToString();
                //    bool isManger = (Session["isManger"] != null) ? (bool)Session["isManger"] : false;

                //    if (loginId != null && isManger)
                //    {
                //        //顯示登入者
                //        string name = Showusername(loginId);
                //        Literal_name.Text = "歡迎, " + name + "!";
                //        //先綁定取得選取預設值:國家
                //        DropDownList1.DataBind();
                //        showDealerList();
                //        loadCountry();
                //        loadDealers();
                //    }
                //    else
                //    {
                //        //非IsManger，請重新登入
                //        Response.Redirect("Login.aspx");
                //    }
                //}
                //else
                //{
                //    //尚未登入，請登入
                //    Response.Redirect("Login.aspx");
                //}
            }
        }

        //增加國家
        protected void Button_add_Click(object sender, EventArgs e)
        {
            string textboxValue = TextBox_country.Text;

            if (string.IsNullOrEmpty(textboxValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('欄位不可為空');", true);
                return;
            }

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
            loadCountry();
        }

        //顯示目前國家
        void loadCountry()
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

            // 重新繫結 DropDownList1
            DropDownList1.DataBind();

            connection.Close();
        }

        //顯示登入者
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
            loadCountry();
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
            loadCountry();
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

            // 重新繫結 DropDownList1
            DropDownList1.DataBind();

            loadCountry();
        }

        protected void GridView_country_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_country.EditIndex = -1;
            loadCountry();
        }

        //如果gridview國家刪除時可以同步刷新DDL
        protected void DeltedCountry(object sender, EventArgs e)
        {
            //刷新國家下拉列表資料
            DropDownList1.DataBind();
        }

        //顯示國家區域RBL(會全部顯示)~目前隱藏未使用
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

        //刪除國家區域~目前隱藏未使用
        protected void BtnDelArea_Click(object sender, EventArgs e)
        {
            //取得選取資料的值
            string selAreaStr = RadioButtonList1.SelectedValue;

            //刪除實際圖檔檔案
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectarea"].ConnectionString);
            string sql = "SELECT dealerImgPath FROM dealers WHERE area = @selAreaStr";
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
            string sqlDel = "DELETE FROM dealers WHERE area = @selAreaStr";
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

        //當DDL選擇國家改變時刷新畫面資料(本次最重要功能)
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView_area.DataBind();
            loadDealers();
        }

        //增加區域
        protected void BtnAddArea_Click(object sender, EventArgs e)
        {
            //取得下拉選單國家的值 (id)
            string selCountry_id = DropDownList1.SelectedValue;
            ////取得輸入欄內的文字
            //string areaStr = TextBox_area.Text;
            ////判斷是否重複用
            //bool isRepeat = false;

            ////取得地區分類
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectarealist"].ConnectionString);
            string sql = $"SELECT area FROM dealers WHERE country_ID = @selCountry_id ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@selCountry_id", selCountry_id);
            connection.Open();
            //SqlDataReader readerCountry = command.ExecuteReader();
            //while (readerCountry.Read())
            //{
            //    string typeStr = readerCountry["area"].ToString();
            //    //判斷有無重複名稱
            //    if (areaStr.Equals(typeStr))
            //    {
            //        isRepeat = true;
            //        //重複警告
            //        TextBox_area.ForeColor = Color.Red;
            //        TextBox_area.Text = "已重複使用";
            //        break;
            //    }
            //}
            //readerCountry.Close();
            //connection.Close();

            //檢查欄位不可為空
            string areaValue = TextBox_area.Text;
            string nameValue = TextBox_name.Text;
            string contactValue = TextBox_contact.Text;
            string addressValue = TextBox_address.Text;
            string telValue = TextBox_tel.Text;
            string emailValue = TextBox_email.Text;

            if (string.IsNullOrEmpty(areaValue) || string.IsNullOrEmpty(nameValue) ||
                string.IsNullOrEmpty(contactValue) || string.IsNullOrEmpty(addressValue) || string.IsNullOrEmpty(telValue) ||
                 string.IsNullOrEmpty(emailValue) )
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('欄位不可為空，請檢查確認');", true);
                return;
            }

            //有檔案才能上傳
            //FileUpload1.PostedFile != null
            if (FileUpload_Img.HasFile)
            {
                try
                {
                    string FileName = Path.GetFileName(FileUpload_Img.FileName);
                    string saveDirectory = Server.MapPath("~/Album/");
                    string savePath = Path.Combine(saveDirectory, FileName);
                    FileUpload_Img.SaveAs(savePath);

                    //新增區域
                    string sql2 = "insert into dealers (country_ID, area, dealerImgPath, name, contact, address, tel, fax, email, link) values(@selCountry_id, @areaStr, @dealerImgPath, @name, @contact, @address, @tel, @fax, @email, @link) ";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@selCountry_id", selCountry_id);
                    command2.Parameters.AddWithValue("@areaStr", TextBox_area.Text.Trim());
                    command2.Parameters.AddWithValue("@dealerImgPath", savePath);
                    command2.Parameters.AddWithValue("@name", TextBox_name.Text.Trim());
                    command2.Parameters.AddWithValue("@contact", TextBox_contact.Text.Trim());
                    command2.Parameters.AddWithValue("@address", TextBox_address.Text.Trim());
                    command2.Parameters.AddWithValue("@tel", TextBox_tel.Text.Trim());
                    command2.Parameters.AddWithValue("@fax", TextBox_fax.Text.Trim());
                    command2.Parameters.AddWithValue("@email", TextBox_email.Text.Trim());
                    command2.Parameters.AddWithValue("@link", TextBox_link.Text.Trim());

                    int rowsAffected = command2.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // 新增成功
                        RadioButtonList1.Items.Clear(); // 清除舊資料
                        showDealerList(); // 重新讀取資料
                        loadDealers();

                        // 清空輸入欄位
                        TextBox_area.Text = "";
                        TextBox_name.Text = "";
                        TextBox_contact.Text = "";
                        TextBox_address.Text = "";
                        TextBox_tel.Text = "";
                        TextBox_fax.Text = "";
                        TextBox_email.Text = "";
                        TextBox_link.Text = "";
                    }
                    else
                    {
                        // 新增失敗，請記錄錯誤或顯示適當訊息
                        Response.Write("<script>alert('新增失敗');</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            connection.Close();
        }

        //顯示目前區域
        void loadDealers()
        {
            //依下拉選單選取國家的值 (id) 取得地區分類
            //string selCountry_id = "1";
            string selCountry_id = DropDownList1.SelectedValue;

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectarealist"].ConnectionString);
            connection.Open();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = "SELECT * FROM dealers WHERE country_ID = @selCountry_id ";

            sqlCommand.Parameters.AddWithValue("@selCountry_id", selCountry_id);
            //將準備的SQL指令給操作物件
            sqlCommand.CommandText = sql;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            GridView_arealist.DataSource = reader;

            GridView_arealist.DataBind();

            connection.Close();
        }

        protected void GridView_arealist_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView_arealist.EditIndex = e.NewEditIndex;
            loadDealers();

            //GridViewRow row = GridView_arealist.Rows[e.NewEditIndex];
            //if (GridView_arealist.EditIndex == e.NewEditIndex)
            //{
            //    // 取得編輯模板中的控制項
            //    FileUpload fileUpload_FileUpload_ImgT = row.FindControl("FileUpload_FileUpload_ImgT") as FileUpload;
            //    string filePath = fileUpload_FileUpload_ImgT.FileName; // 取得上傳檔案的路徑
            //}
        }

        protected void GridView_arealist_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView_arealist.Rows[e.RowIndex];

            int boardId = Convert.ToInt32(GridView_arealist.DataKeys[e.RowIndex].Value);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectarealist"].ConnectionString);

            TextBox textBox_areaT = row.FindControl("TextBox_areaT") as TextBox;
            string changeText_areaT = textBox_areaT.Text;

            // 取得編輯模板中的控制項
            FileUpload fileUpload_ImgT = row.FindControl("FileUpload_ImgT") as FileUpload;
            string fullFilePath = "";

            //如果有重新上傳
            if (fileUpload_ImgT.HasFile)
            {
                string FileName = Path.GetFileName(fileUpload_ImgT.PostedFile.FileName); // 取得上傳檔案的路徑
                string saveDirectory = Server.MapPath("~/Album/");
                string savePath = Path.Combine(saveDirectory, FileName);
                fileUpload_ImgT.SaveAs(savePath);

                // 用完整的網址表示已上傳的檔案路徑
                fullFilePath = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/Album/" + FileName);
            }
            //沒有重新上傳就找舊的
            string originalThumbnailPath = "";
            originalThumbnailPath = GetOriginalThumbnailPathFromDatabase(boardId);

            // Use the original thumbnail path if no new file was uploaded
            if (string.IsNullOrEmpty(fullFilePath))
            {
                fullFilePath = originalThumbnailPath;
            }

            string changeFileUpload_ImgT = fullFilePath;

            //TextBox textBox_FileUpload_ImgT = row.FindControl("TextBox_FileUpload_ImgT") as TextBox;
            //string changeText_FileUpload_ImgT = textBox_FileUpload_ImgT.Text;

            TextBox textBox_nameT = row.FindControl("TextBox_nameT") as TextBox;
            string changeText_nameT = textBox_nameT.Text;

            TextBox textBox_contactT = row.FindControl("TextBox_contactT") as TextBox;
            string changeText_contactT = textBox_contactT.Text;

            TextBox textBox_addressT = row.FindControl("TextBox_addressT") as TextBox;
            string changeText_addressT = textBox_addressT.Text;

            TextBox textBox_telT = row.FindControl("TextBox_telT") as TextBox;
            string changeText_telT = textBox_telT.Text;

            TextBox textBox_faxT = row.FindControl("TextBox_faxT") as TextBox;
            string changeText_faxT = textBox_faxT.Text;

            TextBox textBox_emailT = row.FindControl("TextBox_emailT") as TextBox;
            string changeText_emailT = textBox_emailT.Text;

            TextBox textBox_linkT = row.FindControl("TextBox_linkT") as TextBox;
            string changeText_linkT = textBox_linkT.Text;

            //檢查欄位不可為空
            string areaValue = changeText_areaT;
            string nameValue = changeText_nameT;
            string contactValue = changeText_contactT;
            string addressValue = changeText_addressT;
            string telValue = changeText_telT;
            string emailValue = changeText_emailT;

            if (string.IsNullOrEmpty(areaValue) || string.IsNullOrEmpty(nameValue) ||
                string.IsNullOrEmpty(contactValue) || string.IsNullOrEmpty(addressValue) || string.IsNullOrEmpty(telValue) ||
                string.IsNullOrEmpty(emailValue) )
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('欄位不可為空，請檢查確認');", true);
                return;
            }

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            string sql = $"update dealers set area = @area, dealerImgPath= @dealerImgPath, name = @name, contact = @contact, address = @address, tel = @tel, fax = @fax, email = @email, link = @link where Id = @BoardId ";

            sqlCommand.Parameters.AddWithValue("@BoardId", boardId);
            sqlCommand.Parameters.AddWithValue("@area", changeText_areaT);
            //sqlCommand.Parameters.AddWithValue("@dealerImgPath", changeText_FileUpload_ImgT);
            sqlCommand.Parameters.AddWithValue("@dealerImgPath", changeFileUpload_ImgT);
            sqlCommand.Parameters.AddWithValue("@name", changeText_nameT);
            sqlCommand.Parameters.AddWithValue("@contact", changeText_contactT);
            sqlCommand.Parameters.AddWithValue("@address", changeText_addressT);
            sqlCommand.Parameters.AddWithValue("@tel", changeText_telT);
            sqlCommand.Parameters.AddWithValue("@fax", changeText_faxT);
            sqlCommand.Parameters.AddWithValue("@email", changeText_emailT);
            sqlCommand.Parameters.AddWithValue("@link", changeText_linkT);
            sqlCommand.CommandText = sql;

            sqlCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('更新成功');</script>");
            GridView_arealist.EditIndex = -1;
            loadDealers();
        }

        protected void GridView_arealist_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int boardId = Convert.ToInt32(GridView_arealist.DataKeys[e.RowIndex].Value);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connectarealist"].ConnectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            string deleteSql = $"delete from dealers where Id = @boardId ";
            SqlCommand deleteCommand = new SqlCommand(deleteSql, connection);
            deleteCommand.Parameters.AddWithValue("@boardId", boardId);
            deleteCommand.ExecuteNonQuery();

            connection.Close();

            Response.Write("<script>alert('刪除成功');</script>");

            loadDealers();
        }

        protected void GridView_arealist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_arealist.EditIndex = -1;
            loadDealers();
        }

        //上傳會使用~用來檢所原本就有的縮圖路徑，記得更改資料表抓取
        private string GetOriginalThumbnailPathFromDatabase(int boardId)
        {
            string originalThumbnailPath = "";

            // 使用与您的数据库配置相匹配的连接字符串，执行查询以检索缩略图路径
            string connectionString = WebConfigurationManager.ConnectionStrings["Connectarealist"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT dealerImgPath FROM dealers WHERE Id = @Id";
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
    }
}