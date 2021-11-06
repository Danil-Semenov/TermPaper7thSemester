using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using TENET.Model;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.SqlClient;


namespace TENET
{
    public class ReportViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> Back { get; }
        public ReactiveCommand<Unit, Unit> send1 { get; }
        public ReactiveCommand<Unit, Unit> send2 { get; }
        public ReactiveCommand<Unit, Unit> send3 { get; }
        public ReactiveCommand<Unit, Unit> send4 { get; }
        public ReactiveCommand<Unit, Unit> send5 { get; }
        private Workbook ObjWorkBook;
        private Worksheet ObjWorkSheet;
        
        public ReportViewModel()
        {
            var PublicDataConnecton = new DataConnecton();
            


            Back = ReactiveCommand.Create(() =>
            {

                var Home = new Home();
                Home.Show();
            });
            

            send1 = ReactiveCommand.Create(() =>
            {
                GlobalData.id = numberProject;
                GlobalData.sumProfit = PublicDataConnecton.GetSum(GlobalData.id);
                GlobalData.salaryProg = PublicDataConnecton.GetSalaryProgrammer(GlobalData.id);
                GlobalData.salaryManag = PublicDataConnecton.GetSalaryManager(GlobalData.id);
                GlobalData.name = PublicDataConnecton.GetProjectName(GlobalData.id);
                int a = GlobalData.salaryProg + GlobalData.salaryManag;
                int b = GlobalData.sumProfit - a;
                string c;
                if (b > 0)
                {
                    c = "Да";
                }
                else
                {
                    c = "Нет";
                }

                Microsoft.Office.Interop.Excel.Application exApp = new Microsoft.Office.Interop.Excel.Application();
                 ObjWorkBook = exApp.Workbooks.Add(System.Reflection.Missing.Value);
                 ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
                 //exApp.Workbooks.Add();

                 Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)exApp.ActiveSheet;
                workSheet.Columns.ColumnWidth = 20;
                workSheet.Cells[2, 2] = "Отчет о прибыли";
                workSheet.Cells[3, 2] = "Проект";
                workSheet.Cells[4, 2] = "Команда, №";
                workSheet.Cells[3, 3] = GlobalData.name;
                workSheet.Cells[4, 3] = PublicDataConnecton.GetTeamName(GlobalData.id);
                workSheet.Cells[8, 2] = GlobalData.id;
                workSheet.Cells[8, 3] = DateTime.Now.ToString("dd MMMM yyyy | HH:mm:ss");
                workSheet.Cells[8, 4] = GlobalData.sumProfit;
                workSheet.Cells[8, 5] = a;
                workSheet.Cells[8, 6] = c;
                workSheet.Cells[8, 7] = b;
                workSheet.Cells[7, 7] = "Прибыль";
                workSheet.Cells[7, 6] = "Прибыльный";
                workSheet.Cells[7, 5] = "Расход";
                workSheet.Cells[7, 4] = "Приход";
                workSheet.Cells[7, 3] = "Дата";
                workSheet.Cells[7, 2] = "№";
                
                  //exApp.Quit();
                 exApp.Visible = true;
                 exApp.UserControl = true;
                
            });

