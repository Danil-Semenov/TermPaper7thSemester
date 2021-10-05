using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using TENET.Model;

namespace TENET
{
    public class ViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> TheCommand { get; }
        public ViewModel()
        {
           
            massage = GlobalData.massage;
            password = GlobalData.password;
            login = GlobalData.login;
            TheCommand = ReactiveCommand.Create(() => 
            {
                if ("" == password && "" == login)
                {
                    GlobalData.massage = "Заполните пожалуйста поля \"Password\" и \"Login\"";
                    GlobalData.login = "";
                    GlobalData.password = "";
                    var MainWindow = new MainWindow();
                    MainWindow.Show();
                }
                else if ("" == password)
                {
                    GlobalData.massage = "Заполните пожалуйста поле \"Password\"";
                    GlobalData.password = "";
                    GlobalData.login = login;
                    var MainWindow = new MainWindow();
                    MainWindow.Show();
                }
                else if ( "" == login)
                {
                    GlobalData.massage = "Заполните пожалуйста поле \"Login\"";
                    GlobalData.login = "";
                    GlobalData.password = password;
                    var MainWindow = new MainWindow();
                    MainWindow.Show();
                }
                else
                {
                    GlobalData.password = password;
                    GlobalData.login = login;
                    var Home = new Home();
                    Home.Show();
                }
            });

        }

        [Reactive]
        public string login { get; set; }
        [Reactive]
        public string password { get; set; }
        [Reactive]
        public string massage { get; set; }

    }
}
