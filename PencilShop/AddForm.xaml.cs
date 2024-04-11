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
using System.Data.SqlClient;
using System.IO;
using Microsoft.Win32;

namespace PencilShop
{
    /// <summary>
    /// Логика взаимодействия для AddForm.xaml
    /// </summary>
    public partial class AddForm : Window
    {
        public string ImageName = "";
        public AddForm()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            AdminProducts products = new AdminProducts();
            products.Show();
        }
        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameUnit.Text == null || Category.Text == null || PriceUnit == null)
            {
                MessageBox.Show("Пустые поля!");
                return;
            }
            else
            {
                if (ImageName == "")
                {
                    MessageBox.Show("Добавьте картинку!");
                    return;
                }
                ((App)Application.Current).dr.Close();
                ((App)Application.Current).cmd = new SqlCommand("INSERT INTO Products Values ('" + NameUnit.Text + "', '" + ImageName + "', '" + Category.Text + "', '" + PriceUnit.Text + "')", ((App)Application.Current).cn);
                ((App)Application.Current).cmd.ExecuteReader();
            }
            MessageBox.Show("Продукт добавлен!");
            AdminProducts products = new AdminProducts();
            this.Close();
            products.Show();
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); 
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image Files|*.jpg;*.png";
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string filePath = openFileDialog.FileName;
                // Копирование файла в папку Resources внутри проекта (замените "Resources" на вашу папку)
                string destinationFolder = "Resources";
                string fileName = System.IO.Path.GetFileName(filePath); 
                string destinationPath = Path.Combine("C:\\Users\\yuric\\source\\repos\\PencilShop\\PencilShop\\", destinationFolder, fileName);
                try
                {
                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                    }
                    if (File.Exists(destinationPath))
                    {
                        ImageForUnit.Source = new BitmapImage(new Uri(destinationPath, UriKind.Relative))
                        {
                            CacheOption = BitmapCacheOption.OnLoad,
                            StreamSource = new MemoryStream()
                        };
                        ImageName = fileName; 
                        ImageForUnit.UpdateLayout();
                    }
                    else
                    {
                        File.Copy(filePath, destinationPath);
                        ImageForUnit.Source = new BitmapImage(new Uri(destinationPath, UriKind.Relative))
                        {
                            CacheOption = BitmapCacheOption.OnLoad,
                            StreamSource = new MemoryStream()
                        };
                        ImageName = fileName; 
                        ImageForUnit.UpdateLayout();
                    }
                    MessageBox.Show("Файл успешно сохранен в папку Resources.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении файла: " + ex.Message);
                }
            }
        }
    }
}
