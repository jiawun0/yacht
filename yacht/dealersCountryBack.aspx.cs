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
    }
}