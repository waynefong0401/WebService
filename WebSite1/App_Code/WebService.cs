using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Text;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class WebService : System.Web.Services.WebService {

    public WebService () {
    }

    [WebMethod]
    public TaxInfoDetails GetTaxInfoDetailsXML(int amount)
    {
        TaxInfoDetails taxInfo = new TaxInfoDetails(); 
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @".\SQLEXPRESS";
            builder.AttachDBFilename = @"|DataDirectory|Database.mdf";
            builder.IntegratedSecurity = true;
            builder.UserInstance = true;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {

                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * from income_tax where " + amount.ToString() + " between income_from and income_to -1 order by income_from");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taxInfo = new TaxInfoDetails()
                            {
                                income_from = Convert.ToInt32(reader["INCOME_FROM"]),
                                income_to = Convert.ToInt32(reader["INCOME_TO"]),
                                gross_tax_payable = Convert.ToInt32(reader["GROSS_TAX_PAYABLE"]),
                                tax_rate = Convert.ToDouble(reader["TAX_RATE"]),
                                comment = reader["COMMENT"].ToString()
                            };
                        }
                    }
                }
                connection.Close();
            }
            return taxInfo;

        }
        catch (SqlException exc)
        {
            return taxInfo;
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]  
    public String GetTaxInfoDetailsJSON(int amount)
    {
        TaxInfoDetails taxInfo = new TaxInfoDetails();
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @".\SQLEXPRESS";
            builder.AttachDBFilename = @"|DataDirectory|Database.mdf";
            builder.IntegratedSecurity = true;
            builder.UserInstance = true;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {

                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * from income_tax where " + amount.ToString() + " between income_from and income_to -1 order by income_from");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taxInfo = new TaxInfoDetails()
                            {
                                income_from = Convert.ToInt32(reader["INCOME_FROM"]),
                                income_to = Convert.ToInt32(reader["INCOME_TO"]),
                                gross_tax_payable = Convert.ToInt32(reader["GROSS_TAX_PAYABLE"]),
                                tax_rate = Convert.ToDouble(reader["TAX_RATE"]),
                                comment = reader["COMMENT"].ToString()
                            };
                        }
                    }
                }
                connection.Close();
            }
            return new JavaScriptSerializer().Serialize(taxInfo);

        }
        catch (SqlException exc)
        {
            return new JavaScriptSerializer().Serialize(taxInfo);
        }
    }

    [WebMethod]
    public string GetTaxInfo(int amount)
    {
        string returnText="";
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @".\SQLEXPRESS";
            builder.AttachDBFilename = @"|DataDirectory|Database.mdf";
            builder.IntegratedSecurity = true;
            builder.UserInstance = true;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT comment from income_tax where "+amount.ToString()+" between income_from and income_to -1 order by income_from");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnText += "\n" + reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }
            return returnText;
        }
        catch (SqlException exc)
        {
            return exc.ToString();
        }
    }

    [WebMethod]
    public double GetTaxAmount(int amount)
    {
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @".\SQLEXPRESS";
            builder.AttachDBFilename = @"|DataDirectory|Database.mdf";
            builder.IntegratedSecurity = true;
            builder.UserInstance = true;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT GROSS_TAX_PAYABLE,TAX_RATE,INCOME_FROM from income_tax where " + amount.ToString() + " between income_from and income_to -1  order by income_from");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int gross = Convert.ToInt32(reader["GROSS_TAX_PAYABLE"]);
                            double tax_rate = Convert.ToDouble(reader["TAX_RATE"]);
                            int income_from = Convert.ToInt32(reader["INCOME_FROM"]);
                            double totalTaxPayable = gross + (tax_rate*(amount-income_from));
                            return totalTaxPayable;
                        }
                    }
                }
                connection.Close();
            }
            return 0.0;
        }
        catch (SqlException exc)
        {
            return 0.0;
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]  
    public String GetAllTaxInfoJSON()
    {
        List<TaxInfoDetails> taxInfo = new List<TaxInfoDetails>();
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @".\SQLEXPRESS";
            builder.AttachDBFilename = @"|DataDirectory|Database.mdf";
            builder.IntegratedSecurity = true;
            builder.UserInstance = true;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * from income_tax order by income_from");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taxInfo.Add(new TaxInfoDetails()
                            {
                                income_from = Convert.ToInt32(reader["INCOME_FROM"]),
                                income_to = Convert.ToInt32(reader["INCOME_TO"]),
                                gross_tax_payable = Convert.ToInt32(reader["GROSS_TAX_PAYABLE"]),
                                tax_rate = Convert.ToDouble(reader["TAX_RATE"]),
                                comment = reader["COMMENT"].ToString()
                            });
                        }
                    }
                }
                connection.Close();
            }
            return new JavaScriptSerializer().Serialize(taxInfo); 
        }
        catch (SqlException exc)
        {
            return new JavaScriptSerializer().Serialize(taxInfo); 
        }
    }

    [WebMethod]
    public List<TaxInfoDetails> GetAllTaxInfoXML()
    {
        List<TaxInfoDetails> taxInfo = new List<TaxInfoDetails>();
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @".\SQLEXPRESS";
            builder.AttachDBFilename = @"|DataDirectory|Database.mdf";
            builder.IntegratedSecurity = true;
            builder.UserInstance = true;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {

                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * from income_tax order by income_from");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taxInfo.Add(new TaxInfoDetails()
                            {
                                income_from = Convert.ToInt32(reader["INCOME_FROM"]),
                                income_to = Convert.ToInt32(reader["INCOME_TO"]),
                                gross_tax_payable = Convert.ToInt32(reader["GROSS_TAX_PAYABLE"]),
                                tax_rate = Convert.ToDouble(reader["TAX_RATE"]),
                                comment = reader["COMMENT"].ToString()
                            });
                        }
                    }
                }
                connection.Close();
            }
            return taxInfo;
        }
        catch (SqlException exc)
        {
            return taxInfo;
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public String GetTaxSingleResultJSON(int from)
    {
        TaxInfoDetails taxInfo = new TaxInfoDetails();
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @".\SQLEXPRESS";
            builder.AttachDBFilename = @"|DataDirectory|Database.mdf";
            builder.IntegratedSecurity = true;
            builder.UserInstance = true;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {

                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * from income_tax where " +from.ToString() +" = income_from order by income_from");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taxInfo=new TaxInfoDetails()
                            {
                                income_from = Convert.ToInt32(reader["INCOME_FROM"]),
                                income_to = Convert.ToInt32(reader["INCOME_TO"]),
                                gross_tax_payable = Convert.ToInt32(reader["GROSS_TAX_PAYABLE"]),
                                tax_rate = Convert.ToDouble(reader["TAX_RATE"]),
                                comment = reader["COMMENT"].ToString()
                            };
                        }
                    }
                }
                connection.Close();
            }
            return new JavaScriptSerializer().Serialize(taxInfo);
        }
        catch (SqlException exc)
        {
            return new JavaScriptSerializer().Serialize(taxInfo);
        }
    }

    [WebMethod]
    public TaxInfoDetails GetTaxSingleResultXML(int from)
    {
        TaxInfoDetails taxInfo = new TaxInfoDetails();
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @".\SQLEXPRESS";
            builder.AttachDBFilename = @"|DataDirectory|Database.mdf";
            builder.IntegratedSecurity = true;
            builder.UserInstance = true;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {

                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * from income_tax where " + from.ToString() + " = income_from order by income_from");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taxInfo = new TaxInfoDetails()
                            {
                                income_from = Convert.ToInt32(reader["INCOME_FROM"]),
                                income_to = Convert.ToInt32(reader["INCOME_TO"]),
                                gross_tax_payable = Convert.ToInt32(reader["GROSS_TAX_PAYABLE"]),
                                tax_rate = Convert.ToDouble(reader["TAX_RATE"]),
                                comment = reader["COMMENT"].ToString()
                            };
                        }
                    }
                }
                connection.Close();
            }
            return taxInfo;

        }
        catch (SqlException exc)
        {
            return taxInfo;
        }
    }

    [WebMethod]
    public List<TaxRange> GetTaxRangeXML()
    {
        List<TaxRange> taxRange = new List<TaxRange>();
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            /*builder.DataSource = "waynefong.database.windows.net";
            builder.UserID = "ok515671126";
            builder.Password = "JKLuio789";
            builder.InitialCatalog = "Tax";*/

            builder.DataSource = @".\SQLEXPRESS";
            builder.AttachDBFilename = @"|DataDirectory|Database.mdf";
            builder.IntegratedSecurity = true;
            builder.UserInstance = true;
            // using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {

                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT income_from,income_to from income_tax order by income_from");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taxRange.Add(new TaxRange()
                            {
                                income_from = Convert.ToInt32(reader["INCOME_FROM"]),
                                income_to = Convert.ToInt32(reader["INCOME_TO"])
                            });
                        }
                    }
                }
                connection.Close();
            }

            return taxRange;

        }
        catch (SqlException exc)
        {
            return taxRange;
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public String GetTaxRangeJSON()
    {
        List<TaxRange> taxRange = new List<TaxRange>();
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @".\SQLEXPRESS";
            builder.AttachDBFilename = @"|DataDirectory|Database.mdf";
            builder.IntegratedSecurity = true;
            builder.UserInstance = true;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {

                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT income_from,income_to from income_tax order by income_from");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taxRange.Add(new TaxRange()
                            {
                                income_from = Convert.ToInt32(reader["INCOME_FROM"]),
                                income_to = Convert.ToInt32(reader["INCOME_TO"])
                            });
                        }
                    }
                }
                connection.Close();
            }
            return new JavaScriptSerializer().Serialize(taxRange);
        }
        catch (SqlException exc)
        {
            return new JavaScriptSerializer().Serialize(taxRange);
        }
    }
}
