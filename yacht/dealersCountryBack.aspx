<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="dealersCountryBack.aspx.cs" Inherits="yacht.dealersCountryBack" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <asp:Label ID="Label_HeaderTitle" runat="server" Text="代理商國家後台頁面"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Sidebar" runat="server">
     <!-- 抓取目前登入者的username -->
    <asp:Literal ID="Literal_name" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="title" runat="server">
    <asp:Label ID="Label_title" runat="server" Text="代理商國家"></asp:Label>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="content" runat="server">
    <asp:Label ID="Label_country" runat="server" Text="國家: "></asp:Label>
    <asp:TextBox ID="TextBox_country" runat="server" Placeholder="請輸入國家"></asp:TextBox>
    <br />
    <asp:Button ID="Button_add" runat="server" Text="確認新增" OnClick="Button_add_Click" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="body" runat="server">
    <asp:GridView ID="GridView_country" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCancelingEdit="GridView_country_RowCancelingEdit" OnRowDeleting="GridView_country_RowDeleting" OnRowEditing="GridView_country_RowEditing" OnRowUpdating="GridView_country_RowUpdating" >
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
</asp:Content>
