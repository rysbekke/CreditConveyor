using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using СreditСonveyor.Data.Partners;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using System.Threading;
using System.Net.Http.Headers;
using Zamat;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using static СreditСonveyor.Data.GeneralController;

namespace СreditСonveyor.Data
{
    public class GeneralController
    {
        // GET: General
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        static string connectionStringZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();
        static string connectionStringOBAPIAddress = ConfigurationManager.ConnectionStrings["connectionStringOBAPIAddress"].ToString();




        //public string getDestinationFolder()
        //{
        //    //string destinationFolder = Server.MapPath("~/") + filedir; // 1-вариант
        //    string destinationFolder = @"E:\Uploadfiles\Credits\Dcb"; // 2-вариант
        //    //string destinationFolder = @"C:\Uploadfiles\Credits\Dcb"; // 2-вариант
        //    return destinationFolder;
        //}

        /***********************************/


        //public string filenameAddExt(string destinationFile, string destinationFolder)
        //{
        //    //string destinationFolder = getDestinationFolder();
        //    int temp_ext = 0;
        //    while (System.IO.File.Exists(destinationFile))
        //    {
        //        temp_ext = DateTime.Now.Millisecond;
        //        string ext_name = System.IO.Path.GetExtension(destinationFile);
        //        string filename_no_ext = System.IO.Path.GetFileNameWithoutExtension(destinationFile) + "_" + temp_ext;
        //        string fileName = filename_no_ext + temp_ext + ext_name;
        //        destinationFile = destinationFolder + fileName;
        //    }
        //    return destinationFile;
        //}

        ///***********************************/

        //static string CheckFileDirs(string destinationFolder)
        //{
        //    //if (!System.IO.Directory.Exists(Server.MapPath("~/") + filedir))
        //    //    System.IO.Directory.CreateDirectory(Server.MapPath("~/") + filedir);

        //    string Year = DateTime.Now.Year.ToString();
        //    string Month = (DateTime.Now.Month < 10) ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
        //    string Day = (DateTime.Now.Day < 10) ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
        //    //string Day = DateTime.Now.Day.ToString();
        //    destinationFolder = destinationFolder + "\\" + Year + "\\" + Month + "\\" + Day;
        //    if (!System.IO.Directory.Exists(destinationFolder))
        //        System.IO.Directory.CreateDirectory(destinationFolder);


        //    //string filename_no_ext = System.IO.Path.GetFileNameWithoutExtension(filepath) + "_" + temp_ext;
        //    string randomdir = System.IO.Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
        //    while (System.IO.Directory.Exists(destinationFolder + "\\" + randomdir))
        //    {
        //        //randomdir = DateTime.Now.Millisecond.ToString();
        //        randomdir = Path.GetRandomFileName();
        //        //destinationFolder = destinationFolder + randomdir;
        //    }

        //    if (!System.IO.Directory.Exists(destinationFolder + "\\" + randomdir))
        //        System.IO.Directory.CreateDirectory(destinationFolder + "\\" + randomdir);
        //    return destinationFolder + "\\" + randomdir;
        //}

        ///************************************/

        //public string copyFile(string uriString, string destinationFolder)
        //{
        //    //String uriString = "https://testdoscredobank.local:82/media/online_credit/Screenshot_1_ZgokWby.jpg";
        //    var uri = new Uri(uriString);
        //    //var fileName = uri.Segments.Last();
        //    var fileName = HttpUtility.UrlDecode(uri.Segments.Last());
        //    var remoteUri = uriString.Remove(uriString.Length - fileName.Length, fileName.Length);

        //    //File1 = HttpUtility.UrlDecode(File1);

        //    WebClient myWebClient = new WebClient();
        //    // Concatenate the domain with the Web resource filename.
        //    string myStringWebResource = remoteUri + fileName;
        //    //string myStringWebResource2 = uriString;
        //    //Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);

        //    //Trust all certificates
        //    System.Net.ServicePointManager.ServerCertificateValidationCallback =
        //        ((sender, certificate, chain, sslPolicyErrors) => true);


        //    destinationFolder = CheckFileDirs(destinationFolder);
        //    string destinationFile = destinationFolder + "\\" + fileName;

        //    destinationFile = filenameAddExt(destinationFile, destinationFolder);

        //    // Download the Web resource and save it into the current filesystem folder.
        //    myWebClient.DownloadFile(myStringWebResource, destinationFile);

        //    try
        //    {
        //        //  File.Copy(sourceFile, destinationFile, true);
        //    }
        //    catch (IOException iox)
        //    {
        //        //Console.WriteLine(iox.Message);
        //    }

        //    //Console.ReadKey(true);

        //    return destinationFile;
        //}

        /************************************/

        protected void InitializeCulture()
        {
            CultureInfo CI = new CultureInfo("pt-PT");
            CI.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";

            Thread.CurrentThread.CurrentCulture = CI;
            Thread.CurrentThread.CurrentUICulture = CI;
            InitializeCulture();
        }

        /************************************/




