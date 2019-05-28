using DataAccess.Agents;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XWingsMobile.Services;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace XWingsMobile.Views
{
     
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Escaner : ContentPage
    {
        ReferencesAgent ra = new ReferencesAgent();
        AssemblyInstructionsAgent ai = new AssemblyInstructionsAgent();
        AssemblyInstructions pdf;
        References r;
        string path, filename;

        public Escaner(int user)
        {
            InitializeComponent();

            userid = user;
            btnScan.Clicked += Button_Clicked;
            btnPDF.Clicked += BtnPDF_Clicked;
        }

        private async void BtnPDF_Clicked(object sender, EventArgs e)
        {
            try
            {
                path = "/sdcard/download/" + pdf.FileName;

                File.WriteAllBytes(path, pdf.Instructions);

                await DisplayAlert("S'ha guardat el fitxer", "Ruta: " + path, "OK");

                DependencyService.Get<IFileOpener>().OpenFile(path);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error al carregar", ex.ToString(), "OK");
            }

        }

        private int userid;

        private void Button_Clicked(object sender, EventArgs e)
        {
            Scanner();
        }

        private async void Scanner()
        {
            var scannerPage = new ZXing.Net.Mobile.Forms.ZXingScannerPage();

            scannerPage.Title = "Lector de QR";
            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopModalAsync();
                    var tots = await ra.GetAllAsync();
                    r = tots.Where(x => x.CodeReference == result.ToString()).FirstOrDefault();
                    if(r != null)
                    {
                        lblNom.Text = r.CodeReference;
                        lblInstr.Text = r.DescReference;
                        lblLast.IsVisible = true;

                        var pdfs = await ai.GetAllAsync();
                        pdf = pdfs.Where(x => x.Idreference == r.IdReference).FirstOrDefault();

                        btnPDF.IsVisible = pdf != null;

                    }
                    else
                    {
                        await DisplayAlert("Error", "No s'ha trobat cap peça amb el codi escanejat", "OK");
                    }
                   // www.Reload();
                });
            };

            await Navigation.PushModalAsync(scannerPage);
        }
    }
}