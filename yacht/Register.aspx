<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="yacht.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderTitle" runat="server">
    <asp:Label ID="Label_HeaderTitle" runat="server" Text="後台管理頁面"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Sidebar" runat="server">
    <!-- 抓取目前登入者的username -->
    <asp:Literal ID="Literal_name" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="title" runat="server">
    <asp:Label ID="Label_title" runat="server" Text="管理使用者"></asp:Label>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="content" runat="server">
    <asp:Label ID="Label_name" runat="server" Text="* 姓名: "></asp:Label>
    <asp:TextBox ID="TextBox_name" runat="server" Placeholder="請輸入真實姓名"></asp:TextBox>
    <br />
    <asp:Label ID="Label_account" runat="server" Text="* 帳號: "></asp:Label>
    <asp:TextBox ID="TextBox_account" runat="server" Placeholder="請輸入帳號"></asp:TextBox><asp:Label ID="LabelAdd" runat="server" ></asp:Label>
    <br />
    <asp:Label ID="Label_password" runat="server" Text="* 密碼: "></asp:Label>
    <asp:TextBox ID="TextBox_password" runat="server" Placeholder="請輸入6位以上字元" TextMode="Password"></asp:TextBox>
    <br />
    <asp:Label ID="Label_pwCheck" runat="server" Text="* 確認密碼: "></asp:Label>
    <asp:TextBox ID="TextBox_pwCheck" runat="server" Placeholder="請再次輸入密碼" TextMode="Password"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button_Register" runat="server" Text="確認新增" OnClick="Button_Register_Click" class="btn btn-outline-primary btn-block mt-3" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="body" runat="server">
    <asp:Label ID="Label_user" runat="server" Text="<使用者列表編輯>"></asp:Label>
    <asp:GridView ID="GridView_Register" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCancelingEdit="GridView_Register_RowCancelingEdit" OnRowDeleting="GridView_Register_RowDeleting" OnRowEditing="GridView_Register_RowEditing" OnRowUpdating="GridView_Register_RowUpdating" >
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
            <asp:TemplateField HeaderText="account" SortExpression="account">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox_accountT" runat="server" Text='<%# Bind("account") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label_accountT" runat="server" Text='<%# Bind("account") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
            <asp:TemplateField HeaderText="name" SortExpression="name">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox_nameT" runat="server" Text='<%# Bind("name") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label_nameT" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="isManger" SortExpression="isManger">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox_isManger" runat="server" Checked='<%# Bind("isManger") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox_isManger" runat="server" Checked='<%# Eval("isManger") != DBNull.Value && Convert.ToBoolean(Eval("isManger")) %>' Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreatDate" HeaderText="CreatDate" SortExpression="CreatDate" ReadOnly="True"/>
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectYachtLogin2 %>" SelectCommand="SELECT * FROM [Login]"></asp:SqlDataSource>
</asp:Content>
