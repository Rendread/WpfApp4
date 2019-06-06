using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Net.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace WpfApp10
{



    public partial class MainWindow : MetroWindow
    {
        
        
        string url = "http://localhost:65458";
        public MainWindow()
        {
            InitializeComponent();
            //Window3 window3 = new Window3("kk_mazhor@mail.ru","123123123" );
            //Hide();
            //window3.Show();
            //Close();
        }

        class AuthUser
        {
            [Required(ErrorMessage = "Значение логина должно быть установлено")]
            [Display(Name = "Логин")]
            public string Login { get; set; }

            [Required(ErrorMessage = "Значение пароля должно быть установлено")]
            [Display(Name = "Пароль")]
            [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен быть не короче 6 символов.")]
            public string Pwd { get; set; }

        }
        private async void Button_Click1(object sender, RoutedEventArgs e)
        {
            progressRing.IsActive = true;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            
            

            var user = new AuthUser() { Login = E_mail.Text, Pwd = Password.Password };
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var context = new ValidationContext(user);
            if (!Validator.TryValidateObject(user, context, results, true))
            {
                Label.Content = null;
                foreach (var error in results)
                {
                    Label.Content += error.ErrorMessage + "\n";
                    progressRing.IsActive = false;
                }
            }
            else
            {

                string token = "";
                var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "username", E_mail.Text ),
                    new KeyValuePair<string, string> ( "Password", Password.Password )

                };

                var content = new FormUrlEncodedContent(pairs);

                using (var client = new HttpClient())
                {
                    //ошибка при попытке подключения к отключенному серверу
                    HttpResponseMessage response;
                    string result;
                    Dictionary<string, string> tokenDictionary = null;
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    try
                    {
                        response = client.PostAsync(url + "/Token", content).Result;

                        result = response.Content.ReadAsStringAsync().Result;

                        tokenDictionary = js.Deserialize<Dictionary<string, string>>(result);

                        token = tokenDictionary["access_token"];
                       
                        Hide();
                        Window2 window2 = new Window2(E_mail.Text, token);

                        window2.ShowDialog();
                        Close();

                    }
                    catch (Exception ex)
                    {

                        if (tokenDictionary != null && tokenDictionary.ContainsKey("error_description"))
                        {
                            Label.Content = tokenDictionary["error_description"];
                            
                        }
                        else
                        {
                            Label.Content = ex.Message;
                            
                        }

                    }

                }
                progressRing.IsActive = false;

            }
        }
        private void TextBoxClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window1 window1 = new Window1();
            window1.Show();
            
        }
    }
}
