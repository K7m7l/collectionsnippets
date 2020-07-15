/*if (string.IsNullOrEmpty(length))
            {
                length = "8";
            }

            if ((typegenerated.Trim().ToLower() == "only alphabets(upper and lower)") ||(typegenerated.Trim()=="Aa"))
            {
                validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            else if ((typegenerated.Trim().ToLower() == "only alphabets(upper and lower cases) and numbers")|| (typegenerated.Trim() == "Aa1"))
            {
                validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            }
            else if ((typegenerated.Trim().ToLower() == "only alphabets upper case")|| (typegenerated.Trim() == "A"))
            {
                validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            else if ((typegenerated.Trim().ToLower() == "only alphabets lower case")|| (typegenerated.Trim() == "a"))
            {
                validChars = "abcdefghijklmnopqrstuvwxyz";
            }
            else if ((typegenerated.Trim().ToLower() == "only alphabets upper case and numbers")|| (typegenerated.Trim() == "A1"))
            {
                validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            }
            else if ((typegenerated.Trim().ToLower() == "only alphabets lower case and numbers")|| (typegenerated.Trim() == "a1"))
            {
                validChars = "abcdefghijklmnopqrstuvwxyz0123456789";
            }
            else if ((typegenerated.Trim().ToLower() == "only numbers")|| (typegenerated.Trim() == "1"))
            {
                validChars = "0123456789";
            }
            //else if (typegenerated.Trim().ToLower() == "alphanumeric without special characters")
            //{
            //    validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            //}
            //else if ((typegenerated.Trim().ToLower() == "alphanumeric with special characters")|| (typegenerated.Trim() == "Aa1_"))
            //{
            //    validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~!@#$%^&*()_+-={}[]:\"|;'\\<>?,./ ";
            //}
            else
            {
                validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~!@#$%^&*()_+-={}[]:\"|;'\\<>?,./ ";
            }

            int l = 0; var nonceString = new StringBuilder();


            try
            {
                
                if (int.TryParse(length, out l))
                {
                    for (int i = 0; i < int.Parse(length); i++)
                    {
                        nonceString.Append(validChars[random.Next(0, validChars.Length - 1)]);
                    }
                }*/
                
 private int intConverter(string stringer)
        {
            int returns = 0;
            if (int.TryParse(stringer, out returns))
            {
                return returns;
            }
            else
            {
                return -1;
            }
        }
        public static string TextToHtml(string text)
        {
            text = HttpUtility.HtmlEncode(text);
            text = text.Replace("\r\n", "\r");
            text = text.Replace("\n", "\r");
            text = text.Replace("\n\n", "\r");
            text = text.Replace("\r", "<br>\r\n");
            text = text.Replace("  ", " &nbsp;");
            text = text.Replace("%", " ");
            return text;
        }
        public IList<UniqueId> getMailUniqueIDs(string readtype, IMailFolder xpecialFolder)
        {
            IList<UniqueId> uids = new List<UniqueId>();

            if (readtype.ToLower() == StringHelper.unread)
            {
                uids = xpecialFolder.Search(SearchQuery.NotSeen);
            }
            else if (readtype.ToLower() == StringHelper.all)
            {
                uids = xpecialFolder.Search(SearchQuery.All);
            }
            else if (readtype.ToLower() == StringHelper.answered)
            {
                uids = xpecialFolder.Search(SearchQuery.Answered);
            }
            else if (readtype.ToLower() == StringHelper.deleted)
            {
                uids = xpecialFolder.Search(SearchQuery.Deleted);
            }
            else if (readtype.ToLower() == StringHelper.quottednew)
            {
                uids = xpecialFolder.Search(SearchQuery.New);
            }
            else if (readtype.ToLower() == StringHelper.recent)
            {
                uids = xpecialFolder.Search(SearchQuery.Recent);
            }
            else if (readtype.ToLower() == StringHelper.seen)
            {
                uids = xpecialFolder.Search(SearchQuery.Seen);
            }
            else
            {
                uids = xpecialFolder.Search(SearchQuery.All);
            }

            return uids;
        }
        public bool FindinText(string content, List<string> keys)
        {
            bool returner = false;
            foreach (string key in keys)
            {
                if (content.ToLower().IndexOf(key.ToLower().Trim()) > -1)
                {
                    returner = true;
                }
                else
                {
                    returner = false;
                }
            }
            return returner;
        }
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                    dataTable.Columns.Add(prop.Name, type);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return dataTable;
            }
        }
               public InternetAddressList GetIAList(string addresseswithsemicolons)
        {
            InternetAddressList ialist = new InternetAddressList();
            if (addresseswithsemicolons.Trim() != string.Empty)
            {
                if (addresseswithsemicolons.Contains(";"))
                {
                    List<string> stinger = addresseswithsemicolons.Split(';', StringSplitOptions.None).ToList();
                    stinger.ForEach(sting => ialist.Add(InternetAddress.Parse(sting)));
                }
                else
                {
                    ialist.Add(InternetAddress.Parse(addresseswithsemicolons));
                }
            }
            else
            {

            }
            return ialist;
        }
        public MessageIdList GetMIDList(string midswithsemicolons)
        {
            MessageIdList ialist = new MessageIdList();
            if (midswithsemicolons.Trim() != string.Empty)
            {
                if (midswithsemicolons.Contains(";"))
                {
                    List<string> stinger = midswithsemicolons.Split(';', StringSplitOptions.None).ToList();
                    stinger.ForEach(sting => ialist.Add(sting));
                }
                else
                {
                    ialist.Add(midswithsemicolons);
                }
            }
            else
            {

            }
            return ialist;
        }
        private bool IsValidXmlString(string text)
        {
            try
            {
                XmlConvert.VerifyXmlChars(text);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private string RemoveInvalidXmlChars(string text)
        {
            var validXmlChars = text.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray();
            return new string(validXmlChars);
        }
        private static string GetFormattedCellValue(WorkbookPart workbookPart, Cell cell)
        {
            if (cell == null)
            {
                return null;
            }

            string value = "";
            if (cell.DataType == null) // number & dates
            {
                int styleIndex = (int)cell.StyleIndex.Value;
                CellFormat cellFormat = (CellFormat)workbookPart.WorkbookStylesPart.Stylesheet.CellFormats.ElementAt(styleIndex);
                uint formatId = cellFormat.NumberFormatId.Value;

                if ((formatId == (uint)Formats.DateShort || formatId == (uint)Formats.DateIndian) || (formatId == (uint)Formats.Date || formatId == (uint)Formats.DateLong))
                {
                    double oaDate;
                    if (double.TryParse(cell.InnerText, out oaDate))
                    {
                        value = DateTime.FromOADate(oaDate).ToShortDateString();
                    }
                }
                else
                {
                    value = cell.InnerText;
                }
            }
            else // Shared string or boolean
            {
                switch (cell.DataType.Value)
                {
                    case CellValues.SharedString:
                        SharedStringItem ssi = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(int.Parse(cell.CellValue.InnerText));
                        value = ssi.Text.Text;
                        break;
                    case CellValues.Boolean:
                        value = cell.CellValue.InnerText == "0" ? "false" : "true";
                        break;
                    default:
                        value = cell.CellValue.InnerText;
                        break;
                }
            }

            return value;
        }
        private enum Formats
        {
            General = 0,
            Number = 1,
            Decimal = 2,
            Currency = 164,
            Accounting = 44,
            DateShort = 14,
            DateLong = 165,
            Time = 166,
            Percentage = 10,
            Fraction = 12,
            Scientific = 11,
            Text = 49,
            Date = 68,
            DateIndian = 58
        }
        /*  0 = 'General';
            1 = '0';
            2 = '0.00';
            3 = '#,##0';
            4 = '#,##0.00';
            5 = '$#,##0;\-$#,##0';
            6 = '$#,##0;[Red]\-$#,##0';
            7 = '$#,##0.00;\-$#,##0.00';
            8 = '$#,##0.00;[Red]\-$#,##0.00';
            9 = '0%';
            10 = '0.00%';
            11 = '0.00E+00';
            12 = '# ?/?';
            13 = '# ??/??';
            14 = 'mm-dd-yy';
            15 = 'd-mmm-yy';
            16 = 'd-mmm';
            17 = 'mmm-yy';
            18 = 'h:mm AM/PM';
            19 = 'h:mm:ss AM/PM';
            20 = 'h:mm';
            21 = 'h:mm:ss';
            22 = 'm/d/yy h:mm';

            37 = '#,##0 ;(#,##0)';
            38 = '#,##0 ;[Red](#,##0)';
            39 = '#,##0.00;(#,##0.00)';
            40 = '#,##0.00;[Red](#,##0.00)';

            44 = '_("$"* #,##0.00_);_("$"* \(#,##0.00\);_("$"* "-"??_);_(@_)';
            45 = 'mm:ss';
            46 = '[h]:mm:ss';
            47 = 'mmss.0';
            48 = '##0.0E+0';
            49 = '@';

            27 = '[$-404]e/m/d';
            30 = 'm/d/yy';
            36 = '[$-404]e/m/d';
            50 = '[$-404]e/m/d';
            57 = '[$-404]e/m/d';

            59 = 't0';
            60 = 't0.00';
            61 = 't#,##0';
            62 = 't#,##0.00';
            67 = 't0%';
            68 = 't0.00%';
            69 = 't# ?/?';
            70 = 't# ??/??';
         */
         private KeyValuePair<int, int> ColumnNameToNumber(string col_name)
        {
            int result = 0;

            Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
            Match reresult = re.Match(col_name);

            string alphaPart = reresult.Groups[1].Value;
            string numberPart = reresult.Groups[2].Value;

            // Process each letter.
            for (int i = 0; i < alphaPart.Length; i++)
            {
                result *= 26;
                char letter = alphaPart[i];

                // See if it's out of bounds.
                if (letter < 'A') letter = 'A';
                if (letter > 'Z') letter = 'Z';

                // Add in the value of this letter.
                result += (int)letter - (int)'A' + 1;
            }
            return new KeyValuePair<int, int>(result, Convert.ToInt32(numberPart));
        }
                private bool addSheet(string docName, string sheetname)
        {
            bool isDone = false;
            try
            {
                using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(docName, true))
                {
                    try
                    {
                        // Add a blank WorksheetPart.
                        WorksheetPart newWorksheetPart = spreadSheet.WorkbookPart.AddNewPart<WorksheetPart>();
                        newWorksheetPart.Worksheet = new Worksheet(new SheetData());

                        Sheets sheets = spreadSheet.WorkbookPart.Workbook.GetFirstChild<Sheets>();
                        string relationshipId = spreadSheet.WorkbookPart.GetIdOfPart(newWorksheetPart);

                        // Get a unique ID for the new worksheet.
                        uint sheetId = 1;
                        if (sheets.Elements<Sheet>().Count() > 0)
                        {
                            sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                        }

                        // Give the new worksheet a name.
                        string sheetName = "Sheet" + sheetId;

                        if (sheetname.Trim() != string.Empty)
                        {
                            sheetName = sheetname;
                        }

                        if (sheetname.Length > 30)
                        {
                            sheetName = sheetname.Substring(0, Math.Min(sheetname.Length, 30));
                        }

                        // Append the new worksheet and associate it with the workbook.
                        Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };
                        sheets.Append(sheet);
                        isDone = true;

                        spreadSheet.WorkbookPart.Workbook.Save();
                    }
                    catch { }
                    spreadSheet.Save();
                    spreadSheet.Close();
                }
            }
            catch(Exception ezx)
            {
                string h = ezx.Message;
            }
            return isDone;
        }
        private void DeleteAWorkSheet(string fileName, string sheetToDelete)
        {
            string Sheetid = "";
            //Open the workbook
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileName, true))
            {
                WorkbookPart wbPart = document.WorkbookPart;

                // Get the pivot Table Parts
                IEnumerable<PivotTableCacheDefinitionPart> pvtTableCacheParts = wbPart.PivotTableCacheDefinitionParts;
                Dictionary<PivotTableCacheDefinitionPart, string> pvtTableCacheDefinationPart = new Dictionary<PivotTableCacheDefinitionPart, string>();
                foreach (PivotTableCacheDefinitionPart Item in pvtTableCacheParts)
                {
                    PivotCacheDefinition pvtCacheDef = Item.PivotCacheDefinition;
                    //Check if this CacheSource is linked to SheetToDelete
                    var pvtCahce = pvtCacheDef.Descendants<CacheSource>().Where(s => s.WorksheetSource.Sheet == sheetToDelete);
                    if (pvtCahce.Count() > 0)
                    {
                        pvtTableCacheDefinationPart.Add(Item, Item.ToString());
                    }
                }
                foreach (var Item in pvtTableCacheDefinationPart)
                {
                    wbPart.DeletePart(Item.Key);
                }
                //Get the SheetToDelete from workbook.xml
                Sheet theSheet = wbPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetToDelete).FirstOrDefault();
                if (theSheet != null)
                {
                    // The specified sheet doesn't exist.

                    //Store the SheetID for the reference
                    Sheetid = theSheet.SheetId;

                    // Remove the sheet reference from the workbook.
                    WorksheetPart worksheetPart = (WorksheetPart)(wbPart.GetPartById(theSheet.Id));
                    theSheet.Remove();

                    // Delete the worksheet part.
                    wbPart.DeletePart(worksheetPart);

                    //Get the DefinedNames
                    var definedNames = wbPart.Workbook.Descendants<DefinedNames>().FirstOrDefault();
                    if (definedNames != null)
                    {
                        List<DefinedName> defNamesToDelete = new List<DefinedName>();

                        foreach (DefinedName Item in definedNames)
                        {
                            // This condition checks to delete only those names which are part of Sheet in question
                            if (Item.Text.Contains(sheetToDelete + "!"))
                                defNamesToDelete.Add(Item);
                        }

                        foreach (DefinedName Item in defNamesToDelete)
                        {
                            Item.Remove();
                        }

                    }
                    // Get the CalculationChainPart 
                    //Note: An instance of this part type contains an ordered set of references to all cells in all worksheets in the 
                    //workbook whose value is calculated from any formula

                    CalculationChainPart calChainPart;
                    calChainPart = wbPart.CalculationChainPart;
                    if (calChainPart != null)
                    {
                        var calChainEntries = calChainPart.CalculationChain.Descendants<CalculationCell>().Where(c => c.SheetId == Sheetid);
                        List<CalculationCell> calcsToDelete = new List<CalculationCell>();
                        foreach (CalculationCell Item in calChainEntries)
                        {
                            calcsToDelete.Add(Item);
                        }

                        foreach (CalculationCell Item in calcsToDelete)
                        {
                            Item.Remove();
                        }

                        if (calChainPart.CalculationChain.Count() == 0)
                        {
                            wbPart.DeletePart(calChainPart);
                        }
                    }

                    // Save the workbook.
                    wbPart.Workbook.Save();
                }
            }
        }
        private string ColumnIndexToColumnLetter(int colIndex)
        {
            int div = colIndex;
            string colLetter = String.Empty;
            int mod = 0;

            while (div > 0)
            {
                mod = (div - 1) % 26;
                colLetter = (char)(65 + mod) + colLetter;
                div = (int)((div - mod) / 26);
            }
            return colLetter;
        }
        private string Replacetext(string line)
        {
            int startindex = 0;
            int Endindex = 0;
            if (line.First().ToString() == "\"")
            {
                startindex = line.IndexOf("\"");
            }
            else if (line.Contains(",\""))
            {
                startindex = line.IndexOf(",\"");
            }
            else
            {
                startindex = line.IndexOf("\"");
            }
            if (line.Contains("\","))
            {
                Endindex = line.IndexOf("\",");
            }
            else if (line.Last().ToString() == "\"")
            {
                Endindex = line.LastIndexOf("\"");
            }
            else {
                Endindex = line.IndexOf("\"");
            }
            if (startindex < Endindex)
            {
                string outputstring = string.Empty;
                if (startindex == 0)
                {
                    outputstring = "\"" + line.Substring(startindex + 1, Endindex - startindex - 1) + "\"";
                }
                else {
                    outputstring = line.Substring(startindex + 1, Endindex - startindex - 1) + "\"";
                }
                string finaltext = outputstring;
                finaltext = finaltext.Replace(",", "|");
                finaltext = finaltext.Replace("\"", "");
                line = line.Replace(outputstring, finaltext);
            }
            else {

            }
            if (line.Contains(",\""))
            {
                line = Replacetext(line);
            }
            return line;
        }
