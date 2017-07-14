<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Main.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="NationalFundingDev.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    Center Administration
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cphStyles" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAJAXManager" runat="server">
    <telerik:RadAjaxManager runat="server" ID="ram">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtsCustomerOptions">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rtsAdminOptions" />
                    <telerik:AjaxUpdatedControl ControlID="rmpAdminOptions" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="ralp" runat="server" Skin="Silk" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphImage" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphInformation" runat="server">
    <h2>Center Administration</h2>
    <a href='<%= String.Format("Center.aspx?OrgCode={0}", center.OrgCode) %>' style="color: orange;">Center Home</a> >> Coop Funding<br />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSidePanel" runat="server">
    <h2><asp:Literal runat="server" ID="ltlTitle" /></h2>
    <telerik:RadTabStrip runat="server" ID="rtsAdminOptions" MultiPageID="rmpCustomerOptions" Skin="Silk" Width="100%" Orientation="HorizontalTop" AutoPostBack="true" OnTabClick="rtsAdminOptions_TabClick">
        <Tabs>
            <telerik:RadTab Text="Center Information" Selected="true" TabIndex="0" />
            <telerik:RadTab Text="User Access" TabIndex="1" />
            <telerik:RadTab Text="Directors" TabIndex="2" />
            <telerik:RadTab Text="Financial Reviewers" TabIndex="3" />
            <telerik:RadTab Text="Collection Codes" TabIndex="4" />
            <telerik:RadTab Text="Center Custom Accounts" TabIndex="5" />
            <telerik:RadTab Text="Center Accounts" TabIndex="6" Visible="false" />
        </Tabs>
    </telerik:RadTabStrip>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadMultiPage runat="server" ID="rmpAdminOptions" RenderSelectedPageOnly="true">
        <!-- Center Information -->
        <telerik:RadPageView ID="rpvCenterInfo" runat="server" Selected="true" TabIndex="0">
            <div class="SectionContent">
                <table style="width: 100%;">
                    <tr>
                        <td align="right" style="width: 150px;">Address:</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="rtbAddress" Skin="Silk" EmptyMessage="5555 StreetName Dr." Width="90%" /></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px;">City:</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="rtbCity" Skin="Silk" EmptyMessage="Cityberg" Width="90%" /></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px;">State:</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="rtbState" Skin="Silk" EmptyMessage="Tx" Width="90%" /></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px;">Zip Code:</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="rtbZipCode" Skin="Silk" EmptyMessage="78641" Width="90%" /></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px;">DUNS:</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="rtbDUNS" Skin="Silk" EmptyMessage="128821266" Width="90%" /></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px;">Project Number:</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="rtbProjectNumber" Skin="Silk" EmptyMessage="SJ009ME" Width="90%" /></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 150px;">Project Name:</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="rtbProjectName" Skin="Silk" EmptyMessage="Texas Data Collection Program" Width="90%" /></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="left" style="padding-top: 10px;">
                            <telerik:RadButton runat="server" ID="rbSubmitCenterInformation" Skin="Silk" Text="Submit" AutoPostBack="true" OnClick="rbSubmitCenterInformation_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </telerik:RadPageView>
        <!-- User Access -->
        <telerik:RadPageView ID="rpvUserAccess" runat="server" TabIndex="1">
            <telerik:RadAjaxPanel runat="server" ID="rapRGUserAccess" LoadingPanelID="ralp">
                Click on any entry to edit one or more fields in one or more rows. Click Save Changes when you are finished editing.
                <telerik:RadGrid runat="server" ID="rgUserAccess" OnNeedDataSource="rgUserAccess_NeedDataSource" OnInsertCommand="rgUserAccess_InsertCommand" OnUpdateCommand="rgUserAccess_UpdateCommand" OnDeleteCommand="rgUserAccess_DeleteCommand" AutoGenerateColumns="false">
                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" />
                    <MasterTableView CommandItemDisplay="Top" EditMode="Batch" BatchEditingSettings-EditType="Row" DataKeyNames="EmployeeAccessID">
                        <CommandItemSettings ShowRefreshButton="false" AddNewRecordText="Add New Record" SaveChangesText="Save Changes" CancelChangesText="Cancel Changes" />
                        <Columns>
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="User ID" DataField="EmployeeID" UniqueName="EmployeeID" />
                            <telerik:GridCheckBoxColumn DataType="System.Boolean" HeaderText="Center Admin" DataField="CanViewAdminPortal" UniqueName="CanViewAdminPortal" />
                            <telerik:GridCheckBoxColumn DataType="System.Boolean" HeaderText="Create Records" DataField="CanInsertRecords" UniqueName="CanInsertRecords" />
                            <telerik:GridCheckBoxColumn DataType="System.Boolean" HeaderText="Update Records" DataField="CanUpdateRecords" UniqueName="CanUpdateRecords" />
                            <telerik:GridCheckBoxColumn DataType="System.Boolean" HeaderText="Delete Records" DataField="CanDeleteRecords" UniqueName="CanDeleteRecords" />
                            <telerik:GridButtonColumn ButtonType="ImageButton"
                        CommandName="Delete" Text="Remove" UniqueName="DeleteSiteFunding" HeaderStyle-Width="25px"  />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <br />
                <p style="color:red;"><asp:Literal runat="server" ID="ltlUserAccessErrors"   /></p>
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
        <!-- Directors -->
        <telerik:RadPageView ID="rpvDirectors" runat="server" TabIndex="2">
            <telerik:RadAjaxPanel runat="server" ID="rapRGDirectors" LoadingPanelID="ralp">
                Click on any entry to edit one or more fields in one or more rows. Click Save Changes when you are finished editing.
                <telerik:RadGrid runat="server" ID="rgDirectors" OnNeedDataSource="rgDirectors_NeedDataSource" OnInsertCommand="rgDirectors_InsertCommand" OnUpdateCommand="rgDirectors_UpdateCommand" OnDeleteCommand="rgDirectors_DeleteCommand" AutoGenerateColumns="false">
                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" />
                    <MasterTableView CommandItemDisplay="Top" EditMode="Batch" BatchEditingSettings-EditType="Row" DataKeyNames="CenterDirectorID">
                        <CommandItemSettings ShowRefreshButton="false" AddNewRecordText="Add New Record" SaveChangesText="Save Changes" CancelChangesText="Cancel Changes" />
                        <Columns>
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Name" DataField="Name" UniqueName="Name" />
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Title" DataField="Title" UniqueName="Title" />
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Phone" DataField="Phone" UniqueName="Phone" />
                            <telerik:GridButtonColumn ButtonType="ImageButton"
                        CommandName="Delete" Text="Remove" UniqueName="DeleteSiteFunding" HeaderStyle-Width="25px"  />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <br />
                <p style="color:red;"><asp:Literal runat="server" ID="Literal1"   /></p>
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
        <!-- Financial Reviewers -->
        <telerik:RadPageView ID="rpvFinancialReviewers" runat="server" TabIndex="3">
            <telerik:RadAjaxPanel runat="server" ID="rapRGFinancialReviewers" LoadingPanelID="ralp">
                Click on any entry to edit one or more fields in one or more rows. Click Save Changes when you are finished editing.
                <telerik:RadGrid runat="server" ID="rgFinancialReviewers" OnNeedDataSource="rgFinancialReviewers_NeedDataSource" OnInsertCommand="rgFinancialReviewers_InsertCommand" OnUpdateCommand="rgFinancialReviewers_UpdateCommand" OnDeleteCommand="rgFinancialReviewers_DeleteCommand" AutoGenerateColumns="false">
                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" />
                    <MasterTableView CommandItemDisplay="Top" EditMode="Batch" BatchEditingSettings-EditType="Row" DataKeyNames="CenterFinancialReviewerID">
                        <CommandItemSettings ShowRefreshButton="false" AddNewRecordText="Add New Record" SaveChangesText="Save Changes" CancelChangesText="Cancel Changes" />
                        <Columns>
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Name" DataField="Name" UniqueName="Name" />
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Phone" DataField="Phone" UniqueName="Phone" />
                            <telerik:GridButtonColumn ButtonType="ImageButton"
                        CommandName="Delete" Text="Remove" UniqueName="DeleteSiteFunding" HeaderStyle-Width="25px"  />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <br />
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
        <!-- Collection Codes -->
        <telerik:RadPageView ID="rpvCollectionCodes" runat="server" TabIndex="4">
            <telerik:RadAjaxPanel runat="server" ID="rapRGCollectionCodes" LoadingPanelID="ralp">
                Click on any entry to edit one or more fields in one or more rows. Click Save Changes when you are finished editing.
                <telerik:RadGrid runat="server" ID="rgCollectionCodes" OnNeedDataSource="rgCollectionCodes_NeedDataSource" OnInsertCommand="rgCollectionCodes_InsertCommand" OnUpdateCommand="rgCollectionCodes_UpdateCommand" AutoGenerateColumns="false">
                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" />
                    <MasterTableView CommandItemDisplay="Top" EditMode="Batch" BatchEditingSettings-EditType="Row" DataKeyNames="CollectionCodeID">
                        <CommandItemSettings ShowRefreshButton="false" AddNewRecordText="Add New Record" SaveChangesText="Save Changes" CancelChangesText="Cancel Changes" />
                        <Columns>
                            <telerik:GridTemplateColumn DataType="System.String" HeaderText="Category" DataField="Category" UniqueName="Category" HeaderStyle-Width="85px" >
                                <ItemTemplate>
                                    <%# Eval("Category") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadDropDownList runat="server" ID="CategoryIDDropDown" >
                                        <Items>
                                            <telerik:DropDownListItem Text="clim" Value="clim" />
                                            <telerik:DropDownListItem Text="gw" Value="gw" />
                                            <telerik:DropDownListItem Text="misc" Value="misc" />
                                            <telerik:DropDownListItem Text="gw" Value="gw" />
                                            <telerik:DropDownListItem Text="sed" Value="sed" />
                                            <telerik:DropDownListItem Text="sw" Value="sw" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Code" DataField="Code" UniqueName="Code" HeaderStyle-Width="150px" />
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Description" DataField="Description" UniqueName="Description" />
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Remarks" DataField="Remarks" UniqueName="Remarks" />
                            <telerik:GridCheckBoxColumn DataType="System.Boolean" HeaderText="Active" DataField="Active" UniqueName="Active" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <br />
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
        <!-- Coop Accounts -->
        <telerik:RadPageView runat="server" ID="rpvCoopAccounts" TabIndex="5" >
            <telerik:RadAjaxPanel runat="server" ID="rapCoopAccounts" LoadingPanelID="ralp">
                Click on any entry to edit one or more fields in one or more rows. Click Save Changes when you are finished editing.
                <telerik:RadGrid runat="server" ID="rgCoopAccounts" OnNeedDataSource="rgCoopAccounts_NeedDataSource" OnInsertCommand="rgCoopAccounts_InsertCommand" OnUpdateCommand="rgCoopAccounts_UpdateCommand" OnDeleteCommand="rgCoopAccounts_DeleteCommand" AutoGenerateColumns="false">
                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" />
                    <MasterTableView CommandItemDisplay="Top" EditMode="Batch" BatchEditingSettings-EditType="Row" DataKeyNames="CenterSpecificAccountID">
                        <CommandItemSettings ShowRefreshButton="false" AddNewRecordText="Add New Record" SaveChangesText="Save Changes" CancelChangesText="Cancel Changes" />
                        <Columns>
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Number" DataField="AccountNumber" UniqueName="AccountNumber" HeaderStyle-Width="150px" />
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Name" DataField="AccountName" UniqueName="AccountName" />
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Office Code" DataField="OfficeCode" UniqueName="OfficeCode" />
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Administrator Name (BASIS)" DataField="ManagerName" UniqueName="ManagerName" />
                                
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <br />
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="rpvAccounts" TabIndex="6" Visible="false" >
            <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1" LoadingPanelID="ralp">
                Click on any entry to edit one or more fields in one or more rows. Click Save Changes when you are finished editing.
                <telerik:RadGrid runat="server" ID="rgCenterAccounts" OnNeedDataSource="rgCenterAccounts_NeedDataSource" OnInsertCommand="rgCenterAccounts_InsertCommand" OnUpdateCommand="rgCenterAccounts_UpdateCommand" AutoGenerateColumns="false">
                    <HeaderStyle BackColor="#696f74" Font-Bold="true" ForeColor="White" Font-Size="Small" />
                    <MasterTableView CommandItemDisplay="Top" EditMode="Batch" BatchEditingSettings-EditType="Row" DataKeyNames="AccountID">
                        <CommandItemSettings ShowRefreshButton="false" AddNewRecordText="Add New Record" SaveChangesText="Save Changes" CancelChangesText="Cancel Changes" />
                        <Columns>
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Number" DataField="AccountNumber" UniqueName="AccountNumber" HeaderStyle-Width="150px" />
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Name" DataField="AccountName" UniqueName="AccountName" />
                            <telerik:GridBoundColumn DataType="System.String" HeaderText="Office Code" DataField="OfficeCode" UniqueName="OfficeCode" />
                                
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <br />
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
