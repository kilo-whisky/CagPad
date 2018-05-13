using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Xsl;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Globalization;

namespace GuardianChecks.Helpers
{

    public class UserConfigValue
    {
        public int UserId { get; set; }
        public string UserConfig { get; set; }
        public string ValueString { get; set; }
        public int? ValueInt { get; set; }
        public DateTime? ValueDate { get; set; }
    }

    public class Help
    {

        public static UserConfigValue Config_get(string Config)
        {
            UserConfigValue ucv = new UserConfigValue();
            ucv.UserId = (int)HttpContext.Current.Session["UserId"];
            ucv.UserConfig = Config;
            using (dbHelp dbh = new dbHelp("Core.UserConfig_get", true, "Raven"))
            {
                dbh.addParam("UserId", ucv.UserId);
                dbh.addParam("Config", ucv.UserConfig);
                if (dbh.dr.HasRows)
                {
                    dbh.dr.Read();
                    ucv.ValueString = dbh.drGetString("ValueString");
                    ucv.ValueDate = dbh.drGetDateTimeNull("ValueDate");
                    ucv.ValueInt = dbh.drGetInt32Null("ValueInt");
                }
            }
            return ucv;
        }

        public static void Config_set(string Config, DateTime? ValueDate)
        {
            using (dbHelp dbh = new dbHelp("Core.UserConfig_set", true, "Raven"))
            {
                dbh.addParam("UserId", (int)HttpContext.Current.Session["UserId"]);
                dbh.addParam("Config", Config);
                dbh.addParam("@ValueDate", ValueDate);
                dbh.ExecNoQueryError();
            }
        }

        public static void Config_set(string Config, int? ValueInt)
        {
            using (dbHelp dbh = new dbHelp("Core.UserConfig_set", true, "Raven"))
            {
                dbh.addParam("UserId", (int)HttpContext.Current.Session["UserId"]);
                dbh.addParam("Config", Config);
                dbh.addParam("@ValueInt", ValueInt);
                dbh.ExecNoQueryError();
            }
        }

        public static void Config_set(string Config, string ValueString)
        {
            using (dbHelp dbh = new dbHelp("Core.UserConfig_set", true, "Raven"))
            {
                dbh.addParam("UserId", (int)HttpContext.Current.Session["UserId"]);
                dbh.addParam("Config", Config);
                dbh.addParam("@ValueString", ValueString);
                dbh.ExecNoQueryError();
            }
        }

        public static DateTime GetDate(string value)
        {
            DateTime UKDate = DateTime.ParseExact(value, "d/M/yyyy", CultureInfo.InvariantCulture);
            return UKDate;
        }
        public static DateTime? GetDateNull(string value)
        {
            try
            {
                DateTime UKDate = DateTime.ParseExact(value, "d/M/yyyy", CultureInfo.InvariantCulture);
                return UKDate;
            }
            catch
            {
                return null;
            }
        }
    }

    public class dbHelp : IDisposable
    {
        public string sql { get; set; }
        public List<SqlParameter> paramList { get; set; }
        private SqlDataReader _dr;
        private SqlConnection _cnn;
        private SqlCommand _cmd;
        private DataSet _ds;

        public dbHelp() { }
        public dbHelp(string i_sql, bool isSP, string dsnName)
        {
            sql = i_sql;
            paramList = new List<SqlParameter>();
            _cnn = new SqlConnection(ConfigurationManager.ConnectionStrings[dsnName].ToString());
            _cmd = new SqlCommand(sql, _cnn);
            _cmd.CommandType = isSP ? CommandType.StoredProcedure : CommandType.Text;
            _cnn.Open();
        }

        public void addParam(string paramName, object paramValue)
        {
            if (paramList.Exists(x => x.ParameterName == paramName))
            {
                paramList.RemoveAll(x => x.ParameterName == paramName);
            }
            if (paramValue == null) paramList.Add(new SqlParameter(paramName, DBNull.Value));
            else paramList.Add(new SqlParameter(paramName, paramValue));
        }//addParam

