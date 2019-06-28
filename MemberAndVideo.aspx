<%@ MasterType VirtualPath="~/Site.master" %>
<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeFile="MemberAndVideo.aspx.cs" Inherits="_Default" %>


<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content1"   align='center' ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />

    <h2 class="DDSubHeader" style="margin-top:20px;color:white;font-size:24px">Choose a Category : </h2>

    <br /><br />

    <asp:GridView ID="Menu1" align='center' style = "font-size:20px;font-family:Verdana;line-height:30px;background-color:white;color:black;width:700px" runat="server" AutoGenerateColumns="false"
        CssClass="DDGridView" RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6">
        <Columns >
            <asp:TemplateField  HeaderText="Table Name" SortExpression="TableName">
                <ItemTemplate>
                    <asp:DynamicHyperLink LoginSuccess="1" User="Admin" ID="HyperLink1" runat="server"><%# Eval("DisplayName") %></asp:DynamicHyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
</asp:Content>


