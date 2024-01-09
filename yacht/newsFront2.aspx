﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsFront2.aspx.cs" Inherits="yacht.newsFront2" %>

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
    <form id="form1" runat="server">
        <div class="contain">
            <div class="sub">
                <p><a href="index.htm">Home</a></p>
            </div>
            <!--------------------------------選單開始---------------------------------------------------->
            <div class="menu">
                <ul>
                    <li class="menuli01"><a href="#">Yachts</a></li>
                    <li class="menuli02"><a href="#">NEWS</a></li>
                    <li class="menuli03"><a href="https://localhost:44310/conpanyFront">COMPANY</a></li>
                    <li class="menuli04"><a href="https://localhost:44310/dealersFront">DEALERS</a></li>
                    <li class="menuli05"><a href="/contactFront" class="button-link">CONTACT</a></li>
                </ul>
            </div>
            <!--------------------------------選單開始結束---------------------------------------------------->
            <div>
                <!--遮罩-->
                <!--<div class="bannermasks">
        <img src="images/banner02_masks.png" alt="&quot;&quot;" /></div>-->
                <!--遮罩結束-->

                <!--<div id="buttom01"><a href="#"><img src="images/buttom01.gif" alt="next" /></a></div>-->

                <!--小圖開始-->
                <!--<div class="bannerimg">
<ul>
<li> <a href="#"><div class="on"><p class="bannerimg_p"><img  src="images/pit003.jpg" alt="&quot;&quot;" /></p></div></a></li>
<li> <a href="#"><p class="bannerimg_p"><img src="images/pit003.jpg" alt="&quot;&quot;" width="300" /></p>
</a></li>
<li> <a href="#"><p class="bannerimg_p"><img src="images/pit003.jpg" alt="&quot;&quot;" /></p></a></li>
<li> <a href="#"><p class="bannerimg_p"><img src="images/pit003.jpg" alt="&quot;&quot;" /></p></a></li>
<li> <a href="#"><p class="bannerimg_p"><img src="images/pit003.jpg" alt="&quot;&quot;" /></p></a></li>
<li> <a href="#"><p class="bannerimg_p"><img src="images/pit003.jpg" alt="&quot;&quot;" /></p></a></li>
<li> <a href="#"><p class="bannerimg_p"><img src="images/pit003.jpg" alt="&quot;&quot;" /></p></a></li>
<li> <a href="#"><p class="bannerimg_p"><img src="images/pit003.jpg" alt="&quot;&quot;" /></p></a></li>
</ul>

<ul>
<li> <a class="on" href="#"><p class="bannerimg_p"><img  src="images/pit003.jpg" alt="&quot;&quot;" /></p></a></li>
<li> <p class="bannerimg_p"><a href="#"><img src="images/pit003.jpg" alt="&quot;&quot;" /></p></li>
<li> <a href="#"><p class="bannerimg_p"><img src="images/pit003.jpg" alt="&quot;&quot;" /></p></a></li>
<li> <a href="#"><p class="bannerimg_p"><img src="images/pit003.jpg" alt="&quot;&quot;" /></p></a></li>
<li> <a href="#"><p class="bannerimg_p"><img src="images/pit003.jpg" alt="&quot;&quot;" /></p></a></li>
<li> <a href="#"><p class="bannerimg_p"><img src="images/pit003.jpg" alt="&quot;&quot;" /></p></a></li>
<li> <a href="#"><p class="bannerimg_p"><img src="images/pit003.jpg" alt="&quot;&quot;" /></p></a></li>
<li> <a href="#"><p class="bannerimg_p"><img src="images/pit003.jpg" alt="&quot;&quot;" /></p></a></li>
</ul>


</div>-->
                <!--小圖結束-->


                <!--<div id="buttom02"> <a href="#"><img src="images/buttom02.gif" alt="next" /></a></div>-->

                <!--------------------------------換圖開始---------------------------------------------------->

                <div class="banner">
                    <ul>
                        <li>
                            <img src="images/newbanner.jpg" alt="Tayana Yachts" /></li>
                    </ul>

                </div>
                <!--------------------------------換圖結束---------------------------------------------------->




                <div class="conbg">
                    <!--------------------------------左邊選單開始---------------------------------------------------->
                    <div class="left">
                        <div class="left1">
                            <p><span>NEWS</span></p>
                            <ul>
                                <li><a href="https://localhost:44310/newsFront2">News & Events</a></li>
                            </ul>
                        </div>
                    </div>

                    <!--------------------------------左邊選單結束---------------------------------------------------->

                    <!--------------------------------右邊選單開始---------------------------------------------------->
                    <div id="crumb"><a href="#">Home</a> >> <a href="#">News </a>>> <a href="#"><span class="on1">News & Events</span></a></div>
                    <div class="right">
                        <div class="right1">
                            <div class="title"><span>News & Events</span></div>

                            <!--------------------------------內容開始---------------------------------------------------->

                            <div class="box2_list">
                                <ul>
                                    <asp:Literal ID="newList" runat="server"></asp:Literal>
                                </ul>
                                <div>
                                    <asp:Literal ID="litPage" runat="server"></asp:Literal>
                                </div>
                            </div>


                            <!--------------------------------內容結束------------------------------------------------------>
                        </div>
                    </div>

                    <!--------------------------------右邊選單結束---------------------------------------------------->
                </div>
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
