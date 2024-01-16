<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="yachtOverviewBack.aspx.cs" Inherits="yacht.yachtOverviewBack" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <asp:Label ID="Label_HeaderTitle" runat="server" Text="遊艇 Overview 後台頁面"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Sidebar" runat="server">
    <!-- 抓取目前登入者的username -->
    <asp:Literal ID="Literal_name" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="content" runat="server">
    <asp:Label ID="Label_selyachtModel" runat="server" Text="選擇遊艇型號 :"></asp:Label>
    <asp:DropDownList ID="DropDownList_yachtModel" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="yachtModel" DataValueField="Id" OnSelectedIndexChanged="DropDownList_yachtModel_SelectedIndexChanged"></asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectwithOverview %>" DeleteCommand="DELETE FROM [Yachts] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Yachts] ([yachtModel], [isNewDesign], [isNewBuilding], [guid], [bannerImgPathJSON], [overviewContentHtml], [overviewDimensionsImgPath], [overviewDownloadsFilePath], [overviewDimensionsJSON], [layoutDeckPlanImgPathJSON], [CreatDate]) VALUES (@yachtModel, @isNewDesign, @isNewBuilding, @guid, @bannerImgPathJSON, @overviewContentHtml, @overviewDimensionsImgPath, @overviewDownloadsFilePath, @overviewDimensionsJSON, @layoutDeckPlanImgPathJSON, @CreatDate)" SelectCommand="SELECT * FROM [Yachts]" UpdateCommand="UPDATE [Yachts] SET [yachtModel] = @yachtModel, [isNewDesign] = @isNewDesign, [isNewBuilding] = @isNewBuilding, [guid] = @guid, [bannerImgPathJSON] = @bannerImgPathJSON, [overviewContentHtml] = @overviewContentHtml, [overviewDimensionsImgPath] = @overviewDimensionsImgPath, [overviewDownloadsFilePath] = @overviewDownloadsFilePath, [overviewDimensionsJSON] = @overviewDimensionsJSON, [layoutDeckPlanImgPathJSON] = @layoutDeckPlanImgPathJSON, [CreatDate] = @CreatDate WHERE [Id] = @Id">
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
    <br />
    <asp:Label ID="Label_img" runat="server" Text="<遊艇 Dimension 圖片>"></asp:Label>
    <br />
    <asp:Literal ID="Literal_img" runat="server"></asp:Literal>
    <br />
    <br />
    <asp:Label ID="Label_DimensionsImgPath" runat="server" Text="<上傳遊艇 Dimension 圖片>"></asp:Label>
    <asp:Literal ID="Literal_DimensionsImgPath" runat="server"></asp:Literal>
    <br />
    <asp:FileUpload ID="FileUpload_DimensionsImgPath" runat="server" />
    <br />
    <asp:Button ID="Button_upDimensionsImgPath" runat="server" Text="上傳圖片" class="btn btn-outline-primary btn-block mt-3" OnClick="Button_upDimensionsImgPath_Click" />
    <br />
    <asp:Label ID="Label_downloadPDF" runat="server" Text="<選擇上傳附件>"></asp:Label>
    <asp:Literal ID="Literal_downloadPDF" runat="server"></asp:Literal>
    <br />
    <asp:FileUpload ID="FileUpload_downloadPDF" runat="server" />
    <br />
    <asp:Button ID="Button_download" runat="server" Text="上傳附件" class="btn btn-outline-primary btn-block mt-3" OnClick="Button_download_Click" />
    <br />
    <br />
    <asp:Label ID="Label_Dimension" runat="server" Text="<新增/編輯 Dimension 表格欄位>"></asp:Label>
    <br />
    <asp:Label ID="Label_Specification" runat="server" Text="* 規格 :"></asp:Label>
    <br />
    <asp:TextBox ID="TextBox_Specification" runat="server" type="text" class="form-control" Placeholder="請輸入規格" MaxLength="75"></asp:TextBox>
    <br />
    <asp:Label ID="Label_size" runat="server" Text="* 尺寸 :"></asp:Label>
    <br />
    <asp:TextBox ID="TextBox_size" runat="server" type="text" class="form-control" Placeholder="請輸入尺寸" MaxLength="75"></asp:TextBox>
    <br />
    <asp:Button ID="Button_addDimension" runat="server" Text="新增規格欄位" class="btn btn-outline-primary btn-block mt-3" OnClick="Button_addDimension_Click" />
    <br />
    <asp:GridView ID="GridView_Dimension" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCancelingEdit="GridView_Dimension_RowCancelingEdit" OnRowDeleting="GridView_Dimension_RowDeleting" OnRowEditing="GridView_Dimension_RowEditing" OnRowUpdating="GridView_Dimension_RowUpdating">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id"></asp:BoundField>
            <asp:TemplateField HeaderText="Specification" SortExpression="Specification">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox_SpecificationT" runat="server" Text='<%# Bind("Specification") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label_SpecificationT" runat="server" Text='<%# Bind("Specification") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="size" SortExpression="size">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox_sizeT" runat="server" Text='<%# Bind("size") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label_sizeT" runat="server" Text='<%# Bind("size") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreatDate" HeaderText="CreatDate" SortExpression="CreatDate" ReadOnly="True"></asp:BoundField>
            <asp:BoundField DataField="YachtsId" HeaderText="YachtsId" SortExpression="YachtsId"></asp:BoundField>
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True"></asp:CommandField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectDimension %>" DeleteCommand="DELETE FROM [YachtsDimension] WHERE [Id] = @Id" InsertCommand="INSERT INTO [YachtsDimension] ([Specification], [size], [CreatDate], [YachtsId]) VALUES (@Specification, @size, @CreatDate, @YachtsId)" SelectCommand="SELECT * FROM [YachtsDimension] WHERE ([YachtsId] = @YachtsId)" UpdateCommand="UPDATE [YachtsDimension] SET [Specification] = @Specification, [size] = @size, [CreatDate] = @CreatDate, [YachtsId] = @YachtsId WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Specification" Type="String" />
            <asp:Parameter Name="size" Type="String" />
            <asp:Parameter Name="CreatDate" Type="DateTime" />
            <asp:Parameter Name="YachtsId" Type="Int32" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList_yachtModel" Name="YachtsId" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="Specification" Type="String" />
            <asp:Parameter Name="size" Type="String" />
            <asp:Parameter Name="CreatDate" Type="DateTime" />
            <asp:Parameter Name="YachtsId" Type="Int32" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="body" runat="server">
    <br />
    <asp:Label ID="Label_overviewContent" runat="server" Text="<遊艇 Overview 撰寫>"></asp:Label>
    <br />
    <ckeditor:ckeditorcontrol id="CKEditorControl_overviewContent" runat="server" basepath="/Scripts/ckeditor/"
        toolbar="Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
        NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
        /
        Styles|Format|Font|FontSize
        TextColor|BGColor
        Link|Image"
        height="400px"></ckeditor:ckeditorcontrol>
    <asp:Label ID="UploadnewsoverviewContent" runat="server" Visible="False" ForeColor="#009933" class="d-flex justify-content-center"></asp:Label>
    <asp:Button ID="UploadnewsoverviewContentBtn" runat="server" Text="確認上傳" class="btn btn-outline-primary btn-block mt-3" OnClick="UploadnewsoverviewContentBtn_Click" />
    <br />
</asp:Content>
