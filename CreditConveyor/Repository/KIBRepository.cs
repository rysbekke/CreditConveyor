using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Zamat.Repository
{
    public class KIBRepository
    {

        //public async void SmartSearchIndividualResponse()
        //{

        //    ReportPublicServiceClient client = new ReportPublicServiceClient(EndpointConfiguration.BasicHttpBinding_IReportPublicService);

        //    var certPath = @AppDomain.CurrentDomain.BaseDirectory + "ramazbaev_TEST.PFX"; // Path to PFX fle
        //    var cert = new X509Certificate2(certPath, "Password1@3");

        //    client.ClientCredentials.ClientCertificate.Certificate = cert;

        //    /***********************/

        //    // set the *plaintext* credendials
        //    client.ClientCredentials.UserName.UserName = "ramazbaev";
        //    client.ClientCredentials.UserName.Password = "Password1@34";

        //    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(client.ClientCredentials.UserName.UserName + ":" + client.ClientCredentials.UserName.Password);
        //    var base64EncodedCredentials = System.Convert.ToBase64String(plainTextBytes);

        //    var httpRequestProperty = new HttpRequestMessageProperty();
        //    httpRequestProperty.Headers[System.Net.HttpRequestHeader.Authorization] = "Basic " + base64EncodedCredentials;

        //    Task<SmartSearchIndividualResponse> task_SmartSearchIndividualResponse;
        //    SearchIndividualQuery1 searchIndividualQuery = new SearchIndividualQuery1();


        //    SmartSearchIndividualRequest request_SmartSearchIndividualRequest = new SmartSearchIndividualRequest();

        //    IdNumberPairIndividual idNumberPairIndividual = new IdNumberPairIndividual();
        //    idNumberPairIndividual.IdNumber = "12303199400514";
        //    idNumberPairIndividual.IdNumberType = IDNumberTypeIndividual1.SocCode;

        //    request_SmartSearchIndividualRequest.query = searchIndividualQuery;
        //    request_SmartSearchIndividualRequest.query.InquiryReason = InquiryReasons.ApplicationForCreditOrAmendmentOfCreditTerms;
        //    //request_SmartSearchIndividualRequest.query.Parameters = new SearchIndividualParameters1 { FullName = "Жоомарт кызы Зинат", DateOfBirth = Convert.ToDateTime("1994-03-23").Date, InternalPassport = "ID1081023" };
        //    request_SmartSearchIndividualRequest.query.Parameters = new SearchIndividualParameters1();
        //    request_SmartSearchIndividualRequest.query.Parameters.FullName = "Жоомарт кызы Зинат";
        //    request_SmartSearchIndividualRequest.query.Parameters.DateOfBirth = Convert.ToDateTime("1994-03-23").Date;
        //    request_SmartSearchIndividualRequest.query.Parameters.InternalPassport = "ID1081023";

        //    request_SmartSearchIndividualRequest.query.Parameters.IdNumbers = new IdNumberPairIndividual[] { idNumberPairIndividual };

        //    using (var scope = new OperationContextScope(client.InnerChannel))
        //    {
        //        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
        //        task_SmartSearchIndividualResponse = client.SmartSearchIndividualAsync(request_SmartSearchIndividualRequest);
        //    }
        //    var result = await task_SmartSearchIndividualResponse;


        //    client.Close();



        //    /***************************/




        //}


        //public async void GetPdfReport()
        //{

        //    ReportPublicServiceClient client = new ReportPublicServiceClient(EndpointConfiguration.BasicHttpBinding_IReportPublicService);

        //    var certPath = @AppDomain.CurrentDomain.BaseDirectory + "ramazbaev_TEST.PFX"; // Path to PFX fle
        //    var cert = new X509Certificate2(certPath, "Password1@3");

        //    client.ClientCredentials.ClientCertificate.Certificate = cert;

        //    /***********************/

        //    // set the *plaintext* credendials
        //    client.ClientCredentials.UserName.UserName = "ramazbaev";
        //    client.ClientCredentials.UserName.Password = "Password1@34";

        //    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(client.ClientCredentials.UserName.UserName + ":" + client.ClientCredentials.UserName.Password);
        //    var base64EncodedCredentials = System.Convert.ToBase64String(plainTextBytes);

        //    var httpRequestProperty = new HttpRequestMessageProperty();
        //    httpRequestProperty.Headers[System.Net.HttpRequestHeader.Authorization] = "Basic " + base64EncodedCredentials;

        //    Task<GetPdfReportResponse> task_GetPdfReportResponse;

        //    GetPdfReportRequest request_GetPdfReportRequest = new GetPdfReportRequest();

        //    request_GetPdfReportRequest.parameters = new PdfReportParameters();

        //    request_GetPdfReportRequest.parameters.Consent = true;
        //    request_GetPdfReportRequest.parameters.IDNumber = "18850205";
        //    request_GetPdfReportRequest.parameters.IDNumberType = IDNumberTypeResolvable.CreditinfoId;
        //    request_GetPdfReportRequest.parameters.InquiryReason = InquiryReasons.AnotherReason;
        //    request_GetPdfReportRequest.parameters.InquiryReasonText = "AnotherReason";
        //    request_GetPdfReportRequest.parameters.LanguageCode = "ru-RU";
        //    request_GetPdfReportRequest.parameters.ReportName = "CondensedReportPlus";
        //    request_GetPdfReportRequest.parameters.SubjectType = SubjectType.Individual;


        //    using (var scope = new OperationContextScope(client.InnerChannel))
        //    {
        //        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
        //        task_GetPdfReportResponse = client.GetPdfReportAsync(request_GetPdfReportRequest);
        //    }
        //    var result = await task_GetPdfReportResponse;
        //    byte[] bytes = result.GetPdfReportResult;
        //    //byte[] bytes = Convert.FromBase64String(result);


        //    string sourceFolder = @"D:\test\"; // исходная папка
        //    string zipFile = "report.zip"; // сжатый файл
        //    string targetFolder = @"D:\test\"; // папка, куда распаковывается файл

        //    //string fileName = @"C:\some\path\file.zip";

        //    DeleteFile(sourceFolder, zipFile);
        //    DeleteFile(sourceFolder, "report.pdf");

        //    System.IO.FileStream stream = new FileStream(sourceFolder + zipFile, FileMode.CreateNew);
        //    System.IO.BinaryWriter writer =
        //    new BinaryWriter(stream);
        //    writer.Write(bytes, 0, bytes.Length);
        //    writer.Close();


        //    client.Close();

        //    //string sourceFolder = "D://test/"; // исходная папка
        //    //string zipFile = "D://file.zip"; // сжатый файл
        //    //string targetFolder = "D://newtest"; // папка, куда распаковывается файл

        //    //ZipFile.CreateFromDirectory(sourceFolder, zipFile);
        //    //Console.WriteLine($"Папка {sourceFolder} архивирована в файл {zipFile}");
        //    ZipFile.ExtractToDirectory(sourceFolder + zipFile, targetFolder);
        //    /***************************/




        //}











        //public List<User> GetUsers()
        //{
        //    List<User> users = new List<User>();

        //    string sql = "SELECT * FROM public.\"User\"";
        //    //using (SqlConnection conn = new SqlConnection(Storage.ConnectionString))
        //    using (NpgsqlConnection conn = new NpgsqlConnection(Storage.ConnectionString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //        }
        //        catch
        //        {
        //            MessageBox.Show("Невозможно установить соединение с базой данных!", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return users;
        //        }
        //        NpgsqlCommand command = new NpgsqlCommand(sql, conn);
        //        using (NpgsqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                users.Add(new User
        //                {
        //                    Id = (int)reader[0],
        //                    Login = reader[1].ToString(),
        //                    Password = reader[2].ToString(),
        //                    UserFIO = reader[3].ToString(),
        //                    Address = reader[4].ToString(),
        //                    Phone = reader[5].ToString(),
        //                    Access = reader[6].ToString()
        //                });
        //            }
        //        }
        //    }

        //    return users;
        //}

        //public void Delete(User user)
        //{
        //    string sql = "DELETE FROM Users WHERE Id=" + user.Id;
        //    using (NpgsqlConnection conn = new NpgsqlConnection(Storage.ConnectionString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //        }
        //        catch
        //        {
        //            MessageBox.Show("Невозможно установить соединение с базой данных!", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }
        //        NpgsqlCommand command = new NpgsqlCommand(sql, conn);
        //        command.ExecuteScalar();
        //    }
        //}

        //public void AddOrUpdate(User user)
        //{
        //    if (GetUsers().Any(u => u.Id == user.Id))
        //    {
        //        string sql = "UPDATE Users SET " +
        //                     "Login='" + user.Login +
        //                     "',Password='" + user.Password +
        //                     "',UserFIO='" + user.UserFIO +
        //                     "',Address='" + user.Address +
        //                     "',Phone='" + user.Phone +
        //                     "',Access='" + user.Access +
        //                     "' WHERE Id=" + user.Id;
        //        using (NpgsqlConnection conn = new NpgsqlConnection(Storage.ConnectionString))
        //        {
        //            try
        //            {
        //                conn.Open();
        //            }
        //            catch
        //            {
        //                MessageBox.Show("Невозможно установить соединение с базой данных!", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return;
        //            }
        //            NpgsqlCommand command = new NpgsqlCommand(sql, conn);
        //            command.ExecuteScalar();
        //        }
        //    }
        //    else
        //    {
        //        string sql = "INSERT INTO Users (Login, Password, UserFIO, Address, Phone, Access) Values ('" +
        //                     user.Login + "','" +
        //                     user.Password + "','" +
        //                     user.UserFIO + "','" +
        //                     user.Address + "','" +
        //                     user.Password + "','" +
        //                     user.Access + "')";
        //        using (NpgsqlConnection conn = new NpgsqlConnection(Storage.ConnectionString))
        //        {
        //            try
        //            {
        //                conn.Open();
        //            }
        //            catch
        //            {
        //                MessageBox.Show("Невозможно установить соединение с базой данных!", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return;
        //            }
        //            NpgsqlCommand command = new NpgsqlCommand(sql, conn);
        //            command.ExecuteScalar();
        //        }
        //    }
        //}

    }
}