public bool checkFileexcelorcsv(string filename)
        {
            if(filename.IndexOf("xls") > -1)
            {
                return true;
            }
            else if(filename.IndexOf("XLS") > -1)
            {
                return true;
            }
            else if (filename.IndexOf("XLSX") > -1)
            {
                return true;
            }
            else if (filename.ToString().IndexOf("xlsx") > -1)
            {
                return true;
            }
            else if (filename.ToString().IndexOf("csv") > -1)
            {
                return true;
            }
            else if (filename.ToString().IndexOf("CSV") > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private List<string> DirSearch(string sDir)
        {
            List<string> files = new List<string>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    files.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    files.AddRange(DirSearch(d));
                }
            }
            catch (Exception excpt)
            {
                string ex = excpt.Message;
            }

            return files;
        }
        public enum HttpVerb
{
    GET,
    POST,
    PUT,
    DELETE,
    HEAD,
    CONNECT,
    OPTIONS,
    TRACE,
    PATCH
}

namespace HttpUtils
{
    public class RestClient
    {
        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }
        public string EncodedFormat { get; set; }
        public string Header { get; set; }

        public RestClient()
        {
            EndPoint = "";
            Method = HttpVerb.GET;
            ContentType = "application/json";
            PostData = "";
        }
        public RestClient(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            ContentType = "application/json";
            PostData = "";
        }
        public RestClient(string endpoint, HttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "application/json";
            PostData = "";
        }

