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
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp10.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

using ClientDemo;



namespace WpfApp10
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : MetroWindow
    {
        FormTableName usertable = new FormTableName() { Groups = new List<string>() };
        string f4token;
        public Window3(string email, string token)
        {
            InitializeComponent();
            f4token = token;
            usertable.Email = email;

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            usertable.Subject = checkedListBox1.SelectedItem.ToString().Substring(37);
        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            usertable.Course = checkedListBox2.SelectedItem.ToString().Substring(37);      //SelectedItem.ToString();
        }
        public class CheckBoxViewModel
        {
            public CheckBoxViewModel(bool isChecked)
            {
                IsChecked = isChecked;
            }
            public bool IsChecked { get; set; }
        }


            private void Button_Click(object sender, RoutedEventArgs e)
        {
            usertable.Groups = new List<string>();
            var items1 = checkedListBox3.Items;                                             
            
            for(int i=0; i<items1.Count;i++)
            {
                if ((items1[i].ToString().IndexOf("True"))>0)
                {
                    string item = items1[i].ToString().Substring(41).Substring(0,8);
                    usertable.Groups.Add(item);
                }
            }
            

            

            AuthStuff auth = new AuthStuff();
            try
            {
                using (var client = auth.CreateClient(f4token))
                {
            
                    var response = client.PostAsJsonAsync("http://localhost:65458/" + "api/DBManage/CreateTable/", usertable).Result;
                    label1.Content = response.Content.ReadAsStringAsync().Result;
            
                }
            }
            catch (Exception ex)
            {
                label1.Content = ex.Message;
            }
        }

        
    }
}
