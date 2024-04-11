using Microsoft.VisualBasic.ApplicationServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data.Entity;

namespace PencilShop
{
    /// <summary>
    /// Interaction logic for Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).cmd = new SqlCommand("SELECT Role FROM Users WHERE Name = '" + username.Text + "' and Password = '" + password.Text + "'", ((App)Application.Current).cn);
            ((App)Application.Current).dr = ((App)Application.Current).cmd.ExecuteReader();
            if(((App)Application.Current).dr.Read())
            {
                string a = ((App)Application.Current).dr["Role"].ToString();
                if (((App)Application.Current).dr["Role"].ToString() == "admin")
                {
                    AdminProducts products1 = new AdminProducts();
                    this.Hide();
                    products1.Show();
                }
                else
                {
                    Products products2 = new Products();
                    this.Hide();
                    products2.Show();
                }
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует!");
            }
            ((App)Application.Current).dr.Close();
        }
        

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Registration1 frm = new Registration1();
            this.Hide();
            frm.Show();
        }
    }
}