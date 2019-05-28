using Android.Content;
using DataAccess.Agents;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using XWingsMobile.Services;

namespace XWingsMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PesaDelDia : ContentPage
    {
        DailyUserTasksAgent g = new DailyUserTasksAgent();
        ReferencesAgent r = new ReferencesAgent();
        AssemblyInstructionsAgent ai = new AssemblyInstructionsAgent();
        public PesaDelDia(int iduser)
        {
            InitializeComponent();
            ObtenirTasca();
            IdUser = iduser;
            btnPDF.Clicked += BtnPDF_Clicked;
            
        }

        private async void BtnPDF_Clicked(object sender, EventArgs e)
        {
            try
            {
                var pdfs = await ai.GetAllAsync();
                pdfs.Where(x => x.Idreference == refer.IdReference).FirstOrDefault();

                string path = "/sdcard/download/" + pdfs.FirstOrDefault().FileName;

                File.WriteAllBytes(path, pdfs.FirstOrDefault().Instructions);

                DependencyService.Get<IFileOpener>().OpenFile(path);


            }
            catch (Exception ex)
            {
                await DisplayAlert("Error al carregar", ex.ToString(), "OK");
            }
        }

        public References refer;
        private int IdUser;
        private async void ObtenirTasca()
        {
            var tasques = await g.GetAllAsync();
            var filtrades = tasques.Where(x => x.idUser == IdUser && x.Taskdate.DayOfYear == DateTime.Today.DayOfYear && x.Taskdate.Year == DateTime.Today.Year);

            if (filtrades.Count() > 0)
            {
                refer = await r.GetByIdAsync(filtrades.FirstOrDefault().idreference.ToString());
                lblNom.Text = refer.CodeReference;
                lblInstr.Text = refer.DescReference;
                btnPDF.IsVisible = true;
            }
            else
            {
                
                lblNom.Text = "No hi ha tasques per avui";
            }
        }

       
    }
}