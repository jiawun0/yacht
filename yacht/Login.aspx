<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="yacht.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <asp:Label ID="Label_HeaderTitle" runat="server" Text="後台登入頁面"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Sidebar" runat="server">
    <!-- 抓取目前登入者的username -->
    <asp:Literal ID="Literal_name" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="title" runat="server">
    <asp:Label ID="Label_title" runat="server" Text="登入知道大小事"></asp:Label>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="content" runat="server">
    <asp:Label ID="Label_content" runat="server" Text="請填寫資料"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label_account" runat="server" Text="帳號:"></asp:Label>
    <asp:TextBox ID="TextBox_account" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="Label_password" runat="server" Text="密碼:"></asp:Label>
    <asp:TextBox ID="TextBox_password" runat="server" TextMode="Password"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button_login" runat="server" Text="登入" Style="height: 32px" OnClick="Button_login_Click" />
    <asp:Label ID="Label4" runat="server"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
            <asp:BoundField DataField="account" HeaderText="account" SortExpression="account" />
            <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
            <asp:CheckBoxField DataField="isManger" HeaderText="isManger" SortExpression="isManger" />
            <asp:BoundField DataField="CreatDate" HeaderText="CreatDate" SortExpression="CreatDate" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectYachtLogin2 %>" SelectCommand="SELECT * FROM [Login]"></asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="body" runat="server">
    </asp:Content>