        public RestClient(string endpoint, HttpVerb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "application/json";
            PostData = postData;
        }

        public RestClient(string endpoint, HttpVerb method, string postData, string contentType, string encodedFormat)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = contentType;
            PostData = postData;
            EncodedFormat = encodedFormat;
        }

        public RestClient(string endpoint, HttpVerb method, string postData, string contentType, string encodedFormat, string Headers)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = contentType;
            PostData = postData;
            EncodedFormat = encodedFormat;
            Header = Headers;
        }


        public string MakeRequest()
        {
            return MakeRequest("");
        }

        public string MakeRequest(string parameters)
        {

            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

                request.Method = Method.ToString();
                request.ContentLength = 0;
                request.ContentType = ContentType;
                

                if (Header.Trim() != string.Empty)
                {
                    request.Headers.Add(Header);
                }

                if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
                {
                    var encoding = new UTF8Encoding();
                    //var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                    var bytes = Encoding.GetEncoding(EncodedFormat).GetBytes(PostData);
                    request.ContentLength = bytes.Length;

                    using (var writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }
                else
                {

                }

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var responseValue = string.Empty;

                    /*if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        //throw new ApplicationException(message);
                    }*/

                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }

                    return responseValue;
                }
            }
            catch (Exception ex)
             {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return ex.Message;
            }

        }
    } // class
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
                private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
         public string ExpectSSH(string address, string login, string password, string command, string rootusername, string rootpassword, string rootcommand)
        {
            string output = string.Empty;
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                SshClient sshClient = new SshClient(address, 22, login, password);

                sshClient.Connect();
                IDictionary<Renci.SshNet.Common.TerminalModes, uint> termkvp = new Dictionary<Renci.SshNet.Common.TerminalModes, uint>();
                termkvp.Add(Renci.SshNet.Common.TerminalModes.ECHO, 53);

                ShellStream shellStream = sshClient.CreateShellStream(StringHelper.xterm, 80, 24, 800, 600, 1024, termkvp);
                //Get logged in
                string rep = shellStream.Expect(new Regex(@"[$>]")); //expect user prompt
                output += rep;

                //send command
                shellStream.WriteLine(command);
                rep = shellStream.Expect(new Regex(@"([$#>:])")); //expect password or user prompt
                output += rep;

                //check to send password
                if (rep.Contains(":"))
                {
                    //send password
                    shellStream.WriteLine(rootpassword);
                    rep = shellStream.Expect(new Regex(@"[$#>:]")); //expect user or root prompt
                    output += rep;
                }
                if (rep.Contains("#"))
                {
                    shellStream.WriteLine(rootcommand);
                    rep = shellStream.Expect(new Regex(@"[$#>]"));
                    output += rep;
                }

                sshClient.Disconnect();
            }
            //try to open connection
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                output = ex.Message;
            }
            return output;
        }

        public string ExpectSSHnew(string address, string login, string password, string command, string rootusername, string rootpassword, string rootcommand)
        {
            string output = string.Empty;
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                SshClient sshClient = new SshClient(address, 22, rootusername, rootpassword);

                sshClient.Connect();
                IDictionary<Renci.SshNet.Common.TerminalModes, uint> termkvp = new Dictionary<Renci.SshNet.Common.TerminalModes, uint>();
                termkvp.Add(Renci.SshNet.Common.TerminalModes.ECHO, 53);

                ShellStream shellStream = sshClient.CreateShellStream(StringHelper.xterm, 80, 24, 800, 600, 1024, termkvp);
                //Get logged in
                string rep = string.Empty; //
                //send command
                shellStream.WriteLine(command);
                rep = shellStream.Expect(new Regex(@"([$#>:])")); //expect password or user prompt
                output += rep;

                //check to send password
                if (rep.Contains(":"))
                {
                    //send password
                    shellStream.WriteLine(password);
                    rep = shellStream.Expect(new Regex(@"[$#>]"));

                    //expect user or root prompt
                    shellStream.WriteLine(password);
                    rep = shellStream.Expect(new Regex(@"[$#>]"));
                    output += rep;
                }
                sshClient.Disconnect();
            }
            //try to open connection
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                output = ex.Message;
            }
            return output;
        }

        public ShellStream ExecuteCommand(ConnectionInfo ci, string command)
        {
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                SshClient sshclient = new SshClient(ci);
                sshclient.Connect();
                ShellStream stream = sshclient.CreateShellStream(command, 80, 24, 800, 600, 1024);
                return stream;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return null;
            }
        }

        public StringBuilder sendCommand(string customCMD, ShellStream stream)
        {
            try
            {
                StringBuilder answer;

                var reader = new StreamReader(stream);
                var writer = new StreamWriter(stream);
                writer.AutoFlush = true;
                WriteStream(customCMD, writer, stream);
                answer = ReadStream(reader);
                return answer;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                return null;
            }
        }

        private void WriteStream(string cmd, StreamWriter writer, ShellStream stream)
        {
            try
            {
                writer.WriteLine(cmd);

                while (stream.Length == 0)
                {
                    Thread.Sleep(1500);
                }
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
            }
        }

        private StringBuilder ReadStream(StreamReader reader)
        {
            try
            {
                StringBuilder result = new StringBuilder();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result.AppendLine(line);
                    Thread.Sleep(500);
                }
                return result;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                return null;
            }
        }
        private DataTable convertToTable(string text, bool withheader)
        {
            List<string> rows = new List<string>();
            List<string> HeaderRow = new List<string>();
            List<string> HeaderRow1 = new List<string>();
            List<string> HeaderRow2 = new List<string>();

            DVariables value = new DVariables();
            text = text.TrimStart();
            //rows
            if (text.IndexOf("\n") > -1)
            {
                rows = text.Split(new string[] { "\n" }, StringSplitOptions.None).ToList();
            }
            //HeaderRow
            if (rows[0].IndexOf(" ") > -1)
            {
                HeaderRow = rows[0].Split(new string[] { " ", "  ", "   ", "  " }, StringSplitOptions.None).ToList();
                HeaderRow.ForEach(x => x.Trim()); HeaderRow.RemoveAll(x => x == string.Empty);

                if (rows.Count > 1)
                {
                    HeaderRow1 = rows[1].Split(new string[] { " ", "  ", "   ", "  " }, StringSplitOptions.None).ToList();
                    HeaderRow2 = rows[2].Split(new string[] { " ", "  ", "   ", "  " }, StringSplitOptions.None).ToList();
                    HeaderRow1.ForEach(x => x.Trim()); HeaderRow1.RemoveAll(x => x == string.Empty);
                    HeaderRow2.ForEach(x => x.Trim()); HeaderRow2.RemoveAll(x => x == string.Empty);
                }

            }
            //HeaderRow.ForEach(x => x.Trim()); HeaderRow.RemoveAll(x => x == string.Empty);


            DataTable dt = new DataTable();

            dt.Columns.Add(" "); //db id
            dt.Columns.Add("  "); //db runid

            if (HeaderRow.Count >= HeaderRow1.Count && HeaderRow1.Count == HeaderRow2.Count)
            {
                foreach (string head in HeaderRow)
                {
                    dt.Columns.Add(head);
                }
                foreach (string row in rows)
                {
                    List<string> rower = new List<string>();
                    rower = row.Split(new string[] { " ", "  ", "   ", "  " }, StringSplitOptions.None).ToList();
                    rower.ForEach(x => x.Trim()); rower.RemoveAll(x => x == string.Empty);
                    rower.Insert(0, "");
                    rower.Insert(1, "");
                    dt.Rows.Add(rower.ToArray());
                }
            }
            else
            {
                dt.Columns.Add("Description");
                foreach (string row in rows)
                {
                    List<string> rower = new List<string>();
                    //rower = row.Split(new string[] { " ", "  ", "   ", "  " }, StringSplitOptions.None).ToList();
                    //rower.ForEach(x => x.Trim()); rower.RemoveAll(x => x == string.Empty);
                    rower.Insert(0, "");
                    rower.Insert(1, "");
                    rower.Insert(2, row);
                    dt.Rows.Add(rower.ToArray());
                }
            }

            if (withheader == false)
            {
                dt.Rows[0].Delete();
            }
            return dt;
        }
         public byte[] DownloadFileByte(string SourceFile, string networkPath, NetworkCredential credentials)
        {
            byte[] fileBytes = null; string myNetworkPath = string.Empty;

            using (new ConnectToSharedFolder(networkPath, credentials))
            {

                myNetworkPath = System.IO.Path.Combine(networkPath, SourceFile);

                try
                {
                    fileBytes = System.IO.File.ReadAllBytes(myNetworkPath);
                }
                catch (Exception ex)
                {
                    string Message = ex.Message.ToString();
                }
            }

            return fileBytes;
        }

        public async void FileUpload(string UploadFilePath, string networkPath, NetworkCredential credentials)
        {
            string myNetworkPath = networkPath;

            List<string> fnameParts = UploadFilePath.Split(new string[] { "\\" }, StringSplitOptions.None).ToList();
            string filename = fnameParts[fnameParts.Count - 1];

            try
            {
                using (new ConnectToSharedFolder(networkPath, credentials))
                {
                    myNetworkPath = myNetworkPath + "\\" + filename;

                    byte[] file = System.IO.File.ReadAllBytes(UploadFilePath);

                    using (FileStream fileStream = System.IO.File.Create(myNetworkPath, file.Length))
                    {
                        await fileStream.WriteAsync(file, 0, file.Length);
                        fileStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string Message = ex.Message.ToString();
            }
        }
        public class ConnectToSharedFolder : IDisposable
    {
        readonly string _networkName;

        public ConnectToSharedFolder(string networkName, NetworkCredential credentials)
        {
            _networkName = networkName;

            var netResource = new NetResource
            {
                Scope = ResourceScope.GlobalNetwork,
                ResourceType = ResourceType.Disk,
                DisplayType = ResourceDisplaytype.Share,
                RemoteName = networkName
            };

            var userName = string.IsNullOrEmpty(credentials.Domain)
                ? credentials.UserName
                : string.Format(@"{0}\{1}", credentials.Domain, credentials.UserName);

            var result = WNetAddConnection2(
                netResource,
                credentials.Password,
                userName,
                0);

            if (result != 0)
            {
                //throw new Win32Exception(result, "Error connecting to remote share");
            }
        }

        ~ConnectToSharedFolder()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            WNetCancelConnection2(_networkName, 0, true);
        }

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(NetResource netResource,
            string password, string username, int flags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string name, int flags,
            bool force);

        [StructLayout(LayoutKind.Sequential)]
        public class NetResource
        {
            public ResourceScope Scope;
            public ResourceType ResourceType;
            public ResourceDisplaytype DisplayType;
            public int Usage;
            public string LocalName;
            public string RemoteName;
            public string Comment;
            public string Provider;
        }

        public enum ResourceScope : int
        {
            Connected = 1,
            GlobalNetwork,
            Remembered,
            Recent,
            Context
        };

        public enum ResourceType : int
        {
            Any = 0,
            Disk = 1,
            Print = 2,
            Reserved = 8,
        }

        public enum ResourceDisplaytype : int
        {
            Generic = 0x0,
            Domain = 0x01,
            Server = 0x02,
            Share = 0x03,
            File = 0x04,
            Group = 0x05,
            Network = 0x06,
            Root = 0x07,
            Shareadmin = 0x08,
            Directory = 0x09,
            Tree = 0x0a,
            Ndscontainer = 0x0b
        }
    }
    public class NetworkDrive
    {
        public enum ResourceScope
        {
            RESOURCE_CONNECTED = 1,
            RESOURCE_GLOBALNET,
            RESOURCE_REMEMBERED,
            RESOURCE_RECENT,
            RESOURCE_CONTEXT
        }

        public enum ResourceType
        {
            RESOURCETYPE_ANY,
            RESOURCETYPE_DISK,
            RESOURCETYPE_PRINT,
            RESOURCETYPE_RESERVED
        }

        public enum ResourceUsage
        {
            RESOURCEUSAGE_CONNECTABLE = 0x00000001,
            RESOURCEUSAGE_CONTAINER = 0x00000002,
            RESOURCEUSAGE_NOLOCALDEVICE = 0x00000004,
            RESOURCEUSAGE_SIBLING = 0x00000008,
            RESOURCEUSAGE_ATTACHED = 0x00000010,
            RESOURCEUSAGE_ALL = (RESOURCEUSAGE_CONNECTABLE | RESOURCEUSAGE_CONTAINER | RESOURCEUSAGE_ATTACHED),
        }

        public enum ResourceDisplayType
        {
            RESOURCEDISPLAYTYPE_GENERIC,
            RESOURCEDISPLAYTYPE_DOMAIN,
            RESOURCEDISPLAYTYPE_SERVER,
            RESOURCEDISPLAYTYPE_SHARE,
            RESOURCEDISPLAYTYPE_FILE,
            RESOURCEDISPLAYTYPE_GROUP,
            RESOURCEDISPLAYTYPE_NETWORK,
            RESOURCEDISPLAYTYPE_ROOT,
            RESOURCEDISPLAYTYPE_SHAREADMIN,
            RESOURCEDISPLAYTYPE_DIRECTORY,
            RESOURCEDISPLAYTYPE_TREE,
            RESOURCEDISPLAYTYPE_NDSCONTAINER
        }

        [StructLayout(LayoutKind.Sequential)]
        private class NETRESOURCE
        {
            public ResourceScope dwScope = 0;
            public ResourceType dwType = 0;
            public ResourceDisplayType dwDisplayType = 0;
            public ResourceUsage dwUsage = 0;
            public string lpLocalName = null;
            public string lpRemoteName = null;
            public string lpComment = null;
            public string lpProvider = null;
        }

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(NETRESOURCE lpNetResource, string lpPassword, string lpUsername, int dwFlags);

        public int MapNetworkDrive(string unc, string drive, string user, string password)
        {
            NETRESOURCE myNetResource = new NETRESOURCE();
            myNetResource.lpLocalName = drive;
            myNetResource.lpRemoteName = unc;
            myNetResource.lpProvider = null;
            int result = WNetAddConnection2(myNetResource, password, user, 0);
            return result;
        }
    }
    public class UNCAccess
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct USE_INFO_2
        {
            internal String ui2_local;
            internal String ui2_remote;
            internal String ui2_password;
            internal UInt32 ui2_status;
            internal UInt32 ui2_asg_type;
            internal UInt32 ui2_refcount;
            internal UInt32 ui2_usecount;
            internal String ui2_username;
            internal String ui2_domainname;
        }

        [DllImport("NetApi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern UInt32 NetUseAdd(
            String UncServerName,
            UInt32 Level,
            ref USE_INFO_2 Buf,
            out UInt32 ParmError);

        private string sUNCPath;
        private string sUser;
        private string sPassword;
        private string sDomain;
        private int iLastError;
        public UNCAccess()
        {
        }
        public UNCAccess(string UNCPath, string User, string Domain, string Password)
        {
            login(UNCPath, User, Domain, Password);
        }
        public int LastError
        {
            get { return iLastError; }
        }

        /// <summary>
        /// Connects to a UNC share folder with credentials
        /// </summary>
        /// <param name="UNCPath">UNC share path</param>
        /// <param name="User">Username</param>
        /// <param name="Domain">Domain</param>
        /// <param name="Password">Password</param>
        /// <returns>True if login was successful</returns>
        public bool login(string UNCPath, string User, string Domain, string Password)
        {
            sUNCPath = UNCPath;
            sUser = User;
            sPassword = Password;
            sDomain = Domain;
            return NetUseWithCredentials();
        }
        private bool NetUseWithCredentials()
        {
            uint returncode;
            try
            {
                USE_INFO_2 useinfo = new USE_INFO_2();

                useinfo.ui2_remote = sUNCPath;
                useinfo.ui2_username = sUser;
                useinfo.ui2_domainname = sDomain;
                useinfo.ui2_password = sPassword;
                useinfo.ui2_asg_type = 0;
                useinfo.ui2_usecount = 1;
                uint paramErrorIndex;
                returncode = NetUseAdd(null, 2, ref useinfo, out paramErrorIndex);
                iLastError = (int)returncode;
                return returncode == 0;
            }
            catch
            {
                iLastError = Marshal.GetLastWin32Error();
                return false;
            }
        }
    }
            public string CallWebService(string url, string action)
        {
            var _url = url;
            var _action = action;
            string SResult = string.Empty;

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
            }

            SResult = soapResult;

            return SResult;
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope()
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(@"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/1999/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/1999/XMLSchema""><SOAP-ENV:Body><HelloWorld xmlns=""http://tempuri.org/"" SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><int1 xsi:type=""xsd:integer"">12</int1><int2 xsi:type=""xsd:integer"">32</int2></HelloWorld></SOAP-ENV:Body></SOAP-ENV:Envelope>");
            return soapEnvelopeDocument;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
