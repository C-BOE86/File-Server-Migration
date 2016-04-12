using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.SharePoint.Client;
using System.Security;


namespace Migration
{
    class Program
    {
        static void Main(string[] args)
        {


            ClientContext context = new ClientContext("https://tshstl.sharepoint.com/");

            {
                //SharePoint Credentials

                SecureString password = new SecureString();
                foreach (char c in "Password")
                    password.AppendChar(c);

                context.Credentials = new SharePointOnlineCredentials("UserName", password);

               
                context.ExecuteQuery();

                Console.WriteLine("File Transfer is Complete");
            }

            string[] fileEntries = Directory.GetFiles(@"C:\  ");


            foreach (string fileName in fileEntries)
            {
                Console.WriteLine(fileName);
                Console.WriteLine(Path.GetFileName(fileName));

                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    string destination = "/sites/NameofWebsite/documentlibrary/" + Path.GetFileName(fileName);

                    Microsoft.SharePoint.Client.File.SaveBinaryDirect(context, destination, fs, true);  



                }






            }
        }
    }
}

