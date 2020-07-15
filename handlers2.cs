 /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool CheckForBraces(string input)
        {
            if ((input.IndexOf("{") > -1 && input.IndexOf("}") > -1))
            {
                if (subStringCount(input, "{") == subStringCount(input, "}"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool CheckForComma(string input)
        {
            if (input.IndexOf(",") > -1)
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
        /// <param name="MainString"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public int subStringCount(string MainString, string match)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            do
            {
                index = MainString.IndexOf(match, index);
                if (index != -1)
                {
                    sb.Append(match);
                    index++;
                }
            } while (index != -1);

            string repeats = sb.ToString();
            return index;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool CheckForSquares(string input)
        {
            if (input.IndexOf("[") > -1 && input.IndexOf("]") > -1)
            {
                if (subStringCount(input, "[") == subStringCount(input, "]"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool CheckForColon(string input)
        {
            if (input.IndexOf(":") > -1)
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
        /// <param name="input"></param>
        /// <returns></returns>
        public bool CheckForSemiColon(string input)
        {
            if (input.IndexOf(";") > -1)
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
        /// <param name="input"></param>
        /// <returns></returns>
        public DataTable ConvertColonAndSemicolon(string input)
        {
            DataTable returnValue = new DataTable();
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                if (input.IndexOf(";") > -1)
                {
                    List<string> SemiColonParts = new List<string>();
                    SemiColonParts = input.Split(';').ToList();
                    int counter = 0;
                    foreach (string semicolonpart in SemiColonParts)
                    {
                        List<string> ColonParts = new List<string>();
                        ColonParts = semicolonpart.Split(':').ToList();


                        if (counter == 0)
                        {
                            foreach (var colpart in ColonParts)
                            {
                                returnValue.Columns.Add();
                            }
                            returnValue.Rows.Add(ColonParts.ToArray());
                            counter = ColonParts.Count;
                        }
                        else if (counter == ColonParts.Count)
                        {
                            returnValue.Rows.Add(ColonParts.ToArray());
                        }

                    }

                }
                return returnValue;
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
        /// <param name="dataTable"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public List<string> GetListFromDataTable(DataTable dataTable, string column)
        {
            List<string> NewList = new List<string>();
            if (dataTable != null)
            {
                NewList.AddRange(dataTable.AsEnumerable().Select(r => r.Field<string>(column)).ToList());
            }
            return NewList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public string GetCellFromDataTable(DataTable dataTable, string column, string row)
        {
            string result = string.Empty;

            if (column.Trim() != string.Empty && row.Trim() != string.Empty)
            {
                result = dataTable.Rows[int.Parse(row)].Field<string>(column);
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetBracePositionData(string input)
        {
            List<KeyValuePair<string, string>> positions = new List<KeyValuePair<string, string>>();

            positions.AddRange(AllIndexesOf(input, "{"));
            positions.AddRange(AllIndexesOf(input, "}"));


            return positions;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> AllIndexesOf(string str, string value)
        {
            if (String.IsNullOrEmpty(value))
            {

            }
            List<KeyValuePair<string, string>> indexes = new List<KeyValuePair<string, string>>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(new KeyValuePair<string, string>(value, index.ToString()));
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> ArrangeBracePositionData(string input)
        {
            List<KeyValuePair<string, string>> positions = new List<KeyValuePair<string, string>>();
            return positions;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public string DataTableSerialize(DataTable dataTable)
        {
            string result = JsonConvert.SerializeObject(dataTable);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DataTable DataTableDeserialize(string input)
        {
            DataTable returnValue = new DataTable();
            returnValue = JsonConvert.DeserializeObject<DataTable>(input);
            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datatable"></param>
        /// <returns></returns>
        public string DataTableStringified(DataTable datatable)
        {
            string result = string.Empty;
            return result;
        }
        /// <summary>
        /// used for getting and replacing variables in string which is parameterized 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ParameterizedStringConvert(string input)
        {
            string result = string.Empty;



            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string FullParameterizedIndexedStringCount(string input)
        {
            string result = string.Empty;
            return result;
        }
        public class SCPTransfer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="localFilePath"></param>
        /// <param name="remoteFilePath"></param>
        /// <returns></returns>
        public bool UploadWinSCP(string host, string username, string password, string localFilePath, string remoteFilePath)
        {
            bool truer = true;
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                using (ScpClient client = new ScpClient(host, username, password))
                {
                    client.Connect();

                    using (Stream localFile = File.OpenRead(localFilePath))
                    {
                        client.Upload(localFile, remoteFilePath);
                    }

                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                string exp = ex.Message;
                truer = false;
            }
            return truer;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="localFilePath"></param>
        /// <param name="remoteFilePath"></param>
        /// <returns></returns>
        public bool DownloadWinSCP(string host, string username, string password, string localFilePath, string remoteFilePath)
        {
            bool truer = true;
            try
            {
                using (ScpClient client = new ScpClient(host, username, password))
                {
                    client.Connect();

                    using (Stream localFile = File.Create(localFilePath))
                    {
                        client.Download(remoteFilePath, localFile);
                    }

                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                truer = false;
            }
            return truer;
        }
    }
    public class SFTPTransfer
    {
        /*private string ftpPathSrcFolder = "/Path/Source/";//path should end with /
        private string ftpPathDestFolder = "/Path/Archive/";//path should end with /
        private string ftpServerIP = "Target IP";
        private int ftpPortNumber = 80;//change to appropriate port number
        private string ftpUserID = "FTP USer Name";//
        private string ftpPassword = "FTP Password";*/
        /// <summary>
        /// Move first file from one source folder to another. 
        /// Note: Modify code and add a for loop to move all files of the folder
        /// </summary>
        public void MoveFolderToArchive(string ftpServerIP, int ftpPortNumber, string ftpUserID, string ftpPassword, string ftpPathSrcFolder, string ftpPathDestFolder, bool isMulti)
        {
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                if (isMulti)
                {
                    using (SftpClient sftp = new SftpClient(ftpServerIP, ftpPortNumber, ftpUserID, ftpPassword))
                    {
                        List<SftpFile> RemoteFiles = sftp.ListDirectory(ftpPathSrcFolder).ToList();
                        foreach (SftpFile eachRemoteFile in RemoteFiles)
                        {
                            if (eachRemoteFile.IsRegularFile)//Move only file
                            {
                                bool eachFileExistsInArchive = CheckIfRemoteFileExists(sftp, ftpPathDestFolder, eachRemoteFile.Name);

                                //MoveTo will result in error if filename alredy exists in the target folder. Prevent that error by cheking if File name exists
                                string eachFileNameInArchive = eachRemoteFile.Name;

                                if (eachFileExistsInArchive)
                                {
                                    eachFileNameInArchive = eachFileNameInArchive + "_" + DateTime.Now.ToString("MMddyyyy_HHmmss");//Change file name if the file already exists
                                }
                                eachRemoteFile.MoveTo(ftpPathDestFolder + eachFileNameInArchive);
                            }
                        }
                    }
                }
                else
                {
                    using (SftpClient sftp = new SftpClient(ftpServerIP, ftpPortNumber, ftpUserID, ftpPassword))
                    {
                        SftpFile eachRemoteFile = sftp.ListDirectory(ftpPathSrcFolder).First();//Get first file in the Directory            
                        if (eachRemoteFile.IsRegularFile)//Move only file
                        {
                            bool eachFileExistsInArchive = CheckIfRemoteFileExists(sftp, ftpPathDestFolder, eachRemoteFile.Name);

                            //MoveTo will result in error if filename alredy exists in the target folder. Prevent that error by cheking if File name exists
                            string eachFileNameInArchive = eachRemoteFile.Name;

                            if (eachFileExistsInArchive)
                            {
                                eachFileNameInArchive = eachFileNameInArchive + "_" + DateTime.Now.ToString("MMddyyyy_HHmmss");//Change file name if the file already exists
                            }
                            eachRemoteFile.MoveTo(ftpPathDestFolder + eachFileNameInArchive);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
            }

        }

        /// <summary>
        /// Checks if Remote folder contains the given file name
        /// </summary>
        public bool CheckIfRemoteFileExists(SftpClient sftpClient, string remoteFolderName, string remotefileName)
        {
            bool isFileExists = sftpClient
                                .ListDirectory(remoteFolderName)
                                .Any(
                                        f => f.IsRegularFile &&
                                        f.Name.ToLower() == remotefileName.ToLower()
                                    );
            return isFileExists;
        }

    }
    public class Tokenizer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="posId"></param>
        /// <returns></returns>
        public string getPOSText(string posId)
        {
            string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+"/PythonScripts/pos.json");
            Dictionary<string, string> PartsOfSpeech = JsonConvert.DeserializeObject<Dictionary<string,string>>(json);
            try
            {
                return PartsOfSpeech[posId];
            }
            catch(Exception e)
            {
                string ex = e.Message;
                return posId;
            }
        }
        private char[] delimiters_keep_digits = new char[] {
            '{', '}', '(', ')', '[', ']', '>', '<','-', '_', '=', '+',
            '|', '\\', ':', ';', ' ', ',', '.', '/', '?', '~', '!',
            '@', '#', '$', '%', '^', '&', '*', ' ', '\r', '\n', '\t' };

        // This will discard digits 
        private char[] delimiters_no_digits = new char[] {
            '{', '}', '(', ')', '[', ']', '>', '<','-', '_', '=', '+',
            '|', '\\', ':', ';', ' ', ',', '.', '/', '?', '~', '!',
            '@', '#', '$', '%', '^', '&', '*', ' ', '\r', '\n', '\t',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        /// <summary>
        ///  Tokenizes text into an array of words, using whitespace and
        ///  all punctuation as delimiters.
        /// </summary>
        /// <param name="text"> the text to tokenize</param>
        /// <returns> an array of resulted tokens</returns>
        public string[] GreedyTokenize(string text)
        {
            char[] delimiters = new char[] {
            '{', '}', '(', ')', '[', ']', '>', '<','-', '_', '=', '+',
            '|', '\\', ':', ';', ' ', ',', '.', '/', '?', '~', '!',
            '@', '#', '$', '%', '^', '&', '*', ' ', '\r', '\n', '\t' };

            return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        }


        /// <summary>
        ///  Tokenizes a text into an array of words, using whitespace and
        ///  all punctuation except the apostrophe "'" as delimiters. Digits
        ///  are handled based on user choice.
        /// </summary>
        /// <param name="text"> the text to tokenize</param>
        /// <param name="keepDigits"> true to keep digits; false to discard digits.</param>
        /// <returns> an array of resulted tokens</returns>
        public string[] Tokenize(string text, bool keepDigits)
        {
            string[] tokens = null;
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {


                if (keepDigits)
                    tokens = text.Split(delimiters_keep_digits, StringSplitOptions.RemoveEmptyEntries);
                else
                    tokens = text.Split(delimiters_no_digits, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < tokens.Length; i++)
                {
                    string token = tokens[i];

                    // Change token only when it starts and/or ends with "'" and the 
                    // toekn has at least 2 characters. 
                    if (token.Length > 1)
                    {
                        if (token.StartsWith("'") && token.EndsWith("'"))
                            tokens[i] = token.Substring(1, token.Length - 2); // remove the starting and ending "'" 

                        else if (token.StartsWith("'"))
                            tokens[i] = token.Substring(1); // remove the starting "'" 

                        else if (token.EndsWith("'"))
                            tokens[i] = token.Substring(0, token.Length - 1); // remove the last "'" 
                    }
                }

                return tokens;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return tokens;
            }

        }

        /// <summary>
        ///  Tokenizes a text into an array of words, using whitespace and
        ///  all punctuation except the apostrophe "'" as delimiters. Digits
        ///  are handled based on user choice.
        /// </summary>
        /// <param name="filePaht"> the path of the file to tokenize to tokenize</param>
        /// <param name="keepDigits"> true to keep digits; false to discard digits.</param>
        /// <returns> an array of resulted tokens</returns>
        public string[] TokenizeFile(string filePath, bool keepDigits)
        {
            string[] tokens = null;
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    return null;

                if (!(new FileInfo(filePath)).Exists)
                    return null;



                try
                {
                    string text = File.ReadAllText(filePath);
                    tokens = Tokenize(text, keepDigits);
                }
                catch
                {
                }

                return tokens;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return tokens;
            }
        }
    }
    public static class Tool
    {
        private static Tokenizer tokenizer = new Tokenizer();

        /// <summary>
        ///  Splits an n-letter word into a string array. The array's size will be (n-1) * 2. 
        ///  For example, 'carried', a 7-letter word, will be split into a string array
        ///  containing the following (7-1) * 2 = 12 elements:
        ///  {c, arried, ca, rried, car, ried, carr, ied, carri, ed, carri, d}.
        /// </summary>
        /// <param name="word"> the word to split</param>
        /// <returns> all pairs of strings which the word can be split into</returns>
        public static string[] SplitWord(string word)
        {
            int arrSize = (word.Length - 1) * 2;
            string[] strArr = new string[arrSize];
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                //The word must have at least 2 letters 
                if (word.Length < 2)
                    return null;
                int index = 0;

                for (int cutPosition = 1; cutPosition < word.Length; cutPosition++)
                {
                    strArr[index] = word.Substring(0, cutPosition);
                    strArr[index + 1] = word.Substring(cutPosition, word.Length - cutPosition);

                    //Since we collect two elements per time, we need to increase the index by 2. 
                    index += 2;
                }
                return strArr;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return strArr;
            }
        }

        /// <summary>
        ///  Create a string-integer dictionary out of a linked list of tokens.
        /// </summary>
        /// <param name="tokens">  the tokens to create the frequency table. For this
        ///  method, there is no difference between List and LinkedList types. </param>
        private static Dictionary<string, int> BuildFreqTable(LinkedList<string> tokens)
        {
            Dictionary<string, int> token_freq_table = new Dictionary<string, int>();
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                foreach (string token in tokens)
                {
                    if (token_freq_table.ContainsKey(token))
                        token_freq_table[token]++;
                    else
                        token_freq_table.Add(token, 1);
                }

                return token_freq_table;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return token_freq_table;
            }
        }

        /// <summary>
        ///  Make a string-integer dictionary out of an array of words.
        /// </summary>
        /// <param name="words"> the words out of which to make the dictionary</param>
        /// <returns> a string-integer dictionary</returns>
        public static Dictionary<string, int> ToStrIntDict(string text)
        {
            if (text == null)
                return null;

            string[] words = tokenizer.Tokenize(text, false);
            Dictionary<string, int> dict = new Dictionary<string, int>();
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                foreach (string word in words)
                {
                    // if the word is in the dictionary, increment its freq. 
                    if (dict.ContainsKey(word))
                    {
                        dict[word]++;
                    }
                    // if not, add it to the dictionary and set its freq = 1 
                    else
                    {
                        dict.Add(word, 1);
                    }
                }

                return dict;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return dict;
            }
        }

        /// <summary>
        ///  Sort a string-int dictionary by its entries' values.
        /// </summary>
        /// <param name="strIntDict"> a string-int dictionary to sort</param>
        /// <param name="sortOrder"> one of the two enumerations: Ascending and Descending</param>
        /// <returns> a string-integer dictionary sorted by integer values</returns>
        public static Dictionary<string, int> ListWordsByFreq(Dictionary<string, int> strIntDict, bool isDescending)
        {
            Dictionary<string, int> dictByFreq = new Dictionary<string, int>();
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                // Copy keys and values to two arrays 
                string[] words = new string[strIntDict.Keys.Count];
                strIntDict.Keys.CopyTo(words, 0);

                int[] freqs = new int[strIntDict.Values.Count];
                strIntDict.Values.CopyTo(freqs, 0);

                //Sort by freqs: it sorts the freqs array, but it also rearranges 
                //the words array's elements accordingly (not sorting) 
                Array.Sort(freqs, words);

                // If sort order is descending, reverse the sorted arrays. 
                if (isDescending)
                {
                    //reverse both arrays 
                    Array.Reverse(freqs);
                    Array.Reverse(words);
                }

                //Copy freqs and words to a new Dictionary<string, int> 


                for (int i = 0; i < freqs.Length; i++)
                {
                    dictByFreq.Add(words[i], freqs[i]);
                }

                return dictByFreq;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return dictByFreq;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static double GetTypeTokenRatio(string text)
        {
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                double typeTokenRatio = 0.0;

                // Tokenize text into tokens, no digits or punctuations. 
                string[] tokens = tokenizer.Tokenize(text, false);

                // dump array of words into a HashSet of string.  
                HashSet<string> types = new HashSet<string>();

                // HashSet ignores duplicated elements which ensures for us that duplicated words be counted only once. 
                foreach (string token in tokens)
                {
                    types.Add(token);
                }

                // A sanity check: if types set is empty, set typeTokenRatio = double.NaN, i.e. Not a Number.  
                // Otherwise, we'll get a "divided by 0" Exception. 

                if (types.Count == 0)
                {
                    typeTokenRatio = double.NaN;
                }
                else
                {
                    // Be very aware that you need to cast either types.Count or tokens.Length into  
                    // double type; otherwise you'll always get 0 as the result. 
                    typeTokenRatio = (double)types.Count / tokens.Length;
                }

                return typeTokenRatio;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return 1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static double GetFileTypeTokenRatio(string filePath)
        {
            try
            {
                return GetTypeTokenRatio(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                return 1;
            }
        }

        /// <summary>
        ///  Computes the average type token ratio of an array of tokens, based on the window size.
        ///  Algorithm:
        ///  If windowSize >= tokens.Length, average type token ratio is the general type token
        ///  ratio of all tokens.
        /// </summary>
        /// <param name="tokens"> the array of the tokens to calculate the average type token ratio</param>
        /// <param name="windowSize"> the number of the tokens per window</param>
        private static double GetAverageTypeTokenRatio(string filePath, int windowSize)
        {
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                LinkedList<string> movingWindow = new LinkedList<string>();
                string[] tokens = tokenizer.TokenizeFile(filePath, false);

                int index = 0;
                while (index < windowSize)
                {
                    movingWindow.AddLast(tokens[index]);
                    index++;
                }

                // Build frequency table of this window of tokens 
                Dictionary<string, int> movingFreqTable = BuildFreqTable(movingWindow);

                // This type token ratio keeps changing 
                double finalTTR = (double)movingFreqTable.Count / movingWindow.Count;

                int windowCount = 1;

                // Now index stops at windowSize position of the tokens. 
                while (index < tokens.Length)
                {
                    // Check the first token of the moving window of tokens and remove it from the moving window. 
                    string firstToken = movingWindow.First.Value;
                    movingWindow.RemoveFirst();

                    // Check its frequency in the frequency table. If it is 1, it means that this token 
                    // occurs in the moving window only once, so we can safely remove it from the moving 
                    // window; otherwise, it appears more than once, so we cannot delete it but we can 
                    // reduce its frequency by 1. 
                    if (movingFreqTable[firstToken] == 1)
                        movingFreqTable.Remove(firstToken);
                    else
                        movingFreqTable[firstToken]--;

                    // Find the next available token. If it is in the moving frequency table, increase its 
                    // frequency value by 1; otherwise, add it as a new entry and set its frequency to 1. 
                    string newToken = tokens[index];

                    if (movingFreqTable.ContainsKey(newToken))
                        movingFreqTable[newToken]++;
                    else
                        movingFreqTable.Add(newToken, 1);

                    // Add this word to the moving window so that the window always has the same number of tokens. 
                    movingWindow.AddLast(newToken);

                    // Re-compute the type token ratio of this changed window. 
                    double thisTTR = (double)movingFreqTable.Count / windowSize;

                    // Add this new type token ratio to the final type token ratio. 
                    finalTTR += thisTTR;

                    // Update index position and window counters 
                    index++;
                    windowCount++;
                }

                // We need to divided the final type token ratio by the number of windows 
                finalTTR = finalTTR / windowCount;

                return finalTTR;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex);
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                return 1;
            }
        }
    }
    public class StringFunction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseString"></param>
        /// <param name="subString"></param>
        /// <returns></returns>
        public int countSubstring(string baseString, string subString)
        {
            return Regex.Matches(baseString, subString).Count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseString"></param>
        /// <param name="splitString"></param>
        /// <returns></returns>
        public List<string> splitString2List(string baseString, string splitString)
        {
            return baseString.Split(new string[] { splitString }, StringSplitOptions.None).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseString"></param>
        /// <param name="subString"></param>
        /// <returns></returns>
        public bool isSubString(string baseString, string subString)
        {
            return baseString.IndexOf(subString) > -1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseString"></param>
        /// <param name="subString"></param>
        /// <returns></returns>
        public string removeString(string baseString, string subString)
        {
            int index = baseString.IndexOf(subString, StringComparison.Ordinal);
            return (index < 0)
                ? baseString
                : baseString.Remove(index, subString.Length);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        public void printKeysAndValues(string json)
        {
            var jobject = (JObject)((JArray)JsonConvert.DeserializeObject(json))[0];

            List<KeyValuePair<object, object>> kvp = new List<KeyValuePair<object, object>>();

            foreach (var jproperty in jobject.Properties())
            {
                //Console.WriteLine("{0} - {1}", jproperty.Name, jproperty.Value);
                kvp.Add(new KeyValuePair<object, object>(jproperty.Name, jproperty.Value));
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string getFileNameFromPath(string path)
        {
            List<string> filenameparts = new StringFunction().splitString2List(path, "\\");
            string filename = filenameparts[filenameparts.Count - 1];
            return filename;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getApplicationFolderPath()
        {
            string path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
            path = path.Substring(6);
            return path;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getApplicationRootFolderPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string getDateInFormat(DateTime datetime, string format)
        {
            string result = string.Empty;
            try
            {
                result = datetime.ToString(format);
            }
            catch(Exception ex)
            {
                string exceptionresult = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="seperator1"></param>
        /// <param name="seperator2"></param>
        /// <returns></returns>
        public List<string> getStringPart(string Text, string seperator1, string seperator2)
        {
            List<string> finds = new List<string>();
            List<KeyValuePair<string, string>> msgs = new List<KeyValuePair<string, string>>();
            try
            {
                int counter = 0;

                foreach (string sampling in Text.Split(new string[] { seperator1 }, StringSplitOptions.None))
                {
                    if (counter == 0)
                    {

                    }
                    else
                    {
                        Regex reg = new Regex("[;\\\\/:*?\"<>|&']");
                        string str = reg.Replace(sampling, string.Empty);

                        //split at spaces

                        string result = str.Split(new string[] { seperator2, "\n", "\\n" }, StringSplitOptions.None)[0].Trim();
                        result = result.Split(' ')[0];
                        finds.Add(result);
                    }
                    counter++;
                }
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                msgs.Add(new MethodResultHandler().createMessage(ex.Message, StringHelper.exceptioncode));
                finds.Add(message);
            }

            return finds;
        }

        public string getRandomString(string length)
        {
            Random random = new Random();
            string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            int l = 0; var nonceString = new StringBuilder();
            try
            {
                if (int.TryParse(length, out l))
                {
                    
                }
                else
                {
                    l = 32;
                }

                for (int i = 0; i < int.Parse(length); i++)
                {
                    nonceString.Append(validChars[random.Next(0, validChars.Length - 1)]);
                }

                return nonceString.ToString();
            }
            catch(Exception ex)
            {
                string ext = ex.Message;
                return ext;
            }
        }

        public string getParameterizedString(string input, string executionID)
        {
            string output = input;
            
            if(input.IndexOf("{") > -1 && input.IndexOf("}") > -1)
            {
                List<DVariables> dvars = Variables.dynamicvariables.FindAll(d => d.ExecutionID == executionID);
                //dvars.RemoveAll(dvar => dvar.vlname.Trim() != string.Empty);

                foreach(DVariables dvar in dvars)
                {
                    if (dvar.vlname.Trim() != string.Empty || dvar.vlname.Trim () != "" || dvar.vlname.Trim().ToString().Contains("{"))
                    {
                        if (input.IndexOf(dvar.vlname) > -1)
                        {
                            DVariables der = new VariableHandler().getVariables(dvar.vlname, executionID);

                            if (der.vltype == "string")
                            {
                                string vo = der.vlvalue;
                                output = output.Replace(dvar.vlname, vo);
                            }
                            else if (der.vltype == "datatable")
                            {
                                if (input.IndexOf("[") > -1 && input.IndexOf("]") > -1)
                                {
                                    try
                                    {
                                        DataTable vo = JsonConvert.DeserializeObject<DataTable>("[" + der.vlvalue + "]");
                                        int DataIndex = int.Parse(Regex.Match(input, @"\[([^)]*)\]").Groups[1].Value);
                                        output = output.Replace("[" + DataIndex + "]", "");
                                        output = output.Replace(dvar.vlname, vo.Rows[0][DataIndex].ToString());
                                    }
                                    catch(Exception ex)
                                    {
                                        string ext = ex.Message;
                                        return ext;
                                    }
                                }
                                else
                                {
                                    DataTable vo = JsonConvert.DeserializeObject<DataTable>(der.vlvalue);
                                    output = output.Replace(dvar.vlname, vo.Rows[0][0].ToString());
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                output = input;
            }

            return output;
        }
    }
    public class DigestAuthFixer
    {
        private static string _host;
        private static string _user;
        private static string _password;
        private static string _realm;
        private static string _nonce;
        private static string _qop;
        private static string _cnonce;
        private static DateTime _cnonceDate;
        private static int _nc;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public DigestAuthFixer(string host, string user, string password)
        {
            // TODO: Complete member initialization
            _host = host;
            _user = user;
            _password = password;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string CalculateMd5Hash(string input)
        {
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = MD5.Create().ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (var b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        private string GrabHeaderVar(string varName, string header)
        {
            var regHeader = new Regex(string.Format(@"{0}=""([^""]*)""", varName));
            var matchHeader = regHeader.Match(header);
            if (matchHeader.Success)
                return matchHeader.Groups[1].Value;
            throw new ApplicationException(string.Format("Header {0} not found", varName));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private string GetDigestHeader(string dir)
        {
            _nc = _nc + 1;

            var ha1 = CalculateMd5Hash(string.Format("{0}:{1}:{2}", _user, _realm, _password));
            var ha2 = CalculateMd5Hash(string.Format("{0}:{1}", "GET", dir));
            var digestResponse = CalculateMd5Hash(string.Format("{0}:{1}:{2:00000000}:{3}:{4}:{5}", ha1, _nonce, _nc, _cnonce, _qop, ha2));

            return string.Format("Digest username=\"{0}\", realm=\"{1}\", nonce=\"{2}\", uri=\"{3}\", " + "algorithm=MD5, response=\"{4}\", qop={5}, nc={6:00000000}, cnonce=\"{7}\"", _user, _realm, _nonce, dir, digestResponse, _qop, _nc, _cnonce);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public string GrabResponse(string dir)
        {
            var url = _host + dir;
            var uri = new Uri(url);

            var request = (HttpWebRequest)WebRequest.Create(uri);

            // If we've got a recent Auth header, re-use it!
            if (!string.IsNullOrEmpty(_cnonce) && DateTime.Now.Subtract(_cnonceDate).TotalHours < 1.0)
            {
                request.Headers.Add("Authorization", GetDigestHeader(dir));
            }

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                // Try to fix a 401 exception by adding a Authorization header
                if (ex.Response == null || ((HttpWebResponse)ex.Response).StatusCode != HttpStatusCode.Unauthorized)
                    throw;

                var wwwAuthenticateHeader = ex.Response.Headers["WWW-Authenticate"];
                _realm = GrabHeaderVar("realm", wwwAuthenticateHeader);
                _nonce = GrabHeaderVar("nonce", wwwAuthenticateHeader);
                _qop = GrabHeaderVar("qop", wwwAuthenticateHeader);

                _nc = 0;
                _cnonce = new Random().Next(123400, 9999999).ToString();
                _cnonceDate = DateTime.Now;

                var request2 = (HttpWebRequest)WebRequest.Create(uri);
                request2.Headers.Add("Authorization", GetDigestHeader(dir));
                response = (HttpWebResponse)request2.GetResponse();
            }
            var reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd();
        }
    }
    public class FileDownloadHandler
    {
        public KeyValuePair<string, byte[]> getFileNameandContent(string fileName, string foldername)
        {
            string filePath = Path.Combine(foldername, fileName);

            new Logger().LogException(Environment.CurrentDirectory);
            new Logger().LogException(Directory.GetCurrentDirectory());
            new Logger().LogException(System.Reflection.Assembly.GetExecutingAssembly().Location);
            new Logger().LogException(AppDomain.CurrentDomain.BaseDirectory);
            new Logger().LogException(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase));

            string fullPath = Path.Combine(Environment.CurrentDirectory, filePath);

            if (System.IO.File.Exists(fullPath))
            {
                return new KeyValuePair<string, byte[]>(fullPath, System.IO.File.ReadAllBytes(fullPath));
            }
            else
            {
                return new KeyValuePair<string, byte[]>(fullPath, null);
            }
        }
    }
    public class DataTableHandler
    {
        public DataTable SeperatedByRowNumber(DataTable dty, int rowNumber, int resultDatatableNumber)
        {
            DataTable resultDT = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            dt1 = dty.Clone();
            dt2 = dty.Clone();

            for(int h=0; h<dty.Rows.Count; h++)
            {
                if(h < rowNumber)
                {
                    List<object> list = dty.Rows[h].ItemArray.ToList();
                    dt1.Rows.Add(list.ToArray());
                }
                else
                {
                    List<object> list = dty.Rows[h].ItemArray.ToList();
                    dt2.Rows.Add(list.ToArray());
                }
            }

            if(resultDatatableNumber == 1)
            {
                resultDT = dt2.Copy();
            }
            else
            {
                resultDT = dt1.Copy();
            }

            return resultDT;
        }
        public DataTable SeperatedByRow(DataTable dty, List<object> vs, int resultDatatableNumber)
        {
            DataTable resultDT = new DataTable();
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();

            int counter = 0, tableCounter = 0;
            foreach(DataRow dry in dty.Rows)
            {
                if(counter == 0)
                {
                    if(dataTable.Rows.Count > 0)
                    {
                        dataTable.TableName = tableCounter.ToString();
                        DataTable data = new DataTable();
                        //data = dataTable.Copy();
                        data = dataTable.Clone();
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            data.Rows.Add(dr.ItemArray);
                        }
                        dataSet.Tables.Add(data);
                        tableCounter++;
                    }
                    dataTable.Rows.Clear();
                    dataTable.Columns.Clear();
                    dataTable.Clear();
                    foreach (DataColumn dcy in dty.Columns)
                    {
                        dataTable.Columns.Add(dcy.ColumnName, dcy.DataType);
                    }
                }

                List<object> list = dry.ItemArray.ToList();
                if(list == vs)
                {
                    counter = 0;
                }
                else
                {
                    int rowCounter = 0;
                    for(int t=0; t < vs.Count; t++)
                    {
                        if(vs[t].ToString().ToLower().Trim() == list[t].ToString().ToLower().Trim())
                        {
                            rowCounter = rowCounter + 1;
                        }
                    }

                    if(rowCounter == vs.Count)
                    {
                        counter = 0;
                    }
                    else
                    {
                        dataTable.Rows.Add(list.ToArray());
                        counter++;
                    }
                }
            }

            if (dataTable.Rows.Count > 0)
            {
                dataTable.TableName = tableCounter.ToString();
                DataTable data = new DataTable();
                //data = dataTable.Copy();
                data = dataTable.Clone();
                foreach(DataRow dr in dataTable.Rows)
                {
                    data.Rows.Add(dr.ItemArray);
                }
                dataSet.Tables.Add(data);
                tableCounter++;
            }

            if(dataSet.Tables.Count > 0)
            {
                resultDT = dataSet.Tables[resultDatatableNumber].Copy();
            }

            return resultDT;
        }
    }
    public class StatusModel
    {
        
        public StatusModel(string Code, string Message, List<KeyValuePair<string, string>> Method, string Status)
        {
            this.Code = Code;
            this.Message = Message;
            this.Method = Method;
            this.Status = Convert.ToBoolean(Status);
        }
        public StatusModel()
        {

        }
        public string Code { get; set; }
        public string Message { get; set; }
        public List<KeyValuePair<string, string>> Method { get; set; }
        public bool Status { get; set; }

    }
