using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yacht
{
    public partial class contactFront : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(Recaptcha1.Response))
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Captcha cannot be empty.";
            }
            else
            {
                var result = Recaptcha1.Verify();
                if (result.Success)
                {
                    //此處可加入"我不是機器人驗證"成功後要做的事

                }
                else
                {
                    lblMessage.Text = "Error(s): ";

                    foreach (var err in result.ErrorCodes)
                    {
                        lblMessage.Text = lblMessage.Text + err;
                    }
                }
            }
        }
    }
}