            send2 = ReactiveCommand.Create(() =>
            {
                int a = PublicDataConnecton.CountStaff();
                int b = PublicDataConnecton.CountClients();
                Microsoft.Office.Interop.Excel.Application exApp = new Microsoft.Office.Interop.Excel.Application();
                ObjWorkBook = exApp.Workbooks.Add(System.Reflection.Missing.Value);
                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
                
                Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)exApp.ActiveSheet;
                workSheet.Columns.ColumnWidth = 30;
                workSheet.Cells[2, 1] = "Общее количество пользователей в системе";
                workSheet.Cells[3, 1] = a + b;
                workSheet.Cells[2, 2] = "Клиентов";
                workSheet.Cells[2, 3] = "Сотрудников";
                workSheet.Cells[3, 2] = b;
                workSheet.Cells[3, 3] = a;

                var proektTable = new System.Data.DataTable();
                string sql = "select dbo.Должности.название as 'Должность', COUNT(*) as 'Количество сотрудников' from dbo.Сотрудник,dbo.Должности where (FK_id_должности=id_должности) GROUP BY dbo.Должности.название";
                var cn = new SqlConnection(Connection.String);
                SqlCommand command = new SqlCommand(sql, cn);
                var adapter = new SqlDataAdapter(command);
                cn.Open();
                adapter.Fill(proektTable);
                cn.Close();

                for (var i = 0; i < proektTable.Columns.Count; i++)
                {
                    workSheet.Cells[7, i + 1] = proektTable.Columns[i].ColumnName;
                }

                
                for (var i = 0; i < proektTable.Rows.Count; i++)
                {
                    
                    for (var j = 0; j < proektTable.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 8, j + 1] = proektTable.Rows[i][j];
                    }
                }

                //Диаграмма
                var excelcells = ObjWorkSheet.get_Range("A8", "B9");

