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
    <asp:Button ID="Button_addHeadline" runat="server" Text="新增標題" class="btn btn-outline-primary btn-block mt-3" OnClick="Button_addHeadline_Click"/>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="body" runat="server">
    <%--DataSourceID="SqlDataSource1"--%>
    <asp:ListView ID="ListView_news" runat="server" DataKeyNames="Id" > 
        <AlternatingItemTemplate>
            <li style="">Id:
                <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                <br />
                dateTitle:
                <asp:Label ID="dateTitleLabel" runat="server" Text='<%# Eval("dateTitle") %>' />
                <br />
                headline:
                <asp:Label ID="headlineLabel" runat="server" Text='<%# Eval("headline") %>' />
                <br />
                guid:
                <asp:Label ID="guidLabel" runat="server" Text='<%# Eval("guid") %>' />
                <br />
                <asp:CheckBox ID="isTopCheckBox" runat="server" Checked='<%# Eval("isTop") %>' Enabled="false" Text="isTop" />
                <br />
                summary:
                <asp:Label ID="summaryLabel" runat="server" Text='<%# Eval("summary") %>' />
                <br />
                thumbnailPath:
                <asp:Label ID="thumbnailPathLabel" runat="server" Text='<%# Eval("thumbnailPath") %>' />
                <br />
                newsContentHtml:
                <asp:Label ID="newsContentHtmlLabel" runat="server" Text='<%# Eval("newsContentHtml") %>' />
                <br />
                newsImageJson:
                <asp:Label ID="newsImageJsonLabel" runat="server" Text='<%# Eval("newsImageJson") %>' />
                <br />
                CreatDate:
                <asp:Label ID="CreatDateLabel" runat="server" Text='<%# Eval("CreatDate") %>' />
                <br />
            </li>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <li style="">Id:
                <asp:Label ID="IdLabel1" runat="server" Text='<%# Eval("Id") %>' />
                <br />
                dateTitle:
                <asp:TextBox ID="dateTitleTextBox" runat="server" Text='<%# Bind("dateTitle") %>' />
                <br />
                headline:
                <asp:TextBox ID="headlineTextBox" runat="server" Text='<%# Bind("headline") %>' />
                <br />
                guid:
                <asp:TextBox ID="guidTextBox" runat="server" Text='<%# Bind("guid") %>' />
                <br />
                <asp:CheckBox ID="isTopCheckBox" runat="server" Checked='<%# Bind("isTop") %>' Text="isTop" />
                <br />
                summary:
                <asp:TextBox ID="summaryTextBox" runat="server" Text='<%# Bind("summary") %>' />
                <br />
                thumbnailPath:
                <asp:TextBox ID="thumbnailPathTextBox" runat="server" Text='<%# Bind("thumbnailPath") %>' />
                <br />
                newsContentHtml:
                <asp:TextBox ID="newsContentHtmlTextBox" runat="server" Text='<%# Bind("newsContentHtml") %>' />
                <br />
                newsImageJson:
                <asp:TextBox ID="newsImageJsonTextBox" runat="server" Text='<%# Bind("newsImageJson") %>' />
                <br />
                CreatDate:
                <asp:TextBox ID="CreatDateTextBox" runat="server" Text='<%# Bind("CreatDate") %>' />
                <br />
                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="更新" />
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="取消" />
            </li>
        </EditItemTemplate>
        <EmptyDataTemplate>
            未傳回資料。
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <li style="">dateTitle:
                <asp:TextBox ID="dateTitleTextBox" runat="server" Text='<%# Bind("dateTitle") %>' />
                <br />headline:
                <asp:TextBox ID="headlineTextBox" runat="server" Text='<%# Bind("headline") %>' />
                <br />guid:
                <asp:TextBox ID="guidTextBox" runat="server" Text='<%# Bind("guid") %>' />
                <br />
                <asp:CheckBox ID="isTopCheckBox" runat="server" Checked='<%# Bind("isTop") %>' Text="isTop" />
                <br />summary:
                <asp:TextBox ID="summaryTextBox" runat="server" Text='<%# Bind("summary") %>' />
                <br />thumbnailPath:
                <asp:TextBox ID="thumbnailPathTextBox" runat="server" Text='<%# Bind("thumbnailPath") %>' />
                <br />newsContentHtml:
                <asp:TextBox ID="newsContentHtmlTextBox" runat="server" Text='<%# Bind("newsContentHtml") %>' />
                <br />newsImageJson:
                <asp:TextBox ID="newsImageJsonTextBox" runat="server" Text='<%# Bind("newsImageJson") %>' />
                <br />CreatDate:
                <asp:TextBox ID="CreatDateTextBox" runat="server" Text='<%# Bind("CreatDate") %>' />
                <br />
                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="插入" />
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="清除" />
            </li>
        </InsertItemTemplate>
        <itemseparatortemplate>
