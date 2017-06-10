using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace encryption_in_asp.net
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populateUploadedfile();

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //adding code to file with encryption.........................

            byte[] file = new byte[FileUpload1.PostedFile.ContentLength];
            FileUpload1.PostedFile.InputStream.Read(file, 0, FileUpload1.PostedFile.ContentLength);
            string filename = FileUpload1.PostedFile.FileName;
            byte[] key = Encoding.UTF8.GetBytes("asdf!@#12345ASDF");
            try
            {

                string outputfile = Path.Combine(Server.MapPath("~/uploads"), filename);
                if (File.Exists(outputfile))
                {
                    Response.Write("File AlreaDY Exist!...........");
                    
                }
                else
                {

                    FileStream fs = new FileStream(outputfile,FileMode.Create);
                    RijndaelManaged rmcryp = new RijndaelManaged();
                    CryptoStream cs = new CryptoStream(fs,rmcryp.CreateEncryptor(key,key),CryptoStreamMode.Write);

                    foreach (var data in file)
                    {

                        cs.WriteByte((byte)data);

                    }

                    cs.Close();
                    fs.Close();

                }

                populateUploadedfile();

            }
            catch (Exception)
            {
                Response.Write("Ecryption Failed...........");
            
            }









        
        
        }//btn event end.....................




        private void populateUploadedfile()
        {
            DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/uploads"));
            List<file> uploadfile = new List<file>();
            foreach (var file in di.GetFiles())
            {
                uploadfile.Add
                    (
                    new file
                    {
                        filename = file.Name,
                        fileExtension = Path.GetExtension(file.Name),
                        filepath = file.FullName,
                        size = (file.Length / 1024), //for get size in kb 
                        icon = geticonpath(Path.GetExtension(file.FullName))


                    }

                    );


            }

            DataList1.DataSource = uploadfile;
            DataList1.DataBind();



        } //method end..............









        private string geticonpath(string fileexetension)
        {
            string iconpath = "/Images";
            switch (fileexetension.ToLower())
            {

                case ".txt":
                    iconpath += "/txt.png";

                    break;


                case ".doc":
                case ".docx":

                    iconpath += "/word.png";

                    break;


                case ".xls":
                case ".xlsx":

                    iconpath += "/xls.png";

                    break;

                case ".pdf":
                    iconpath += "/pdf.png";

                    break;

                case ".rar":
                    iconpath += "/rar.png";

                    break;


                case ".zip":
                case ".7z":

                    iconpath += "/zip.png";

                    break;


                default:
                    break;
            }

            return iconpath;
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {

           if (e.CommandName=="Download")
            {
                string filepath = e.CommandArgument.ToString();
                byte[] key = Encoding.UTF8.GetBytes("asdf!@#12345ASDF");

                FileStream fs = new FileStream(filepath, FileMode.Open);
                RijndaelManaged rmcryp = new RijndaelManaged();
                CryptoStream cs = new CryptoStream(fs, rmcryp.CreateDecryptor(key, key), CryptoStreamMode.Read);

                try
                {
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("content-Disposition","attachment; filename="+Path.GetFileName(filepath)+Path.GetExtension(filepath));
                    int data;
                    while ((data=cs.ReadByte())!=-1)
                    {
                        Response.OutputStream.WriteByte((byte)data);
                        Response.Flush();

                    }

                }
                catch (Exception ex)
                {

                    Response.Write(ex.Message);

                }
                finally
                {
                    cs.Close();
                    fs.Close();
                }
            

            
            } //if check end................



        
        
        } //event end..............................





    }
}