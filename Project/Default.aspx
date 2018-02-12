<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<script runat="server">

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
</script>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 122px;
        }
        .style2
        {
            width: 67px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Label ID="Label2" runat="server" Text="Verify Your NRIC : " 
    Font-Bold="True"></asp:Label>
    <br />
    <asp:TextBox ID="txtNRIC" runat="server" Height="21px" Width="151px"></asp:TextBox>
    <asp:Button ID="btnNRIC" runat="server" onclick="btnNRIC_Click" Text="Verify" />
    <br />
    <asp:Label ID="lblNRIC" runat="server"></asp:Label>
    <br />
    <div style="height: 664px" runat="server" id="mainDIV">
        <table style="width:33%;">
        <tr>
            <td colspan="4">
    <asp:Label ID="Label3" runat="server" Text="Your income : " Font-Bold="True"></asp:Label>
                <br />
                <asp:TextBox ID="TextBox2" runat="server" Width="407px" 
                   ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
    <asp:Button ID="btnAmount" runat="server" onclick="btnAmount_Click" 
    Text="Amount" Width="100px" />
            </td>
            <td>
    <asp:Button ID="btnInfo" runat="server" onclick="btnInfo_Click" 
    Text="Info" Width="100px" />
            </td>
            <td class="style1">
                <asp:Button ID="btnGetTable" runat="server" onclick="btnGetTable_Click" Text="Get Tax Table" 
                    Width="100px" />
            </td>
            <td class="style1">
                <asp:Button ID="btnTranslate" runat="server" onclick="btnTranslate_Click" Text="Translate" 
                    Width="100px" />
            </td>
        </tr>
        </table>
    <asp:TextBox ID="TextBox1" runat="server" Height="98px" 
        ontextchanged="TextBox1_TextChanged" TextMode="MultiLine" 
    Width="506px" Enabled="False"></asp:TextBox>
            <asp:Label ID="lblTranslate" runat="server"></asp:Label>
            <table style="width:54%;" id="table1" runat="server" border="2">
                <tr>
                    <th>Income Start Point</th>
                    <th>Income End Point</th>
                    <th>Gross Tax Payable</th>
                    <th class="style2">Tax Rate</th>
                </tr>
            </table>
    <br />
    <asp:DropDownList ID="DropDownList1" runat="server">
    </asp:DropDownList>
    <asp:Button ID="btnRange" runat="server" onclick="btnRange_Click" Text="Go" 
        Width="65px" />
        <br />
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </div>
    <br />
            </asp:Content>
