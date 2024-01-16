<%@ Page Title="" Language="C#" MasterPageFile="~/Newoverview.Master" AutoEventWireup="true" CodeBehind="yachtSpecificationFront.aspx.cs" Inherits="yacht.yachtSpecificationFront" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box5">
        <h4>DETAIL SPECIFICATION</h4>

        <p>HULL STRUCTURE & DECKS</p>
        <ul>
            <asp:Literal ID="Literal_specificationContentHtml" runat="server"></asp:Literal>
        </ul>
</asp:Content>
