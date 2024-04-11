using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace PencilShop
{
    public partial class Registration1 : Window
    {
        public Registration1()
        {
            InitializeComponent();
        }
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            /*if (password.Length < 8)
            {
                MessageBox.Show("Пароль должен содержать как минимум 8 символов.");
                return;
            }
            if (!Regex.IsMatch(password, "[a-zA-Z]"))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну букву.");
                return;
            }
            if (!Regex.IsMatch(password, "[0-9]"))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну цифру.");
                return;
            }
            if (!Regex.IsMatch(username, "[a-zA-Z]"))
            {
                MessageBox.Show("Имя должно содержать хотя бы одну букву.");
                return;
            }
            if (username.Length < 4)
            {
                MessageBox.Show("Имя должно содержать как минимум 4 символа.");
                return;
            }*/
            ((App)Application.Current).cmd = new SqlCommand("SELECT * FROM Users WHERE name = '" + username.Text + "' and password = '" + password.Text + "'", ((App)Application.Current).cn);
            ((App)Application.Current).dr = ((App)Application.Current).cmd.ExecuteReader();
            if (((App)Application.Current).dr.Read())
            {
                MessageBox.Show("Такой пользователь уже существует!");
            }
            else
            {
                ((App)Application.Current).dr.Close();
                ((App)Application.Current).cmd = new SqlCommand("INSERT INTO Users Values ('" + username.Text + "', '" + password.Text + "', 'user')", ((App)Application.Current).cn);
                ((App)Application.Current).cmd.ExecuteReader();
            }
            MessageBox.Show("Регистрация завершена!");
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Autorization frm = new Autorization();
            this.Hide();
            frm.Show();
        }
    }
}
