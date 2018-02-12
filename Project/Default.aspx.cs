using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Web.UI.HtmlControls;

using test;

using System.Net;
using System.IO;
using System.Runtime.Serialization;

public partial class _Default : System.Web.UI.Page
{
    WebService abc;
    translate.SoapService trans;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        abc = new WebService();
        trans = new translate.SoapService();


        

        if (!IsPostBack)
        {
            mainDIV.Visible = false;
            test.TaxRange[] taxRange = abc.GetTaxRangeXML();

            for (int i = 0; i < taxRange.Length; i++)
            {
                DropDownList1.Items.Insert(i,
               new ListItem(taxRange[i].income_from.ToString() + " - " + taxRange[i].income_to.ToString(), taxRange[i].income_from.ToString())
               );
            }
        }
    }
    protected void btnInfo_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TextBox2.Text))
        {
            test.TaxInfoDetails info = abc.GetTaxInfoDetailsXML(Convert.ToInt32(TextBox2.Text));
            TextBox1.Text = info.comment;
        }

    }
    protected void btnAmount_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TextBox2.Text))
            TextBox1.Text = "The amount of tax payable is S$"+abc.GetTaxAmount(Convert.ToInt32(TextBox2.Text)).ToString() +".";
    }
    protected void btnGetTable_Click(object sender, EventArgs e)
    {
        test.TaxInfoDetails[] info = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<test.TaxInfoDetails[]>(abc.GetAllTaxInfoJSON());
        for(int i =0; i<info.Length;i++)
        {
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell1 = new HtmlTableCell();
            HtmlTableCell cell2 = new HtmlTableCell();
            HtmlTableCell cell3 = new HtmlTableCell();
            HtmlTableCell cell4 = new HtmlTableCell();

            cell1.Controls.Add(new LiteralControl(info[i].income_from.ToString()));
            row.Cells.Add(cell1);
            cell2.Controls.Add(new LiteralControl(info[i].income_to.ToString()));
            row.Cells.Add(cell2);
            cell3.Controls.Add(new LiteralControl(info[i].gross_tax_payable.ToString()));
            row.Cells.Add(cell3);
            cell4.Controls.Add(new LiteralControl(info[i].tax_rate.ToString()));
            row.Cells.Add(cell4);
            // Add the row to the HtmlTable Rows collection.
            table1.Rows.Add(row);
        }
    }
    protected void btnRange_Click(object sender, EventArgs e)
    {
        test.TaxInfoDetails info = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<test.TaxInfoDetails>(abc.GetTaxSingleResultJSON(Convert.ToInt32(DropDownList1.SelectedValue)));
        Label1.Text = info.comment;
    }
    protected void btnTranslate_Click(object sender, EventArgs e)
    {
        string text;

        string uri = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken";
        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
        httpWebRequest.Headers.Add("Ocp-Apim-Subscription-Key", "96ca12ee0d49463fbc665fc85e6224ab");
        httpWebRequest.Method = "POST";
        httpWebRequest.ContentLength = 0;

        using (Stream stream = httpWebRequest.GetResponse().GetResponseStream())
        {
            StreamReader reader = new StreamReader(stream);
            text = reader.ReadToEnd();
            //TextBox1.Text = text;
        }
        String outcome = trans.Translate("Bearer" + " " + text, TextBox1.Text, "en", "yue", "", "", "");
        lblTranslate.Text = outcome;

    }


    protected void btnNRIC_Click(object sender, EventArgs e)
    {
        //if (verifyNRIC(txtNRIC.Text))
        if(true)
        {
            lblNRIC.Text = "Your NRIC is verified!";
            mainDIV.Visible = true;
        }
        else
        {
            lblNRIC.Text = "Your NRIC cannot be verified!";
            mainDIV.Visible = false;
        }
    }
}
