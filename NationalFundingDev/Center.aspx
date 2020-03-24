<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Main.Master" AutoEventWireup="true" CodeBehind="Center.aspx.cs" Inherits="NationalFundingDev.CenterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cphStyles" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAJAXManager" runat="server">
    <telerik:RadAjaxManager runat="server" ID="ram">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtsCenterOptions">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rtsCenterOptions" />
                    <telerik:AjaxUpdatedControl ControlID="rmpCenterOptions" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgCustomers">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCustomers" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgCoopFunding">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCoopFunding" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbShowAll">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCoopFunding" LoadingPanelID="ralp" />
                    <telerik:AjaxUpdatedControl ControlID="rsbCoopFunding" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rsbCoopFunding">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCoopFunding" LoadingPanelID="ralp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="ralp" Skin="Silk" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphImage" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphInformation" runat="server">
    <b><%: center.Name %> Home</b><br />
    <%: CenterAddress %><br />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSidePanel" runat="server">
    <telerik:RadTabStrip runat="server" ID="rtsCenterOptions" MultiPageID="rmpCenterOptions" Orientation="HorizontalTop" Skin="Silk" AutoPostBack="true" OnTabClick="rtsCenterOptions_TabClick">
        <Tabs>
            <telerik:RadTab Text="Customers" Selected="true" TabIndex="0" />
        </Tabs>
    </telerik:RadTabStrip>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadMultiPage runat="server" ID="rmpCenterOptions" RenderSelectedPageOnly="true">
        <!-- Customers Section -->
        <telerik:RadPageView ID="rpvCustomers" runat="server" Selected="true" TabIndex="0">
            <telerik:RadComboBox runat="server"  ID="rcbCustomer" OnSelectedIndexChanged="rcbCustomer_SelectedIndexChanged" AutoPostBack="true" Skin="Silk">
                <Items>
                    <telerik:RadComboBoxItem Text="All Customers" Value="All" />
                    <telerik:RadComboBoxItem Text="Active Customers" Value="Active" />
                    <telerik:RadComboBoxItem Text="Customers (Recent)" Value="Recent" Selected="true" />
                </Items>
            </telerik:RadComboBox>
            <br />
            <br />
            <telerik:RadGrid runat="server" ID="rgCustomers" AllowSorting="True"
                OnNeedDataSource="rgCustomers_NeedDataSource" OnUpdateCommand="rgCustomers_UpdateCommand" OnInsertCommand="rgCustomers_InsertCommand"
                AutoGenerateColumns="False" Width="99%" Skin="Silk">
                <MasterTableView AutoGenerateColumns="False"
                    DataKeyNames="CustomerID" CommandItemDisplay="None" ShowFooter="true"
                    AlternatingItemStyle-BackColor="White" EditMode="EditForms">
                    <EditFormSettings EditFormType="WebUserControl" UserControlName="Controls/RadGrid/CustomerControl.ascx" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="Edit" Visible="false" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="27px" DataType="System.SByte" DataField="Icon">
                            <ItemTemplate>
                                <a href='<%# String.Format("Customer.aspx?CustomerID={0}", Eval("CustomerID")) %>' style="color: #4a95a1;">
                                    <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" DataValue='<%# GetIconBytes(Eval("Icon")) %>'
                                AutoAdjustImageControlSize="false" Height="25px" Width="25px" ToolTip='<%#Eval("Name", "{0} Icon") %>'
                                AlternateText='<%#Eval("Name", "{0} Icon") %>'></telerik:RadBinaryImage>
                                    </a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Name" SortExpression="Name" DataField="Name" UniqueName="Name">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbAgreementId" runat="server" ForeColor="#4a95a1"
                                    PostBackUrl='<%# "Customer.aspx?CustomerID=" + Eval("CustomerID") %>'
                                    Text='<%# Eval("Name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadTextBox runat="server" ID="rtbCName" Text='<%# Eval("Name") %>' />
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Code" HeaderText="Cust. Code"
                            SortExpression="Code" UniqueName="Code" />
                        <telerik:GridBoundColumn DataField="Number" HeaderText="FBMS No."
                            SortExpression="Number" UniqueName="Number" DataType="System.Int64" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        
    </telerik:RadMultiPage>
</asp:Content>
