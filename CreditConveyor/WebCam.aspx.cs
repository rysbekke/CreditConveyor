using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace СreditСonveyor
{
    public partial class WebCam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["DosCredobankConnectionStringRWZ"].ToString();
        //public string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        //public string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        public string connectionStringRWZ = ConfigurationManager.ConnectionStrings["connectionStringZ"].ToString();
        public System.Drawing.Image Base64ToImage()
        {
            byte[] imageBytes = Convert.FromBase64String(HiddenField1.Value);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }


        protected void btnSavePhoto_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);

            string reqid = Request.QueryString["reqid"];
            string filedir = "BeeCredits";
            int? role = dbRWZ.Requests.Where(r => r.RequestID == int.Parse(reqid)).FirstOrDefault().AgentRoleID;

            if ((role == 1) || (role == 4) || (role == 12)) filedir = "Nurcredits";
            if ((role == 8) || (role == 9) || (role == 18)) filedir = "Beecredits";
            if ((role == 13) || (role == 14) || (role == 22)) filedir = "Partcredits";
            // Base64ToImage().Save(Server.MapPath("~/Portals/0/uploadfiles/Hello.jpg"));
            // Base64ToImage().Save(PortalSettings.HomeDirectoryMapPath("~/Images/Hello.jpg"));
            //string filename = "photo.jpg", fullfilename = "";
            string temp_ext = DateTime.Now.Millisecond.ToString();
            string filename = "Photo" + reqid.ToString() + DateTime.Today.Date.ToString("_ddMMyyyy_") + temp_ext + ".jpg", fullfilename = "";
            fullfilename = UploadImageAndSave(true, filedir, filename);
            Base64ToImage().Save(Server.MapPath("~/") + "\\" + filedir + "\\" + fullfilename);
            RequestsFile newRequestFile = new RequestsFile
            {
                Name = filename,
                RequestID = Convert.ToInt32(Convert.ToInt32(reqid)),
                ContentType = "",
                //Data = bytes,
                //FullName = PortalSettings.HomeDirectory + filedir + "\\" + fullfilename,
                FullName = "\\Portals\\0\\" + filedir + "\\" + fullfilename,
                FullName2 = "https://credit.doscredobank.kg\\" + "Portals\\0\\" + filedir + "\\" + fullfilename,
                FileDescription = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                IsPhoto = true
            };
            //ItemController ctl = new ItemController();
            ItemRequestFilesAddItem(newRequestFile);
        }


        protected string UploadImageAndSave(bool hasfile, string filedir, string filename) //main function
        {
            if (hasfile)
            {
                CheckImageDirs(filedir);
                //string filepath = PortalSettings.HomeDirectoryMapPath + filedir + "\\" + filename;
                string filepath = Server.MapPath("~/") + "\\" + filedir + "\\" + filename;
                int temp_ext = 0;
                while (System.IO.File.Exists(filepath))
                {
                    temp_ext = DateTime.Now.Millisecond;
                    string ext_name = System.IO.Path.GetExtension(filepath);
                    string filename_no_ext = System.IO.Path.GetFileNameWithoutExtension(filepath) + "_" + temp_ext;
                    filename = filename_no_ext + temp_ext + ext_name;
                    //filepath = PortalSettings.HomeDirectoryMapPath + filedir + "\\" + filename;
                    filepath = Server.MapPath("~/") + "\\" + filedir + "\\" + filename;
                }
                string path = System.IO.Path.GetFileName(filename);
                //AsyncUpload1.UploadedFiles[0].SaveAs(filepath);
                //Base64ToImage().Save(Server.MapPath(filepath));
            }
            return filename;
        }

        protected void CheckImageDirs(string filedir)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/") + filedir))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/") + filedir);

            if (!System.IO.Directory.Exists(Server.MapPath("~/") + filedir))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/") + filedir);
        }

        public void ItemRequestFilesAddItem(RequestsFile newItem)
        {

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbRWZ.RequestsFiles.InsertOnSubmit(newItem);
            dbRWZ.RequestsFiles.Context.SubmitChanges();
        }





    }
}