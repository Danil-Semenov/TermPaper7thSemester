using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using TENET.Model;

namespace TENET
{
    public class EditViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> Back { get; }
        public ReactiveCommand<Unit, Unit> Save { get; }
        public EditViewModel()
        {
            var PublicDataConnecton = new DataConnecton();
            FIO = GlobalData.name;
            Login = GlobalData.login;
            Password = GlobalData.password;
            Result = GlobalData.result;
            Back = ReactiveCommand.Create(() =>
            {
                Console.WriteLine(Password);
                var Home = new Home();
                Home.Show();
            });
            Save = ReactiveCommand.Create(() =>
            {
                GlobalData.name = FIO;
                GlobalData.login = Login;
                GlobalData.password = Password;
                GlobalData.result = Result;
                PublicDataConnecton.EditFioLoginPasswordUser(GlobalData.id, GlobalData.password, GlobalData.login, GlobalData.name, GlobalData.result);
            });


        }

        [Reactive]
        public string FIO { get; set; }
        [Reactive]
        public string Result { get; set; }
        [Reactive]
        public string Login { get; set; }
        [Reactive]
        public string Password { get; set; }
    }
}