        public void AddParamDate(string paramName, string paramValue)
        {
            try
            {
                DateTime UKDate = DateTime.ParseExact(paramValue, "d/M/yyyy", CultureInfo.InvariantCulture);
                paramList.Add(new SqlParameter(paramName, UKDate));
            }
            catch
            {
                paramList.Add(new SqlParameter(paramName, DBNull.Value));
            }
        }//addParamDate

        public void addParamDateTime(string paramName, string paramValue)
        {
            try
            {
                DateTime UKDate = DateTime.ParseExact(paramValue, "d/M/yyyy HH:mm", CultureInfo.InvariantCulture);
                paramList.Add(new SqlParameter(paramName, UKDate));
            }
            catch
            {
                paramList.Add(new SqlParameter(paramName, DBNull.Value));
            }
        }//addParamDateTime

        public void AddParamTable(string ParamName, DataTable TableData, string TableTypeName)
        {
            if (TableData.Rows.Count == 0) return;
            if (paramList.Exists(x => x.ParameterName == ParamName))
            {
                paramList.RemoveAll(x => x.ParameterName == ParamName);
            }
            SqlParameter p = new SqlParameter(ParamName, TableData);
            p.SqlDbType = SqlDbType.Structured;
            p.TypeName = TableTypeName;
            paramList.Add(p);
        }

        public SqlCommand cmd
        {
            get
            {
                //close any existing readers
                if (_dr != null)
                {
                    if (!_dr.IsClosed) _dr.Close();
                }
                // refresh params
                if (_cmd.Parameters.Count > 0) _cmd.Parameters.Clear();
                if (paramList.Count > 0) _cmd.Parameters.AddRange(paramList.ToArray());
                return _cmd;
            }
        }

        public void drReset()
        {
            _dr = null;
        }

        public SqlDataReader dr
        {
            get
            {
                if (_dr == null) _dr = cmd.ExecuteReader();
                return _dr;
            }
        }//dr

        public string drGetString(string fieldName)
        {
            if (_dr.IsDBNull(_dr.GetOrdinal(fieldName)))
            {
                return "";
            }
            else
            {
                return _dr[fieldName].ToString();
            }
        }//drGetString

        public byte[] DrGetBytes(string fieldName)
        {
            return (byte[])_dr.GetValue(dr.GetOrdinal(fieldName));
        }//drGetBytes

        public Int32 drGetInt32(string fieldName)
        {
            return _dr.GetInt32(dr.GetOrdinal(fieldName));
        }//drGetInt32

        public Int16 drGetInt16(string fieldName)
        {
            return _dr.GetInt16(dr.GetOrdinal(fieldName));
        }//drGetInt16

        public Int32? drGetInt32Null(string fieldName)
        {
            if (_dr.IsDBNull(_dr.GetOrdinal(fieldName)))
            {
                return null;
            }
            else
            {
                return drGetInt32(fieldName);
            }
        }//drGetInt32Null

        public DateTime drGetDateTime(string fieldName)
        {
            return _dr.GetDateTime(dr.GetOrdinal(fieldName));
        }//drGetDateTime

        public DateTime? drGetDateTimeNull(string fieldName)
        {

            if (_dr.IsDBNull(_dr.GetOrdinal(fieldName)))
            {
                return null;
            }
            else
            {
                return _dr.GetDateTime(_dr.GetOrdinal(fieldName));
            }
        }//drGetDateTimeNull

        public Decimal drGetDecimal(string fieldName)
        {
            return _dr.GetDecimal(dr.GetOrdinal(fieldName));
        }//drGetDecimal

        public Decimal? drGetDecimalNull(string fieldName)
        {
            if (_dr.IsDBNull(_dr.GetOrdinal(fieldName)))
            {
                return null;
            }
            else
            {
                return drGetDecimal(fieldName);
            }
        }//drGetDecimalNull


        public Guid drGetGuid(string fieldName)
        {
            return _dr.GetGuid(dr.GetOrdinal(fieldName));
        }//drGetGuid