<br />
        </itemseparatortemplate>
        <ItemTemplate>
            <li style="">Id:
                <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                <br />
                dateTitle:
                <asp:Label ID="dateTitleLabel" runat="server" Text='<%# Eval("dateTitle") %>' />
                <br />
                headline:
                <asp:Label ID="headlineLabel" runat="server" Text='<%# Eval("headline") %>' />
                <br />
                guid:
                <asp:Label ID="guidLabel" runat="server" Text='<%# Eval("guid") %>' />
                <br />
                <asp:CheckBox ID="isTopCheckBox" runat="server" Checked='<%# Eval("isTop") %>' Enabled="false" Text="isTop" />
                <br />
                summary:
                <asp:Label ID="summaryLabel" runat="server" Text='<%# Eval("summary") %>' />
                <br />
                thumbnailPath:
                <asp:Label ID="thumbnailPathLabel" runat="server" Text='<%# Eval("thumbnailPath") %>' />
                <br />
                newsContentHtml:
                <asp:Label ID="newsContentHtmlLabel" runat="server" Text='<%# Eval("newsContentHtml") %>' />
                <br />
                newsImageJson:
                <asp:Label ID="newsImageJsonLabel" runat="server" Text='<%# Eval("newsImageJson") %>' />
                <br />
                CreatDate:
                <asp:Label ID="CreatDateLabel" runat="server" Text='<%# Eval("CreatDate") %>' />
                <br />
            </li>
        </ItemTemplate>
        <LayoutTemplate>
            <ul id="itemPlaceholderContainer" runat="server" style="">
                <li runat="server" id="itemPlaceholder" />
            </ul>
            <div style="">
            </div>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <li style="">Id:
                <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                <br />
                dateTitle:
                <asp:Label ID="dateTitleLabel" runat="server" Text='<%# Eval("dateTitle") %>' />
                <br />
                headline:
                <asp:Label ID="headlineLabel" runat="server" Text='<%# Eval("headline") %>' />
                <br />
                guid:
                <asp:Label ID="guidLabel" runat="server" Text='<%# Eval("guid") %>' />
                <br />
                <asp:CheckBox ID="isTopCheckBox" runat="server" Checked='<%# Eval("isTop") %>' Enabled="false" Text="isTop" />
                <br />
                summary:
                <asp:Label ID="summaryLabel" runat="server" Text='<%# Eval("summary") %>' />
                <br />
                thumbnailPath:
                <asp:Label ID="thumbnailPathLabel" runat="server" Text='<%# Eval("thumbnailPath") %>' />
                <br />
                newsContentHtml:
                <asp:Label ID="newsContentHtmlLabel" runat="server" Text='<%# Eval("newsContentHtml") %>' />
                <br />
                newsImageJson:
                <asp:Label ID="newsImageJsonLabel" runat="server" Text='<%# Eval("newsImageJson") %>' />
                <br />
                CreatDate:
                <asp:Label ID="CreatDateLabel" runat="server" Text='<%# Eval("CreatDate") %>' />
                <br />
            </li>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Connectnews %>" DeleteCommand="DELETE FROM [news] WHERE [Id] = @Id" InsertCommand="INSERT INTO [news] ([dateTitle], [headline], [guid], [isTop], [summary], [thumbnailPath], [newsContentHtml], [newsImageJson], [CreatDate]) VALUES (@dateTitle, @headline, @guid, @isTop, @summary, @thumbnailPath, @newsContentHtml, @newsImageJson, @CreatDate)" SelectCommand="SELECT * FROM [news]" UpdateCommand="UPDATE [news] SET [dateTitle] = @dateTitle, [headline] = @headline, [guid] = @guid, [isTop] = @isTop, [summary] = @summary, [thumbnailPath] = @thumbnailPath, [newsContentHtml] = @newsContentHtml, [newsImageJson] = @newsImageJson, [CreatDate] = @CreatDate WHERE [Id] = @Id">
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
