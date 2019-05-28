using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XWingsMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentPage
    {
        private int user;
        public MainMenu(int iduser)
        {
            InitializeComponent();
            btnDailyTask.Clicked += BtnDailyTask_Clicked;
            btnScan.Clicked += BtnScan_Clicked;
        }

        private async void BtnScan_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Escaner(user));
        }

        

        private async void BtnDailyTask_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PesaDelDia( user));
        }
    }
}