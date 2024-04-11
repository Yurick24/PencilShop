using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Data.SqlClient;

namespace PencilShop
{
    /// <summary>
    /// Логика взаимодействия для ProductUnitFromAdmin.xaml
    /// </summary>
    public partial class ProductUnitFromAdmin : UserControl
    {
        public string ImageName = "";
        public ProductUnitFromAdmin()
        {
            InitializeComponent();
            //ImageName = ImageForUnit.Source.ToString().Replace("/Resources/","");
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
                string destinationPath = System.IO.Path.Combine("C:\\Users\\yuric\\source\\repos\\PencilShop\\PencilShop\\", destinationFolder, fileName);
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

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы точно хотите удалить товар?","Подтверждение",MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ((App)Application.Current).dr.Close();
                ((App)Application.Current).cmd = new SqlCommand("Delete From Products Where id = '" + idF.Content + "'", ((App)Application.Current).cn);
                ((App)Application.Current).cmd.ExecuteReader();
                MessageBox.Show("Продукт удален!");
                Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).Close();
                AdminProducts products = new AdminProducts();
                products.Show();
            }
            else
            {
                MessageBox.Show("Продукт не был удален!");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
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
                ((App)Application.Current).cmd = new SqlCommand("UPDATE Products Set Name = '" + NameUnit.Text + "', SourceName = '" + ImageName + "', Category = '" + Category.Text + "', Prise = '" + PriceUnit.Text + "' WHERE id = '" + idF.Content + "'", ((App)Application.Current).cn);
                ((App)Application.Current).cmd.ExecuteReader();
            }
            MessageBox.Show("Продукт изменён!");
        }
    }
}
