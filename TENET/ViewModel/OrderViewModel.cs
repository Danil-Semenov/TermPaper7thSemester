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
    public class OrderViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> Back { get; }
        public ReactiveCommand<Unit, Unit> Save { get; }

        public OrderViewModel()
        {
            var PublicDataConnecton = new DataConnecton();
            Back = ReactiveCommand.Create(() =>
            {

                var Home = new Home();
                Home.Show();
            });

            Save = ReactiveCommand.Create(() =>
            {
                GlobalData.name = order;
                GlobalData.contractName = contract;
                GlobalData.DataEnd = dataend;
                //var date1 = DateTime.Now + GlobalData.DataEnd;
                DateTime date2 = DateTime.Now;
               
                var b = date2.AddDays(dataend);
                GlobalData.id = Convert.ToInt32(PublicDataConnecton.GetUserId(GlobalData.login, GlobalData.password));
                PublicDataConnecton.UpdateProject(GlobalData.name, GlobalData.contractName, b, GlobalData.id);
            });


        }

        [Reactive]
        public string order { get; set; }
        [Reactive]
        public string contract { get; set; }
        [Reactive]
        public int dataend { get; set; }
    }
}
