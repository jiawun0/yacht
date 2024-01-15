﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="yachtOverviewFront2.aspx.cs" Inherits="yacht.yachtOverviewFront2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/jquery.ad-gallery.css">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.ad-gallery.js"></script>
    <script type="text/javascript">
        $(function () {

            var galleries = $('.ad-gallery').adGallery();
            galleries[0].settings.effect = 'fade';
            if ($('.banner1 input[type=hidden]').val() == "0") {
                $(".bannermasks").hide();
                $(".banner1").hide();
                $("#crumb1").css("top", "125px");
            }
        });
    </script>

    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link href="css/homestyle2.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.ad-gallery" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="contain">
            <div class="sub">
                <p><a href="/index">Home</a></p>
            </div>
            <!--------------------------------選單開始---------------------------------------------------->
            <div class="menu">
                <ul>
                    <li class="menuli01"><a href="/yachtOverviewFront">
                        <img src="images/mmmmeeeee.gif" alt="" />Yachts</a></li>
                    <li class="menuli02"><a href="/newsFront2">
                        <img src="images/mmmmeeeee.gif" alt="" />NEWS</a></li>
                    <li class="menuli03"><a href="/conpanyFront">
                        <img src="images/mmmmeeeee.gif" alt="" />COMPANY</a></li>
                    <li class="menuli04"><a href="/dealersFront">
                        <img src="images/mmmmeeeee.gif" alt="" />DEALERS</a></li>
                    <li class="menuli05"><a href="/contactFront">
                        <img src="images/mmmmeeeee.gif" alt="" />CONTACT</a></li>
                </ul>
            </div>
            <!--------------------------------選單開始結束---------------------------------------------------->
            <div>
                <%--<!--遮罩-->
                <div class="bannermasks">
                    <img src="images/banner01_masks.png" alt="&quot;&quot;" /></div>--%>
                <!--遮罩結束-->

                <div class="banner1">
                    <input type="hidden" name="HiddenField1" id="Gallery1_HiddenField1" value="1" />
                    <div id="gallery" class="ad-gallery">
                        <div class="ad-image-wrapper">
                        </div>
                        <div class="ad-controls">
                        </div>
                        <div class="ad-nav">
                            <div class="ad-thumbs">
                                <ul class="ad-thumb-list">
                                    <asp:Repeater ID="Repeater_photo" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <a href='<%#Eval("PhotoPath")%>'>
                                                    <img src='<%#Eval("PhotoPath")%>' class="image0" alt="" height="59px" />
                                                </a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                   
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="conbg">
                    <!--------------------------------左邊選單開始---------------------------------------------------->
                    <div class="left">
                        <div class="left1">
                            <p><span>YACHTS</span></p>
                            <ul>
                                <asp:Literal ID="LeftMenuHtml" runat="server"></asp:Literal>
                                <%--<li><a href="#">Dynasty 72</a></li>
                                <li><a href="#">Tayana 64</a></li>
                                <li><a href="#">Tayana 58</a></li>
                                <li><a href="#">Tayana 55</a></li>--%>
                            </ul>
                        </div>
                    </div>

                    <!--------------------------------左邊選單結束---------------------------------------------------->

                    <!--------------------------------右邊選單開始---------------------------------------------------->
                    <div id="crumb1"><a href="/index">Home</a> >> <a href="#">Yachts</a> >> <a href="/yachtOverviewFront"><span class="on1" id="LabLink" runat="server">Tayana 37</span></a></div>
                    <div class="right">
                        <div class="right1">
                            <div class="title"><span id="LabTitle" runat="server">Tayana 37</span></div>

                            <!--------------------------------內容開始---------------------------------------------------->

                            <!--次選單-->
                            <div class="menu_y">
                                <ul>
                                    <li class="menu_y00">YACHTS</li>
                                    <asp:Literal ID="TopMenuHtml" runat="server"></asp:Literal>
                                    <%--<li><a class="menu_yli01" href="#">Interior</a></li>
                                    <li><a class="menu_yli02" href="#">Layout & deck pla</a>n</li>
                                    <li><a class="menu_yli03" href="#">Specification</a></li>--%>
                                </ul>
                            </div>
                            <!--次選單-->
                            


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
