<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeFile="Details.aspx.cs" Inherits="Details" %>


<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="FormView1" />
        </DataControls>
    </asp:DynamicDataManager>

    <h2 class="DDSubHeader" style="margin-top:20px;color:white;font-size:24px">Entry from table <%= table.DisplayName %></h2>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                HeaderText="List of validation errors" CssClass="DDValidator" />
            <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1" Display="None" CssClass="DDValidator" />

            <asp:FormView runat="server"   ID="FormView1"  DataSourceID="DetailsDataSource" OnItemDeleted="FormView1_ItemDeleted" RenderOuterTable="false">
                <ItemTemplate >
                    <table id="detailsTable" style="margin-top:20px;font-size:24px;color:black;font-family:verdana" onclick ="Table(this)" style = "margin-left:300px;font-size:20px;font-family:Verdana;line-height:30px;background-color:white;color:black;width:700px" class="DDDetailsTable" cellpadding="6">
                        <asp:DynamicEntity  runat="server" />
                    
                    </table>
               <script>
                   if (window.location.pathname == "/Users/Details.aspx") {
                       document.getElementById("ContentPlaceHolder1_FormView1_ctl02_ctl03___Videos_HyperLink1").onclick = function () {
                           var URLString = document.getElementById("ContentPlaceHolder1_FormView1_ctl02_ctl03___Videos_HyperLink1").href;
                           var url = new URL(URLString);
                           var id = url.searchParams.get("UserID");
                           document.getElementById("ContentPlaceHolder1_FormView1_ctl02_ctl03___Videos_HyperLink1").href = "/Videos/List.aspx?LoginSuccess=1&User=Admin&UserID=" + id;

                       }
                   }
                       
                   if (window.location.pathname == "/Videos/Details.aspx") {
                       document.getElementById("ContentPlaceHolder1_FormView1_ctl02_ctl07___User_HyperLink1").onclick = function () {
                           var URLString = document.getElementById("ContentPlaceHolder1_FormView1_ctl02_ctl07___User_HyperLink1").href;
                           var url = new URL(URLString);
                           var id = url.searchParams.get("UserID");
                           document.getElementById("ContentPlaceHolder1_FormView1_ctl02_ctl07___User_HyperLink1").href = "/Users/Details.aspx?LoginSuccess=1&User=Admin&UserID=" + id;

                       } 
                   }
                
                 
                  
                   
                  
               </script>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div class="DDNoItem">No such item.</div>
                </EmptyDataTemplate>
            </asp:FormView>

            <asp:LinqDataSource ID="DetailsDataSource" runat="server" EnableDelete="true" />

            <asp:QueryExtender TargetControlID="DetailsDataSource" ID="DetailsQueryExtender" runat="server">
                <asp:DynamicRouteExpression />
            </asp:QueryExtender>

            <br />

            <div class="DDBottomHyperLink" style="font-size:20px;">
                <asp:DynamicHyperLink   LoginSuccess="1" User="Admin" ID="ListHyperLink"  runat ="server" Action="List">Show all items</asp:DynamicHyperLink>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

