using ReactiveUI;
using TENET;
using System.Windows;

namespace TENET
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : ReactiveWindow<HomeViewModel>
    {
        public Home()
        {
            DataContext = ViewModel = new HomeViewModel();
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(this.ViewModel, x => x.nameUser, x => x.TextBlock1.Text);
                
                //this.BindCommand(this.ViewModel, x => x.Message, x => MessageButton);
                this.Bind(this.ViewModel, x=> x.Button1Text, x=> MessageButton.Content);

               // this.BindCommand(this.ViewModel, x => x.EditInformation, x => EditInformationButton);
                this.Bind(this.ViewModel, x => x.Button2Text, x => EditInformationButton.Content);

              //  this.BindCommand(this.ViewModel, x => x.OrderNewProject, x => OrderNewProjectButton);
                this.Bind(this.ViewModel, x => x.Button3Text, x => OrderNewProjectButton.Content);

                //this.BindCommand(this.ViewModel, x => x.ViewProgress, x => ViewProgressButton);
                this.Bind(this.ViewModel, x => x.Button4Text, x => ViewProgressButton.Content);
            });
        }
    }
}
