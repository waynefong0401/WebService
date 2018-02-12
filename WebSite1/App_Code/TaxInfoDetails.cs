using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TaxInfoDetails
/// </summary>
public class TaxInfoDetails
{
    public int income_from { get; set; }
    public int income_to { get; set; }
    public int gross_tax_payable { get; set; }
    public double tax_rate { get; set; }
    public string comment { get; set; }
	//public TaxInfoDetails()
	//{
	//}
}