        public string DateRandodir(string destinationFolder)
        {
            //if (!System.IO.Directory.Exists(Server.MapPath("~/") + filedir))
            //    System.IO.Directory.CreateDirectory(Server.MapPath("~/") + filedir);

            string Year = DateTime.Now.Year.ToString();
            string Month = (DateTime.Now.Month < 10) ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            string Day = (DateTime.Now.Day < 10) ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
            //string Day = DateTime.Now.Day.ToString();
            string dateFolder = Year + "\\" + Month + "\\" + Day;
            string destinationFolderDate = destinationFolder + "\\" + dateFolder;
            if (!System.IO.Directory.Exists(destinationFolderDate))
                System.IO.Directory.CreateDirectory(destinationFolderDate);


            //string filename_no_ext = System.IO.Path.GetFileNameWithoutExtension(filepath) + "_" + temp_ext;
            string randomdir = System.IO.Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            while (System.IO.Directory.Exists(destinationFolderDate + "\\" + randomdir))
            {
                //randomdir = DateTime.Now.Millisecond.ToString();
                randomdir = Path.GetRandomFileName();
                //destinationFolder = destinationFolder + randomdir;
            }

            if (!System.IO.Directory.Exists(destinationFolderDate + "\\" + randomdir))
                System.IO.Directory.CreateDirectory(destinationFolderDate + "\\" + randomdir);
            return dateFolder + "\\" + randomdir;
        }



        public string fileNameAddExt(string destinationFile, string destinationFolder, string dateRandodir) //Возврат файл + ext
        {
            //string destinationFolder = getDestinationFolder();
            //int temp_ext = 0;
            //string ext_name = System.IO.Path.GetExtension(destinationFile);
            //string filename_no_ext = System.IO.Path.GetFileNameWithoutExtension(destinationFile) + "_" + temp_ext;
            //string fileName = filename_no_ext + ext_name;
            //string destinationFoldereFile = destinationFolder + fileName;
            string destinationFoldereFile = destinationFolder + "\\" + dateRandodir + "\\" + destinationFile;
            string fileName = destinationFile;

            while (System.IO.File.Exists(destinationFoldereFile))
            {
                int temp_ext = DateTime.Now.Millisecond;
                string ext_name = System.IO.Path.GetExtension(destinationFile);
                string filename_no_ext = System.IO.Path.GetFileNameWithoutExtension(destinationFile) + "_" + temp_ext;
                fileName = filename_no_ext + temp_ext + ext_name;
                destinationFoldereFile = destinationFolder + fileName;
            }
            return fileName;
        }



        public string copyFile(string uriString, string destinationFolder, string dateRandodir)
        {
            //String uriString = "https://testdoscredobank.local:82/media/online_credit/Screenshot_1_ZgokWby.jpg";
            var uri = new Uri(uriString);
            //var fileName = uri.Segments.Last();
            var fileName = HttpUtility.UrlDecode(uri.Segments.Last());
            var remoteUri = uriString.Remove(uriString.Length - fileName.Length, fileName.Length);

            //File1 = HttpUtility.UrlDecode(File1);

            WebClient myWebClient = new WebClient();
            // Concatenate the domain with the Web resource filename.
            string myStringWebResource = remoteUri + fileName;
            //string myStringWebResource2 = uriString;
            //Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);

            //Trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);

            //string sourceFile = @"D:\\remove\\ConsoleApp1\\ConsoleApp1\\bin\\Debug" + "\\" + fileName;
            //string sourceFile = Environment.CurrentDirectory + "\\" + fileName;
            //string sourceFile = Environment.CurrentDirectory + "\\" + fileName;

            //destinationFolder = CheckFileDirs(destinationFolder);
            //string destinationFile = destinationFolder + "\\" + fileName;

            //destinationFile = filenameAddExt(destinationFile);

            string destinationFile = fileNameAddExt(fileName, destinationFolder, dateRandodir);

            // Download the Web resource and save it into the current filesystem folder.
            myWebClient.DownloadFile(myStringWebResource, destinationFolder + "\\" + dateRandodir + "\\" + destinationFile);
            //Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", fileName, myStringWebResource);
            //string path;
            //path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            //Console.WriteLine("\nDownloaded file saved in the following file system folder:\n\t" + path);






            //string destinationFile = @"d:\remove\qwer.jpg";

            //string filepath = Server.MapPath("~/") + filedir + "\\" + filename;
            //int temp_ext = 0;
            //while (System.IO.File.Exists(destinationFile))
            //{
            //    temp_ext = DateTime.Now.Millisecond;
            //    string ext_name = System.IO.Path.GetExtension(destinationFile);
            //    string filename_no_ext = System.IO.Path.GetFileNameWithoutExtension(destinationFile) + "_" + temp_ext;
            //    fileName = filename_no_ext + temp_ext + ext_name;
            //    destinationFile = destinationFolder + fileName;
            //}




            try
            {
                //  File.Copy(sourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                //Console.WriteLine(iox.Message);
            }

            //Console.ReadKey(true);

            return destinationFile;
        }


        public string copyFileBee(string uriString, string destinationFolder, string dateRandodir)
        {
            //String uriString = "https://testdoscredobank.local:82/media/online_credit/Screenshot_1_ZgokWby.jpg";
            var uri = new Uri(uriString);
            //var fileName = uri.Segments.Last();
            var fileName = HttpUtility.UrlDecode(uri.Segments.Last());
            var remoteUri = uriString.Remove(uriString.Length - fileName.Length, fileName.Length);
            //File1 = HttpUtility.UrlDecode(File1);

            // Concatenate the domain with the Web resource filename.
            string myStringWebResource = remoteUri + fileName;
            string destinationFile = fileNameAddExt(fileName, destinationFolder, dateRandodir) + ".jpg";

            string username = "admin";
            string password = "{bcrypt}$2y$12$2NifkACWpuREnmUu8vOVM.ncKRzHdsMcThijJJHWyvt9b27WNH.2W";


            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(username, password);
                    //client.DownloadFile(uriString, destinationFolder + "\\" + dateRandodir + "\\" + destinationFile);
                    client.DownloadFile(uriString, destinationFolder + "\\" + dateRandodir + "\\" + destinationFile);

                }
            }
            catch (IOException iox)
            {
                //Console.WriteLine(iox.Message);
            }

