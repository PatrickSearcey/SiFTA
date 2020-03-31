using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;
namespace NationalFundingDev
{
    public partial class CenterReport : System.Web.UI.Page
    {
        private SiftaDBDataContext siftaDB = new SiftaDBDataContext();
        private Center center;
        public User user;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Grab the Center
            center = siftaDB.Centers.FirstOrDefault(p => p.OrgCode == Request.QueryString["OrgCode"]);
            //If the center doesn't exist return them to the default page
            if (center == null) Response.Redirect("Default.aspx".AppendBaseURL());
            //Set the Session variable Title 
            Session["Title"] = center.Name.Replace(" Water Science Center", " ");
            //Set the title
            ltlTitle.Text = String.Format("{0} Report", center.Name);
            if(!IsPostBack)
            {
                rdpFOStart.SelectedDate = DateTime.Now;
                rdpAgreementStatusEndDate.SelectedDate = DateTime.Now;
                rdpUnfundedRTSitesDate.SelectedDate = DateTime.Now;
                rdpAgreementDifference.SelectedDate = DateTime.Now;
                rdpAgreementsMissingDocuments.SelectedDate = DateTime.Now;
            }
        }

        #region Tab Clicking
        protected void rtsCenterReportOptions_TabClick(object sender, RadTabStripEventArgs e)
        {
            if (e.Tab.Tabs.Count > 0)
            {
                //make the first child selected
                e.Tab.Tabs.First().Selected = true;
                //Set the multipage to show the selected childs tabindex
                rmpCenterReportOptions.SelectedIndex = e.Tab.Tabs.First().TabIndex;
            }
            else
            {
                //Set the multipage to show the selected Page
                rmpCenterReportOptions.SelectedIndex = e.Tab.TabIndex;
            }
            DataRebind();
        }
        private void DataRebind()
        {
            switch(rmpCenterReportOptions.SelectedIndex)
            {
                case 0:
                    rgUnfundedSites.Rebind();
                    break;
                case 1:
                    rgFundingOverview.Rebind();
                    break;
                case 2:
                    rgContacts.Rebind();
                    break;
                case 3:
                    rgCollectionCodes.Rebind();
                    break;
                case 4:
                    rgCustomersMissingIcons.Rebind();
                    break;
                case 5:
                    rgAgreementsMissingDocuments.Rebind();
                    break;
                case 6:
                    rgAgreementStatus.Rebind();
                    break;
                case 7:
                    rgAgreementDifference.Rebind();
                    break;
            }
        }
        #endregion

