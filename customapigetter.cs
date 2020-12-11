        public string getServices()
        {
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                string result = string.Empty;

                List<MethodDefinition> md = new List<MethodDefinition>();

                string result1 = new ClassHandler().getMethods(typeof(ICrudService));
                List<MethodDefinition> lmd1 = JsonConvert.DeserializeObject<List<MethodDefinition>>(result1);

                string result2 = new ClassHandler().getMethods(typeof(IDesktopService));
                List<MethodDefinition> lmd2 = JsonConvert.DeserializeObject<List<MethodDefinition>>(result2);

                string result3 = new ClassHandler().getMethods(typeof(IExecutionService));
                List<MethodDefinition> lmd3 = JsonConvert.DeserializeObject<List<MethodDefinition>>(result3);

                md.AddRange(lmd1);
                md.AddRange(lmd2);
                md.AddRange(lmd3);

                result = JsonConvert.SerializeObject(md);

                return result;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return null;
            }
        }
        public string postServices(string obj, string service, string name)
        {
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                string result = string.Empty;
                result = new ClassHandler().InvokeMethod(name, service, obj);

                return result;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return null;
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
