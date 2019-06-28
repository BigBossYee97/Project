<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeFile="List.aspx.cs" Inherits="List" %>

<%@ Register src="~/DynamicData/Content/GridViewPager.ascx" tagname="GridViewPager" tagprefix="asp" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content1"   ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:DynamicDataManager   ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls >
            <asp:DataControlReference  ControlID="GridView1" />
        </DataControls>
    </asp:DynamicDataManager>

    <h2 class="DDSubHeader" style="margin-top:20px;color:white;font-size:24px"><%= table.DisplayName%></h2>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="DD" style="font-size:20px;color:white">
                <asp:ValidationSummary ID="ValidationSummary1" style= "position:absolute" runat="server" EnableClientScript="true"
                    HeaderText="List of validation errors" CssClass="DDValidator" />
                <asp:DynamicValidator runat="server" ID="GridViewValidator" ControlToValidate="GridView1" Display="None" CssClass="DDValidator" />

                <asp:QueryableFilterRepeater runat="server" ID="FilterRepeater">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("DisplayName") %>' OnPreRender="Label_PreRender" />
                        <asp:DynamicFilter   runat="server" ID="DynamicFilter" OnFilterChanged="DynamicFilter_FilterChanged" /><br />
                    </ItemTemplate>
                </asp:QueryableFilterRepeater>
                <br />
            </div>

            <asp:GridView ID="GridView1" style = "margin-left:300px;font-size:16px;font-family:Verdana;line-height:30px;background-color:white;color:black;width:700px" runat="server" DataSourceID="GridDataSource" EnablePersistedSelection="true"
                AllowPaging="True" AllowSorting="True" CssClass="DDGridView"
                RowStyle-CssClass="td"  onclick = "Table(this)" HeaderStyle-CssClass="th" CellPadding="6">
                <Columns>
                   
                    <asp:TemplateField>
                        <ItemTemplate>    
                            <asp:DynamicHyperLink LoginSuccess="1" User="Admin" runat="server" Text="Details" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                
                <PagerStyle CssClass="DDFooter"/>        
                <PagerTemplate>
                    <asp:GridViewPager runat="server" />
                </PagerTemplate>
                <EmptyDataTemplate>
                    There are currently no items in this table.
                </EmptyDataTemplate>
            </asp:GridView>

            <asp:LinqDataSource ID="GridDataSource" runat="server" EnableDelete="true" />
            
            <asp:QueryExtender TargetControlID="GridDataSource" ID="GridQueryExtender" runat="server">
                <asp:DynamicFilterExpression ControlID="FilterRepeater" />
            </asp:QueryExtender>

            <br />

            <div class="DDBottomHyperLink" >
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        if (window.location.pathname == "/Users/List.aspx") {
            $(document).ready(function () {
                $("#ContentPlaceHolder1_GridView1").click();
            });

            document.getElementById("ContentPlaceHolder1_GridView1").onclick = function () {
                var e = document.getElementById("ContentPlaceHolder1_GridView1");
                var row = document.getElementById("ContentPlaceHolder1_GridView1").rows.length;
                for (var i = 0; i < row; i++) {
                    e.parentNode.childNodes[1].childNodes[1].childNodes[i].childNodes[5].style.display = "none";
                }
            }
        }

        if (window.location.pathname == "/Videos/List.aspx") {
            $(document).ready(function () {
                $("#ContentPlaceHolder1_GridView1").click();
            });

            document.getElementById("ContentPlaceHolder1_GridView1").onclick = function () {
                var e = document.getElementById("ContentPlaceHolder1_GridView1");
                var row = document.getElementById("ContentPlaceHolder1_GridView1").rows.length;
                for (var i = 0; i < row; i++) {
                    e.parentNode.childNodes[1].childNodes[1].childNodes[i].childNodes[9].style.display = "none";
                }
            }
        }

      
      
                </script>
</asp:Content>

