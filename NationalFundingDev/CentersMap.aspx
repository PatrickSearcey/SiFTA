<%@ Page Title="" Language="C#" MasterPageFile="~/Themes/Base/Empty.Master" AutoEventWireup="true" CodeBehind="CentersMap.aspx.cs" Inherits="NationalFundingDev.CentersMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cphStyles" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <br />
    <center>
        <p style="font-size:medium; font-weight:bold;">Click on a state to visit its center.</p>
        <asp:ImageMap ID="imUSMap" runat="server" HotSpotMode="Navigate" ImageUrl="~/Images/Maps/usmap.png">
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="389,255,384,327,334,323,356,350,359,361,380,375,387,365,407,369,418,388,429,399,435,419,461,430,465,393,496,374,512,365,515,343,506,326,507,306,490,302,456,300,427,290,427,256" NavigateUrl="~/Center.aspx?OrgCode=GCSJ"  AlternateText="Texas" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="312,237,390,245,383,327,312,323,310,329,300,330" NavigateUrl="~/Center.aspx?OrgCode=GCRG" AlternateText="New Mexico" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="248,227,312,238,299,329,274,325,226,296,227,285,237,269,231,257,236,239,246,243" NavigateUrl="~/Center.aspx?OrgCode=GCZF" AlternateText="Arizona" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="193,129,265,146,246,241,235,239,231,252,181,177" NavigateUrl="~/Center.aspx?OrgCode=GWZJ" AlternateText="Nevada" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="145,116,145,116,193,129,181,176,232,252,232,261,237,269,229,285,227,295,192,289,190,271,182,265,155,248,157,232,147,207,143,181,137,168,140,152,135,140,141,134" NavigateUrl="~/Center.aspx?OrgCode=GWZG" AlternateText="California" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="168,53,179,57,178,64,186,67,204,68,221,69,242,74,248,80,234,99,239,102,228,137,146,116,146,100,152,93" NavigateUrl="~/Center.aspx?OrgCode=GWYF" AlternateText="Oregon" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="195,13,255,28,244,75,221,69,210,67,203,69,192,66,180,64,177,54,169,52,171,41,169,14,182,24" NavigateUrl="~/Center.aspx?OrgCode=GWYG" AlternateText="Washington" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="263,30,255,29,244,75,248,83,237,98,240,103,230,136,299,151,305,111,299,107,282,109,276,86,269,87,273,72,261,59" NavigateUrl="~/Center.aspx?OrgCode=GWYE" AlternateText="Idaho" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="266,147,299,153,297,164,319,172,313,237,249,228" NavigateUrl="~/Center.aspx?OrgCode=GCZK" AlternateText="Utah" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="406,182,321,173,313,239,403,248" NavigateUrl="~/Center.aspx?OrgCode=GCRE" AlternateText="Colorado" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="307,104,309,103,388,113,381,178,320,173,298,165" NavigateUrl="~/Center.aspx?OrgCode=GWRS" AlternateText="Wyoming-Montana" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="393,48,263,31,262,58,274,71,270,86,277,87,284,108,298,107,306,110,307,102,388,113" NavigateUrl="~/Center.aspx?OrgCode=GWRS" AlternateText="Wyoming-Montana" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="468,53,395,49,389,97,475,101" NavigateUrl="~/Center.aspx?OrgCode=GENT" AlternateText="North Dakota" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="473,156,475,142,475,112,472,106,473,102,390,99,386,145,454,149,457,152,465,150" NavigateUrl="~/Center.aspx?OrgCode=GENT" AlternateText="South Dakota" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="491,198,474,155,466,150,460,153,454,151,386,146,383,176,403,182,408,195" NavigateUrl="~/Center.aspx?OrgCode=GENR" AlternateText="Nebraska" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="499,250,500,213,492,207,496,202,491,199,406,196,405,247" NavigateUrl="~/Center.aspx?OrgCode=GCSE" AlternateText="Kansas" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="502,303,499,252,391,248,391,253,427,257,430,290,457,300" NavigateUrl="~/Center.aspx?OrgCode=GCSH" AlternateText="Oklahoma" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="556,255,501,257,502,304,508,309,508,313,548,313,545,303,563,262,555,262" NavigateUrl="~/Center.aspx?OrgCode=GEML" AlternateText="Lower Mississippi Gulf" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="514,365,513,367,515,344,506,325,507,312,548,315,554,325,542,345,569,345,572,353,576,374,553,374,540,365,531,370" NavigateUrl="~/Center.aspx?OrgCode=GEML" AlternateText="Lower Mississippi Gulf" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="586,278,589,351,573,354,570,346,544,344,553,326,546,304,559,279" NavigateUrl="~/Center.aspx?OrgCode=GEML" AlternateText="Lower Mississippi Gulf" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="620,275,588,279,590,350,602,349,601,342,636,338,633,317" NavigateUrl="~/Center.aspx?OrgCode=GEML" AlternateText="Lower Mississippi Gulf" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="651,270,622,275,636,338,639,343,676,340,680,334,684,309" NavigateUrl="~/Center.aspx?OrgCode=GEMP" AlternateText="South Atlantic Water Science Center" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="605,348,601,343,635,339,640,344,676,342,681,335,715,396,714,420,707,422,699,414,692,414,684,399,677,392,672,377,670,364,650,352,643,358,635,359,627,351" NavigateUrl="~/Center.aspx?OrgCode=GEMC" AlternateText="Caribbean-Florida" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="273,406,284,391,298,385,338,399,371,409,401,433,419,456,398,475,380,471,341,457" NavigateUrl="~/Center.aspx?OrgCode=GWZH" AlternateText="Pacific Islands" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="200,332,217,417,241,415,283,444,269,454,240,431,221,423,207,422,178,436,161,462,131,470,86,481,35,476,5,460,89,461,77,409,94,376,115,350,148,325" NavigateUrl="~/Center.aspx?OrgCode=GWWB" AlternateText="Alaska" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="685,310,654,270,661,266,685,264,699,265,715,274,706,293" NavigateUrl="~/Center.aspx?OrgCode=GEMP" AlternateText="South Atlantic Water Science Center" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="746,228,742,259,730,274,716,276,700,265,665,265,654,269,637,270,643,261,667,241" NavigateUrl="~/Center.aspx?OrgCode=GEMP" AlternateText="South Atlantic Water Science Center" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="568,253,558,280,638,272,642,264,666,240,585,248" NavigateUrl="~/Center.aspx?OrgCode=GEML" AlternateText="Lower Mississippi Gulf" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="470,54,491,45,549,64,537,79,523,87,523,100,517,103,520,120,540,139,476,142" NavigateUrl="~/Center.aspx?OrgCode=GENK" AlternateText="Upper Midwest Water Science Center" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="538,190,489,190,476,158,475,143,538,142,540,150,554,162" NavigateUrl="~/Center.aspx?OrgCode=GENE" AlternateText="Central Midwest Water Science Center" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="501,257,558,256,557,260,567,262,569,253,569,246,565,235,553,223,555,215,536,189,491,192,497,202,492,206,501,213" NavigateUrl="~/Center.aspx?OrgCode=GENE" AlternateText="Central Midwest Water Science Center" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="524,87,540,80,546,89,572,97,586,110,579,153,547,155,540,144,540,138,519,121,519,102,524,101" NavigateUrl="~/Center.aspx?OrgCode=GENK" AlternateText="Upper Midwest Water Science Center" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="579,69,544,82,550,89,573,94,581,103,589,110,593,163,632,160,646,138,630,97,604,78" NavigateUrl="~/Center.aspx?OrgCode=GENK" AlternateText="Upper Midwest Water Science Center" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="549,155,578,155,585,166,589,213,581,225,583,230,579,243,566,243,554,223,556,216,541,191,554,163" NavigateUrl="~/Center.aspx?OrgCode=GENE" AlternateText="Central Midwest Water Science Center" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="623,209,612,224,601,227,584,228,579,225,590,215,586,169,592,162,618,163" NavigateUrl="~/Center.aspx?OrgCode=GENF" AlternateText="Indiana-Kentucky-Ohio" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="571,253,571,244,578,244,586,229,615,224,623,207,635,212,643,207,653,212,661,229,645,242,581,249" NavigateUrl="~/Center.aspx?OrgCode=GENF" AlternateText="Indiana-Kentucky-Ohio" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="619,164,633,162,640,164,667,153,670,175,670,191,656,211,642,207,636,211,625,208" NavigateUrl="~/Center.aspx?OrgCode=GENF" AlternateText="Indiana-Kentucky-Ohio" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="650,242,661,230,683,224,688,202,694,205,703,190,709,188,720,193,728,201,744,229" NavigateUrl="~/Center.aspx?OrgCode=GELM" AlternateText="Virginia-West Virginia" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="671,179,671,189,659,211,657,214,665,228,682,225,687,203,694,203,704,189,690,190,688,184,676,186" NavigateUrl="~/Center.aspx?OrgCode=GELM" AlternateText="Virginia-West Virginia" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="669,152,676,147,677,151,730,139,738,146,738,158,743,165,733,177,689,184,680,186,671,179" NavigateUrl="~/Center.aspx?OrgCode=GELL" AlternateText="Pennsylvania" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="793,195,739,196,732,179,689,185,692,191,710,188,728,202,745,216,795,212" NavigateUrl="~/Center.aspx?OrgCode=GELF" AlternateText="Maryland" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="791,179,792,195,740,195,733,178,748,182,765,178" NavigateUrl="~/Center.aspx?OrgCode=GELF" AlternateText="Delaware" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="790,178,788,159,750,158,748,150,738,148,738,158,744,164,734,178,750,181,764,178" NavigateUrl="~/Center.aspx?OrgCode=GELJ" AlternateText="New Jersey" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="677,147,682,126,698,122,707,121,713,115,710,104,722,88,740,84,750,120,750,132,753,145,766,146,766,156,751,156,746,150,738,148,729,138,679,150" NavigateUrl="~/Center.aspx?OrgCode=GELK" AlternateText="New York" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="740,84,741,84,730,74,724,60,774,34,792,31,834,88,826,156,790,155,766,144,754,144,750,134,750,118" NavigateUrl="~/Center.aspx?OrgCode=GELG" AlternateText="New England" />
        <asp:PolygonHotSpot HotSpotMode="Navigate" Coordinates="527,422,578,418,625,412,660,405,668,457,640,462,569,463,522,464,501,455" NavigateUrl="~/Center.aspx?OrgCode=GEMC" AlternateText="Caribbean-Florida" />
    </asp:ImageMap>
    </center>
</asp:Content>
