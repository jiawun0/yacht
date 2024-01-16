<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="conpanyBack.aspx.cs" Inherits="yacht.conpanyBack" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <asp:Label ID="Label_HeaderTitle" runat="server" Text="公司後台頁面"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Sidebar" runat="server">
    <!-- 抓取目前登入者的username -->
    <asp:Literal ID="Literal_name" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="title" runat="server">
    <asp:Label ID="Label_title" runat="server" Text="公司"></asp:Label>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="content" runat="server">
    <asp:Label ID="Label_AboutUs" runat="server" Text="About Us 內容編輯 :"></asp:Label>
    <ckeditor:ckeditorcontrol id="CKEditorControl_aboutUs" runat="server" basepath="/Scripts/ckeditor/"
        toolbar="Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
        NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
        /
        Styles|Format|Font|FontSize
        TextColor|BGColor
        Link|Image"
        height="400px">
    </ckeditor:ckeditorcontrol>
    <asp:Label ID="UploadAboutUsLab" runat="server" Visible="False" ForeColor="#009933" class="d-flex justify-content-center"></asp:Label>
    <asp:Button ID="UploadAboutUsBtn" runat="server" Text="上傳" class="btn btn-outline-primary btn-block mt-3" OnClick="UploadAboutUsBtn_Click" />
    <br />
    <asp:Label ID="Label_Certificat" runat="server" Text="Certificat 內容編輯 :"></asp:Label>
    <ckeditor:ckeditorcontrol id="CKEditorControl_Certificat" runat="server" basepath="/Scripts/ckeditor/"
        toolbar="Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
        NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
        /
        Styles|Format|Font|FontSize
        TextColor|BGColor
        Link|Image"
        height="400px"></ckeditor:ckeditorcontrol>
    <asp:Label ID="uploadCertificatLab" runat="server" Visible="False" ForeColor="#009933" class="d-flex justify-content-center"></asp:Label>
    <asp:Button ID="uploadCertificatBtn" runat="server" Text="確認上傳" class="btn btn-outline-primary btn-block mt-3" OnClick="uploadCertificatBtn_Click" />
    <br />

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" Visible="False">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id"></asp:BoundField>
            <asp:BoundField DataField="aboutUsHtml" HeaderText="aboutUsHtml" SortExpression="aboutUsHtml"></asp:BoundField>
            <asp:BoundField DataField="certificatHtml" HeaderText="certificatHtml" SortExpression="certificatHtml"></asp:BoundField>
            <asp:BoundField DataField="certificatVerticalImgJSON" HeaderText="certificatVerticalImgJSON" SortExpression="certificatVerticalImgJSON"></asp:BoundField>
            <asp:BoundField DataField="certificatHorizontalImgJSON" HeaderText="certificatHorizontalImgJSON" SortExpression="certificatHorizontalImgJSON"></asp:BoundField>
            <asp:BoundField DataField="CreatDate" HeaderText="CreatDate" SortExpression="CreatDate"></asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:YachtConnectionString %>" SelectCommand="SELECT * FROM [company]"></asp:SqlDataSource>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="body" runat="server">
</asp:Content>

<%--<asp:Label ID="Label_Certificat1" runat="server" Text="Certificat 內容 :"></asp:Label>
    <asp:TextBox ID="certificatTbox" runat="server" type="text" class="form-control" placeholder="Enter certificat text" TextMode="MultiLine" Height="200px"></asp:TextBox>
    <asp:Label ID="uploadCertificatLab" runat="server" Visible="False" ForeColor="#009933" class="d-flex justify-content-center"></asp:Label>
    <asp:Button ID="uploadCertificatBtn" runat="server" Text="確認上傳" class="btn btn-outline-primary btn-block mt-3" OnClick="uploadCertificatBtn_Click" />
    <br />
    <asp:Label ID="Label_Certificat2" runat="server" Text="Certificat 圖片 :"></asp:Label>
    <asp:FileUpload ID="imageUploadV" runat="server" class="btn btn-outline-primary btn-block" AllowMultiple="True" />
    <asp:Button ID="UploadVBtn" runat="server" Text="確認上傳" class="btn btn-primary" OnClick="UploadVBtn_Click" />
    <br />--%>
