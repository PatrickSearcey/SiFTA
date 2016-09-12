<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Empty.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="NationalFundingDev.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cphStyles" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div style="min-height: 700px;">
        <telerik:RadAjaxManager runat="server" ID="ram">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rsbSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgResults" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rgResults">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgResults" LoadingPanelID="ralp" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel runat="server" ID="ralp" Skin="Silk" />
            <table style="width: 100%;">
                <tr>
                    <td style="padding-left: 130px; padding-top: 25px;">
                        <asp:Image runat="server" ID="imgSearchLogo" ImageUrl="~/Images/SearchLogo.png" /><br />
                        <telerik:RadSearchBox runat="server" ID="rsbSearch" DropDownSettings-Height="0px" Width="600px" OnSearch="rsbSearch_Search"
                            Skin="Silk" DataTextField="name" DataValueField="url" />
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 50px;" valign="top">
                        <telerik:RadGrid ID="rgResults" BorderWidth="0px" runat="server" OnNeedDataSource="rgResults_NeedDataSource"
                            AutoGenerateColumns="False" AlternatingItemStyle-BackColor="White" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="#696f74" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="Larger"
                            Width="700px" Skin="Metro">
                            <MasterTableView CommandItemDisplay="None" ShowFooter="false" ShowHeader="false" ShowGroupFooter="true" AllowCustomPaging="false" AllowPaging="true" PageSize="10" PagerStyle-AlwaysVisible="true">
                                <ItemStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" />
                                <AlternatingItemStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" />
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="Search Results" HeaderStyle-HorizontalAlign="Center" Visible="true">
                                        <ItemTemplate>
                                            <table style="border: none;">
                                                <tr style="border: none;">
                                                    <td align="right" valign="top" style="width: 50px; border: none;">
                                                        <asp:Image ID="imgLogo" runat="server" Visible='<%# CustomerImage(Eval("Type")) %>' ImageUrl='<%# ImageURL(Eval("URL")) %>' Height="50px" Width="50px" />
                                                    </td>
                                                    <td align="left" style="border: none;">
                                                        <asp:Literal runat="Server" ID="ltlName" Text='<%# NameLink(Eval("Type"), Eval("URL"), Eval("Name")) %>' /><br />
                                                        <font style="color: Green; padding: 0px;"><%# AppendURL(Eval("URL")) %></font>
                                                        <br />
                                                        <%# Eval("description") %>
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <PagerStyle Mode="NextPrevAndNumeric" Visible="true" PageSizeControlType="None"></PagerStyle>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
    </div>
</asp:Content>
