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
    public class AddClientViewModel : ReactiveObject
    {

        public ReactiveCommand<Unit, Unit> Back { get; }
        public ReactiveCommand<Unit, Unit> Add { get; }
        
        
        public AddClientViewModel()
        {
            var PublicDataConnecton = new DataConnecton();

            Back = ReactiveCommand.Create(() =>
            {

                var Home = new Home();
                Home.Show();
            });

            Add = ReactiveCommand.Create(() =>
            {
                GlobalData.name = FIO;
                GlobalData.log = Login;
                GlobalData.pass = Password;
                GlobalData.companyName = Company;
                PublicDataConnecton.AddCompany(GlobalData.companyName);
                GlobalData.idCompany = Convert.ToInt32(PublicDataConnecton.GetIdCompany());
                PublicDataConnecton.AddProject(GlobalData.name = FIO);
                GlobalData.idProject = Convert.ToInt32(PublicDataConnecton.GetIdProject());
                PublicDataConnecton.AddClient(GlobalData.pass, GlobalData.log, GlobalData.name, GlobalData.idProject, GlobalData.idCompany);
            });

            

        }

        [Reactive]
        public string FIO { get; set; }
        [Reactive]
        public string Company { get; set; }
        [Reactive]
        public string Login { get; set; }
        [Reactive]
        public string Password { get; set; }
    }
}