        #region Unfunded Real-Time Site
        protected void rgUnfundedSites_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DateTime dt;
            if (rdpUnfundedRTSitesDate.SelectedDate != null) dt = Convert.ToDateTime(rdpUnfundedRTSitesDate.SelectedDate); else dt = DateTime.Now;
            try
            {
                Dictionary<String, String> fundedSites = siftaDB.vSiteFundings.Where(p => p.EndDate >= dt).Select(p => p.SiteNumber).Distinct().ToDictionary(p => p.Trim(), p => p.Trim());
                var rtSites = siftaDB.Sites.Where(p => p.OrgCode == center.OrgCode && p.RealTime == true).ToList();
                var finalSites = rtSites.Where(p => !fundedSites.ContainsKey(p.SiteNumber));
                rgUnfundedSites.DataSource = finalSites;
                ltlRTSiteUnfundedNumber.Text = finalSites.Count().ToString();
            }
            catch(Exception ex)
            {
                rgUnfundedSites.DataSource = new List<string>();
                ltlRTSiteUnfundedNumber.Text = "0";
            }
            
        }

        protected void rdpUnfundedRTSitesDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            rgUnfundedSites.Rebind();
        }

        protected void btnViewMap_Click(object sender, EventArgs e)
        {
            Response.Redirect("../National/RealTimeSitesFunding.aspx");
        }
        #endregion

        #region Funding Overview
        protected void rbFundingOverviewDownload_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("Documents/Download.aspx?Type=ReportFundingOverview&OrgCode={0}", center.OrgCode).AppendBaseURL());
        }
        protected void rgFundingOverview_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var data = siftaDB.vReportFundingOverviews.Where(p => p.OrgCode == center.OrgCode);
            if(rdpFOStart.SelectedDate > rdpFOEnd.SelectedDate)
            {
                rdpFOEnd.SelectedDate = rdpFOStart.SelectedDate;
            }
            if(rdpFOEnd.SelectedDate != null)
            {
                data = data.Where(p => p.EndDate <= rdpFOEnd.SelectedDate);
            }
            if(rdpFOStart.SelectedDate != null)
            {
                data = data.Where(p => p.EndDate >= rdpFOStart.SelectedDate);
            }
            rgFundingOverview.DataSource = data.ToList();
            ltlAgreementsEnding.Text = data.Count().ToString();
        }
        protected void agreementEndingChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            rgFundingOverview.Rebind();
        }
        #endregion

        #region Contacts
        protected void rgContacts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgContacts.DataSource = siftaDB.vCustomerContacts.Where(p => p.OrgCode == center.OrgCode).OrderBy(p => p.CustomerName);
        }
        #endregion

        #region Collection Codes
        protected void rgCollectionCodes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgCollectionCodes.DataSource = siftaDB.vReportCollectionCodes.Where(p => p.OrgCode == center.OrgCode).OrderBy(p => p.ProperCategory).OrderBy(p => p.ProperName);
        }
        #endregion

        #region Exceptions

        #region Missing Icons
        protected void rgCustomersMissingIcons_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgCustomersMissingIcons.DataSource = siftaDB.Customers.Where(p => p.OrgCode == center.OrgCode && p.Icon == null);
        }
        #endregion

        #region Agreements Missing Documents
        protected void rdpAgreementsMissingDocuments_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            rgAgreementsMissingDocuments.Rebind();
        }
        protected void rgAgreementsMissingDocuments_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //Holds all the agreements that don't have documents
            var ds = new Dictionary<int, String>();
            //Grabs all the JFA agreements for this center
            var dt = rdpAgreementsMissingDocuments.SelectedDate;
            if (dt == null) dt = new DateTime(1800, 1, 25);
            var agreements = siftaDB.vReportAgreementDocuments.Where(p => p.Type != "FED" && p.OrgCode == center.OrgCode && p.EndDate >= dt).ToDictionary(p => p.AgreementID, p => p.PurchaseOrderNumber);
            //THe base path for all Documents
            foreach(var agreement in agreements)
            {
                var path = String.Format("D:\\siftaroot\\Documents\\Agreements\\{0}\\{1}.pdf", agreement.Key, agreement.Value);
                if(!File.Exists(path))
                {
                    ds.Add(agreement.Key, agreement.Value);
                }
            }
            rgAgreementsMissingDocuments.DataSource = ds;
        }
        #endregion

        #region Agreement Status
        protected void rdpAgreementStatusEndDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            rgAgreementStatus.Rebind();
        }
        protected void rgAgreementStatus_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var status = siftaDB.vAgreementStatus.Where(p => p.Request == null || p.ReviewReturn == null || p.ReviewSent == null || p.SignCustomerDate == null || p.SignUSGSDate == null).Where(p=>p.OrgCode == center.OrgCode).ToList();
            if (rdpAgreementStatusEndDate.SelectedDate != null) status = status.Where(p => Convert.ToDateTime(p.EndDate) >= rdpAgreementStatusEndDate.SelectedDate).ToList();
            rgAgreementStatus.DataSource = status;
        }
        protected void btnClearFilters_Click(object sender, EventArgs e)
        {
            foreach(GridColumn column in rgAgreementStatus.MasterTableView.OwnerGrid.Columns)
            {
                column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                column.CurrentFilterValue = string.Empty;
            }
            rgAgreementStatus.MasterTableView.FilterExpression = string.Empty;
            rgAgreementStatus.Rebind();
        }
        #endregion

        #region Agreement Difference
        protected void rbRefreshAgreementDifference_Click(object sender, EventArgs e)
        {
            rgAgreementDifference.Rebind();
        }
        protected void rdpAgreementDifference_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            rgAgreementDifference.Rebind();
        }
        protected void rgAgreementDifference_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var ds = siftaDB.vAgreementModDifferences.Where(p => p.OrgCode == center.OrgCode).Where(p => p.CustomerDifference != 0 || p.USGSCMFDifference != 0 || p.OtherDifference != 0).OrderBy(p=>p.Name).ToList();
            if (rdpAgreementDifference.SelectedDate != null) ds = ds.Where(p => p.EndDate >= rdpAgreementDifference.SelectedDate).ToList();
            rgAgreementDifference.DataSource = ds;
        }
        protected void rgAgreementDifference_ItemDataBound(object sender, GridItemEventArgs e)
        {
            var ColumnNames = new List<String>() { "USGS", "Customer", "Other", "Total" };
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                //Iterate throught the columns
                foreach (string ColumnName in ColumnNames)
                {
                    Double val;
                    if (Double.TryParse(item[ColumnName].Text.Replace("$", "").Replace(",", "").Replace("(", "-").Replace(")", ""), out val))
                    {
                        //The value is higher, Recorded > Allocated
                        if (val > 0)
                        {
                            item[ColumnName].BackColor = System.Drawing.Color.FromName("#FFCEC2");
                        }
                        //The value is lower, Allocated > Recorded
                        if (val < 0)
                        {
                            item[ColumnName].BackColor = System.Drawing.Color.FromName("#FFFFAD");
                        }
                    }
                }
            }
        }
        protected void rgAgreementDifference_PreRender(object sender, EventArgs e)
        {
            var ColumnNames = new List<String>() { "USGS", "Customer", "Other", "Total" };
            foreach(GridDataItem item in rgAgreementDifference.Items)
            {
                //Iterate throught the columns
                foreach (string ColumnName in ColumnNames)
                {
                    Double val;
                    if (Double.TryParse(item[ColumnName].Text.Replace("$", "").Replace(",", "").Replace("(", "-").Replace(")", ""), out val))
                    {
                        //The value is higher, Recorded > Allocated
                        if (val > 0)
                        {
                            item[ColumnName].BackColor = System.Drawing.Color.FromName("#FFCEC2");
                        }
                        //The value is lower, Allocated > Recorded
                        if (val < 0)
                        {
                            item[ColumnName].BackColor = System.Drawing.Color.FromName("#FFFFAD");
                        }
                    }
                }
            }
        }
        #endregion
        #endregion

        #region Inline Code
        public string AppendBaseURL(String path)
        {
            return path.AppendBaseURL();
        }
        public String FormatPhone(object obj)
        {
            if (obj == null) return "";
            return obj.ToString().ToPhoneFormat();
        }
        public String DateLink(object agreementID, object date, int target)
        {
            //If the date isn't null set it to the date and return it
            if (date != null)
            {
                return String.Format("<b>{0}</b>", Convert.ToDateTime(date).ToString("d"));
            }
            else //The date is null return an add as a link with the proper link
            {
                var url = String.Format("Agreement.aspx?AgreementID={0}&Selected={1}", agreementID.ToString(), target).AppendBaseURL();
                return String.Format("<center><a style='color: #4a95a1;' href='{0}' target='_blank'>Add</a></center>", url);
            }  
        }
        #endregion




        /*
        #region Cooperative Funding
        protected void rgCoopFunding_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var ds = siftaDB.spCooperativeFunding(center.OrgCode, rsbCoopFunding.Text).OrderByDescending(p => p.StartDate);
            rgCoopFunding.DataSource = ds;
        }

        protected void rgCoopFunding_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            if (e.DetailTableView.Name == "Accounts")
            {
                var AgreementID = Convert.ToInt32(dataItem.GetDataKeyValue("AgreementID"));
                e.DetailTableView.DataSource = siftaDB.CooperativeFundings.Where(p => p.AgreementID == AgreementID).OrderBy(p => p.FiscalYear);
            }
        }

        protected void rgCoopFunding_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //Add metrics
            var metric = new MetricHandler(center.OrgCode, null, null, MetricType.RecordAdded, "Cooperative Funding", "Cooperative Funding Added");
            metric.SubmitChanges();
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            GridDataItem parentItem = (GridDataItem)e.Item.OwnerTableView.ParentItem;
            var AgreementID = Convert.ToInt32(parentItem.GetDataKeyValue("AgreementID"));
            var cf = new CooperativeFunding();
            GrabCooperativeFundingValuesFromForm(ref cf, userControl);
            cf.AgreementID = AgreementID;
            cf.CreatedBy = user.ID;
            cf.CreatedDate = DateTime.Now;
            siftaDB.CooperativeFundings.InsertOnSubmit(cf);
            siftaDB.SubmitChanges();
        }

        protected void rgCoopFunding_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            GridDataItem parentItem = (GridDataItem)e.Item.OwnerTableView.ParentItem;
            GridEditableItem editedItem = (GridEditableItem)e.Item;
            var CooperativeFundingID = Convert.ToInt32(editedItem.GetDataKeyValue("CooperativeFundingID"));
            //Add metrics
            var metric = new MetricHandler(center.OrgCode, null, null, MetricType.RecordUpdate, "Cooperative Funding", String.Format("Updated CooperativeFundingID = {0}", CooperativeFundingID));
            metric.SubmitChanges();
            var cf = siftaDB.CooperativeFundings.FirstOrDefault(p => p.CooperativeFundingID == CooperativeFundingID);
            GrabCooperativeFundingValuesFromForm(ref cf, userControl);
            siftaDB.SubmitChanges();
        }
        private void GrabCooperativeFundingValuesFromForm(ref CooperativeFunding cf, UserControl control)
        {
            var rntbFiscalYear = (RadNumericTextBox)control.FindControl("rntbFiscalYear");
            var rcbMod = (RadComboBox)control.FindControl("rcbMod");
            var rcbAccount = (RadComboBox)control.FindControl("rcbAccount");
            var rntbUSGS = (RadNumericTextBox)control.FindControl("rntbUSGS");
            var rntbCooperator = (RadNumericTextBox)control.FindControl("rntbCooperator");
            var rcbStatus = (RadComboBox)control.FindControl("rcbStatus");
            var rtbRemarks = (RadTextBox)control.FindControl("rtbRemarks");

            var mod = siftaDB.AgreementMods.FirstOrDefault(p => p.AgreementModID.ToString() == rcbMod.SelectedValue);
            cf.ModNumber = mod.Number;
            cf.AgreementModID = mod.AgreementModID;
            cf.FiscalYear = Convert.ToInt32(rntbFiscalYear.Value);
            cf.AccountNumber = rcbAccount.Text;
            cf.FundingUSGSCMF = Convert.ToDouble(rntbUSGS.Value);
            cf.FundingCustomer = Convert.ToDouble(rntbCooperator.Value);
            cf.Status = rcbStatus.SelectedValue;
            cf.Remarks = rtbRemarks.Text;
            cf.ModifiedBy = user.ID;
            cf.ModifiedDate = DateTime.Now;
        }
        protected void rbShowAll_Click(object sender, EventArgs e)
        {
            rsbCoopFunding.Text = "";
            rgCoopFunding.Rebind();
        }

        protected void rsbCoopFunding_Search(object sender, SearchBoxEventArgs e)
        {
            rgCoopFunding.Rebind();
        }

        protected void rbViewReport_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("Reports/Center/CoopFunding.aspx?OrgCode={0}", center.OrgCode).AppendBaseURL());
        }
        #endregion
        */

        /*
         
        
        <!-- Cooperative Funding -->
        <!-- <telerik:RadPageView runat="server" ID="rpvCooperativeFunding" Visible="false">
            <table>
                <tr>
                    <td>
                        <telerik:RadSearchBox Width="250px" runat="server" ID="rsbCoopFunding" OnSearch="rsbCoopFunding_Search" Skin="Silk" DropDownSettings-Height="0px" />
                    </td>
                    <td>
                        <telerik:RadButton runat="server" ID="rbShowAll" Text="Show All"
                            Skin="Silk" OnClick="rbShowAll_Click" /> <telerik:RadButton runat="server" ID="rbViewReport" Text="View Report"
                            Skin="Silk" OnClick="rbViewReport_Click" /> 
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="rgCoopFunding" runat="server" Width="100%"
                AutoGenerateColumns="False" Skin="Silk" AllowPaging="true" 
                PageSize="10" AllowMultiRowSelection="False" OnNeedDataSource="rgCoopFunding_NeedDataSource" AllowSorting="true" OnDetailTableDataBind="rgCoopFunding_DetailTableDataBind" OnInsertCommand="rgCoopFunding_InsertCommand" OnUpdateCommand="rgCoopFunding_UpdateCommand">
                <PagerStyle PageSizes="10" Mode="NextPrevAndNumeric" />
                <ItemStyle />
                <MasterTableView Width="100%" DataKeyNames="AgreementID" AllowMultiColumnSorting="True">
                    <CommandItemSettings ShowRefreshButton="false" />
                    <GroupHeaderItemStyle ForeColor="Black" />
                    <DetailTables>
                        <telerik:GridTableView AllowSorting="true" InsertItemDisplay="Bottom"  CommandItemDisplay="Top" ShowGroupFooter="true" ShowFooter="true" DataKeyNames="CooperativeFundingID"
                            Name="Accounts" Width="100%" EditMode="EditForms" AllowPaging="false">
                            <EditFormSettings UserControlName="Controls/RadGrid/CoopFundingControl.ascx" EditFormType="WebUserControl" />
                            <HeaderStyle BackColor="LightBlue" ForeColor="Black"  Font-Bold="true" />
                            <CommandItemSettings AddNewRecordText="Add New Account" ShowRefreshButton="false"  />
                            <Columns>
                                <telerik:GridButtonColumn ButtonType="ImageButton" HeaderStyle-BackColor="#acdbe3" HeaderStyle-Font-Size="Small"
                                    CommandName="Edit" Text="Edit" UniqueName="Edit" />
                                <telerik:GridBoundColumn HeaderText="Fiscal Year" DataField="FiscalYear" SortExpression="FiscalYear" AllowSorting="true" />
                                <telerik:GridTemplateColumn HeaderText="Mod" DataField="ModNumber" >
                                    <ItemTemplate>
                                        <%# (Eval("ModNumber").ToString() == "0") ? "" : String.Format("{0}", Eval("ModNumber")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Account" DataField="AccountNumber" FooterAggregateFormatString="Agreement Total" FooterStyle-HorizontalAlign="Right" Aggregate="Count" />
                                <telerik:GridBoundColumn HeaderText="USGS" DataField="FundingUSGSCMF" DataFormatString="{0:c2}" Aggregate="Sum" />
                                <telerik:GridBoundColumn HeaderText="Cooperator" DataField="FundingCustomer" DataFormatString="{0:c2}" Aggregate="Sum" />
                                <telerik:GridBoundColumn HeaderText="Status" DataField="Status" />
                                <telerik:GridBoundColumn HeaderText="Remarks" DataField="Remarks" />
                                <telerik:GridBoundColumn HeaderText="Last Edited" DataField="ModifiedDate" DataFormatString="{0:d}" />
                                <telerik:GridBoundColumn HeaderText="Edited By" DataField="ModifiedBy" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                    <Columns>
                        <telerik:GridTemplateColumn DataField="Code" HeaderText="Customer" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e5e4e4">
                            <ItemTemplate>
                                <a style="color: #2dabc1" title='<%# Eval("Name")  %>' href='<%# string.Format("Customer.aspx?CustomerID={0}",Eval("CustomerID")) %>'><%# Eval("Code")%></a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Number" HeaderText="Number" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#e5e4e4" />
                        <telerik:GridTemplateColumn DataField="PurchaseOrderNumber" HeaderText="Agreement">
                            <ItemTemplate>
                                <a style="color: #2dabc1" href='<%# string.Format("Agreement.aspx?AgreementID={0}",Eval("AgreementID")) %>'><%# Eval("PurchaseOrderNumber")%></a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="MatchPair" HeaderText="Match Pair" ItemStyle-HorizontalAlign="Center"  />
                        <telerik:GridBoundColumn DataField="SalesDocument" HeaderText="Sales Order" ItemStyle-HorizontalAlign="Center"  />
                        <telerik:GridBoundColumn DataField="StartDate" DataFormatString="{0:d}" HeaderText="Start"
                            ItemStyle-HorizontalAlign="Center"  />
                        <telerik:GridBoundColumn DataField="EndDate" DataFormatString="{0:d}" HeaderText="End"  />
                        <telerik:GridBoundColumn DataField="SignUSGSDate" DataFormatString="{0:d}" HeaderText="USGS Sign"/>
                        <telerik:GridBoundColumn DataField="SignCustomerDate" DataFormatString="{0:d}" HeaderText="Cust. sign"  />
                        <telerik:GridBoundColumn DataField="FundsType" HeaderText="Fund Type"  />
                        <telerik:GridBoundColumn DataField="BillingCycleFrequency" HeaderText="Cycle"  />
                    </Columns>
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="Name" FormatString="{0}"  />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="Name" FormatString="{0}"  />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
             -->
          
         
         */

    }
}