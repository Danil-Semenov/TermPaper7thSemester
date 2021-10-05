using System.Windows;
using ReactiveUI;
using TENET;

namespace TENET
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class EditWindow : ReactiveWindow<EditViewModel>
    {
        public EditWindow()
        {
            DataContext = ViewModel = new EditViewModel();
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.BindCommand(this.ViewModel, x => x.Back, x => x.ButtonBack);
                this.BindCommand(this.ViewModel, x => x.Save, x => x.ButtonSave);
                this.Bind(this.ViewModel, x => x.FIO, x => x.EditFIO.Text);
                //this.Bind(this.ViewModel, x => x.Result, x => x.EditResult.Text);
                this.Bind(this.ViewModel, x => x.Login, x => x.EditLogin.Text);
                this.Bind(this.ViewModel, x => x.Password, x => x.EditPassword.Text);
            });
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        private void button_Click_Save(object sender, RoutedEventArgs e)
        {
            this.UpdateLayout();
        }
    }
}
