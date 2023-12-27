﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contactEmail.aspx.cs" Inherits="yacht.contactEmail" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link href="css/homestyle2.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form runat="server" method="post" name="aspnetForm" id="aspnetForm">
        <div class="contain">
            <div class="sub">
                <p><a href="index.htm">Home</a></p>
            </div>
            <!--------------------------------選單開始---------------------------------------------------->
            <div class="menu">
                <ul>
                    <li class="menuli01"><a href="#">Yachts</a></li>
                    <li class="menuli02"><a href="#">NEWS</a></li>
                    <li class="menuli03"><a href="#">COMPANY</a></li>
                    <li class="menuli04"><a href="#">DEALERS</a></li>
                    <li class="menuli05"><a href="#">CONTACT</a></li>
                </ul>
            </div>
            <!--------------------------------選單開始結束---------------------------------------------------->
            <div>

                <!--------------------------------落款開始---------------------------------------------------->
                <div class="footer">

                    <div class="footerp00">
                        <a href="#">
                            <img src="images/tog.jpg" alt="&quot;&quot;" /></a>
                        <p class="footerp001">© 1973-2011 Tayana Yachts, Inc. All Rights Reserved</p>
                    </div>
                    <div class="footer01">
                        <span>No. 60, Hai Chien Road, Chung Men Li, Lin Yuan District, Kaohsiung City, Taiwan, R.O.C.</span><br />
                        <span>TEL：+886(7)641-2721</span> <span>FAX：+886(7)642-3193</span><span><a href="mailto:tayangco@ms15.hinet.net">E-mail：tayangco@ms15.hinet.net</a>.</span>
                    </div>
                </div>
                <!--------------------------------落款結束---------------------------------------------------->

            </div>
        </div>
        </div>
    </form>
</body>

</html>
