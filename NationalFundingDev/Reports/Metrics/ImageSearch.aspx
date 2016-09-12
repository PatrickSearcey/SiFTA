<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageSearch.aspx.cs" Inherits="NationalFundingDev.Reports.Metrics.ImageSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager runat="server" ID="rsm" />
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
    </form>
</body>
</html>
