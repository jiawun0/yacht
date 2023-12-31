﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht
{
    public partial class conpanyCertificatFront : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadContentText();
            }
        }

        private void loadContentText()
        {
            //從資料庫取內文資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtConnectionString"].ConnectionString);
            string sqlCountry = "SELECT TOP 1 certificatHtml FROM company";
            SqlCommand command = new SqlCommand(sqlCountry, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                //渲染畫面
                Literal1.Text = HttpUtility.HtmlDecode(reader["certificatHtml"].ToString());
            }
            connection.Close();
        }
    }
}