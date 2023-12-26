<%@ Page Title="" Language="C#" MasterPageFile="~/New.Master" AutoEventWireup="true" CodeBehind="contactFront.aspx.cs" Inherits="yacht.contactFront" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<!--遮罩-->
    <div class="bannermasks">
        <img src="images/contact.jpg" alt="&quot;&quot;" width="967" height="371" /></div>
    <!--遮罩結束-->--%>

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
                <p><span>CONTACT</span></p>
                <ul>
                    <li><a href="#">contacts</a></li>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb"><a href="#">Home</a> >> <a href="#"><span class="on1">Contact</span></a></div>
        <div class="right">
            <div class="right1">
                <div class="title"><span>Contact</span></div>

                <!--------------------------------內容開始---------------------------------------------------->
                <!--表單-->
                <div class="from01">
                    <p>
                        Please Enter your contact information<span class="span01">*Required</span>
                    </p>
                    <br />
                    <table>
                        <tr>
                            <!--輸入姓名、Email、Phone-->
                            <td class="from01td01">Name :</td>
                            <td><span>*</span><input type="text" name="textfield" id="textfield" />
                            </td>
                        </tr>
                        <tr>
                            <td class="from01td01">Email :</td>
                            <td><span>*</span><input type="text" name="textfield" id="textfield" /></td>
                        </tr>
                        <tr>
                            <td class="from01td01">Phone :</td>
                            <td><span>*</span><input type="text" name="textfield" id="textfield" /></td>
                        </tr>
                        <tr>
                            <td class="from01td01">Country :</td>
                            <td><span>*</span>
                                <select name="select" id="select">
                                    <option>Annapolis</option>
                                </select></td>
                        </tr>
                        <tr>
                            <td colspan="2"><span>*</span>Brochure of interest  *Which Brochure would you like to view?</td>
                        </tr>
                        <tr>
                            <td class="from01td01">&nbsp;</td>
                            <td>
                                <select name="select" id="select">
                                    <option>Dynasty 72 </option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="from01td01">Comments:</td>
                            <td>
                                <textarea name="textarea" id="textarea" cols="45" rows="5"></textarea></td>
                        </tr>
                        <tr>
                            <td class="from01td01">&nbsp;</td>
                            <td class="f_right"><a href="#">
                                <img src="images/buttom03.gif" alt="submit" width="59" height="25" /></a></td>
                        </tr>
                    </table>
                </div>
                <!--表單-->

                <div class="box1">
                    <span class="span02">Contact with us</span><br />
                    Thanks for your enjoying our web site as an introduction to the Tayana world and our range of yachts.
As all the designs in our range are semi-custom built, we are glad to offer a personal service to all our potential customers. 
If you have any questions about our yachts or would like to take your interest a stage further, please feel free to contact us.
                </div>

                <div class="list03">
                    <p>
                        <span>TAYANA HEAD OFFICE</span><br />
                        NO.60 Haichien Rd. Chungmen Village Linyuan Kaohsiung Hsien 832 Taiwan R.O.C<br />
                        tel. +886(7)641 2422<br />
                        fax. +886(7)642 3193<br />
                        info@tayanaworld.com<br />
                    </p>
                </div>


                <div class="list03">
                    <p>
                        <span>SALES DEPT.</span><br />
                        +886(7)641 2422  ATTEN. Mr.Basil Lin<br />
                        <br />
                    </p>
                </div>

                <div class="box4">
                    <h4>Location</h4>
                    <p>
                        <iframe width="695" height="518" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3683.83084638078!2d120.29628881502028!3d22.63007423831491!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x346e0528d8c86d63%3A0x39c39df5b1c20474!2s56%2C%20Minsheng%201st%20Rd%2C%20Xinxing%20District%2C%20Kaohsiung%20City%2C%20800!5e0!3m2!1sen!2stw!4v1607358266457!5m2!1sen!2stw"></iframe>
                    </p>

                </div>

                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>

        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>
