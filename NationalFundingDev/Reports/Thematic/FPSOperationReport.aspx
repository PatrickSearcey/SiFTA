<%@ Page Language="C#" MasterPageFile="~/Themes/Base/Empty.Master" AutoEventWireup="true" CodeBehind="FPSOperationReport.aspx.cs" Inherits="NationalFundingDev.Reports.NSIPOperationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cphStyles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadAjaxManager runat="server" ID="ram">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rcbFY">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rbMap" />
                    <telerik:AjaxUpdatedControl ControlID="rgNSIP" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbMap">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rbMap" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="padding:20px;">
    <telerik:RadAjaxLoadingPanel runat="server" Skin="Silk" ID="ralp" />
    <h2>FPS Operation Report</h2>
    <table>
        <tr>
            <td>
                <telerik:RadComboBox runat="server" ID="rcbFY" AppendDataBoundItems="true" Skin="Silk" AutoPostBack="true" OnSelectedIndexChanged="rcbFY_SelectedIndexChanged">
                    
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadButton runat="server" ID="rbMapIt" Text="View Sites On Map" OnClick="rbMapIt_Click" />
            </td>
        </tr>
    </table>
    <telerik:RadGrid runat="server" ID="rgNSIP" AllowSorting="True" OnNeedDataSource="rgNSIP_NeedDataSource"
        AutoGenerateColumns="False" Width="100%" Skin="Silk">
        <MasterTableView AutoGenerateColumns="False" EditMode="EditForms" CommandItemDisplay="None" FooterStyle-BackColor="#2dabc1" FooterStyle-ForeColor="White"
            ShowFooter="true" AlternatingItemStyle-BackColor="Beige" EnableHeaderContextMenu="true" AllowSorting="true">
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Site Specific Funding Details" Name="FundingCount" HeaderStyle-HorizontalAlign="Center" />
                <telerik:GridColumnGroup HeaderText="Funded Site Count" Name="SiteCount" ParentGroupName="FundingCount" HeaderStyle-HorizontalAlign="Center" />
            </ColumnGroups>
            <Columns>
                <telerik:GridTemplateColumn HeaderText="Water Science Center" SortExpression="OrgName" HeaderStyle-HorizontalAlign="Center" FooterText="Totals:" FooterStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <a style="color: #4a95a1;" href='<%# AppendBaseURL(String.Format("Customer.aspx?CustomerID={0}", Eval("CustomerID"))) %>' target="_blank"><%# Eval("Name").ToString().Replace(" Water Science Center","").Replace(" Science Center","") %></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Signed">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="cbSigned" Checked='<%# Signed(Eval("SignUSGSDate")) %>' Enabled="false" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="Full" ColumnGroupName="SiteCount" DataField="data_coll_full" SortExpression="data_coll_full" ItemStyle-HorizontalAlign="Center" Aggregate="Sum" FooterAggregateFormatString="{0}" FooterStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderText="Partial" ColumnGroupName="SiteCount" DataField="data_coll_partial" SortExpression="data_coll_partial" ItemStyle-HorizontalAlign="Center" Aggregate="Sum" FooterAggregateFormatString="{0}" FooterStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderText="Total" ColumnGroupName="SiteCount" DataField="data_coll_total" SortExpression="data_coll_total" ItemStyle-HorizontalAlign="Center" Aggregate="Sum" FooterAggregateFormatString="{0}" FooterStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderText="Data Collection (QCONT)" ColumnGroupName="FundingCount" DataField="data_coll_ss_dollars" SortExpression="data_coll_ss_dollars" DataFormatString="{0:C0}" HeaderStyle-Width="140px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" Aggregate="Sum" FooterAggregateFormatString="{0:c0}" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderText="QCONT-INST and MISC-Upgrades" ColumnGroupName="FundingCount" DataField="infra_ss_dollars" SortExpression="infra_ss_dollars" DataFormatString="{0:C0}" HeaderStyle-Width="140px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" Aggregate="Sum" FooterAggregateFormatString="{0:c0}" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderText="Studies/Support Funding Details" DataField="funded_studies_total" SortExpression="funded_studies_total" DataFormatString="{0:C0}" HeaderStyle-Width="140px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" Aggregate="Sum" FooterAggregateFormatString="{0:c0}" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderText="FPS Allocation" DataField="funding_total" SortExpression="funding_total" DataFormatString="{0:C0}" HeaderStyle-Width="140px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" Aggregate="Sum" FooterAggregateFormatString="{0:c0}" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderText="Difference Between Total Funding and FPS Allocation" DataField="funding_balance" SortExpression="funding_balance" DataFormatString="{0:C0}" HeaderStyle-Width="140px" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" Aggregate="Sum" FooterAggregateFormatString="{0:c0}" FooterStyle-HorizontalAlign="Right" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid></div>
</asp:Content>
