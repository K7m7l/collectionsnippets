public class FilePathHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        public string cutFilePath(string path, string delimeter)
        {
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                if (path.ToLower().IndexOf(delimeter) > -1)
                {
                    return path = path.Remove(path.ToLower().IndexOf(delimeter));
                }
                else
                {
                    return StringHelper.ffalse;
                }
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return ex.Message;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string cutFileStringinPath(string path)
        {
            try
            {
                if (path.ToLower().IndexOf("file:///") > -1)
                {
                    return path = path.Split(new string[] { "file:///" }, StringSplitOptions.None)[1];
                }
                else
                {
                    return StringHelper.ffalse;
                }
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                return ex.Message;
            }
        }
    }
    public class TypeHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataSet ToDataSet<T>(IList<T> list)
        {
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                Type elementType = typeof(T);
                DataTable t = new DataTable();
                ds.Tables.Add(t);

                //add a column to table for each public property on T
                foreach (var propInfo in elementType.GetProperties())
                {
                    Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                    t.Columns.Add(propInfo.Name, ColType);
                }

                //go through each property on T and add each value to the table
                foreach (T item in list)
                {
                    DataRow row = t.NewRow();

                    foreach (var propInfo in elementType.GetProperties())
                    {
                        row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                    }

                    t.Rows.Add(row);
                }

                return ds;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return ds;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string StringtoJsonD(string text)
        {
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                List<string> rows = new List<string>();
                List<string> HeaderRow = new List<string>();

                //text = text.Replace("?", "q"); text = text.Replace('?', 'q');

                text = text.TrimStart();
                //rows
                if (text.IndexOf("\n") > -1)
                {
                    rows = text.Split(new string[] { "\n" }, StringSplitOptions.None).ToList();
                }
                DataTable dt = new DataTable();


                dt.Columns.Add(" ");
                dt.Columns.Add(rows[0]);
                foreach (string row in rows)
                {
                    DataRow workRow = dt.NewRow();

                    string val = row.Trim();
                    if (!string.IsNullOrEmpty(val))
                    {
                        workRow[0] = "";
                        workRow[1] = row;
                        dt.Rows.Add(workRow);
                    }
                }
                return JsonConvert.SerializeObject(dt);
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return ex.Message;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string StringtoJson(string text)
        {
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                List<string> rows = new List<string>();
                List<string> HeaderRow = new List<string>();
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
                }
                HeaderRow.ForEach(x => x.Trim()); HeaderRow.RemoveAll(x => x == string.Empty);


                DataTable dt = new DataTable(); dt.Columns.Add(" ");
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
                    dt.Rows.Add(rower.ToArray());
                }
                return JsonConvert.SerializeObject(dt);
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return ex.Message;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public bool IsValidJson(string strInput)
        {
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                strInput = strInput.Trim();
                if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                    (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
                {
                    try
                    {
                        var obj = JToken.Parse(strInput);
                        return true;
                    }
                    catch (JsonReaderException jex)
                    {
                        //Exception in parsing json
                        new Logger().LogException(jex);
                        return false;
                    }
                    catch (Exception ex) //some other exception
                    {
                        new Logger().LogException(ex);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public bool IsFormattedString(string strInput)
        {
            try
            {
                strInput = strInput.Trim();
                if (strInput.StartsWith("{") && strInput.EndsWith("}")) //For object
                {
                    try
                    {
                        return true;
                    }
                    catch (Exception ex) //some other exception
                    {
                        new Logger().LogException(ex);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formattedstring"></param>
        /// <returns></returns>
        public string ConvertFormattedString(string formattedstring,string executionID)
        {
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                string returns = string.Empty;

                List<string> varslist1 = formattedstring.Split('}').ToList();
                varslist1.RemoveAll(x => x.Trim() == string.Empty);
                string newString = formattedstring;
                List<string> varslist2 = new List<string>();

                foreach (string varsin in varslist1)
                {
                    varslist2.Add(varsin.Split('{')[1].Trim());
                }

                StringBuilder sr = new StringBuilder(newString);

                foreach (string varsin in varslist2)
                {
                    DVariables value = new VariableHandler().getVariables(varsin, executionID);
                    if (value != null && value.vlvalue.GetType() == typeof(string))
                    {
                        sr.Replace("{" + varsin + "}", value.vlvalue);
                    }
                }

                returns = sr.ToString();
                return returns;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return ex.Message;
            }
        }
        public DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }
            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> FlattenType(Type type)
        {
            var properties = type.GetProperties();
            foreach (PropertyInfo info in properties)
            {
                if (info.PropertyType.Assembly.GetName().Name == "mscorlib")
                {
                    yield return info;
                }
                else
                {
                    foreach (var childInfo in FlattenType(info.PropertyType))
                    {
                        yield return childInfo;
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public string cDataTableToString(DataTable dataTable)
        {
            if (dataTable != null)
            {
                var output = new StringBuilder();
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        output.AppendLine(dataTable.Columns[i].ColumnName + ":" + row[i]);
                    }
                    output.AppendLine();
                }
                return output.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public string ConvertDataTableToString(DataTable dataTable)
        {
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                if (dataTable != null)
                {
                    var output = new StringBuilder();

                    var columnsWidths = new int[dataTable.Columns.Count];

                    // Get column widths
                    foreach (DataRow row in dataTable.Rows)
                    {
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            var length = row[i].ToString().Length;
                            if (columnsWidths[i] < length)
                                columnsWidths[i] = length;
                        }
                    }

                    // Get Column Titles
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        var length = dataTable.Columns[i].ColumnName.Length;
                        if (columnsWidths[i] < length)
                            columnsWidths[i] = length;
                    }

                    // Write Column titles
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        var text = dataTable.Columns[i].ColumnName;
                        output.Append("|" + PadCenter(text, columnsWidths[i] + 2));
                    }
                    output.Append("|\n" + new string('=', output.Length) + "\n");

                    // Write Rows
                    foreach (DataRow row in dataTable.Rows)
                    {
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            var text = row[i].ToString();
                            output.Append("|" + PadCenter(text, columnsWidths[i] + 2));
                        }
                        output.Append("|\n");
                    }
                    return output.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return null;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        private string PadCenter(string text, int maxLength)
        {
            int diff = maxLength - text.Length;
            return new string(' ', diff / 2) + text + new string(' ', (int)(diff / 2.0 + 0.5));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataTable ListtoDatatable(List<string> list)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("0");
            foreach (var l in list)
            {
                dataTable.Rows.Add(l);
            }

            return dataTable;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool isPermittedDataTable(int row, int column, DataTable dt)
        {
            if(row < dt.Rows.Count && column < dt.Columns.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        /// 
        public string getColumnAsListFromDataTable(DataTable dt, string columnName)
        {
            List<string> returns = new List<string>();
            returns.AddRange(dt.AsEnumerable().Select(r => r.Field<string>(columnName)).ToList());

            return JsonConvert.SerializeObject(returns);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public List<string> ExtractEmails(string Text)
        {
            List<string> outputlist = new List<string>();
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
                MatchCollection emailMatches = emailRegex.Matches(Text);
                StringBuilder sb = new StringBuilder();
                foreach (Match emailMatch in emailMatches)
                {
                    sb.AppendLine(emailMatch.Value);
                    outputlist.Add(emailMatch.Value);
                }
                return outputlist;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return outputlist;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public List<string> ExtractPhoneNumbers(string Text)
        {
            List<string> output = new List<string>();
            try
            {
                return output;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                return output;
            }
        }
        public enum PasswordScore
            {
                Blank = 0,
                VeryWeak = 1,
                Weak = 2,
                Medium = 3,
                Strong = 4,
                VeryStrong = 5
            }
            public static PasswordScore CheckStrength(string password)
            {
                List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
                int score = 0;

                if (password.Length < 1)
                    return PasswordScore.Blank;
                if (password.Length < 4)
                    return PasswordScore.VeryWeak;

                if ((password.Length >= 4) && (password.Length < 8))
                    score++;
                if (password.Length >= 8)
                    score++;

                Regex rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
                if (Regex.Match(password, @"^[0-9]*$", RegexOptions.ECMAScript).Success)
                    score++;
                if (Regex.Match(password, @"^[a-z]*$", RegexOptions.ECMAScript).Success ||
                  Regex.Match(password, @"^[A-Z]*$", RegexOptions.ECMAScript).Success)
                    score++;
                if (Regex.Match(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
                    score++;
                if (Regex.Match(password, @"^[a-zA-Z09!@#$%^&*(),]*$", RegexOptions.ECMAScript).Success)
                    return PasswordScore.VeryStrong;
                return (PasswordScore)score;
            }
            private bool ValidateEmail(string mailid)
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(mailid);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            public class EncoderDecoderBase64
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string Base64Encode(string plainText)
        {
            if (plainText == null || plainText.Trim() == string.Empty)
            {
                plainText = string.Empty;
            }
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        public string Base64Decode(string base64EncodedData)
        {
            if (base64EncodedData == null || base64EncodedData.Trim() == string.Empty)
            {
                base64EncodedData = string.Empty;
            }
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsBase64String(string value)
        {
            if (value == null || value.Length == 0 || value.Length % 4 != 0 || value.Contains(' ') || value.Contains('\t') || value.Contains('\r') || value.Contains('\n'))
                return false;
            var index = value.Length - 1;
            if (value[index] == '=')
                index--;
            if (value[index] == '=')
                index--;
            for (var i = 0; i <= index; i++)
                if (IsInvalid(value[i]))
                    return false;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsInvalid(char value)
        {
            var intValue = (Int32)value;
            if (intValue >= 48 && intValue <= 57)
                return false;
            if (intValue >= 65 && intValue <= 90)
                return false;
            if (intValue >= 97 && intValue <= 122)
                return false;
            return intValue != 43 && intValue != 47;
        }
    }
    public class ClassHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string getMethods(Type type)
        {
            //List<MethodInfo> methodsList = Type.GetType(type).GetMethods().ToList();
            List<MethodInfo> methodsList = type.GetMethods().ToList();
            List<MethodDefinition> methods = new List<MethodDefinition>();
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                foreach (MethodInfo method in methodsList)
                {
                    List<ParameterInfo> paramss = method.GetParameters().ToList();
                    List<string> paras = new List<string>();
                    if (paramss.Count > 0)
                        paramss.ForEach(p => paras.Add(p.ParameterType + "  " + p.Name));
                    MethodDefinition md = new MethodDefinition();
                    md.Name = method.Name;

                    if (method.ReturnType.Name.ToLower().IndexOf("list") > -1)
                    {
                        md.ReturnType = "List<" + method.ReturnType.GenericTypeArguments[0] + ">";
                    }
                    else if (method.ReturnType.Name.ToLower().IndexOf("dictionary") > -1)
                    {
                        md.ReturnType = "Dictionary<" + method.ReturnType.GenericTypeArguments[0] + "," + method.ReturnType.GenericTypeArguments[1] + " > ";
                    }
                    else
                    {
                        md.ReturnType = method.ReturnType.Name;
                    }

                    var attrs = method.GetCustomAttributesData().ToList();

                    string route = string.Empty;
                    string httptype = string.Empty;

                    //a => a.GetCustomAttributes(typeof(WebInvokeAttribute), true)

                    foreach (var ttr in attrs)
                    {
                        if (ttr.NamedArguments.Count > 0)
                        {
                            foreach (var attru in ttr.NamedArguments)
                            {
                                if (attru.MemberName.ToLower() == "uritemplate")
                                {
                                    route = attru.TypedValue.Value.ToString();
                                }
                                else if (attru.MemberName.ToLower() == "method")
                                {
                                    httptype = attru.TypedValue.Value.ToString();
                                }
                            }
                        }
                    }

                    if (type.IsInterface)
                    {
                        // We get the current assembly through the current class
                        var currentAssembly = this.GetType().GetTypeInfo().Assembly;

                        // we filter the defined classes according to the interfaces they implement
                        List<TypeInfo> iAssemblies = currentAssembly.DefinedTypes.Where(typer => typer.ImplementedInterfaces.Any(inter => inter == type)).ToList();

                        string Classes = string.Empty;
                        Classes = String.Join(", ", iAssemblies.Select(x => x.Name));
                        md.ClassName = Classes;
                    }
                    else
                    {
                        md.ClassName = type.Name;
                    }

                    md.Parameters = string.Join(" , ", paramss);
                    md.Route = route;
                    md.Flag = httptype;

                    methods.Add(md);
                }
                return JsonConvert.SerializeObject(methods);
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return null;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="className"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string InvokeMethod(string methodName, string className, string obj)
        {
            string result = string.Empty;
            //Assembly assembly = this.GetType().GetTypeInfo().Assembly;
            Type typo = Type.GetType("ezBot.services." + className);
            //Type type = assembly.GetType("services." + className);
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                if (typo != null)
                {
                    MethodInfo methodInfo = typo.GetMethod(methodName);

                    if (methodInfo != null)
                    {

                        List<ParameterInfo> parameters = methodInfo.GetParameters().ToList();
                        object classInstance = Activator.CreateInstance(typo, null);

                        if (parameters.Count == 0)
                        {
                            // This works fine
                            result = JsonConvert.SerializeObject(methodInfo.Invoke(classInstance, null));
                        }
                        else
                        {
                            List<string> objers = new List<string>(); List<ParameterDefinitionFactor> ldf = new List<ParameterDefinitionFactor>();
                            objers = obj.Split(',').ToList();

                            object[] parametersArray = new object[] { }; List<object> objList = new List<object>();

                            foreach (var objer in objers)
                            {
                                string name = objer.Split(':')[0].Trim();
                                string value = objer.Split(':')[1].Trim();
                                string type = parameters.FindAll(par => par.Name == name)[0].ParameterType.Name;

                                if (type.Trim() == "String")
                                {
                                    objList.Add(value);
                                }
                                else if (type.Trim().ToLower().IndexOf("int") > -1)
                                {
                                    objList.Add(int.Parse(value));
                                }
                                else if (type.Trim().ToLower().IndexOf("bool") > -1)
                                {
                                    objList.Add(bool.Parse(value));
                                }
                            }


                            parametersArray = objList.ToArray();

                            // The invoke does NOT work;
                            // it throws "Object does not match target type"             
                            result = JsonConvert.SerializeObject(methodInfo.Invoke(classInstance, parametersArray));
                        }
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return ex.Message;
            }

        }


        public class MethodDefinition
        {
            public string Name { get; set; }
            public string ReturnType { get; set; }
            public string Parameters { get; set; }
            public string Route { get; set; }
            public string Flag { get; set; }
            public string Description { get; set; }
            public string ClassName { get; set; }
        }
        public class ParameterDefinitionFactor
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string Value { get; set; }
        }
    }
    public class CurrentWindowsSystemFacts
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DetailsList getSystemDetails()
        {
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                DetailsList dl = new DetailsList();
                dl.Username = Environment.UserName;
                dl.MacAddress = NetworkInterface.GetAllNetworkInterfaces().Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback).Select(nic => nic.GetPhysicalAddress().ToString()).FirstOrDefault();

                dl.IPAddress = string.Empty;

                foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        dl.IPAddress = ip.ToString();
                    }
                }
                dl.SystemName = System.Environment.MachineName;


                return dl;
            }
            catch (Exception ex)
            {
                string ht = ex.Message;
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SERVICENAME"></param>
        /// <returns></returns>
        public string thisSystemServices(string SERVICENAME)
        {
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                ServiceController sc = new ServiceController(SERVICENAME);

                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        return StringHelper.Running;
                    case ServiceControllerStatus.Stopped:
                        return StringHelper.Stopped;
                    case ServiceControllerStatus.Paused:
                        return StringHelper.Paused;
                    case ServiceControllerStatus.StopPending:
                        return StringHelper.Stopping;
                    case ServiceControllerStatus.StartPending:
                        return StringHelper.Starting;
                    default:
                        return StringHelper.StatusChanging;
                }
            }
            catch (Exception ex)
            {
                string htr = ex.Message;
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return "No Such Service Exists";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SERVICENAME"></param>
        /// <param name="MACHINENAME"></param>
        /// <returns></returns>
        public string remoteSystemServices(string SERVICENAME, string MACHINENAME)
        {
            try
            {
                ServiceController sc = new ServiceController(SERVICENAME, MACHINENAME);
                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        return StringHelper.Running;
                    case ServiceControllerStatus.Stopped:
                        return StringHelper.Stopped;
                    case ServiceControllerStatus.Paused:
                        return StringHelper.Paused;
                    case ServiceControllerStatus.StopPending:
                        return StringHelper.Stopping;
                    case ServiceControllerStatus.StartPending:
                        return StringHelper.Starting;
                    default:
                        return StringHelper.StatusChanging;
                }
            }
            catch (Exception ex)
            {
                string htr = ex.Message;
                return "No Such Service Exists";
            }
        }
        public class DetailsList
        {
            public string Username { get; set; }
            public string MacAddress { get; set; }
            public string IPAddress { get; set; }
            public string SystemName { get; set; }
        }
    }
    public class HostedSitesList
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetHostName()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).HostName;
        }
    }
