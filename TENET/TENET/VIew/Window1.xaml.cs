using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using TENET.Model;

namespace TENET
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window1 : ReactiveWindow<Window1ViewModel>
    {
        List<string> newContent = new List<string>();
        StringBuilder message = new StringBuilder();
        public Window1()
        {
            InitializeComponent();

            var proektTable = new DataTable();
            string sql = "SELECT Название AS [Вид Работы]  FROM dbo.[Вид_работы] ";
            var cn = new SqlConnection(Connection.String);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            WorkGrid.ItemsSource = proektTable.DefaultView;
            WorkGrid.MouseLeftButtonDown += GotFocus;
        }
        private void GotFocus(System.Object sender, System.EventArgs e)
        {
            var drv = WorkGrid.SelectedItem as DataRowView;
            var sValue = drv != null ? drv.Row["Вид Работы"] as string : string.Empty;
            newContent.Add(sValue);
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var PublicDataConnecton = new DataConnecton();

            int idWork = PublicDataConnecton.GetIdWork(PublicDataConnecton.GetIdProject(GlobalData.proekt));


            for (var z = 0; z < newContent.Count; z++)
            {
                var cn = new SqlConnection(Connection.String);
                cn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = $"Update [dbo].[Вид_работы] set [fk_id_работа] = '{idWork}' Where Название = '{newContent[z]}'; ";
                cmd.ExecuteNonQuery();
                cn.Close();
                message.AppendLine(newContent[z]);
            }
            MessageBox.Show(message.ToString());
        }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
