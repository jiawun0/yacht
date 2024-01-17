<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="newsBack.aspx.cs" Inherits="yacht.newsBack" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

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
    <asp:Label ID="Label_Date" runat="server" Text="* 選擇日期 :"></asp:Label>
    <asp:TextBox ID="TextBox_Date" runat="server" TextMode="Date" AutoPostBack="True" OnTextChanged="TextBox_Date_TextChanged"></asp:TextBox>
    <br />
    <asp:Label ID="Label_Headline" runat="server" Text="* 標題 :"></asp:Label>
    <asp:TextBox ID="TextBox_Headline" runat="server" type="text" class="form-control" Placeholder="請輸入標題" MaxLength="75"></asp:TextBox>
    <br />
    <asp:CheckBox ID="CheckBox_IsTop" runat="server" Text="置頂標籤" Width="100%"></asp:CheckBox>
    <br />
    <asp:Label ID="Label_summary" runat="server" Text="摘要 :"></asp:Label>
    <asp:TextBox ID="TextBox_summary" runat="server" type="text" class="form-control" Placeholder="請輸入摘要" MaxLength="75"></asp:TextBox>
    <br />
    <asp:Label ID="Label_thumbnailPath" runat="server" Text="縮圖 :"></asp:Label>
    <asp:FileUpload ID="FileUpload_thumbnailPath" runat="server" />
    <asp:Button ID="Button_addHeadline" runat="server" Text="新增新聞" class="btn btn-outline-primary btn-block mt-3" OnClick="Button_addHeadline_Click" />
    <br />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="body" runat="server">
    <asp:Label ID="Label_selectedone" runat="server" Text="<新聞編輯>"></asp:Label>
    <br />
    <%--<asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="auto-style1" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="headline" DataValueField="Id" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"></asp:RadioButtonList>--%>
    <%--DataSourceID="SqlDataSource1"--%>
    <asp:Label ID="Label_selectdate" runat="server" Text="選擇標題 :"></asp:Label>
    <asp:DropDownList ID="DropDownList_Headline" runat="server" AutoPostBack="True" DataTextField="headline" DataValueField="Id" OnSelectedIndexChanged="DropDownList_Headline_SelectedIndexChanged1"></asp:DropDownList>

    <asp:DetailsView ID="DetailsView_news" runat="server" Height="100px" Width="200px" BorderWidth="1" BorderStyle="Solid" AutoGenerateRows="False" DataKeyNames="Id" OnItemDeleting="DetailsView_news_ItemDeleting" OnItemUpdating="DetailsView_news_ItemUpdating" OnModeChanging="DetailsView_news_ModeChanging" OnItemCommand="DetailsView_news_ItemCommand">
        <Fields>
            <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
            <asp:TemplateField HeaderText="dateTitle" SortExpression="dateTitle">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox_dateTitleT" runat="server" Text='<%# Bind("dateTitle") %>' TextMode="Date"></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox_dateTitleTT" runat="server" Text='<%# Bind("dateTitle") %>' TextMode="Date"></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label_dateTitleT" runat="server" Text='<%# Bind("dateTitle") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="headline" SortExpression="headline">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox_headlineT" runat="server" Text='<%# Bind("headline") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox_headlineTT" runat="server" Text='<%# Bind("headline") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label_headlineT" runat="server" Text='<%# Bind("headline") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField DataField="guid" HeaderText="guid" SortExpression="guid" ReadOnly="True" />--%>
            <asp:TemplateField HeaderText="isTop" SortExpression="isTop">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox_isTopT" runat="server" Checked='<%# Bind("isTop") %>'></asp:CheckBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:CheckBox ID="CheckBox_isTopTT" runat="server" Checked='<%# Bind("isTop") %>'></asp:CheckBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox_isTopT" runat="server" Checked='<%# Bind("isTop") %>' Enabled="false"></asp:CheckBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="summary" SortExpression="summary">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox_summaryT" runat="server" Text='<%# Bind("summary") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox_summaryTT" runat="server" Text='<%# Bind("summary") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label_summaryT" runat="server" Text='<%# Bind("summary") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="thumbnailPath" SortExpression="thumbnailPath">
                <EditItemTemplate>
                    <asp:FileUpload ID="FileUpload_thumbnailPathT" runat="server"></asp:FileUpload>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox_thumbnailPathTT" runat="server" Text='<%# Bind("thumbnailPath") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label_thumbnailPathT" runat="server" Text='<%# Bind("thumbnailPath") %>' Style="max-width: 50px;"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <%--            <asp:TemplateField HeaderText="newsContentHtml" SortExpression="newsContentHtml">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox_newsContentHtmlT" runat="server" Text='<%# Bind("newsContentHtml") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox_newsContentHtmlT" runat="server" Text='<%# Bind("newsContentHtml") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label_newsContentHtmlT" runat="server" Text='<%# Bind("newsContentHtml") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <%--<asp:BoundField DataField="newsContentHtml" HeaderText="newsContentHtml" SortExpression="newsContentHtml" ReadOnly="True" />
            <asp:BoundField DataField="newsImageJson" HeaderText="newsImageJson" SortExpression="newsImageJson" ReadOnly="True"/>--%>
            <asp:BoundField DataField="CreatDate" HeaderText="CreatDate" SortExpression="CreatDate" ReadOnly="True" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True"></asp:CommandField>
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
    <br />
    <asp:Label ID="Label_newsContent" runat="server" Text="<新聞稿撰寫>"></asp:Label>
    <br />
    <ckeditor:ckeditorcontrol id="CKEditorControl_newsContent" runat="server" basepath="/Scripts/ckeditor/"
        toolbar="Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
        NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
        /
        Styles|Format|Font|FontSize
        TextColor|BGColor
        Link|Image"
        height="400px">
    </ckeditor:ckeditorcontrol>
    <asp:Label ID="UploadnewsContent" runat="server" Visible="False" ForeColor="#009933" class="d-flex justify-content-center"></asp:Label>
    <asp:Button ID="UploadnewsContentBtn" runat="server" Text="新聞稿上傳" class="btn btn-outline-primary btn-block mt-3" OnClick="UploadnewsContentBtn_Click" />
    <br />
</asp:Content>
