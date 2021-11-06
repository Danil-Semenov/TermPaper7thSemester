using System.Windows;
using ReactiveUI;
using TENET;
using System.Data;
using System.Data.SqlClient;
using System;
using TENET.Model;

namespace TENET
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class ProjectEditWindow : ReactiveWindow<ProjectEditViewModel>
    {
        public ProjectEditWindow()
        {
            DataContext = ViewModel = new ProjectEditViewModel();
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.BindCommand(this.ViewModel, x => x.Back, x => x.ButtonBack);

            });

            TextBox.Text = GlobalData.proekt;
            TextBox1.Text = GlobalData.team;
            //TextBox2.Text = GlobalData.work;
            Fill(GlobalData.proekt);
            Work.MouseLeftButtonDown += GotFocus;
        }

        private void GotFocus(System.Object sender, System.EventArgs e)
        {
            var drv = Work.SelectedItem as DataRowView;
            var sValue = drv != null ? drv.Row["Название"] as string : string.Empty;
            TextBox2.Text=sValue;
            GlobalData.work = sValue;
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void button_Click_Save(object sender, RoutedEventArgs e)
        {
            var PublicDataConnecton = new DataConnecton();
            PublicDataConnecton.UpdateProject(GlobalData.proekt, TextBox.Text);
            PublicDataConnecton.UpdateTeam(GlobalData.team, TextBox1.Text);
            PublicDataConnecton.UpdateWork(GlobalData.work, TextBox2.Text);
            Fill(GlobalData.proekt);
            GlobalData.work = TextBox2.Text;
            MessageBox.Show($"Проект успешно отредактирован");
        }

        public void Fill(string name)

        {

            var PublicDataConnecton = new DataConnecton();
            var proektTable = new DataTable();
            string sql = $"select dbo.Вид_работы.Название from dbo.Вид_работы where fk_id_работа =(select id_работа from dbo.Проект_работа where fk_id_проект=(select id_проект from dbo.Проект where dbo.Проект.Название='{name}'))";
            var cn = new SqlConnection(Connection.String);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            Work.ItemsSource = proektTable.DefaultView;
            cn.Close();

        }
    }
}
