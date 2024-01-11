<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="yachtBack.aspx.cs" Inherits="yacht.yachtBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <asp:Label ID="Label_HeaderTitle" runat="server" Text="遊艇後台頁面"></asp:Label>
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
    <asp:Label ID="Label_AlbumPhoto" runat="server" Text="<上傳遊艇照片>"></asp:Label>
    <br />
    <asp:Label ID="Label_YachtsAlbum" runat="server" Text="選擇遊艇型號 :"></asp:Label>
    <br />
    <asp:DropDownList ID="DropDownList_YachtsAlbum" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="yachtModel" DataValueField="Id" OnSelectedIndexChanged="DropDownList_YachtsAlbum_SelectedIndexChanged"></asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectYachts %>" SelectCommand="SELECT * FROM [Yachts]"></asp:SqlDataSource>
    <br />
    <asp:Label ID="Label_YachtsPhoto" runat="server" Text="照片上傳 :"></asp:Label>
    <br />
    <asp:FileUpload ID="FileUpload_YachtsPhoto" runat="server" />
    <br />
    <asp:Button ID="Button_AddPhotoPath" runat="server" Text="新增照片" class="btn btn-outline-primary btn-block mt-3" OnClick="Button_AddPhotoPath_Click" />
    <br />
    <asp:Label ID="Label_PhotoList" runat="server" Text="照片列表 :"></asp:Label>
    <br />
    <asp:RadioButtonList ID="RadioButtonList_PhotoPath" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList_PhotoPath_SelectedIndexChanged" DataSourceID="SqlDataSource3" DataTextField="YachtId" DataValueField="Id"></asp:RadioButtonList>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectYachtAlbum %>" SelectCommand="SELECT * FROM [YachtsAlbum]"></asp:SqlDataSource>
    <br />
    <asp:Button ID="Button_DelPhotoPath" runat="server" Text="刪除照片" class="btn btn-outline-primary btn-block mt-3" OnClick="Button_DelPhotoPath_Click" />
</asp:Content>
