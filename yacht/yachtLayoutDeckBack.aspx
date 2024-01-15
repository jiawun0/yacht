<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="yachtLayoutDeckBack.aspx.cs" Inherits="yacht.yachtLayoutDeckBack" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <asp:Label ID="Label_HeaderTitle" runat="server" Text="遊艇 LayoutDeck 後台頁面"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Sidebar" runat="server">
    <!-- 抓取目前登入者的username -->
    <asp:Literal ID="Literal_name" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="title" runat="server">
    <asp:Label ID="Label_selyachtModel" runat="server" Text="選擇遊艇型號 :"></asp:Label>
    <br />
    <asp:DropDownList ID="DropDownList_yachtModel" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="yachtModel" DataValueField="Id" OnSelectedIndexChanged="DropDownList_yachtModel_SelectedIndexChanged" ></asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectLayoutDeck %>" SelectCommand="SELECT * FROM [Yachts]"></asp:SqlDataSource>
    <br />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="content" runat="server">
    <asp:Label ID="Label_LayoutDeck" runat="server" Text="<上傳遊艇 LayoutDeck 圖片>"></asp:Label>
    <br />
    <asp:FileUpload ID="FileUpload_LayoutDeck" runat="server" />
    <br />
    <asp:Button ID="Button_upLayoutDeckImgPath" runat="server" Text="上傳圖檔" class="btn btn-outline-primary btn-block mt-3" OnClick="Button_upLayoutDeckImgPath_Click" />
    <br />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="body" runat="server">
    <br />
    <asp:Label ID="Label_specificationContent" runat="server" Text="<遊艇 Specification 撰寫>"></asp:Label>
    <br />
    <CKEditor:CKEditorControl ID="CKEditorControl_specificationContent" runat="server" basepath="/Scripts/ckeditor/"
        toolbar="Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
        NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
        /
        Styles|Format|Font|FontSize
        TextColor|BGColor
        Link|Image"
        height="400px"></CKEditor:CKEditorControl>
    <asp:Label ID="UploadspecificationContent" runat="server" Visible="False" ForeColor="#009933" class="d-flex justify-content-center"></asp:Label>
    <asp:Button ID="UploadspecificationContentBtn" runat="server" Text="內文上傳" class="btn btn-outline-primary btn-block mt-3" OnClick="UploadspecificationContentBtn_Click" />
    <br />
</asp:Content>
