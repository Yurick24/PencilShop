using System.Configuration;
using System.Data;
using System.Windows;
using System.Data.SqlClient;

namespace PencilShop
{

    public partial class App : Application
    {
        public SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Products;Integrated Security=True;MultipleActiveResultSets=true");
        public SqlCommand cmd;
        public SqlDataReader dr;
        public string globalRole;

        public App()
        {
            cn.Open();
        }
    }

}
