using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
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

namespace ShopSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Product> List = new ObservableCollection<Product>();
        public string ConnectionString { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            searchResult.ItemsSource = List;
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List.Clear();
            var request = searchBox.Text;
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    var query = $"select * from Product where Product.ProductName like '{request}%'";
                    var command = new SqlCommand(query, connection);
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var product = new Product();
                            product.Id = reader.GetInt32(0);
                            product.ProductName = reader.GetString(1);
                            product.SupplierId = reader.GetInt32(2);
                            product.UnitPrice = reader.GetDecimal(3);
                            product.Package = reader.GetString(4);
                            product.IsDiscounted = reader.GetBoolean(5);
                            List.Add(product);
                        }
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
