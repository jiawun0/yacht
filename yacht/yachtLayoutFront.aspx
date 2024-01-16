<%@ Page Title="" Language="C#" MasterPageFile="~/Newoverview.Master" AutoEventWireup="true" CodeBehind="yachtLayoutFront.aspx.cs" Inherits="yacht.yachtLayoutFront" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box6">
        <p>Layout & deck plan</p>
        <ul>
            <asp:Literal ID="Literal_layoutDeckPlanImgPath" runat="server"></asp:Literal>
        </ul>
    </div>
</asp:Content>
