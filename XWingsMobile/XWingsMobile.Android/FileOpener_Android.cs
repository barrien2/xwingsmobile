using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XWingsMobile.Droid;
using XWingsMobile.Services;

[assembly: Dependency(typeof(FileOpener_Android))]

namespace XWingsMobile.Droid
{
    public class FileOpener_Android : IFileOpener
    {
        public void OpenFile (string filepath)
        {
            var bytes = File.ReadAllBytes(filepath);

            //Copy the private file's data to the EXTERNAL PUBLIC location
            string externalStorageState = global::Android.OS.Environment.ExternalStorageState;
            string application = "";

            string extension = System.IO.Path.GetExtension(filepath);

            switch (extension.ToLower())
            {
                case ".doc":
                case ".docx":
                    application = "application/msword";
                    break;
                case ".pdf":
                    application = "application/pdf";
                    break;
                case ".xls":
                case ".xlsx":
                    application = "application/vnd.ms-excel";
                    break;
                case ".jpg":
                case ".jpeg":
                case ".png":
                    application = "image/jpeg";
                    break;
                default:
                    application = "*/*";
                    break;
            }
            var externalPath = global::Android.OS.Environment.ExternalStorageDirectory.Path + "/report" + extension;
            File.WriteAllBytes(externalPath, bytes);

            Java.IO.File file = new Java.IO.File(externalPath);
            file.SetReadable(true);
            Android.Net.Uri uri = Android.Net.Uri.Parse("content://" + filepath);
            //Android.Net.Uri uri = Android.Net.Uri.FromFile(file);
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(uri, application);
            intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);

            try
            {
                Xamarin.Forms.Forms.Context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Xamarin.Forms.Forms.Context, "No Application Available to View PDF", ToastLength.Short).Show();
                Toast.MakeText(Xamarin.Forms.Forms.Context, ex.ToString(), ToastLength.Long).Show();
            }
        }
    }
}