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
        public ReactiveCommand<Unit, Unit> TheCommand { get; }
        public ReactiveCommand<Unit, Unit> ViewProgress {  get; set; }
        public ReactiveCommand<Unit, Unit> Chat { get;  }
        public ReactiveCommand<Unit, Unit> EditInformation { get;  }
        public ReactiveCommand<Unit, Unit> OrderNewProject { get; }
        public ReactiveCommand<Unit, Unit> Back { get; }
        public ReactiveCommand<Unit, Unit> Report { get; }


        public HomeViewModel()
        {
            var PublicDataConnecton = new DataConnecton();
            GlobalData.name = PublicDataConnecton.GetUserName(GlobalData.login, GlobalData.password);
            GlobalData.id = Convert.ToInt32(PublicDataConnecton.GetUserId(GlobalData.login, GlobalData.password));
            GlobalData.level = PublicDataConnecton.GetUserLevel(GlobalData.id);
            //GlobalData.result = PublicDataConnecton.GetWorkResult(GlobalData.id);
            //GlobalData.ProjectId = PublicDataConnecton.GerProjectId(GlobalData.id);

            if (GlobalData.name == "не зарегестрированный пользователь")
                GlobalData.level = 4;
            nameUser = GlobalData.name;

           // GlobalData.level = 4;

            if (GlobalData.level == 3)//руководитель 
            {
                Vision = false;
                Button1Vision = true;
                Button5Vision = false;
                Button1Text = "Открыть модуль ГИС";

                Chat = ReactiveCommand.Create(() =>
                {
                    System.Diagnostics.Process.Start(@"A:\Курсач ПИС\Maps\Maps\bin\Debug\Maps.exe");
                });

            }

            if (GlobalData.level==0) //клиент
            {
                Vision = true;
                Button1Vision = false;
                Button5Vision = false;
                //Button1Text = "Написать сообщение";
                Button2Text = "Редактировать личную информацию";
                Button3Text = "Заказать новый проект";
                Button4Text = "Ход разработки проекта"; 

                OrderNewProject = ReactiveCommand.Create(() =>
                {
                    var OrderWindow = new OrderWindow();
                    OrderWindow.Show();
                });

                EditInformation = ReactiveCommand.Create(() =>
                {
                    var EditWindow = new EditWindow();
                    EditWindow.Show();
                });
                
                    ViewProgress = ReactiveCommand.Create(() =>
                    {
                        var ProgressWindow = new ProgressWindow();
                        ProgressWindow.Show();
                    });

            }

            if (GlobalData.level == 1) //manager
            {
                Vision = true;
                Button1Vision = true;
                Button5Vision = true;
                Button1Text = "Написать сообщение"; 
                Button2Text = "Добавить нового клиента";
                Button3Text = "Просмотр клиентов";
                Button4Text = "Курирование проектов"; // сделать
                Button5Text = "Отчеты"; 

                Chat = ReactiveCommand.Create(() =>
                {
                    var MessageWindow = new MessageWindow();
                    MessageWindow.Show();
                });

                EditInformation = ReactiveCommand.Create(() =>
                {
                    var AddClientWindow = new AddClientWindow();
                    AddClientWindow.Show();
                });

                Report = ReactiveCommand.Create(() =>
                {
                    var ReportWindow = new ReportWindow();
                    ReportWindow.Show();
                });

                OrderNewProject = ReactiveCommand.Create(() =>
                {
                    var ClientsWindow = new ClientsWindow();
                    ClientsWindow.Show();
                });

                ViewProgress = ReactiveCommand.Create(() =>
                {
                    var EditProjectsWindow = new EditProjectsWindow();
                    EditProjectsWindow.Show();
                });

            }

            if (GlobalData.level == 2) // programmer
            {
                Vision = true;
                Button1Vision = true;
                Button5Vision = false;
                Button1Text = "Написать сообщение"; // -
                Button2Text = "Редактировать личную информацию";
                Button3Text = "Просмотр проектов";
                Button4Text = "Технические задания"; 
                //Button5Text = "Редактировать ход роботы"; 

                EditInformation = ReactiveCommand.Create(() =>
                {
                    var EditWindow = new EditWindow();
                    EditWindow.Show();
                });

                Chat = ReactiveCommand.Create(() =>
                {
                    var MessageWindow = new MessageWindow();
                    MessageWindow.Show();
                });

                OrderNewProject = ReactiveCommand.Create(() =>
                {
                    var ProjectsWindow = new ProjectsWindow();
                    ProjectsWindow.Show();
                });

                ViewProgress = ReactiveCommand.Create(() =>
                {
                    var TasksWindow = new TasksWindow();
                    TasksWindow.Show();
                });

            }

            if (GlobalData.level == 4)
            {
                Vision = false;
                Button1Vision = false;
                Button5Vision = false;
            }
        Back = ReactiveCommand.Create(() =>
            {
                GlobalData.password = "";
                GlobalData.login = "";
                var MainWindow = new MainWindow();
                MainWindow.Show();
            });
            
        }

        [Reactive]
        public bool Vision { get; set; }
        [Reactive]
        public bool Button1Vision { get; set; }
        [Reactive]
        public bool Button5Vision { get; set; }

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
        [Reactive]
        public string Button5Text { get; set; }
    }
}