                //exApp.ActiveChart.FullSeriesCollection(1).XValues = "=Лист1!$D$15:$H$15";
                excelcells.Select();
                Microsoft.Office.Interop.Excel.Chart excelchart = (Microsoft.Office.Interop.Excel.Chart)exApp.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                excelchart.Activate();
                excelchart.Select(Type.Missing);
                //Изменяем тип диаграммы
                //exApp.ActiveChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DPieExploded;
                ((Microsoft.Office.Interop.Excel.Axis)exApp.ActiveChart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory,
                        Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary)).HasTitle = true;
                ((Microsoft.Office.Interop.Excel.Axis)exApp.ActiveChart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory,
                    Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary)).AxisTitle.Text = "Должность";
                ((Microsoft.Office.Interop.Excel.Axis)exApp.ActiveChart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue,
                        Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary)).HasTitle = true;
                ((Microsoft.Office.Interop.Excel.Axis)exApp.ActiveChart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue,
                    Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary)).AxisTitle.Text = "Количество людей";
                    
                //Название легенды
                //Microsoft.Office.Interop.Excel.SeriesCollection seriesCollection = (Microsoft.Office.Interop.Excel.SeriesCollection)exApp.ActiveChart.SeriesCollection(Type.Missing);
                //Microsoft.Office.Interop.Excel.Series series = seriesCollection.Item(1);
               // series.Name = "Менеджер";

                //не работает
                //Microsoft.Office.Interop.Excel.SeriesCollection seriesCollection2 = (Microsoft.Office.Interop.Excel.SeriesCollection)exApp.ActiveChart.SeriesCollection(Type.Missing);
               // Microsoft.Office.Interop.Excel.Series series2 = seriesCollection.Item(2);
                //series2.Name = "Программист";


                exApp.ActiveChart.HasLegend = false;
                //exApp.ActiveChart.Legend.Position = Microsoft.Office.Interop.Excel.XlLegendPosition.xlLegendPositionLeft;
                exApp.ActiveChart.Location(Microsoft.Office.Interop.Excel.XlChartLocation.xlLocationAsObject, "Лист1");


                var excelsheets = ObjWorkBook.Worksheets;
                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelsheets.get_Item(1);
                ObjWorkSheet.Shapes.Item(1).IncrementLeft(-201);
                ObjWorkSheet.Shapes.Item(1).IncrementTop((float)20.5);


                
                



                exApp.Visible = true;
                exApp.UserControl = true;
            });

            send3 = ReactiveCommand.Create(() =>
            {
                GlobalData.id = numberProject;
                GlobalData.name = PublicDataConnecton.GetProjectName(GlobalData.id);

                Microsoft.Office.Interop.Excel.Application exApp = new Microsoft.Office.Interop.Excel.Application();
                ObjWorkBook = exApp.Workbooks.Add(System.Reflection.Missing.Value);
                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
                
                Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)exApp.ActiveSheet;
                workSheet.Columns.ColumnWidth = 30;
                workSheet.Cells[2, 2] = "Отчет по проекту";
                workSheet.Cells[2, 3] = GlobalData.name;
                workSheet.Cells[3, 2] = "Клиент, заказавший проект";
                workSheet.Cells[3, 3] = PublicDataConnecton.GetClientName(GlobalData.id);
                workSheet.Cells[4, 2] = "Компания клиента";
                workSheet.Cells[4, 3] = PublicDataConnecton.GetClientCompany(GlobalData.id);
                workSheet.Cells[5, 2] = "Дата заказа проекта";
                workSheet.Cells[5, 3] = PublicDataConnecton.GetDataStartProject(GlobalData.id);
                workSheet.Cells[6, 2] = "Дата предоставления проекта клиенту";
                workSheet.Cells[6, 3] = PublicDataConnecton.GetDataEndProject(GlobalData.id); ;
                workSheet.Cells[7, 2] = "Ход выполнения работы,%";
                workSheet.Cells[7, 3] = PublicDataConnecton.GetProjectPerformance(GlobalData.id);
                exApp.Visible = true;
                exApp.UserControl = true;
            });


            send4 = ReactiveCommand.Create(() =>
            {
                
                Microsoft.Office.Interop.Excel.Application exApp = new Microsoft.Office.Interop.Excel.Application();
                ObjWorkBook = exApp.Workbooks.Add(System.Reflection.Missing.Value);
                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
                Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)exApp.ActiveSheet;
                workSheet.Columns.ColumnWidth = 20;
                workSheet.Cells[1, 1] = "Отчет по клиентам";
                
                var proektTable = new System.Data.DataTable();
                string sql = "Select ФИО,dbo.Компания.Название as 'Компания',логин,пароль from dbo.Клиент,dbo.Компания where (id_компании=fk_id_компания)";
                var cn = new SqlConnection(Connection.String);
                SqlCommand command = new SqlCommand(sql, cn);
                var adapter = new SqlDataAdapter(command);
                cn.Open();
                adapter.Fill(proektTable);
                cn.Close();

                for (var i = 0; i < proektTable.Columns.Count; i++)
                {
                    workSheet.Cells[2, i + 1] = proektTable.Columns[i].ColumnName;
                }


                for (var i = 0; i < proektTable.Rows.Count; i++)
                {

                    for (var j = 0; j < proektTable.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 3, j + 1] = proektTable.Rows[i][j];
                    }
                }





                exApp.Visible = true;
                exApp.UserControl = true;
            });


            send5 = ReactiveCommand.Create(() =>
            {
                Microsoft.Office.Interop.Excel.Application exApp = new Microsoft.Office.Interop.Excel.Application();
                ObjWorkBook = exApp.Workbooks.Add(System.Reflection.Missing.Value);
                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
                Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)exApp.ActiveSheet;
                workSheet.Columns.ColumnWidth = 20;
                workSheet.Cells[1, 1] = "Отчет по сотрудникам";

                var proektTable = new System.Data.DataTable();
                string sql = "select ФИО,[Дата начала работы] as 'Дата начала работы',dbo.Должности.название as 'Должность',паспорт from dbo.Сотрудник, dbo.Должности where (id_должности=FK_id_должности)";
                var cn = new SqlConnection(Connection.String);
                SqlCommand command = new SqlCommand(sql, cn);
                var adapter = new SqlDataAdapter(command);
                cn.Open();
                adapter.Fill(proektTable);
                cn.Close();

                for (var i = 0; i < proektTable.Columns.Count; i++)
                {
                    workSheet.Cells[2, i + 1] = proektTable.Columns[i].ColumnName;
                }


                for (var i = 0; i < proektTable.Rows.Count; i++)
                {

                    for (var j = 0; j < proektTable.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 3, j + 1] = proektTable.Rows[i][j];
                    }
                }





                exApp.Visible = true;
                exApp.UserControl = true;

            });

        }



        
        
        [Reactive]
        public int numberProject { get; set; }

    }
}