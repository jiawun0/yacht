<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="dealersCountryBack.aspx.cs" Inherits="yacht.dealersCountryBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            margin-right: 34px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <asp:Label ID="Label_HeaderTitle" runat="server" Text="代理商後台頁面"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Sidebar" runat="server">
    <!-- 抓取目前登入者的username -->
    <asp:Literal ID="Literal_name" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="content" runat="server">
    <asp:Label ID="Label_title" runat="server" Text="<代理商國家>"></asp:Label>
    <br />
    <asp:Label ID="Label_country" runat="server" Text="國家: "></asp:Label>
    <asp:TextBox ID="TextBox_country" runat="server" Placeholder="請輸入國家"></asp:TextBox>
    <br />
    <asp:Button ID="Button_add" runat="server" Text="新增國家" OnClick="Button_add_Click" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="body" runat="server">
    <asp:GridView ID="GridView_country" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCancelingEdit="GridView_country_RowCancelingEdit" OnRowDeleting="GridView_country_RowDeleting" OnRowEditing="GridView_country_RowEditing" OnRowUpdating="GridView_country_RowUpdating" OnRowDeleted="DeltedCountry">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
            <asp:TemplateField HeaderText="countrySort" SortExpression="countrySort">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox_countrySortT" runat="server" Text='<%# Bind("countrySort") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label_countrySortT" runat="server" Text='<%# Bind("countrySort") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreatDate" HeaderText="CreatDate" SortExpression="CreatDate" ReadOnly="True" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True"></asp:CommandField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Connectcountry %>" DeleteCommand="DELETE FROM [countrySort] WHERE [Id] = @Id" InsertCommand="INSERT INTO [countrySort] ([countrySort], [CreatDate]) VALUES (@countrySort, @CreatDate)" SelectCommand="SELECT * FROM [countrySort]" UpdateCommand="UPDATE [countrySort] SET [countrySort] = @countrySort, [CreatDate] = @CreatDate WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="countrySort" Type="String" />
            <asp:Parameter Name="CreatDate" Type="DateTime" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="countrySort" Type="String" />
            <asp:Parameter Name="CreatDate" Type="DateTime" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
    <asp:Label ID="Label_titlearea" runat="server" Text="<代理商區域>"></asp:Label>
    <br />
    <asp:Label ID="Label_countryDDL" runat="server" Text="請選擇國家: "></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource2" DataTextField="countrySort" DataValueField="Id" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectcountryDDL %>" SelectCommand="SELECT * FROM [countrySort]"></asp:SqlDataSource>
    <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="auto-style1" DataSourceID="SqlDataSource_area_radioBL" DataTextField="area" DataValueField="Id" Visible="False"></asp:RadioButtonList>
    <asp:SqlDataSource ID="SqlDataSource_area_radioBL" runat="server" ConnectionString="<%$ ConnectionStrings:Connectarea_RBL %>" SelectCommand="SELECT * FROM [dealers]"></asp:SqlDataSource>
    <asp:Button ID="BtnDelArea" runat="server" Text="刪除區域" OnClick="BtnDelArea_Click" Visible="False" />
    <asp:GridView ID="GridView_area" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource_area" Visible="False">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id"></asp:BoundField>
            <asp:BoundField DataField="country_ID" HeaderText="country_ID" SortExpression="country_ID"></asp:BoundField>
            <asp:BoundField DataField="area" HeaderText="area" SortExpression="area"></asp:BoundField>
            <asp:BoundField DataField="dealerImgPath" HeaderText="dealerImgPath" SortExpression="dealerImgPath"></asp:BoundField>
            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name"></asp:BoundField>
            <asp:BoundField DataField="contact" HeaderText="contact" SortExpression="contact"></asp:BoundField>
            <asp:BoundField DataField="address" HeaderText="address" SortExpression="address"></asp:BoundField>
            <asp:BoundField DataField="tel" HeaderText="tel" SortExpression="tel"></asp:BoundField>
            <asp:BoundField DataField="email" HeaderText="email" SortExpression="email"></asp:BoundField>
            <asp:BoundField DataField="link" HeaderText="link" SortExpression="link"></asp:BoundField>
            <asp:BoundField DataField="CreatDate" HeaderText="CreatDate" SortExpression="CreatDate"></asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource_area" runat="server" ConnectionString="<%$ ConnectionStrings:Connectarea %>" DeleteCommand="DELETE FROM [dealers] WHERE [Id] = @Id" InsertCommand="INSERT INTO [dealers] ([country_ID], [area], [dealerImgPath], [name], [contact], [address], [tel], [fax], [email], [link], [CreatDate]) VALUES (@country_ID, @area, @dealerImgPath, @name, @contact, @address, @tel, @fax, @email, @link, @CreatDate)" SelectCommand="SELECT * FROM [dealers]" UpdateCommand="UPDATE [dealers] SET [country_ID] = @country_ID, [area] = @area, [dealerImgPath] = @dealerImgPath, [name] = @name, [contact] = @contact, [address] = @address, [tel] = @tel, [fax] = @fax, [email] = @email, [link] = @link, [CreatDate] = @CreatDate WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="country_ID" Type="Int32" />
            <asp:Parameter Name="area" Type="String" />
            <asp:Parameter Name="dealerImgPath" Type="String" />
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="contact" Type="String" />
            <asp:Parameter Name="address" Type="String" />
            <asp:Parameter Name="tel" Type="String" />
            <asp:Parameter Name="fax" Type="Object" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="link" Type="String" />
            <asp:Parameter Name="CreatDate" Type="DateTime" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="country_ID" Type="Int32" />
            <asp:Parameter Name="area" Type="String" />
            <asp:Parameter Name="dealerImgPath" Type="String" />
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="contact" Type="String" />
            <asp:Parameter Name="address" Type="String" />
            <asp:Parameter Name="tel" Type="String" />
            <asp:Parameter Name="fax" Type="Object" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="link" Type="String" />
            <asp:Parameter Name="CreatDate" Type="DateTime" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <asp:Label ID="Label_area" runat="server" Text="區域: "></asp:Label>
    <asp:TextBox ID="TextBox_area" runat="server" Placeholder="請輸入區域"></asp:TextBox>
    <br />
    <asp:Label ID="Label_dealerImgPath" runat="server" Text="相片: "></asp:Label>
    <asp:FileUpload ID="FileUpload_Img" runat="server" Placeholder="請選取代理商相片"/>
    <br />
    <asp:Label ID="Label_name" runat="server" Text="公司: "></asp:Label>
    <asp:TextBox ID="TextBox_name" runat="server" Placeholder="請輸入公司名稱"></asp:TextBox>
    <br />
    <asp:Label ID="Label_contact" runat="server" Text="聯絡: "></asp:Label>
    <asp:TextBox ID="TextBox_contact" runat="server" Placeholder="請輸入聯絡人"></asp:TextBox>
    <br />
    <asp:Label ID="Label_address" runat="server" Text="地址: "></asp:Label>
    <asp:TextBox ID="TextBox_address" runat="server" Placeholder="請輸入地址"></asp:TextBox>
    <br />
    <asp:Label ID="Label_tel" runat="server" Text="電話: "></asp:Label>
    <asp:TextBox ID="TextBox_tel" runat="server" Placeholder="請輸入電話"></asp:TextBox>
    <br />
    <asp:Label ID="Label_fax" runat="server" Text="傳真: "></asp:Label>
    <asp:TextBox ID="TextBox_fax" runat="server" Placeholder="請輸入傳真"></asp:TextBox>
    <br />
    <asp:Label ID="Label_email" runat="server" Text="信箱: "></asp:Label>
    <asp:TextBox ID="TextBox_email" runat="server" Placeholder="請輸入信箱" TextMode="Email"></asp:TextBox>
    <br />
    <asp:Label ID="Label_link" runat="server" Text="網址: "></asp:Label>
    <asp:TextBox ID="TextBox_link" runat="server" Placeholder="請輸入網址"></asp:TextBox>
    <br />
    <asp:Button ID="BtnAddArea" runat="server" Text="新增區域" OnClick="BtnAddArea_Click" />
    <br />
    <br />
    <asp:GridView ID="GridView_arealist" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource_arealist" OnRowCancelingEdit="GridView_arealist_RowCancelingEdit" OnRowDeleting="GridView_arealist_RowDeleting" OnRowEditing="GridView_arealist_RowEditing" OnRowUpdating="GridView_arealist_RowUpdating">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id"></asp:BoundField>
            <asp:BoundField DataField="country_ID" HeaderText="country_ID" SortExpression="country_ID"></asp:BoundField>
            <asp:BoundField DataField="area" HeaderText="area" SortExpression="area"></asp:BoundField>
            <asp:BoundField DataField="dealerImgPath" HeaderText="dealerImgPath" SortExpression="dealerImgPath"></asp:BoundField>
            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name"></asp:BoundField>
            <asp:BoundField DataField="contact" HeaderText="contact" SortExpression="contact"></asp:BoundField>
            <asp:BoundField DataField="address" HeaderText="address" SortExpression="address"></asp:BoundField>
            <asp:BoundField DataField="tel" HeaderText="tel" SortExpression="tel"></asp:BoundField>
            <asp:BoundField DataField="fax" HeaderText="fax" SortExpression="fax"></asp:BoundField>
            <asp:BoundField DataField="email" HeaderText="email" SortExpression="email"></asp:BoundField>
            <asp:BoundField DataField="link" HeaderText="link" SortExpression="link"></asp:BoundField>
            <asp:BoundField DataField="CreatDate" HeaderText="CreatDate" SortExpression="CreatDate"></asp:BoundField>
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True"></asp:CommandField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource_arealist" runat="server" ConnectionString="<%$ ConnectionStrings:Connectarealist %>" DeleteCommand="DELETE FROM [dealers] WHERE [Id] = @Id" InsertCommand="INSERT INTO [dealers] ([country_ID], [area], [dealerImgPath], [name], [contact], [address], [tel], [fax], [email], [link], [CreatDate]) VALUES (@country_ID, @area, @dealerImgPath, @name, @contact, @address, @tel, @fax, @email, @link, @CreatDate)" SelectCommand="SELECT * FROM [dealers] WHERE ([country_ID] = @country_ID)" UpdateCommand="UPDATE [dealers] SET [country_ID] = @country_ID, [area] = @area, [dealerImgPath] = @dealerImgPath, [name] = @name, [contact] = @contact, [address] = @address, [tel] = @tel, [fax] = @fax, [email] = @email, [link] = @link, [CreatDate] = @CreatDate WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="country_ID" Type="Int32" />
            <asp:Parameter Name="area" Type="String" />
            <asp:Parameter Name="dealerImgPath" Type="String" />
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="contact" Type="String" />
            <asp:Parameter Name="address" Type="String" />
            <asp:Parameter Name="tel" Type="String" />
            <asp:Parameter Name="fax" Type="String" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="link" Type="String" />
            <asp:Parameter Name="CreatDate" Type="DateTime" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" Name="country_ID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="country_ID" Type="Int32" />
            <asp:Parameter Name="area" Type="String" />
            <asp:Parameter Name="dealerImgPath" Type="String" />
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="contact" Type="String" />
            <asp:Parameter Name="address" Type="String" />
            <asp:Parameter Name="tel" Type="String" />
            <asp:Parameter Name="fax" Type="String" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="link" Type="String" />
            <asp:Parameter Name="CreatDate" Type="DateTime" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
