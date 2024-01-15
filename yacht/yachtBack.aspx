<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="yachtBack.aspx.cs" Inherits="yacht.yachtBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <asp:Label ID="Label_HeaderTitle" runat="server" Text="遊艇 Model & Photo 後台頁面"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Sidebar" runat="server">
    <!-- 抓取目前登入者的username -->
    <asp:Literal ID="Literal_name" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="content" runat="server">
    <asp:Label ID="Label_title" runat="server" Text="<上傳遊艇>"></asp:Label>
    <br />
    <asp:Label ID="Label_yachtModel" runat="server" Text="遊艇型號 :"></asp:Label>
    <asp:TextBox ID="TextBox_yachtModel" runat="server" type="text" class="form-control" Placeholder="請輸入型號" MaxLength="75"></asp:TextBox>
    <br />
    <asp:CheckBox ID="CheckBox_isNewDesign" runat="server" Text="新設計" Width="100%"></asp:CheckBox>
    <br />
    <asp:CheckBox ID="CheckBox_isNewBuilding" runat="server" Text="新建製" Width="100%"></asp:CheckBox>
    <br />
    <asp:Button ID="Button_addyachtModel" runat="server" Text="新增遊艇" class="btn btn-outline-primary btn-block mt-3" OnClick="Button_addyachtModel_Click" />
    <br />
    <br />
    <asp:Label ID="Label_edit" runat="server" Text="<編輯遊艇>"></asp:Label>
    <br />
    <asp:GridView ID="GridView_Yachts" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCancelingEdit="GridView_Yachts_RowCancelingEdit" OnRowDeleting="GridView_Yachts_RowDeleting" OnRowEditing="GridView_Yachts_RowEditing" OnRowUpdating="GridView_Yachts_RowUpdating">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id"></asp:BoundField>
            <asp:TemplateField HeaderText="yachtModel" SortExpression="yachtModel">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox_yachtModelT" runat="server" Text='<%# Bind("yachtModel") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label_yachtModelT" runat="server" Text='<%# Bind("yachtModel") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="isNewDesign" SortExpression="isNewDesign">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox_isNewDesignT" runat="server" Checked='<%# Bind("isNewDesign") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox_isNewDesignT" runat="server" Checked='<%# Bind("isNewDesign") %>' Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="isNewBuilding" SortExpression="isNewBuilding">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox_isNewBuildingT" runat="server" Checked='<%# Bind("isNewBuilding") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox_isNewBuildingT" runat="server" Checked='<%# Bind("isNewBuilding") %>' Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreatDate" HeaderText="CreatDate" SortExpression="CreatDate"></asp:BoundField>
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True"></asp:CommandField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectYachtall %>" DeleteCommand="DELETE FROM [Yachts] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Yachts] ([yachtModel], [isNewDesign], [isNewBuilding], [guid], [bannerImgPathJSON], [overviewContentHtml], [overviewDimensionsImgPath], [overviewDownloadsFilePath], [overviewDimensionsJSON], [layoutDeckPlanImgPathJSON], [CreatDate]) VALUES (@yachtModel, @isNewDesign, @isNewBuilding, @guid, @bannerImgPathJSON, @overviewContentHtml, @overviewDimensionsImgPath, @overviewDownloadsFilePath, @overviewDimensionsJSON, @layoutDeckPlanImgPathJSON, @CreatDate)" SelectCommand="SELECT * FROM [Yachts]" UpdateCommand="UPDATE [Yachts] SET [yachtModel] = @yachtModel, [isNewDesign] = @isNewDesign, [isNewBuilding] = @isNewBuilding, [guid] = @guid, [bannerImgPathJSON] = @bannerImgPathJSON, [overviewContentHtml] = @overviewContentHtml, [overviewDimensionsImgPath] = @overviewDimensionsImgPath, [overviewDownloadsFilePath] = @overviewDownloadsFilePath, [overviewDimensionsJSON] = @overviewDimensionsJSON, [layoutDeckPlanImgPathJSON] = @layoutDeckPlanImgPathJSON, [CreatDate] = @CreatDate WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="yachtModel" Type="String" />
            <asp:Parameter Name="isNewDesign" Type="Boolean" />
            <asp:Parameter Name="isNewBuilding" Type="Boolean" />
            <asp:Parameter Name="guid" Type="String" />
            <asp:Parameter Name="bannerImgPathJSON" Type="String" />
            <asp:Parameter Name="overviewContentHtml" Type="String" />
            <asp:Parameter Name="overviewDimensionsImgPath" Type="String" />
            <asp:Parameter Name="overviewDownloadsFilePath" Type="String" />
            <asp:Parameter Name="overviewDimensionsJSON" Type="String" />
            <asp:Parameter Name="layoutDeckPlanImgPathJSON" Type="String" />
            <asp:Parameter Name="CreatDate" Type="DateTime" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="yachtModel" Type="String" />
            <asp:Parameter Name="isNewDesign" Type="Boolean" />
            <asp:Parameter Name="isNewBuilding" Type="Boolean" />
            <asp:Parameter Name="guid" Type="String" />
            <asp:Parameter Name="bannerImgPathJSON" Type="String" />
            <asp:Parameter Name="overviewContentHtml" Type="String" />
            <asp:Parameter Name="overviewDimensionsImgPath" Type="String" />
            <asp:Parameter Name="overviewDownloadsFilePath" Type="String" />
            <asp:Parameter Name="overviewDimensionsJSON" Type="String" />
            <asp:Parameter Name="layoutDeckPlanImgPathJSON" Type="String" />
            <asp:Parameter Name="CreatDate" Type="DateTime" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="body" runat="server">
    <asp:Label ID="Label_yachtsPhoto" runat="server" Text="<上傳遊艇照片>"></asp:Label>
    <br />
    <asp:Label ID="Label_selyachtModel" runat="server" Text="選擇遊艇型號 :"></asp:Label>
    <br />
    <asp:DropDownList ID="DropDownList_yachtModel" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="yachtModel" DataValueField="Id" OnSelectedIndexChanged="DropDownList_yachtModel_SelectedIndexChanged"></asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectYachts %>" SelectCommand="SELECT * FROM [Yachts]"></asp:SqlDataSource>
    <br />
    <asp:Label ID="Label_YachtsPhotoup" runat="server" Text="照片上傳 :"></asp:Label>
    <br />
    <asp:FileUpload ID="FileUpload_YachtsPhoto" runat="server" />
    <br />
    <asp:Button ID="Button_AddYachtsPhoto" runat="server" Text="新增照片" class="btn btn-outline-primary btn-block mt-3" OnClick="Button_AddYachtsPhoto_Click" />
    <br />
    <asp:Label ID="Label_PhotoList" runat="server" Text="照片列表 :"></asp:Label>
    <br />
    <asp:GridView ID="GridView_YachtsPhoto" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowDeleting="GridView_YachtsPhoto_RowDeleting">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id"></asp:BoundField>
            <asp:BoundField DataField="PhotoPath" HeaderText="PhotoPath" SortExpression="PhotoPath"></asp:BoundField>
            <asp:BoundField DataField="CreatTime" HeaderText="CreatTime" SortExpression="CreatTime"></asp:BoundField>
            <asp:BoundField DataField="YachtsId" HeaderText="YachtsId" SortExpression="YachtsId"></asp:BoundField>
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True"></asp:CommandField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectYachtsPhoto %>" DeleteCommand="DELETE FROM [YachtsPhoto] WHERE [Id] = @Id" InsertCommand="INSERT INTO [YachtsPhoto] ([PhotoName], [PhotoDescription], [PhotoPath], [IsCover], [CreatTime], [YachtsId]) VALUES (@PhotoName, @PhotoDescription, @PhotoPath, @IsCover, @CreatTime, @YachtsId)" SelectCommand="SELECT * FROM [YachtsPhoto]" UpdateCommand="UPDATE [YachtsPhoto] SET [PhotoName] = @PhotoName, [PhotoDescription] = @PhotoDescription, [PhotoPath] = @PhotoPath, [IsCover] = @IsCover, [CreatTime] = @CreatTime, [YachtsId] = @YachtsId WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="PhotoName" Type="String" />
            <asp:Parameter Name="PhotoDescription" Type="String" />
            <asp:Parameter Name="PhotoPath" Type="String" />
            <asp:Parameter Name="IsCover" Type="Boolean" />
            <asp:Parameter Name="CreatTime" Type="DateTime" />
            <asp:Parameter Name="YachtsId" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="PhotoName" Type="String" />
            <asp:Parameter Name="PhotoDescription" Type="String" />
            <asp:Parameter Name="PhotoPath" Type="String" />
            <asp:Parameter Name="IsCover" Type="Boolean" />
            <asp:Parameter Name="CreatTime" Type="DateTime" />
            <asp:Parameter Name="YachtsId" Type="Int32" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    </asp:Content>
