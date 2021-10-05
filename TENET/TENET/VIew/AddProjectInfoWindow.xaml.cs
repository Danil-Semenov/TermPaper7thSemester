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
    public partial class AddProjectInfoWindow : ReactiveWindow<AddProjectInfoViewModel>
    {
        string sValue = "";
        public AddProjectInfoWindow()
        {

            DataContext = ViewModel = new AddProjectInfoViewModel();
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.BindCommand(this.ViewModel, x => x.Back, x => x.ButtonBack);

            });

            var proektTable = new DataTable();
            string sql = "Select Название as 'Проект',[Дата_начала] as 'Дата начала',[Дата_сдачи] as 'Дата сдачи',[Предмет_договора] as 'Предмет договора' from Проект where ([Дата_начала] > GETDATE()-7)";
            //string sql = "SELECT Distinct [Проект].[Название] as 'Проект' ,[Дата_начала],[Дата_сдачи],[Предмет_договора], Команда.Название FROM [dbo].[Проект], [dbo].[Техническое_задание],[dbo].[Команда],[dbo].[Вид_работы], [dbo].[Проект_работа] Where (Команда.fk_id_проект = Проект.id_проект) and ([Проект_работа].fk_id_проект = Проект.id_проект and [Проект_работа].id_работа = [Вид_работы].fk_id_работа) and ([Дата_начала] > GETDATE()-7) ";
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            var cn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            ProectGrid.ItemsSource = proektTable.DefaultView;
            cn.Close();
            ProectGrid.MouseLeftButtonDown += GotFocus;
            Fill();
            Staff.MouseLeftButtonDown += GotFocus1;


        }
        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void GotFocus1(System.Object sender, System.EventArgs e)
        {
            var zxc = new DataConnecton();
            var drv = Staff.SelectedItem as DataRowView;
            sValue = drv != null ? drv.Row["ФИО"] as string : string.Empty;
            GlobalData.programmer = sValue;
            GlobalData.idprog = zxc.GetIdProg(sValue);

        }

        private void GotFocus(System.Object sender, System.EventArgs e)
        {
            var drv = ProectGrid.SelectedItem as DataRowView;
            sValue = drv != null ? drv.Row["Проект"] as string : string.Empty;
            var zxc = new DataConnecton();
            TeamTextBox.Text = zxc.GetTeamByName(sValue);
            ClockTextBox.Text = zxc.GetClockByName(sValue);
            DogovorTextBox.Text = zxc.GetDogovorByName(sValue);
            TaskTextBox.Text = zxc.GetTaskByName(sValue);
            GlobalData.proekt = sValue;

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var PublicDataConnecton = new DataConnecton();

            PublicDataConnecton.InsertInfo(GlobalData.proekt,ClockTextBox.Text, DogovorTextBox.Text, TaskTextBox.Text, GlobalData.id, Convert.ToInt32(GlobalData.idprog));
            PublicDataConnecton.InsertWorkProgress(GlobalData.id);
            int a = PublicDataConnecton.GetIdWorkProgress();
            int b = PublicDataConnecton.GetIdProject(GlobalData.proekt);
            int c = PublicDataConnecton.GetIdTechEx();
            PublicDataConnecton.InsertTeam(TeamTextBox.Text, GlobalData.id, Convert.ToInt32(GlobalData.idprog),b,a);
            int f = PublicDataConnecton.GetIdTeam();

            PublicDataConnecton.UpdateIdTeam(f,c, GlobalData.programmer, GlobalData.id);
            MessageBox.Show("Информация успешно добавлена");
        }

        public void Fill()
        {
            var proektTable2 = new DataTable();
            string sql2 = "select ФИО from dbo.Сотрудник where FK_id_должности=3";
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            var cn2 = new SqlConnection(connectionString);
            SqlCommand command2 = new SqlCommand(sql2, cn2);
            var adapter2 = new SqlDataAdapter(command2);
            cn2.Open();
            adapter2.Fill(proektTable2);
            Staff.ItemsSource = proektTable2.DefaultView;
            cn2.Close();
        }

        private void button_Click2(object sender, RoutedEventArgs e)
        {
            var Window1 = new Window1();
            Window1.Show();
        }
    }
}
