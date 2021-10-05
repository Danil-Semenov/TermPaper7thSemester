using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data.Common;
using Newtonsoft.Json;
using System.IO;

namespace TENET.Model
{
    public class LogMessage
    {
        public LogMessage(int id_user, string message, DateTime date_time)
        {
            Id_user = id_user;
            Message = message;
            Date_time = date_time;
        }
        public int Id_user { get; set; }
        public string Message { get; set; }
        public DateTime Date_time { get; set; }
    }

    public class DataConnecton
    {
        // Кто работает та строка и основная а другая просто закомичена
        // Строка подключения Данила
        //const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=LAPTOP-Q4M9643N";
        // Строка подключения Влада
        const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";

        public string SqlQueryResponseString(string select, SqlCommand newCommand) 
        {
            var answer = "";
            using (var reader = newCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        foreach (var sel in select.Split(','))
                        {
                            var sell = sel.Replace(" ", "");
                            answer = reader.GetString(reader.GetOrdinal(sell));
                        }
                    }
                }
            }
            return answer;
        }

        //
        public List<string> SqlQueryResponseString1(string select, SqlCommand newCommand)
        {

            List<string> User = new List<string>();
            using (var reader = newCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        foreach (var sel in select.Split(','))
                        {
                            var sell = sel.Replace(" ", "");
                            User.Add(Convert.ToString(reader.GetString(reader.GetOrdinal(sell))));

                        }
                    }
                }
            }
            return User;
        }
        //
        public int SqlQueryResponseInt(string select, SqlCommand newCommand)
        {
            var answer = 0;
            using (var reader = newCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        foreach (var sel in select.Split(','))
                        {
                            var sell = sel.Replace(" ", "");
                            answer = reader.GetInt32(reader.GetOrdinal(sell));
                        }
                    }
                }
            }
            return answer;
        }

        public void Otpravka(string message)
        {
            var log = new LogMessage(GlobalData.id, message, DateTime.Now);
            var output = JsonConvert.SerializeObject(log);
            File.AppendAllText("log.json", output + ",");
        }


        public int GetIdWork(int id)
        {

            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = 0;
            var select = "id_работа";
            cmd.CommandText = $"Select id_работа From [dbo].[Проект_работа] where fk_id_проект='{id}' ;";
            vernyt = SqlQueryResponseInt(select, cmd);
            cn.Close();
            return vernyt;

        }


        public void UpdateIdTeam(int id,int id2, string name, int id3)
        {

            var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = $"Update dbo.Сотрудник set [fk_id_команды]='{id}' where ФИО='{name}'";
            command.ExecuteNonQuery();
            Otpravka(command.CommandText);
            command.CommandText = $"Update dbo.Сотрудник set [fk_id_команды]='{id}' where id_сотрудника='{id3}'";
            command.ExecuteNonQuery();
            Otpravka(command.CommandText);
            command.CommandText = $"Update dbo.Сотрудник set [fk_id_тех_зад]='{id2}' where ФИО='{name}'";
            command.ExecuteNonQuery();
            Otpravka(command.CommandText);
            connection.Close();

        }

        public int GetIdTeam()
        {

            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = 0;
            var select = "id_команда";
            cmd.CommandText = $"Select id_команда From [dbo].[Команда] where id_команда = (select max(id_команда) from [dbo].[Команда]);";
            vernyt = SqlQueryResponseInt(select, cmd);
            cn.Close();
            return vernyt;

        }

        public int GetIdTechEx()
        {

            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = 0;
            var select = "id_тех_зад";
            cmd.CommandText = $"Select id_тех_зад From [dbo].[Техническое_задание] where id_тех_зад = (select max(id_тех_зад) from [dbo].[Техническое_задание]);";
            vernyt = SqlQueryResponseInt(select, cmd);
            cn.Close();
            return vernyt;

        }


        public int GetIdProject(string name)
        {

            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = 0;
            var select = "id_проект";
            cmd.CommandText = $"Select id_проект From [dbo].[Проект] where Название='{name}';";
            vernyt = SqlQueryResponseInt(select, cmd);
            cn.Close();
            return vernyt;

        }

        public int GetIdWorkProgress()
        {

            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = 0;
            var select = "id_ход_раб";
            cmd.CommandText = $"Select id_ход_раб From [dbo].[Ход_работы] where id_ход_раб = (select max(id_ход_раб) from [dbo].[Ход_работы]);";
            vernyt = SqlQueryResponseInt(select, cmd);
            cn.Close();
            return vernyt;

        }


        public string GetIdProg(string name)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var select = $"select id_сотрудника from dbo.Сотрудник where ФИО='{name}' ";
            var cmd = new SqlCommand(select, cn);
            return (string)Convert.ToString(cmd.ExecuteScalar());
        }


        public void InsertWorkProgress(int id1)
        {
            var date = new DateTime();
            date = DateTime.Now;
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = $"INSERT INTO [oil].[dbo].[Ход_работы] VALUES('{date}','0%','{id1}'); ";
            command.ExecuteNonQuery();
            Otpravka(command.CommandText);
            connection.Close();
        }

        public void InsertTeam(string name,int id1, int id2,int id3,int id4)
        {
            
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = $"INSERT INTO [oil].[dbo].[Команда] VALUES('{name}','{id1}','{id2}','{id1}','{id3}','{id4}'); ";
            command.ExecuteNonQuery();
            Otpravka(command.CommandText);
            connection.Close();

        }



        public void InsertInfo(string name, string hours, string dogovor, string text,int id1, int id2)
        {
            
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = $"INSERT INTO [oil].[dbo].[Проект_работа] VALUES(1,NULL); ";
            command.ExecuteNonQuery();
            Otpravka(command.CommandText);
            command.CommandText = $"Update dbo.Проект_работа set[часы работы] = '{hours}', fk_id_проект = (select id_проект from dbo.Проект where Название = '{name}') where[часы работы] = 1";
            command.ExecuteNonQuery();
            Otpravka(command.CommandText);
            command.CommandText = $"Update dbo.Проект set Предмет_договора='{dogovor}' where Название='{name}'";
            command.ExecuteNonQuery();
            Otpravka(command.CommandText);
            command.CommandText = $"Insert into dbo.Техническое_задание values ('{text}', '{id1}', '{id2}')";
            command.ExecuteNonQuery();
            Otpravka(command.CommandText);
            connection.Close();

        }


        ///
        public string GetTeamByName(string name)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var select = $"Select Название from Команда where [fk_id_проект] = (Select id_проект from Проект where Название = '{name}')";
            var cmd = new SqlCommand(select, cn);
            return (string)cmd.ExecuteScalar();
        }
        public string GetClockByName(string name)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var select = $"Select [часы работы] from [Проект_работа] where [fk_id_проект] = (Select id_проект from Проект where Название = '{name}')";
            var cmd = new SqlCommand(select, cn);
            return (string)Convert.ToString(cmd.ExecuteScalar());
        }
        public string GetDogovorByName(string name)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var select = $"Select [Предмет_договора] from Проект where Название = '{name}';";
            var cmd = new SqlCommand(select, cn);
            return (string)cmd.ExecuteScalar();
        }
        public string GetTaskByName(string name)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var select = $"Select Текст from [Техническое_задание] where FK_id_составитель = (Select [fk_id_менеджер] from Команда where [fk_id_проект] = (Select id_проект from Проект where Название = '{name}'))";
            var cmd = new SqlCommand(select, cn);
            return (string)cmd.ExecuteScalar();
        }



        ///
        public void UpdateWork(string name1, string name2)
        {

            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = $"Update dbo.Вид_работы set [Название] = '{name2}' where Название = '{name1}' ";
            cmd.ExecuteNonQuery();
            Otpravka(cmd.CommandText);
            cn.Close();

        }


        public void UpdateTeam(string name1, string name2)
        {

            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = $"Update dbo.Команда set [Название] = '{name2}' where Название = '{name1}' ";
            cmd.ExecuteNonQuery();
            Otpravka(cmd.CommandText);
            cn.Close();

        }


        public void UpdateProject(string name1, string name2)
        {

            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = $"Update dbo.Проект set [Название] = '{name2}' where Название = '{name1}' ";
            cmd.ExecuteNonQuery();
            Otpravka(cmd.CommandText);
            cn.Close();

        }

        
        public string GetTeam(string name)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = "";
            var select = "Название";
            cmd.CommandText = $"select Название from dbo.Команда where fk_id_проект =(select id_проект from dbo.Проект where dbo.Проект.Название='{name}')";
            vernyt = SqlQueryResponseString(select, cmd);
            cn.Close();
            return vernyt;
        }



        public void UpdateProjects(int idProject, int time)
        {
            
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = $"Update dbo.Проект_работа set [часы работы] = '{time}' where fk_id_проект = '{idProject}' ";
            cmd.ExecuteNonQuery();
            Otpravka(cmd.CommandText);
            cn.Close();

        }

        public string GetClientCompany(int id)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = "";
            var select = "Название";
            cmd.CommandText = $"Select Название From [dbo].[Компания] Where [id_компании] = (select fk_id_компания from dbo.Клиент where fk_id_проект = '{id}');";
            vernyt = SqlQueryResponseString(select, cmd);
            cn.Close();
            return vernyt;
        }

        public DateTime GetDataEndProject(int id)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var select = "select Дата_сдачи from dbo.Проект where id_проект =" + id + "";
            var cmd = new SqlCommand(select, cn);
            var sum = (DateTime)cmd.ExecuteScalar();
            return sum;
        }

        public DateTime GetDataStartProject(int id)
        {
            
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var select = "select Дата_начала from dbo.Проект where id_проект =" + id + "";
            var cmd = new SqlCommand(select, cn);
            var sum = (DateTime)cmd.ExecuteScalar();
            return sum;
        }


        public string GetProjectPerformance(int id)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = "";
            var select = "Оценка_эксперта";
            cmd.CommandText = $"select Оценка_эксперта from dbo.Ход_работы where (id_ход_раб= (select fk_id_ход_раб from dbo.Команда where fk_id_проект='{id}'));";
            vernyt = SqlQueryResponseString(select, cmd);
            cn.Close();
            return vernyt;
        }


        public string GetClientName(int id)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = "";
            var select = "ФИО";
            cmd.CommandText = $"Select ФИО From [dbo].[Клиент] Where [fk_id_проект] = {id};";
            vernyt = SqlQueryResponseString(select, cmd);
            cn.Close();
            return vernyt;
        } 


        public int CountStaff()
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var select = "Select COUNT (*) From dbo.Сотрудник";
            var cmd = new SqlCommand(select, cn);
            var sum = (int)cmd.ExecuteScalar();
            return sum;
        }


        public int CountClients()
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var select = "Select COUNT (*) From dbo.Клиент";
            var cmd = new SqlCommand(select, cn);
            var sum = (int)cmd.ExecuteScalar();
            return sum;
        }

        public void UpdatePerfromance(string name, int id,string name2)

        {
            var date = new DateTime();
            date = DateTime.Now;
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = $"Update dbo.Ход_работы set [Дата_оценки]='{date}' ,[Оценка_эксперта] = '{name}' where (id_ход_раб = (select fk_id_ход_раб from dbo.Команда where dbo.Команда.fk_id_проект = (select id_проект from dbo.Проект where dbo.Проект.Название='{name2}')))";
            cmd.ExecuteNonQuery();
            Otpravka(cmd.CommandText);
            cn.Close();
        }

        
        public int GerProjectId(int id)

        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = 0;
            var select = "fk_id_проект";
            cmd.CommandText = $"select fk_id_проект from dbo.Команда where fk_id_программист = '{id}';";
            vernyt = SqlQueryResponseInt(select, cmd);
            cn.Close();
            return vernyt;
        }
        
        
        public void SendMassege(int id1, int id2, string massage)
        {
            var date = new DateTime();
            date = DateTime.Now;
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = $"INSERT INTO [oil].[dbo].[Сообщение] VALUES('{massage}', {id1}, {id2}, '{date}'); ";
            command.ExecuteNonQuery();
            Otpravka(command.CommandText);
            connection.Close();

        }
        public List<string> GetFioList()
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var select = "ФИО";
            cmd.CommandText = $"Select ФИО from dbo.Клиент;";
            SqlQueryResponseString1(select, cmd);

            return SqlQueryResponseString1(select, cmd);
        }

        public int GetRecipientid(string username)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = 0;
            var select = "id_клиент";
            cmd.CommandText = $"Select id_клиент from dbo.Клиент Where ФИО LIKE '{username}';";
            vernyt = SqlQueryResponseInt(select, cmd);
            cn.Close();
            return vernyt;
        }




        
        public void UpdateProject(string name, string dogovor, DateTime data, int id)

        {
            var a = DateTime.Now;
            
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = $"Update dbo.Проект set Название = '{name}', [Предмет_договора] = '{dogovor}', [Дата_начала] ='{a}', [Дата_сдачи] = '{data}'  Where id_проект = (select fk_id_проект from dbo.Клиент where id_клиент = '{id}');";
            cmd.ExecuteNonQuery();
            Otpravka(cmd.CommandText);
            cn.Close();

        }


        public int GetTeamName(int id)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = 0;
            var select = "id_команда";
            cmd.CommandText = $"Select id_команда From [dbo].[Команда] Where [fk_id_проект] = {id};";
            vernyt = SqlQueryResponseInt(select, cmd);
            cn.Close();
            return vernyt;
        }



        public string GetProjectName(int id)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = "";
            var select = "Название";
            cmd.CommandText = $"Select Название From [dbo].[Проект] Where [id_проект] = {id};";
            vernyt = SqlQueryResponseString(select, cmd);
            cn.Close();
            return vernyt;
        }


        public int GetIdCompany()
        {

            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = 0;
            var select = "id_компании";
            cmd.CommandText = $"Select id_компании From [dbo].[Компания] where id_компании = (select max(id_компании) from [dbo].[Компания]);";
            vernyt = SqlQueryResponseInt(select, cmd);
            cn.Close();
            return vernyt;

        }
        
        public int GetIdProject()

        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = 0;
            var select = "id_проект";
            cmd.CommandText = $"Select id_проект From [dbo].[Проект] where id_проект = (select max(id_проект) from [dbo].[Проект]);";
            vernyt = SqlQueryResponseInt(select, cmd);
            cn.Close();
            return vernyt;
        }

        public int GetSalaryProgrammer(int id)

        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = 0;
            var select = "ставка";
            cmd.CommandText = $"Select ставка from Должности Where(id_должности = (Select FK_id_должности from Сотрудник Where (id_сотрудника = (Select fk_id_программист from Команда Where fk_id_проект = '{id}'))));";
            vernyt = SqlQueryResponseInt(select, cmd);
            cn.Close();
            return vernyt;
        }

        public int GetSalaryManager(int id)

        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = 0;
            var select = "ставка";
            cmd.CommandText = $"Select ставка from Должности Where(id_должности = (Select FK_id_должности from Сотрудник Where (id_сотрудника = (Select fk_id_менеджер from Команда Where fk_id_проект = '{id}'))));";
            vernyt = SqlQueryResponseInt(select, cmd);
            cn.Close();
            return vernyt;
        }



        public int GetSum(int id)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var select = "Select SUM(Стоимость) From[Вид_работы] Where fk_id_работа = (Select id_работа from[Проект_работа] Where(fk_id_проект =" + id + "))";
            var cmd = new SqlCommand(select, cn);
            var sum = (int)cmd.ExecuteScalar();
            return sum;
        }

        public void AddProject(string name)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = $"INSERT INTO [oil].[dbo].[Проект] ([Название]) VALUES ('{name}');";
            cmd.ExecuteNonQuery();
            Otpravka(cmd.CommandText);
            cn.Close();
        }

        public void AddCompany(string name)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = $"INSERT INTO [oil].[dbo].[Компания] ([Название]) VALUES ('{name}');";
            cmd.ExecuteNonQuery();
            Otpravka(cmd.CommandText);
            cn.Close();
        }
        
        

        public void AddClient(string pasword, string login, string fio, int idProject, int idCompany)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = $"INSERT INTO [oil].[dbo].[Клиент] ([ФИО],[логин],[пароль],[fk_id_проект],[fk_id_компания]) VALUES ('{fio}', '{login}', '{pasword}', '{idProject}', '{idCompany}');";
            cmd.ExecuteNonQuery();
            Otpravka(cmd.CommandText);
            cn.Close();
        }
        
       
        
        public string GetUserName(string userLogin, string userPassword)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = "";
            var select = "ФИО";
            cmd.CommandText = $"Select ФИО from dbo.Клиент Where логин LIKE '{userLogin}' and пароль LIKE '{userPassword}';";
            vernyt = SqlQueryResponseString(select, cmd);
            if (vernyt == "")
            {
                cmd.CommandText = $"Select ФИО from dbo.Сотрудник Where логин LIKE '{userLogin}' and пароль LIKE '{userPassword}';";
                vernyt = SqlQueryResponseString(select, cmd);
            }
            if (vernyt == "")
            {
                vernyt = "не зарегестрированный пользователь";
            }

                cn.Close();
            return vernyt;
        }
        public int GetUserId(string userLogin, string userPassword)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = 0;
            var select = "id_клиент";
            cmd.CommandText = $"Select id_клиент from dbo.Клиент Where логин LIKE '{userLogin}' and пароль LIKE '{userPassword}';";
            vernyt = SqlQueryResponseInt(select, cmd);
            if (vernyt == 0)
            {
                select = "id_сотрудника";
                cmd.CommandText = $"Select id_сотрудника from dbo.Сотрудник Where логин LIKE '{userLogin}' and пароль LIKE '{userPassword}';";
                vernyt = SqlQueryResponseInt(select, cmd);
            }
            if (vernyt == 0)
            {
                vernyt = 0;
            }
            cn.Close();
            return vernyt;
        }

        public int GetUserLevel(int id)
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            var vernyt = 0;
            var select = "уровень_допуска";
            cmd.CommandText = $"Select [уровень_допуска] From [dbo].[Должности] Where [id_должности] = (Select [FK_id_должности] FROM [dbo].[Сотрудник] Where [id_сотрудника] = {id});";
            vernyt = SqlQueryResponseInt(select, cmd);
            cn.Close();
            return vernyt;
        }
        public void EditFioLoginPasswordUser(int id,string pasword, string login, string fio, string result)
        {
            
            if (id<9999)
            {
                var connection = new SqlConnection(connectionString);
                connection.Open();
                var command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = $"Update  [dbo].[Сотрудник] set [пароль] = '{pasword}', [логин] = '{login}', [ФИО] ='{fio}'  Where [id_сотрудника] = {id};";
                command.ExecuteNonQuery();
                Otpravka(command.CommandText);
                
                connection.Close();
               
            


            } else
            {
                var connection = new SqlConnection(connectionString);
                connection.Open();
                var command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = $"Update dbo.Клиент set пароль = '{pasword}', логин = '{login}', ФИО ='{fio}'  Where id_клиент = {id};";
                command.ExecuteNonQuery();
                Otpravka(command.CommandText);
                connection.Close();

            }
                        
        }
    }
}
