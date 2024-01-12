<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="yacht.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>TtayanaWorld</title>

    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.cycle.all.2.74.js"></script>
    <script type="text/javascript">


        $(function () {

            // 先取得 #abgne-block-20110111 , 必要參數及輪播間隔
            var $block = $('#abgne-block-20110111'),
                timrt, speed = 4000;


            // 幫 #abgne-block-20110111 .title ul li 加上 hover() 事件
            var $li = $('.title ul li', $block).hover(function () {
                // 當滑鼠移上時加上 .over 樣式
                $(this).addClass('over').siblings('.over').removeClass('over');
            }, function () {
                // 當滑鼠移出時移除 .over 樣式
                $(this).removeClass('over');
            }).click(function () {
                // 當滑鼠點擊時, 顯示相對應的 div.info
                // 並加上 .on 樣式

                $(this).addClass('on').siblings('.on').removeClass('on');
                $('#abgne-block-20110111 .bd .banner ul:eq(0)').children().hide().eq($(this).index()).fadeIn(1000);
            });

            // 幫 $block 加上 hover() 事件
            $block.hover(function () {
                // 當滑鼠移上時停止計時器
                clearTimeout(timer);
            }, function () {
                // 當滑鼠移出時啟動計時器
                timer = setTimeout(move, speed);
            });

            // 控制輪播
            function move() {
                var _index = $('.title ul li.on', $block).index();
                _index = (_index + 1) % $li.length;
                $li.eq(_index).click();

                timer = setTimeout(move, speed);
            }

            // 啟動計時器
            timer = setTimeout(move, speed);

        });


    </script>

    <link href="css/style01.css" rel="stylesheet" type="text/css" />
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
                <%--<!--遮罩-->
                <div class="bannermasks">
                    <img src="images/banner00_masks.png" alt="&quot;&quot;" /></div>--%>
                <!--遮罩結束-->

                <!--------------------------------換圖開始---------------------------------------------------->
                <div id="abgne-block-20110111">
                    <div class="bd">
                        <div class="banner" style='border-radius: 5px; height: 424px; width: 978px;'>
                            <ul>
                                <asp:Literal ID="Literal_banner" runat="server"></asp:Literal>
                            </ul>
                            <%--<ul>
                                <li class="info on"><a href="#">
                                    <img src="images/banner001b.jpg" /></a><!--文字開始--><div class="wordtitle">TAYANA <span>48</span><br />
                                        <p>SPECIFICATION SHEET</p>
                                    </div>
                                    <!--文字結束-->
                                </li>
                                <li class="info"><a href="#">
                                    <img src="images/banner002b.jpg" /></a><!--文字開始--><div class="wordtitle">TAYANA <span>54</span><br />
                                        <p>SPECIFICATION SHEET</p>
                                    </div>
                                    <!--文字結束-->
                                    <!--新船型開始  54型才出現其於隱藏 -->
                                    <div class="new">
                                        <img src="images/new01.png" alt="new" /></div>
                                    <!--新船型結束-->
                                </li>
                                <li class="info"><a href="#">
                                    <img src="images/banner003b.jpg" /></a><!--文字開始--><div class="wordtitle">TAYANA <span>37</span><br />
                                        <p>SPECIFICATION SHEET</p>
                                    </div>
                                    <!--文字結束-->
                                </li>
                                <li class="info"><a href="#">
                                    <img src="images/banner004b.jpg" /></a><!--文字開始--><div class="wordtitle">TAYANA <span>64</span><br />
                                        <p>SPECIFICATION SHEET</p>
                                    </div>
                                    <!--文字結束-->
                                </li>
                                <li class="info"><a href="#">
                                    <img src="images/banner005b.jpg" /></a><!--文字開始--><div class="wordtitle">TAYANA <span>58</span><br />
                                        <p>SPECIFICATION SHEET</p>
                                    </div>
                                    <!--文字結束-->
                                </li>
                                <li class="info"><a href="#">
                                    <img src="images/banner006b.jpg" /></a><!--文字開始--><div class="wordtitle">TAYANA <span>55</span><br />
                                        <p>SPECIFICATION SHEET</p>
                                    </div>
                                    <!--文字結束-->
                                </li>
                            </ul>--%>


                            <!--小圖開始-->
                            <div class="bannerimg title" <%--style="display: none"--%>>

                                <ul>
                                    <asp:Literal ID="Literal_bannerNum" runat="server"></asp:Literal>
                                </ul>
                                <%--<ul>
                                    <li class="on">
                                        <div>
                                            <p class="bannerimg_p">
                                                <img src="images/i001.jpg" alt="&quot;&quot;" />
                                            </p>
                                        </div>
                                    </li>
                                    <li>
                                        <div>
                                            <p class="bannerimg_p">
                                                <img src="images/i002.jpg" alt="&quot;&quot;" />
                                            </p>
                                        </div>
                                    </li>
                                    <li>
                                        <div>
                                            <p class="bannerimg_p">
                                                <img src="images/i003.jpg" alt="&quot;&quot;" />
                                            </p>
                                        </div>
                                    </li>
                                    <li>
                                        <div>
                                            <p class="bannerimg_p">
                                                <img src="images/i004.jpg" alt="&quot;&quot;" />
                                            </p>
                                        </div>
                                    </li>
                                    <li>
                                        <div>
                                            <p class="bannerimg_p">
                                                <img src="images/i005.jpg" alt="&quot;&quot;" />
                                            </p>
                                        </div>
                                    </li>
                                    <li>
                                        <div>
                                            <p class="bannerimg_p">
                                                <img src="images/i006.jpg" alt="&quot;&quot;" />
                                            </p>
                                        </div>
                                    </li>
                                </ul>--%>
                            </div>
                            <!--小圖結束-->
                        </div>
                    </div>
                </div>
                <!--------------------------------換圖結束---------------------------------------------------->


                <!--------------------------------最新消息---------------------------------------------------->
                <div class="news">
                    <div class="newstitle">
                        <p class="newstitlep1">
                            <img src="images/news.gif" alt="news" />
                        </p>
                        <p class="newstitlep2"><a href="https://localhost:44310/newsFront2">More>></a></p>
                    </div>
                    <ul>
                        <asp:Literal ID="Literal_News" runat="server"></asp:Literal>
                    </ul>
                </div>
                <!--------------------------------最新消息結束---------------------------------------------------->



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
