<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Main.Master" AutoEventWireup="true" CodeBehind="GroupAccountDetails.aspx.cs" Inherits="NationalFundingDev.Reports.Thematic.GroupAccountDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    Group Account Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStyles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphAJAXManager" runat="server">
    <telerik:RadAjaxManager runat="server" ID="ram">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rcbGroup" >
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbYear" />
                    <telerik:AjaxUpdatedControl ControlID="rgResults" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbYear" >
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgResults" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphImage" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphInformation" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphSidePanel" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cphBody" runat="server">
    <table style="width:100%;">
        <tr>
            <td style="width:200px;">
                <telerik:RadComboBox runat="server" ID="rcbGroup" AutoPostBack="true" OnSelectedIndexChanged="rcbGroup_SelectedIndexChanged" Width="100%" >
                    <Items>
                        <telerik:RadComboBoxItem Text="USACE" Value="USACE" />
                        <telerik:RadComboBoxItem Text="NSIP" Value="NSIP" />
                        <telerik:RadComboBoxItem Text="IJC" Value="IJC" />
                        <telerik:RadComboBoxItem Text="USGSCRN" Value="USGSCRN" />
                    </Items>
                </telerik:RadComboBox>
                
            </td>
            <td>
                <telerik:RadComboBox runat="server" ID="rcbYear" AutoPostBack="true" OnSelectedIndexChanged="rcbYear_SelectedIndexChanged">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-top:20px;">
                <telerik:RadGrid runat="server" ID="rgResults" OnNeedDataSource="rgResults_NeedDataSource" Width="100%" AutoGenerateColumns="false" >
                    <ExportSettings ExportOnlyData="true">
                    </ExportSettings>
                    <MasterTableView CommandItemDisplay="Top" >
                        <CommandItemSettings ShowExportToExcelButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                        
                        <ColumnGroups>
                            <telerik:GridColumnGroup HeaderText="Totals" Name="totals" />
                        </ColumnGroups>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Center" HeaderStyle-Width="550px" >
                                <ItemTemplate>
                                    <a href="<%# GetURL(String.Format("/Center.aspx?OrgCode={0}", Eval("OrgCode"))) %>" style="color:#4A95A1;"><%# Eval("WSC") %></a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="PO Number" DataField="PurchaseOrderNumber"   />
                            <telerik:GridBoundColumn HeaderText="Parent Account" DataField="ParentAccount"   />
                            <telerik:GridBoundColumn HeaderText="Account" DataField="AccountNumber"   />
                            <telerik:GridBoundColumn HeaderText="USGS Signed" DataField="SignUSGSDate" DataFormatString="{0:MM/dd/yyyy}" />
                            <telerik:GridBoundColumn HeaderText="Customer Funding" DataField="FundingCustomer" DataFormatString="{0:c0}" />
                            <telerik:GridBoundColumn HeaderText="Status" DataField="Status"   />
                            <telerik:GridBoundColumn HeaderText="Remarks" DataField="Remarks"   />
                            <telerik:GridBoundColumn HeaderText="Modified By" DataField="ModifiedBy"   />
                            <telerik:GridBoundColumn HeaderText="Modified Date" DataField="ModifiedDate" DataFormatString="{0:MM/dd/yyyy}"   />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>

