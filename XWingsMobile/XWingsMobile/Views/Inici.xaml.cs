using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XWingsMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inici : ContentPage
    {
        DataAccess.Agents.FactoryUsersAgent g = new DataAccess.Agents.FactoryUsersAgent();
        public Inici()
        {
            
            InitializeComponent();
            btnLogin.Clicked += BtnLogin_ClickedAsync;
        }
        int userid;

        private void BtnLogin_ClickedAsync(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(entryUser.Text)&& !string.IsNullOrWhiteSpace(entryPasswd.Text))
            {
                TryLogin();
            }
            else
            {
                DisplayAlert("nomano", "Ompli el usuari i la contrasenya", "Omplir");
            }
            
        }
        private async void TryLogin()
        {
            if (await CheckLogin(entryUser.Text, entryPasswd.Text))
            {
                await Navigation.PushModalAsync(new MainMenu(userid), false);
            }
            else
            {
                await DisplayAlert("contrassenya incorrecta", "aprengui a escriure", "aprendre");
            }

        }
        private async Task<bool> CheckLogin(string userName, string passwd)
        {
            try
            {

                var usersList = await g.GetAllAsync();
                string hash = MD5Hash(passwd).ToUpper();
                var matchingUsers = usersList.Where(user => user.UserName == userName && user.Password.ToUpper() == hash).ToList();

                //await g.UpdateAsync(matchingUsers[0]);
                bool res = matchingUsers.Count() > 0;
                if(res) userid = matchingUsers[0].IdUser;
                return res;
            }
            catch
            {
                await DisplayAlert("Error al login", "Sembla que no s'ha pogut connectar amb el servidor :(", "Torna a intentar");
                return false;
            }
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}