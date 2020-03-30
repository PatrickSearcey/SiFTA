<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Themes/Base/Main.Master" CodeBehind="ImageSearch.aspx.cs" Inherits="NationalFundingDev.Reports.Metrics.ImageSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cphStyles" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAJAXManager" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphImage" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphInformation" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSidePanel" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphBody" runat="server">
    <div>
        <telerik:RadTextBox runat="server" ID="rtbSearch" EmptyMessage="Customer Name" Width="500px" /><telerik:RadButton runat="server" ID="rbSearchClick" OnClick="rbSearchClick_Click" Text="Search" />
        <telerik:RadGrid runat="server" ID="rgImages" OnNeedDataSource="rgImages_NeedDataSource" AutoGenerateColumns="false" >
            <MasterTableView>
                <Columns>
                    <telerik:GridBinaryImageColumn HeaderText="Image" DataField="Icon" />
                    <telerik:GridTemplateColumn HeaderText="WxH" >
                        <ItemTemplate>
                            <%# GetWxH(Eval("Icon")) %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn HeaderText="Name" DataField="Name" />
                    <telerik:GridTemplateColumn HeaderText="Links">
                        <ItemTemplate>
                            <%# GetLinks(Eval("Name"), Eval("Icon")) %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>