<%@ Page Title="" Language="C#" MasterPageFile="~/Newoverview.Master" AutoEventWireup="true" CodeBehind="yachtOverviewFront.aspx.cs" Inherits="yacht.yachtOverviewFront" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box1">
        <asp:Literal ID="Literal_overviewContentHtml" runat="server"></asp:Literal>
    </div>
    <div class="box3" id="dimensionTable" runat="server">
        <asp:Literal ID="Literal_dimensionTitle" runat="server"></asp:Literal>
        <asp:Literal ID="Literal_Dimension" runat="server"></asp:Literal>
        <asp:Literal ID="Literal_overviewDimensionsImgPath" runat="server"></asp:Literal>
        
        <%--<img src='<%# GetRelativeImagePath(Eval("overviewDimensionsImgPath").ToString()) %>' class="image0" alt="" style="height: 59px;" />--%>
        <%--<table class="table02">
            <tbody>
                <tr>
                    <td class="table02td01">
                        <table>
                            <tbody>
                                <asp:Literal ID="Literal_Dimension" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </td>
                    <asp:Literal ID="Literal_overviewDimensionsImgPath" runat="server"></asp:Literal>
                </tr>
            </tbody>
        </table>--%>
    </div>
    <%--<p class="topbuttom"><img src="image/top.gif" /></p>--%>
    <div id="divDownload" class="downloads" runat="server">
        <%--<p><img src="image/downloads.gif" alt="&quot;&quot;" /></p>--%>
        <ul>
            <li>
                <asp:Literal ID="Literal_overviewDownloadsFilePath" runat="server"></asp:Literal>
            </li>
        </ul>
    </div>
</asp:Content>
