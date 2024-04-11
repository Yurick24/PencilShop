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
using System.Data.SqlClient;

namespace PencilShop
{
    /// <summary>
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Window
    {
        public Products()
        {
            InitializeComponent();
        }

        private void WorkGround_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).dr.Close(); 
            ((App)Application.Current).cmd = new SqlCommand("SELECT * FROM Products", ((App)Application.Current).cn);
            ((App)Application.Current).dr = ((App)Application.Current).cmd.ExecuteReader();

            int rowCount = 0;
            while (((App)Application.Current).dr.Read())
            {
                /*ProductUnit unit = new ProductUnit();
                string a = ((App)Application.Current).dr[1].ToString();
                unit.NameUnit.Text = a;

                WorkGround.Children.Add(unit);*/

                ProductUnit product = new ProductUnit();
                product.VerticalAlignment = VerticalAlignment.Top;
                product.HorizontalAlignment = HorizontalAlignment.Stretch;
                product.Height = 152;
                product.MaxHeight = 175;
                product.MinWidth = 800;
                product.MaxWidth = 1644;
                //product.Id.Visibility = ((App)Application.Current).IsAdmin ? Visibility.Visible : Visibility.Hidden;
                product.NameUnit.Text = Convert.ToString(((App)Application.Current).dr[1]); 
                product.Category.Text = Convert.ToString(((App)Application.Current).dr[3]);
                product.PriceUnit.Text = Convert.ToString(((App)Application.Current).dr[4]);
                product.ImageForUnit.Source = new BitmapImage(new Uri("/Resources/" + Convert.ToString(((App)Application.Current).dr[2]), UriKind.Relative));
                product.Margin = new Thickness(0, 20 + (rowCount * product.Height * 1.2), 0, 0);
                WorkGround.Children.Add(product);
                rowCount++;
            } 
            ((App)Application.Current).dr.Close();
        }
    }
}
