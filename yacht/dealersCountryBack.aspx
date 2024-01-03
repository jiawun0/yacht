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
    <br />
    <asp:Label ID="Label_area" runat="server" Text="請輸入區域: "></asp:Label>
    <asp:TextBox ID="TextBox_area" runat="server" Placeholder="請輸入區域"></asp:TextBox>
    <br />
    <asp:Button ID="BtnAddArea" runat="server" Text="新增區域" OnClick="BtnAddArea_Click" />
    <br />
    <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="auto-style1" DataSourceID="SqlDataSource_area_radioBL" DataTextField="area" DataValueField="Id"></asp:RadioButtonList>
    <asp:SqlDataSource ID="SqlDataSource_area_radioBL" runat="server" ConnectionString="<%$ ConnectionStrings:Connectarea_RBL %>" SelectCommand="SELECT * FROM [dealers]"></asp:SqlDataSource>
    <br />
    <asp:Button ID="BtnDelArea" runat="server" Text="刪除區域" OnClick="BtnDelArea_Click" />
    <br />
    <br />
    <asp:GridView ID="GridView_area" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource_area">
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
    <asp:SqlDataSource ID="SqlDataSource_area" runat="server" ConnectionString="<%$ ConnectionStrings:Connectarea %>" DeleteCommand="DELETE FROM [dealers] WHERE [Id] = @Id" InsertCommand="INSERT INTO [dealers] ([country_ID], [area], [dealerImgPath], [name], [contact], [address], [tel], [fax], [email], [link], [CreatDate]) VALUES (@country_ID, @area, @dealerImgPath, @name, @contact, @address, @tel, @fax, @email, @link, @CreatDate)" SelectCommand="SELECT * FROM [dealers] WHERE ([country_ID] = @country_ID)" UpdateCommand="UPDATE [dealers] SET [country_ID] = @country_ID, [area] = @area, [dealerImgPath] = @dealerImgPath, [name] = @name, [contact] = @contact, [address] = @address, [tel] = @tel, [fax] = @fax, [email] = @email, [link] = @link, [CreatDate] = @CreatDate WHERE [Id] = @Id">
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
            <asp:Parameter Name="fax" Type="Object" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="link" Type="String" />
            <asp:Parameter Name="CreatDate" Type="DateTime" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
