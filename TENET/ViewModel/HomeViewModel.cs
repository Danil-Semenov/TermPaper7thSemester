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
   public class HomeViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> ViewProgress { get; set; }
        public ReactiveCommand<Unit, Unit> Message { get; set; }
        public ReactiveCommand<Unit, Unit> EditInformation { get; set; }
        public ReactiveCommand<Unit, Unit> OrderNewProject { get; set; }
        public HomeViewModel()
        {
            var PublicDataConnecton = new DataConnecton();
            GlobalData.name = PublicDataConnecton.GetUserName(GlobalData.login, GlobalData.password) ;
            GlobalData.id =  Convert.ToInt32(PublicDataConnecton.GetUserId(GlobalData.login, GlobalData.password)) ;
            nameUser = GlobalData.name;
            if (GlobalData.id >9999)
            {
                Button1Text = "Написать сообщение";
                Button2Text = "Редактировать личную информацию";
                Button3Text = "Заказать новый проект";
                Button4Text = "Ход разработки проектов";
            }
            ViewProgress = ReactiveCommand.Create(() =>
            { Console.WriteLine("ss"); });
            Message = ReactiveCommand.Create(() =>
            { Console.WriteLine("ss"); });
            EditInformation = ReactiveCommand.Create(() =>
            { Console.WriteLine("ss"); });
            OrderNewProject = ReactiveCommand.Create(() =>
            { Console.WriteLine("ss"); });
        }


        [Reactive]
        public string nameUser { get; set; }
        [Reactive]
        public string Button1Text { get; set; }
        [Reactive]
        public string Button2Text { get; set; }
        [Reactive]
        public string Button3Text { get; set; }
        [Reactive]
        public string Button4Text { get; set; }
    }
}
