using System.Windows;
using ReactiveUI;
using TENET;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.SqlClient;

namespace TENET
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class ReportWindow : ReactiveWindow<ReportViewModel>
    {

        
        

        public ReportWindow()
        {
            DataContext = ViewModel = new ReportViewModel();
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.BindCommand(this.ViewModel, x => x.Back, x => x.ButtonBack);
                this.BindCommand(this.ViewModel, x => x.send1, x => x.Button1);
                this.BindCommand(this.ViewModel, x => x.send2, x => x.Button2);
                this.BindCommand(this.ViewModel, x => x.send3, x => x.Button3);
                this.BindCommand(this.ViewModel, x => x.send4, x => x.Button4);
                this.BindCommand(this.ViewModel, x => x.send5, x => x.Button5);
                this.Bind(this.ViewModel, x => x.numberProject, x => x.project.Text);



            });

            var proektTable = new System.Data.DataTable();
            string sql = "SELECT [id_проект] as 'Номер-проекта', [Название] as 'Проект', Дата_Начала as 'Дата Начала', [Дата_сдачи] as 'Дата Сдачи', [Предмет_договора] as 'Предмет договора' FROM dbo.Проект WHERE [Название] IS NOT NULL AND [Предмет_договора] IS NOT NULL AND [Дата_Начала] IS NOT NULL AND [Дата_сдачи] IS NOT NULL";
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            var cn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            Projects.ItemsSource = proektTable.DefaultView;
            cn.Close();
            //adapter.Dispose();

            
        }

        

       


        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void button_Click2(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            this.UpdateLayout();
        }

        private void button_Click3(object sender, RoutedEventArgs e)
        {
            this.UpdateLayout();
        }

        private void button_Click4(object sender, RoutedEventArgs e)
        {
            this.UpdateLayout();
        }

        private void button_Click5(object sender, RoutedEventArgs e)
        {
            this.UpdateLayout();
        }

        private void button_Click6(object sender, RoutedEventArgs e)
        {
            this.UpdateLayout();
        }
    }
}
