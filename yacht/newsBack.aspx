<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="newsBack.aspx.cs" Inherits="yacht.newsBack" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <asp:Label ID="Label_HeaderTitle" runat="server" Text="新聞後台頁面"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Sidebar" runat="server">
    <!-- 抓取目前登入者的username -->
    <asp:Literal ID="Literal_name" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="title" runat="server">
    <asp:Label ID="Label_title" runat="server" Text="新聞"></asp:Label>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="content" runat="server">
    <asp:Label ID="Label_Date" runat="server" Text="選擇日期 :"></asp:Label>
    <asp:TextBox ID="TextBox_Date" runat="server" TextMode="Date" AutoPostBack="True"></asp:TextBox>
    <br />
    <asp:Label ID="Label_Headline" runat="server" Text="標題 :"></asp:Label>
    <asp:TextBox ID="TextBox_Headline" runat="server" type="text" class="form-control" Placeholder="請輸入標題" MaxLength="75"></asp:TextBox>
    <br />
    <asp:CheckBox ID="CheckBox_IsTop" runat="server" Text="置頂標籤" Width="100%"></asp:CheckBox>
    <br />
    <asp:Button ID="Button_addHeadline" runat="server" Text="新增新聞" class="btn btn-outline-primary btn-block mt-3" OnClick="Button_addHeadline_Click"/>
    <br />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="body" runat="server">
    <asp:DropDownList ID="DropDownList_Headline" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="dateTitle" DataValueField="Id" OnSelectedIndexChanged="DropDownList_Headline_SelectedIndexChanged"></asp:DropDownList>
    <br />
    <%--DataSourceID="SqlDataSource1"--%>
    <asp:DetailsView ID="DetailsView_news" runat="server" Height="50px" Width="125px" AutoGenerateRows="False" DataKeyNames="Id" >
        <Fields>
            <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
            <asp:BoundField DataField="dateTitle" HeaderText="dateTitle" SortExpression="dateTitle" />
            <asp:BoundField DataField="headline" HeaderText="headline" SortExpression="headline" />
            <asp:BoundField DataField="guid" HeaderText="guid" SortExpression="guid" />
            <asp:CheckBoxField DataField="isTop" HeaderText="isTop" SortExpression="isTop" />
            <asp:BoundField DataField="summary" HeaderText="summary" SortExpression="summary" />
            <asp:BoundField DataField="thumbnailPath" HeaderText="thumbnailPath" SortExpression="thumbnailPath" />
            <asp:BoundField DataField="newsContentHtml" HeaderText="newsContentHtml" SortExpression="newsContentHtml" />
            <asp:BoundField DataField="newsImageJson" HeaderText="newsImageJson" SortExpression="newsImageJson" />
            <asp:BoundField DataField="CreatDate" HeaderText="CreatDate" SortExpression="CreatDate" />
        </Fields>
</asp:DetailsView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Connectnews2 %>" DeleteCommand="DELETE FROM [news] WHERE [Id] = @Id" InsertCommand="INSERT INTO [news] ([dateTitle], [headline], [guid], [isTop], [summary], [thumbnailPath], [newsContentHtml], [newsImageJson], [CreatDate]) VALUES (@dateTitle, @headline, @guid, @isTop, @summary, @thumbnailPath, @newsContentHtml, @newsImageJson, @CreatDate)" SelectCommand="SELECT * FROM [news]" UpdateCommand="UPDATE [news] SET [dateTitle] = @dateTitle, [headline] = @headline, [guid] = @guid, [isTop] = @isTop, [summary] = @summary, [thumbnailPath] = @thumbnailPath, [newsContentHtml] = @newsContentHtml, [newsImageJson] = @newsImageJson, [CreatDate] = @CreatDate WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter DbType="Date" Name="dateTitle" />
            <asp:Parameter Name="headline" Type="String" />
            <asp:Parameter Name="guid" Type="String" />
            <asp:Parameter Name="isTop" Type="Boolean" />
            <asp:Parameter Name="summary" Type="String" />
            <asp:Parameter Name="thumbnailPath" Type="String" />
            <asp:Parameter Name="newsContentHtml" Type="String" />
            <asp:Parameter Name="newsImageJson" Type="String" />
            <asp:Parameter Name="CreatDate" Type="DateTime" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter DbType="Date" Name="dateTitle" />
            <asp:Parameter Name="headline" Type="String" />
            <asp:Parameter Name="guid" Type="String" />
            <asp:Parameter Name="isTop" Type="Boolean" />
            <asp:Parameter Name="summary" Type="String" />
            <asp:Parameter Name="thumbnailPath" Type="String" />
            <asp:Parameter Name="newsContentHtml" Type="String" />
            <asp:Parameter Name="newsImageJson" Type="String" />
            <asp:Parameter Name="CreatDate" Type="DateTime" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
</asp:SqlDataSource>
    </asp:Content>
