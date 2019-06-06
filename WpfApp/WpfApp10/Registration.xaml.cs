using System;
using WpfApp10.Models;
using ClientDemo;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
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
using System.Text;
using System.Threading;
namespace WpfApp10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Window1 : MetroWindow
    {
        AuthStuff auth;
        private const string url = "http://localhost:65458";
        public Window1()
        {
            InitializeComponent();

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var registerModel = new ProfessorRegisterModel() { Email = textBox3.Text, SpecOrGroup = textBox2.Text, UserName = textBox1.Text, Password = textBox4.Password };
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var context = new ValidationContext(registerModel);
            if (!Validator.TryValidateObject(registerModel, context, results, true))
            {
                label.Content = null;
                foreach (var error in results)
                {
                    label.Content += error.ErrorMessage + "\n";
                }
            }

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = null;

                try
                {
                    response = await client.PostAsJsonAsync(url + "/api/Account/Register", registerModel);

                    label.Content = "Регистрация прошла успешно.";

                    Thread.Sleep(1000);
                    

                }
                catch (Exception ex)
                {
                    if (response != null)
                    {
                        label.Content = response.Content.ReadAsStringAsync().ToString() + ex.Message;
                    }
                    else
                    {
                        label.Content = ex.Message;
                    }
                }


            }
        }
        private void TextBoxClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