            //Console.ReadKey(true);

            return destinationFile;
        }


        public static int getRoleIDfromСreditСonveyor(string username)
        {
            //var psw = password;
            //var sha1 = System.Security.Cryptography.SHA1.Create(psw);
            //string sha1 = tosha(password);
            //string sha1 = SHA1HashStringForUTF8String(password);
            dbdataDataContext dbZ = new dbdataDataContext(connectionStringZ);
            var usr = (from u in dbZ.Users2s where (u.UserName == username) select u).ToList();
            if (usr.Count > 0)
            {
                var roles = dbZ.RequestsUsersRoles.Where(ur => ur.UserID == usr.FirstOrDefault().UserID).ToList();
                int userRoleID = -1;
                userRoleID = Convert.ToInt32(roles.FirstOrDefault().RoleID);
                return userRoleID;
            }


            else return -1;
        }

        public bool check_security_page(string UserName, string fileName1, string fileName2)
        {
            //dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            //string userName = Session["UserName"] as string;

            //string path = HttpContext.Current.Request.Url.AbsolutePath;

            //string UserName = (Session["UserName"] != null) ? Session["UserName"].ToString():"";
            //if (Session["UserName"] == null)
            bool f = false;
            if (UserName == "")
            {
                //var usr = (from u in dbR.Users where (u.UserName == userName) select u).FirstOrDefault();
                //hfUserID.Value = usr.UserID.ToString();
                //Response.Redirect("/Account/Login");
                f = false;
            }
            else
            {
                //string UserName = Session["UserName"].ToString();
                int userRoleID = GeneralController.getRoleIDfromСreditСonveyor(UserName);

                //string[] file = Request.CurrentExecutionFilePath.Split('/');
                //string fileName = file[file.Length - 1];
               

                //if (fileName == "Dashboard.aspx")


                if (userRoleID == 8) //агент билайн
                {
                    if (fileName2.Contains("AgentView")) f = true;
                    if (fileName2.Contains("Card")) f = true;
                    //menuItem.NavigateUrl = "/Beeline/AgentView.aspx";
                    //menuItem2.NavigateUrl = "/Card/Card.aspx";
                }

                if (userRoleID == 9) //админ билайн
                {
                    if (fileName2.Contains("AgentView")) f = true;
                    //menuItem.NavigateUrl = "/Beeline/AgentView.aspx";
                }


                if (userRoleID == 1) //агент нур
                {
                    if (fileName2.Contains("Nurtelecom")) f = true;
                    //if (fileName2.Contains("Card")) f = true;
                    //menuItem.NavigateUrl = "/Nurtelecom/Nurtelecom.aspx";
                    //menuItem2.NavigateUrl = "/Card/Card.aspx";
                }

                if (userRoleID == 4) //админ нур
                {
                    if (fileName2.Contains("Nurtelecom")) f = true;
                    //if (fileName2.Contains("Card")) f = true;
                    //menuItem.NavigateUrl = "/Nurtelecom/Nurtelecom.aspx";
                }

                if (userRoleID == 13) //агент техно
                {
                    if (fileName2.Contains("Partners")) f = true;
                    if (fileName1.Contains("Partners")) f = true;
                    //menuItem.NavigateUrl = "/Partners/Partners.aspx";
                }

                if ((userRoleID == 2) || (userRoleID == 5)) //эксперты
                {
                    if (fileName2.Contains("Nurtelecom")) f = true;
                    if (fileName2.Contains("AgentView")) f = true;
                    if (fileName2.Contains("Partners")) f = true;
                    if (fileName1.Contains("Microcredit")) f = true;
                    if (fileName1.Contains("Partners")) f = true;
                    //if (fileName2.Contains("Loans")) f = true;
                    //menuItem.NavigateUrl = "/Nurtelecom/Nurtelecom.aspx";
                    //menuItem2.NavigateUrl = "/Beeline/AgentView.aspx";
                    //menuItem3.NavigateUrl = "/Partners/Partners.aspx";
                }

                if (userRoleID == 19) //опер кард
                {
                    if (fileName2.Contains("Card")) f = true;
                    //if (fileName2.Contains("Microcredit")) f = true;
                    if (fileName1.Contains("Microcredit")) f = true;
                    //menuItem.NavigateUrl = "/Card/Card.aspx";
                }

                if (userRoleID == 20) //опер ГБ кард
                {
                    if (fileName2.Contains("Card")) f = true;
                    //menuItem.NavigateUrl = "/Card/Card.aspx";
                }

                if (userRoleID == 24) //эксперт бизнес
                {
                    if (fileName2.Contains("Partners")) f = true;
                    //menuItem.NavigateUrl = "/Card/Card.aspx";
                }

                if (userRoleID == 25) //админ скоринг
                {
                    if (fileName2.Contains("Webadmin")) f = true;
                    //menuItem.NavigateUrl = "/Card/Card.aspx";
                }

                if (UserName == "rtentiev")
                {
                    if (fileName2.Contains("Partners")) f = true;
                    if (fileName2.Contains("Webadmin")) f = true;
                    //menuItem4.NavigateUrl = "/Webadmin/Webadmin.aspx";
                }

                //if (f == false) Response.Redirect("/Account/Login");
            }

            return f;
        }





        /***********************************************************/
        public string CreateRequestWithAPI(Root root)
        {
            string rt = root.ToString();

            string resultContent = "";
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //var json = new JavaScriptSerializer().Serialize(customerD);
                    //var json = new System.Text.Json.JsonSerializer.Serialize(customerD);
                    var json = System.Text.Json.JsonSerializer.Serialize(root).Replace("{\"File\":null}", "").Replace("{\"File\":\"\"}", ""); ;
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(root).Replace("{\"File\":null}", "").Replace("{\"File\":\"\"}", ""); ;

                    var response = client.PostAsync("https://" + connectionStringOBAPIAddress + "/OnlineBank.IntegrationService/api/Loans/CreateLoanRequest", new StringContent(json, Encoding.UTF8, "application/json"));

                    while (response.Status == System.Threading.Tasks.TaskStatus.WaitingForActivation)
                    {

                    }
                    var result = response.Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;
                    string creditID = getCreditID(resultContent);
                    //var result = await response.Content.ReadAsStringAsync();
                    //if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //var list = await Task.Run(() => JsonConvert.DeserializeObject<List<MyObject>>(response.Content));
                        //var data = await Task.Run(() => JsonConvert.DeserializeObject(result));
                        //var data = JsonConvert.DeserializeObject(result);
                        //var data = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        //var statusCode = data.ToString();
                        //TextBox1.Text = statusCode.ToString();
                        //Results data = await Task.Run(() => Newtonsoft.Json.JsonConvert.DeserializeObject<Results>(result));
                        //result = "200";
                    }
                    //var statusCode = data["statusCode"];
                    return resultContent;
                }
            }
            catch (Exception ex)
            {
                //TextBox1.Text = TextBox1.Text + ex.Message;
                return resultContent + ex.ToString();
            }
            finally
            {
                //TextBox1.Text = TextBox1.Text + Response.ToString();
                //return "";
            }
        }
        /************************************/

        public string UpdateRequestWithAPI(RootUpdate root)
        {
            string rt = root.ToString();

            string resultContent = "";
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //var json = new JavaScriptSerializer().Serialize(customerD);
                    //var json = new System.Text.Json.JsonSerializer.Serialize(customerD);
                    var json = (System.Text.Json.JsonSerializer.Serialize(root).Replace("{\"File\":null}", "")).Replace("{\"File\":\"\"}", ""); 
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(root).Replace("{\"File\":null}", "").Replace("{\"File\":\"\"}", ""); 


                    var response = client.PostAsync("https://" + connectionStringOBAPIAddress + "/OnlineBank.IntegrationService/api/Loans/UpdateLoanRequest", new StringContent(json, Encoding.UTF8, "application/json"));

                    while (response.Status == System.Threading.Tasks.TaskStatus.WaitingForActivation)
                    {

                    }
                    var result = response.Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;
                    string creditID = getCreditID(resultContent);


                    //var result = await response.Content.ReadAsStringAsync();
                    //if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //var list = await Task.Run(() => JsonConvert.DeserializeObject<List<MyObject>>(response.Content));
                        //var data = await Task.Run(() => JsonConvert.DeserializeObject(result));
                        //var data = JsonConvert.DeserializeObject(result);
                        //var data = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        //var statusCode = data.ToString();
                        //TextBox1.Text = statusCode.ToString();
                        //Results data = await Task.Run(() => Newtonsoft.Json.JsonConvert.DeserializeObject<Results>(result));
                        //result = "200";
                    }
                    //var statusCode = data["statusCode"];
                    return resultContent;
                }
            }
            catch (Exception ex)
            {
                //TextBox1.Text = TextBox1.Text + ex.Message;
                return resultContent + ex.ToString();
            }
            finally
            {
                //TextBox1.Text = TextBox1.Text + Response.ToString();
                //return "";
            }
        }
        /**************************************/

        public string UpdateRequestDynWithAPI(object root)
        {
            string rt = root.ToString();

            string resultContent = "";
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //var json = new JavaScriptSerializer().Serialize(customerD);
                    //var json = new System.Text.Json.JsonSerializer.Serialize(customerD);
                    var json = System.Text.Json.JsonSerializer.Serialize(root);
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(root);

                    var response = client.PostAsync("https://" + connectionStringOBAPIAddress + "/OnlineBank.IntegrationService/api/Loans/UpdateLoanRequest", new StringContent(json, Encoding.UTF8, "application/json"));

                    while (response.Status == System.Threading.Tasks.TaskStatus.WaitingForActivation)
                    {

                    }
                    var result = response.Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;
                    string creditID = getCreditID(resultContent);
                    //var result = await response.Content.ReadAsStringAsync();
                    //if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //var list = await Task.Run(() => JsonConvert.DeserializeObject<List<MyObject>>(response.Content));
                        //var data = await Task.Run(() => JsonConvert.DeserializeObject(result));
                        //var data = JsonConvert.DeserializeObject(result);
                        //var data = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        //var statusCode = data.ToString();
                        //TextBox1.Text = statusCode.ToString();
                        //Results data = await Task.Run(() => Newtonsoft.Json.JsonConvert.DeserializeObject<Results>(result));
                        //result = "200";
                    }
                    //var statusCode = data["statusCode"];
                    return resultContent;
                }
            }
            catch (Exception ex)
            {
                //TextBox1.Text = TextBox1.Text + ex.Message;
                return resultContent + ex.ToString();
            }
            finally
            {
                //TextBox1.Text = TextBox1.Text + Response.ToString();
                //return "";
            }
        }

        /********************************/

        /**************************************/

        public string CreateCustomerWithAPI(object root)
        {
            string rt = root.ToString();

            string resultContent = "";
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var options1 = new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                        WriteIndented = true
                    };
                   // JsonSerializerOptions jso = new JsonSerializerOptions();
                   // jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;


                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var json = System.Text.Json.JsonSerializer.Serialize(root, options1).Replace("\r\n","");
                    
                   // string jsonString = System.Text.Json.JsonSerializer.Serialize(root, jso);
                    var response = client.PostAsync("https://" + connectionStringOBAPIAddress + "/OnlineBank.IntegrationService/api/customer/Add", new StringContent(json, Encoding.UTF8, "application/json"));
                    while (response.Status == System.Threading.Tasks.TaskStatus.WaitingForActivation)
                    {

                    }
                    var result = response.Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;
                    //string creditID = getCreditID(resultContent);
                    return resultContent.Replace("\"", "");
                }
            }
            catch (Exception ex)
            {
                //TextBox1.Text = TextBox1.Text + ex.Message;
                return resultContent + ex.ToString();
            }
            //finally
            //{
            //    //TextBox1.Text = TextBox1.Text + Response.ToString();
            //    //return "";
            //    return resultContent;
            //}
        }

        /********************************/

        public string UpdateRequestJsonWithAPI(string json)
        {
            //string rt = root.ToString();

            string resultContent = "";
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.PostAsync("https://" + connectionStringOBAPIAddress + "/OnlineBank.IntegrationService/api/Loans/UpdateLoanRequest", new StringContent(json, Encoding.UTF8, "application/json"));

                    while (response.Status == System.Threading.Tasks.TaskStatus.WaitingForActivation)
                    {

                    }
                    var result = response.Result;
                    resultContent = result.Content.ReadAsStringAsync().Result;
                    //string creditID = getCreditID(resultContent);
                    return resultContent.Replace("\"", "");
                }
            }
            catch (Exception ex)
            {
                //TextBox1.Text = TextBox1.Text + ex.Message;
                return resultContent + ex.ToString();
            }
            finally
            {
                //TextBox1.Text = TextBox1.Text + Response.ToString();
                //return "";
            }
        }
        /*******************************/


        public class respCreateReq
        {
            public string CreditID { get; set; }
            public string CreditStatus { get; set; }
        }
        public string getCreditID(string result)
        {
            try
            {

                respCreateReq dez = JsonConvert.DeserializeObject<respCreateReq>(result.ToString());
                if (dez.CreditID != null)
                    return dez.CreditID.ToString();
                else return result;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /******************************/

        public class Data
        {
            public string Surname { get; set; }
            public string CustomerName { get; set; }
            public string Otchestvo { get; set; }
            public int CustomerTypeID { get; set; }
            ////    PersonActivityTypeID = 19,
            ////    WorkTypeID = 0,
            public bool IsResident { get; set; }
            public string IdentificationNumber { get; set; }
            public string DocumentSeries { get; set; }
            public string DocumentNo { get; set; }
            public DateTime IssueDate { get; set; }
            public DateTime DocumentValidTill { get; set; }
            public bool IsDocUnlimited { get; set; }
            public string IssueAuthority { get; set; }
            public bool Sex { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string RegistrationStreet { get; set; }
            public string RegistrationHouse { get; set; }
            public string RegistrationFlat { get; set; }
            public string ResidenceStreet { get; set; }
            public string ResidenceHouse { get; set; }
            public string ResidenceFlat { get; set; }
            public int DocumentTypeID { get; set; }
            public int NationalityID { get; set; }
            public int BirthCountryID { get; set; }
            public int RegistrationCountryID { get; set; }
            public int ResidenceCountryID { get; set; }
            public int BirthCityID { get; set; }
            public int RegistrationCityID { get; set; }
            public int ResidenceCityID { get; set; }
            public string ContactPhone1 { get; set; }
        }


        public class IncomesStructure
        {
            public int CurrencyID { get; set; }
            public decimal TotalPercents { get; set; }
        }

        public class IncomesStructuresActualDate
        {
            public DateTime ActualDate { get; set; }
            public List<IncomesStructure> IncomesStructures { get; set; }
        }

        public class Picture
        {
            //public string FileName { get; set; }
            //public DateTime ChangeDate { get; set; }
            public string File { get; set; }
        }

        public class Partner
        {
            public int PartnerCompanyID { get; set; }
            public decimal LoanPartnerSumV { get; set; }
            public decimal CommissionSum { get; set; }
            public int IssueComissionPaymentTypeID { get; set; }
        }

        public class Guarantor
        {
            public int CustomerID { get; set; }
            public decimal GuaranteeAmount { get; set; }
            public DateTime StartDate { get; set; }
            //public DateTime EndDate { get; set; }
            public int Status { get; set; }
        }

        public class CreditOfficer
        {
            public string OfficerUserName { get; set; }
            public int CreditOfficerTypeID { get; set; }
            public DateTime CreditOfficerStartDate { get; set; }
            public int OfficerID { get; set; }
        }

        public class Root
        {
            public int CustomerID { get; set; }
            public int ProductID { get; set; }
            //public int LoanStatus { get; set; }
            public int CreditID { get; set; }
            public int MortrageTypeID { get; set; }
            public int IncomeApproveTypeID { get; set; }
            public int RequestCurrencyID { get; set; }
            public decimal RequestSumm { get; set; }
            public int MarketingSourceID { get; set; }
            public DateTime RequestDate { get; set; }
            //public string IssueAccountNo { get; set; }
            //public string OfficerUserName { get; set; }
            //public int CreditOfficerTypeID { get; set; }
            //public DateTime CreditOfficerStartDate { get; set; }
            //public DateTime CreditOfficerEndDate { get; set; }
            //public int OfficeID { get; set; }
            //public int OfficerID { get; set; }
            public List<IncomesStructuresActualDate> IncomesStructuresActualDates { get; set; }
            public List<Guarantor> Guarantors { get; set; }
            public List<Picture> Pictures { get; set; }
            public List<Partner> Partners { get; set; }

            public List<CreditOfficer> CreditOfficers { get; set; }
            public int RequestPeriod { get; set; }
            public decimal RequestRate { get; set; }
            public int PaymentSourceID { get; set; }
            //public object NonPaymentRisk { get; set; }
            //public object CreditFraudStatus { get; set; }
            //public object InformationID { get; set; }
            //public object ParallelRelativeCredit { get; set; }
            public object CreditPurpose { get; set; }
            public int LoanPurposeTypeID { get; set; }
            public string LoanLocation { get; set; }
            //public object Options { get; set; }
            //public object ReasonRefinancing { get; set; }
            //public object ExternalProgramID { get; set; }
            //public object ExternalCashDeskID { get; set; }
            //public object RedealType { get; set; }
            //public bool IsGroup { get; set; }
            //public string GroupName { get; set; }
            //public object CreditGroupCode { get; set; }
            //public object RequestGrantComission { get; set; }
            //public int RequestGrantComissionType { get; set; }
            //public object RequestReturnComission { get; set; }
            //public object RequestReturnComissionType { get; set; }
            //public object RequestTrancheIssueComission { get; set; }
            //public object RequestTrancheIssueComissionType { get; set; }
            
        }




        public class RootUpdate
        {
            public int CustomerID { get; set; }
            public int ProductID { get; set; }
            public int LoanStatus { get; set; }
            public int CreditID { get; set; }
            public int MortrageTypeID { get; set; }
            public int IncomeApproveTypeID { get; set; }
            public int RequestCurrencyID { get; set; }
            public decimal RequestSumm { get; set; }
            public int MarketingSourceID { get; set; }
            //public DateTime RequestDate { get; set; }
            public string IssueAccountNo { get; set; }
            //public string OfficerUserName { get; set; }
            //public int CreditOfficerTypeID { get; set; }
            //public DateTime CreditOfficerStartDate { get; set; }
            //public DateTime CreditOfficerEndDate { get; set; }
            public int OfficeID { get; set; }
            //public int OfficerID { get; set; }
            public List<IncomesStructuresActualDate> IncomesStructuresActualDates { get; set; }
            public List<Guarantor> Guarantors { get; set; }
            public List<Picture> Pictures { get; set; }
            public List<Partner> Partners { get; set; }

            public List<CreditOfficer> CreditOfficers { get; set; }
            public int RequestPeriod { get; set; }
            public decimal RequestRate { get; set; }
            //public int PaymentSourceID { get; set; }
            //public object NonPaymentRisk { get; set; }
            //public object CreditFraudStatus { get; set; }
            //public object ParallelRelativeCredit { get; set; }
            public string CreditPurpose { get; set; }
            public object LoanPurposeTypeID { get; set; }
            //public string LoanLocation { get; set; }
            //public object Options { get; set; }
            //public object ReasonRefinancing { get; set; }
            //public object ExternalProgramID { get; set; }
            //public object ExternalCashDeskID { get; set; }
            //public object RedealType { get; set; }
            //public bool IsGroup { get; set; }
            //public string GroupName { get; set; }
            //public object CreditGroupCode { get; set; }
            //public object RequestGrantComission { get; set; }
            //public int RequestGrantComissionType { get; set; }
            //public object RequestReturnComission { get; set; }
            //public object RequestReturnComissionType { get; set; }
            //public object RequestTrancheIssueComission { get; set; }
            //public object RequestTrancheIssueComissionType { get; set; }
        }



        public class RootUpdate2
        {
            public int CustomerID { get; set; }
            public int ProductID { get; set; }
            //public int LoanStatus { get; set; }
            public int CreditID { get; set; }
            public int MortrageTypeID { get; set; }
            public int IncomeApproveTypeID { get; set; }
            public int RequestCurrencyID { get; set; }
            public decimal RequestSumm { get; set; }
            //public int MarketingSourceID { get; set; }
            //public DateTime RequestDate { get; set; }
            //public string IssueAccountNo { get; set; }
            //public string OfficerUserName { get; set; }
            //public int CreditOfficerTypeID { get; set; }
            //public DateTime CreditOfficerStartDate { get; set; }
            //public DateTime CreditOfficerEndDate { get; set; }
            //public int OfficeID { get; set; }
            public int OfficerID { get; set; }
            public List<IncomesStructuresActualDate> IncomesStructuresActualDates { get; set; }
            public List<Guarantor> Guarantors { get; set; }
            public List<Picture> Pictures { get; set; }
            public List<Partner> Partners { get; set; }

            public List<CreditOfficer> CreditOfficers { get; set; }
            public int RequestPeriod { get; set; }
            public decimal RequestRate { get; set; }
            //public int PaymentSourceID { get; set; }
            //public object NonPaymentRisk { get; set; }
            //public object CreditFraudStatus { get; set; }
            //public object ParallelRelativeCredit { get; set; }
            public object CreditPurpose { get; set; }
            public int LoanPurposeTypeID { get; set; }
            //public string LoanLocation { get; set; }
            //public object Options { get; set; }
            //public object ReasonRefinancing { get; set; }
            //public object ExternalProgramID { get; set; }
            //public object ExternalCashDeskID { get; set; }
            //public object RedealType { get; set; }
            //public bool IsGroup { get; set; }
            //public string GroupName { get; set; }
            //public object CreditGroupCode { get; set; }
            //public object RequestGrantComission { get; set; }
            //public int RequestGrantComissionType { get; set; }
            //public object RequestReturnComission { get; set; }
            //public object RequestReturnComissionType { get; set; }
            //public object RequestTrancheIssueComission { get; set; }
            //public object RequestTrancheIssueComissionType { get; set; }
        }










        /*****************************/

        //public System.Drawing.Image Base64ToImage()
        //{
        //    byte[] imageBytes = Convert.FromBase64String(HiddenField1.Value);
        //    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
        //    ms.Write(imageBytes, 0, imageBytes.Length);
        //    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
        //    return image;
        //}
        /************************************************************/


        public void SaveToLoanRequest(Root root, int requestID)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            LoanRequest loan = new LoanRequest()
            {
                RequestID = requestID,
                ProductID = root.ProductID,
                //LoanStatus = root.LoanStatus
                //IncomeApproveTypeID = root.IncomeApproveTypeID,
                RequestCurrencyID = root.RequestCurrencyID,
                //MarketingSourceID = root.MarketingSourceID,
                //IssueAccountNo = root.IssueAccountNo,
                //OfficerUserName = root.OfficerUserName,
                //CreditOfficerTypeID = root.CreditOfficerTypeID,
                CreditOfficerStartDate = root.CreditOfficers.LastOrDefault().CreditOfficerStartDate,
                //CreditOfficerEndDate = root.CreditOfficerEndDate,
                OfficerID = root.CreditOfficers.LastOrDefault().OfficerID,
                PaymentSourceID = root.
                //NonPaymentRisk = root.
                //CreditFraudStatus = root.CreditFraudStatus,
                //InformationID = root.InformationID,
                //ParallelRelativeCredit = root.ParallelRelativeCredit,
                LoanPurposeTypeID = root.LoanPurposeTypeID,
                LoanLocation = root.LoanLocation,
                //Options = root.Options,
                //ReasonRefinancing = root.ReasonRefinancing,
                //ExternalProgramID = root.ExternalProgramID,
                //ExternalCashDeskID = root.ExternalCashDeskID,
                //RedealType = root.RedealType,
                //IsGroup = root.IsGroup,
                //GroupName = root.GroupName,
                //CreditGroupCode = root.CreditGroupCode,
                //RequestGrantComissionType = root.RequestGrantComissionType,
                //RequestReturnComission = root.RequestReturnComission,
                //RequestReturnComissionType = root.RequestReturnComissionType,
                //RequestTrancheIssueComission = root.RequestTrancheIssueComission,
                //RequestTrancheIssueComissionType = root.RequestTrancheIssueComissionType,
                //CurrencyID = root.CurrencyID,
                //TotalPercents = root.TotalPercents,
                //GuaranteeAmount = root.GuaranteeAmount,
                //StartDate = root.StartDate,
                //EndDate = root.EndDate,
                //Status = root.Status,
                //File = root.File

            };
            dbRWZ.LoanRequests.InsertOnSubmit(loan);
            dbRWZ.LoanRequests.Context.SubmitChanges();
        }


        public void UpdateToLoanRequest(RootUpdate root, int requestID)
        {

            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            //var lst = dbW.HistoriesCustomers.Where(r => r.CreditID == item.CreditID).FirstOrDefault();
            //lst.CreditPurpose = item.CreditPurpose;
            //lst.RequestSumm = item.RequestSumm;
            //lst.CustomerID = item.CustomerID;
            //lst.ApprovedSumm = item.ApprovedSumm;
            //dbW.HistoriesCustomers.Context.SubmitChanges();


            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            LoanRequest loan = dbRWZ.LoanRequests.Where(x => x.RequestID == requestID).FirstOrDefault();
            {

                //loan.CustomerID = root.CustomerID;
                loan.ProductID = root.ProductID;
                //loan.LoanStatus = root.loanStatus;
                //loan.CreditID = root.CreditID;
                //loan.MortrageTypeID = root.MortrageTypeID;
                loan.IncomeApproveTypeID = root.IncomeApproveTypeID;
                loan.RequestCurrencyID = root.RequestCurrencyID;
                //loan.RequestSumm = root.RequestSumm;
                //MarketingSourceID = 5,
                //RequestDate = Convert.ToDateTime(actdate, //dateTimeNow, 
                //IssueAccountNo = null,
                //OfficerUserName = null,
                //CreditOfficerTypeID = 1,
                //CreditOfficerStartDate = Convert.ToDateTime("2021-09-15T11:28:42"), //dateTimeNow, 
                // CreditOfficerEndDate = null, // Convert.ToDateTime(actdate), //dateTimeNow, 
                //loan.OfficeID = root.OfficeID;
                //OfficerID = Convert.ToInt32(credOfficerID), //6804,
                //loan.RequestPeriod = root.RequestPeriod;
                //loan.RequestRate = root.RequestRate;
                //PaymentSourceID = 1,
                //NonPaymentRisk = null,
                //CreditFraudStatus = null,
                //InformationID = null,
                //ParallelRelativeCredit = null,
                //loan.CreditPurpose = root.CreditPurpose;
                //loan.LoanPurposeTypeID = root.LoanPurposeTypeID;
                //LoanLocation = "",
                //Options = null,
                //ReasonRefinancing = null,
                //ExternalProgramID = null,
                //ExternalCashDeskID = null,
                //RedealType = null,
                //IsGroup = false,
                //GroupName = "",
                //CreditGroupCode = null,
                //RequestGrantComission = null,
                //RequestGrantComissionType = 1,
                //RequestReturnComission = null,
                //RequestReturnComissionType = null,
                //RequestTrancheIssueComission = null,
                //RequestTrancheIssueComissionType = null
                //loan.CurrencyID = root.CurrencyID;
                //loan.TotalPercents = root.TotalPercents
                //loan.ActualDate = root.ActualDate;
                //loan.File = root.File;
                //loan.PartnerCompanyID = root.PartnerCompanyID;
                //loan.LoanPartnerSumV = root.LoanPartnerSumV;
                //loan.CommissionSum = root.CommissionSum;
                //IssueComissionPaymentTypeID = 1

            };

            dbRWZ.LoanRequests.Context.SubmitChanges();
        }


        public void AddRowToJournalScoring(Request req, string status, int userID)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == req.RequestID) select v).ToList().FirstOrDefault();
            //var usr = dbRWZ.Users2s.Where(z => z.UserID == Convert.ToInt32(hfUserID.Value)).FirstOrDefault();
            var usr = dbRWZ.Users2s.Where(z => z.UserID == userID).FirstOrDefault();
            int N1 = 0, N2 = 0;
            if (lst != null)
            {
                var dateNow = DateTime.Now.Date;
                //var yestedey = DateTime.Now.AddDays(-1).Date;


                var jourAll = (from v in dbRWZ.JournalScorings orderby v.DateVerif select v).ToList();
                var jourNow = (from v in dbRWZ.JournalScorings where (v.DateVerif == dateNow) orderby v.DateVerif select v).ToList();

                if (jourAll.Count != 0)
                {

                    //var jourNow = (from v in dbRWZ.JournalNanos where (v.DateVerif == dateNow) select v).ToList().FirstOrDefault();

                    if (jourNow.Count == 0)
                    {

                        N1 = Convert.ToInt32(jourAll.Last().Nomer1) + 1;
                        N2 = 1;
                    }
                    else
                    {
                        N1 = Convert.ToInt32(jourNow.Last().Nomer1);
                        N2 = Convert.ToInt32(jourNow.Last().Nomer2) + 1;
                    }

                }
                else
                {
                    N1 = 1;
                    N2 = 1;
                }

                JournalScoring jourCurr = (from v in dbRWZ.JournalScorings where v.RequestID == req.RequestID select v).FirstOrDefault();
                if (jourCurr != null)
                {
                    //JournalNano Item = new JournalNano();
                    GeneralController ctlItem = new GeneralController();

                    //Item = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
                    //Item = jourCurr.FirstOrDefault();
                    jourCurr.Status = status;
                    ctlItem.ItemJournalScoringUpd(jourCurr);
                }
                else
                {
                    if (lst.RequestStatus == "Утверждено")
                    {
                        JournalScoring newItem = new JournalScoring()
                        {
                            RequestID = req.RequestID,
                            Nomer1 = N1,
                            Nomer2 = N2,
                            FIO = req.Surname + " " + req.CustomerName + " " + req.Otchestvo,
                            RequesSum = req.RequestSumm,
                            RequestPeriod = req.RequestPeriod,
                            BranchID = req.BranchID.ToString(),
                            Verificator = usr.Fullname,
                            Status = status,
                            DateVerif = Convert.ToDateTime(DateTime.Now).Date

                            //AgentID = Convert.ToInt32(Session["UserID"].ToString()),
                            //CreditID = Convert.ToInt32(hfCreditID.Value), //CreditsHistoriesID,
                            //CustomerID = Convert.ToInt32(hfCustomerID.Value),
                            //StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                            //Status = "Исправить",
                            //note = tbNote.Text,
                            //RequestID = Convert.ToInt32(hfRequestID.Value) // requestID
                        };
                        GeneralController itm = new GeneralController();
                        itm.ItemJournalScoringAdd(newItem);
                    }
                }
            }

        }

        public void ItemJournalScoringAdd(JournalScoring newItem)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            dbRWZ.JournalScorings.InsertOnSubmit(newItem);
            dbRWZ.JournalScorings.Context.SubmitChanges();
        }

        public void ItemJournalScoringUpd(JournalScoring item)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            var itm = dbRWZ.JournalScorings.Where(r => r.ID == item.ID).FirstOrDefault();
            itm.Status = item.Status;
            dbRWZ.JournalScorings.Context.SubmitChanges();
        }

    }
}