        /// <summary>
        /// This one is designed to allow us to catch the error higher up if we
        /// want to report it to the web page
        /// </summary>
        /// <param name="retString"></param>
        /// <returns></returns>
        internal string ExecNoQueryError(string retString = "")
        {
            //add return parameter
            if (paramList.Exists(x => x.Direction == ParameterDirection.ReturnValue))
            {
                paramList.RemoveAll(x => x.Direction == ParameterDirection.ReturnValue);
            }
            SqlParameter returnValue = new SqlParameter();
            returnValue.Direction = ParameterDirection.ReturnValue;
            paramList.Add(returnValue);

            cmd.ExecuteNonQuery().ToString();
            return returnValue.Value.ToString();
        }

        internal string ExecNoQuery(string retString = "")
        {
            try
            {
                //add return parameter
                if (paramList.Exists(x => x.Direction == ParameterDirection.ReturnValue))
                {
                    paramList.RemoveAll(x => x.Direction == ParameterDirection.ReturnValue);
                }
                SqlParameter returnValue = new SqlParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                paramList.Add(returnValue);

                cmd.ExecuteNonQuery().ToString();
                return returnValue.Value.ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public bool DrGetBoolean(string fieldName)
        {
            return _dr.GetBoolean(dr.GetOrdinal(fieldName));
        }//drGetBoolean

        public bool? DrGetBooleanNull(string fieldName)
        {
            if (_dr.IsDBNull(_dr.GetOrdinal(fieldName)))
            {
                return null;
            }
            else
            {
                return DrGetBoolean(fieldName);
            }
        }//drGetBooleanNULL


        public DataSet dsFill(string dsName)
        {
            if (_ds == null)
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                _ds = new DataSet(dsName);
                da.Fill(_ds);
            }
            return _ds;
        }//ds

        public DataSet ds
        {
            get { return _ds; }
        }

        public void dsTableName(int id, string tableName)
        {
            _ds.Tables[id].TableName = tableName;
        }//dsTableName

        public void addNestedRef(string pTable, string pCol, string cTable, string cCol)
        {
            DataColumn p = _ds.Tables[pTable].Columns[pCol];
            DataColumn c = _ds.Tables[cTable].Columns[cCol];
            DataRelation r1 = new DataRelation("", p, c);
            r1.Nested = true;
            _ds.Relations.Add(r1);
        }//addNestedRef

        internal string getHTML(string xsltPath)
        {
            return XML2HTML.RenderXml(_ds.GetXml(), HttpContext.Current.Server.MapPath(xsltPath));
        }//getHTML


        public void Dispose()
        {
            _cnn.Dispose();
            _cmd.Dispose();
            if (_dr != null) _dr.Dispose();
            if (_ds != null) _ds.Dispose();
        }//Dispose
    }

    public static class XML2HTML
    {

        public static HtmlString RenderXml(this HtmlHelper helper, string xml, string xsltPath)
        {
            return xml2Htmlstring(xml, xsltPath);
        }//end RenderXml

        public static string RenderXml(string xml, string xsltPath)
        {
            return xml2Htmlstring(xml, xsltPath).ToString();
        }//end controllerXML

        public static HtmlString xml2Htmlstring(string xml, string xsltPath)
        {
            if (string.IsNullOrEmpty(xml)) { return new HtmlString(""); } //prevent error if NO xml data
            XsltArgumentList args = new XsltArgumentList();
            XslCompiledTransform t = new XslCompiledTransform();
            t.Load(xsltPath);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationType = ValidationType.DTD;
            settings.CheckCharacters = false; //required cope with odd characters in KB articles
            using (XmlReader reader = XmlReader.Create(new StringReader(xml), settings))
            {
                StringWriter writer = new StringWriter();
                t.Transform(reader, args, writer);
                HtmlString htmlString = new HtmlString(writer.ToString());
                return htmlString;
            }
        } //end xml2Htmlstring

    }//end class XML2HTML


}//end WebApp.Helpers