using System.Windows;
using ReactiveUI;
using TENET;

namespace TENET
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class OrderWindow : ReactiveWindow<OrderViewModel>
    {
        public OrderWindow()
        {
            DataContext = ViewModel = new OrderViewModel();
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.BindCommand(this.ViewModel, x => x.Back, x => x.ButtonBack);
                this.BindCommand(this.ViewModel, x => x.Save, x => x.ButtonSave);
                this.Bind(this.ViewModel, x => x.order, x => x.OrderName.Text);
                this.Bind(this.ViewModel, x => x.contract, x => x.Contract.Text);
                this.Bind(this.ViewModel, x => x.dataend, x => x.DataEnd.Text);

            });
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void button_Click_Save(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Проект успешно заказан");
            this.UpdateLayout();
        }
    }
}
