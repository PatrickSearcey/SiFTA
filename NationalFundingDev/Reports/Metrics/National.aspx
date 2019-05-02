<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Empty.Master" AutoEventWireup="true" CodeBehind="National.aspx.cs" Inherits="NationalFundingDev.Reports.Metrics.National" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    National Metrics
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStyles" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadAjaxPanel runat="server" ID="rap">
        <div style="width: 100%; padding-top: 20px; padding-left: 10px; padding-right: 10px;">
            <b>Record Time Range</b><br />
            <asp:DropDownList runat="server" ID="rcbTimeRange" AutoPostBack="true" OnTextChanged="rcbTimeRange_TextChanged">
                <asp:ListItem Text="All" Value="all" ></asp:ListItem>
                <asp:ListItem Text="Today" Value="today" Selected="true" ></asp:ListItem>
                <asp:ListItem Text="Past 7 Days" Value="7" ></asp:ListItem>
                <asp:ListItem Text="Past 30 Days" Value="30" ></asp:ListItem>
                <asp:ListItem Text="Past 90 Days" Value="90" ></asp:ListItem>
            </asp:DropDownList>
        </div>

        <span id="TableContent" runat="server">
            <br />
            <span id="TitleContent" runat="server" style="margin-left: 20px; font-weight: bolder;">
            </span>
            <br /><br />
            <span id="BaseDataContent" runat="server" style="margin-left: 20px;">
            </span>
            <br /><br />
            <span id="CenterDataContent" runat="server" style="margin-left: 20px;">
            </span>
            <br /><br />
            <span id="TotalsCountContent" runat="server" style="margin-left: 20px;">
            </span>
            <br /><br />
            <span id="DaysCountContent" runat="server" style="margin-left: 20px;">
            </span>
        </span>


        <!--
        <table style="padding-top: 20px; padding-left: 10px; padding-right: 10px;">
            <tr>
                <td>
                    <telerik:RadHtmlChart runat="server" ID="rchartTotals" Width="650px" Height="400px" Transitions="true" Skin="Silk">
                        <ChartTitle Text="User Activity">
                            <Appearance>
                                <TextStyle Bold="true" Color="#2895a8" FontSize="20px" />
                            </Appearance>
                        </ChartTitle>
                        <PlotArea>
                            <XAxis>
                                <TitleAppearance Text="Action" />
                            </XAxis>
                            <YAxis>
                                <TitleAppearance Text="Count" />
                            </YAxis>
                            <Series>
                                <telerik:ColumnSeries Name="Unique Users" Stacked="false" Gap="1" Spacing="0.2" DataFieldY="UniqueUsers">
                                    <TooltipsAppearance DataFormatString="{0} Unique Users" />
                                    <LabelsAppearance DataFormatString="{0} Users" />
                                    <Appearance>
                                        <FillStyle BackgroundColor="#036476" />
                                    </Appearance>
                                </telerik:ColumnSeries>
                                <telerik:ColumnSeries Name="Records Added" Stacked="false" DataFieldY="RecordsAdded">
                                    <TooltipsAppearance DataFormatString="{0} Records Added" />
                                    <LabelsAppearance DataFormatString="{0} Added" />
                                    <Appearance>
                                        <FillStyle BackgroundColor="#73C4D3" />
                                    </Appearance>
                                </telerik:ColumnSeries>
                                <telerik:ColumnSeries Name="Records Updated" Stacked="false" DataFieldY="RecordsUpdated">
                                    <TooltipsAppearance DataFormatString="{0} Records Updated" />
                                    <LabelsAppearance DataFormatString="{0} Updated" />
                                    <Appearance>
                                        <FillStyle BackgroundColor="#2CC84E" />
                                    </Appearance>
                                </telerik:ColumnSeries>
                                <telerik:ColumnSeries Name="Records Deleted" Stacked="false" DataFieldY="RecordsDeleted">
                                    <TooltipsAppearance DataFormatString="{0} Records Deleted" />
                                    <LabelsAppearance DataFormatString="{0} Deleted" />
                                    <Appearance>
                                        <FillStyle BackgroundColor="#FF4B38" />
                                    </Appearance>
                                </telerik:ColumnSeries>
                                <telerik:ColumnSeries Name="Agreements Copied" Stacked="false" DataFieldY="AgreementsCopied">
                                    <TooltipsAppearance DataFormatString="{0} Agreements Copied" />
                                    <LabelsAppearance DataFormatString="{0} Copied" />
                                    <Appearance>
                                        <FillStyle BackgroundColor="#FFC138" />
                                    </Appearance>
                                </telerik:ColumnSeries>
                                <telerik:ColumnSeries Name="JFA Downloads" Stacked="false" DataFieldY="JFADownloads">
                                    <TooltipsAppearance DataFormatString="{0} JFA Downloads" />
                                    <LabelsAppearance DataFormatString="{0} Downloads" />
                                    <Appearance>
                                        <FillStyle BackgroundColor="#C08400" />
                                    </Appearance>
                                </telerik:ColumnSeries>
                            </Series>
                        </PlotArea>

                    </telerik:RadHtmlChart>
                </td>
                <td>
                    <telerik:RadHtmlChart runat="server" ID="rchartUserCommunity" Height="400px" Width="550px" Legend-Appearance-Position="Right">
                        <ChartTitle Text="User Community">
                            <Appearance>
                                <TextStyle Bold="true" Color="#2895a8" FontSize="20px" />
                            </Appearance>
                        </ChartTitle>
                        <PlotArea>
                            <Series>
                                <telerik:PieSeries StartAngle="90" DataFieldY="Value" NameField="Key">
                                    <TooltipsAppearance Color="White">
                                        <ClientTemplate>
                                            #= dataItem.Value # Actions Performed by #= dataItem.Key#s
                                        </ClientTemplate>
                                    </TooltipsAppearance>
                                </telerik:PieSeries>
                            </Series>
                        </PlotArea>
                    </telerik:RadHtmlChart>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:RadHtmlChart runat="server" ID="rchartCenterParticipation" Height="400px" Width="600px">
                        <ChartTitle Text="Center Activity">
                            <Appearance>
                                <TextStyle Bold="true" Color="#2895a8" FontSize="20px" />
                            </Appearance>
                        </ChartTitle>
                        <PlotArea>
                            <Series>
                                <telerik:PieSeries StartAngle="90" DataFieldY="Value" NameField="Key">
                                    <TooltipsAppearance Color="White">
                                        <ClientTemplate>
                                            #= dataItem.Value # actions in the #= dataItem.Key#
                                        </ClientTemplate>
                                    </TooltipsAppearance>
                                </telerik:PieSeries>
                            </Series>
                        </PlotArea>
                    </telerik:RadHtmlChart>
                </td>
                <td>
                    <telerik:RadHtmlChart runat="server" ID="rchartDayBreakDown" Height="400px" Transitions="true" Skin="Silk">
                        <ChartTitle Text="Weekly Activity">
                            <Appearance>
                                <TextStyle Bold="true" Color="#2895a8" FontSize="20px" />
                            </Appearance>
                        </ChartTitle>
                        <PlotArea>
                            <YAxis>
                                <TitleAppearance Text="Count" />
                            </YAxis>
                            <Series>
                                <telerik:ColumnSeries Name="Sunday" Stacked="false" DataFieldY="Sunday" >
                                    <TooltipsAppearance DataFormatString="{0} Actions Sunday" />
                                    <LabelsAppearance DataFormatString="Sunday  ({0})" Position="Center" RotationAngle="90"  />
                                </telerik:ColumnSeries>
                            </Series>
                            <Series>
                                <telerik:ColumnSeries Name="Monday" Stacked="false" DataFieldY="Monday">
                                    <TooltipsAppearance DataFormatString="{0} Actions Monday" />
                                    <LabelsAppearance DataFormatString="Monday  ({0})" Position="Center" RotationAngle="90" />
                                </telerik:ColumnSeries>
                                </Series>
                            <Series>
                                <telerik:ColumnSeries Name="Tuesday" Stacked="false" DataFieldY="Tuesday">
                                    <TooltipsAppearance DataFormatString="{0} Actions Tuesday" />
                                    <LabelsAppearance DataFormatString="Tuesday ({0})" Position="Center" RotationAngle="90" />
                                </telerik:ColumnSeries>
                                </Series>
                            <Series>
                                <telerik:ColumnSeries Name="Wednesday" Stacked="false" DataFieldY="Wednesday">
                                    <TooltipsAppearance DataFormatString="{0} Actions Wednesday" />
                                    <LabelsAppearance DataFormatString="Wednesday  ({0})" Position="Center" RotationAngle="90" />
                                </telerik:ColumnSeries>
                                </Series>
                            <Series>
                                <telerik:ColumnSeries Name="Thursday" Stacked="false" DataFieldY="Thursday">
                                    <TooltipsAppearance DataFormatString="{0} Actions Thursday" />
                                    <LabelsAppearance DataFormatString="Thursday  ({0})" Position="Center" RotationAngle="90" />
                                </telerik:ColumnSeries>
                                </Series>
                            <Series>
                                <telerik:ColumnSeries Name="Friday" Stacked="false" DataFieldY="Friday">
                                    <TooltipsAppearance DataFormatString="{0} Actions Friday" />
                                    <LabelsAppearance DataFormatString="Friday  ({0})" Position="Center" RotationAngle="90" />
                                </telerik:ColumnSeries>
                                </Series>
                            <Series>
                                <telerik:ColumnSeries Name="Saturday" Stacked="false" DataFieldY="Saturday">
                                    <TooltipsAppearance DataFormatString="{0} Actions Saturday" />
                                    <LabelsAppearance DataFormatString="Saturday  ({0})" Position="Center" RotationAngle="90" />
                                </telerik:ColumnSeries>
                            </Series>
                        </PlotArea>
                    </telerik:RadHtmlChart>
                </td>
            </tr>
        </table>
            -->



        <br /><br />
    </telerik:RadAjaxPanel>
</asp:Content>
