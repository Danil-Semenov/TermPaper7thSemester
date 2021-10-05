using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data.Common;

namespace TENET.Model
{
    public class DataConnecton
    {
        // Кто работает та строка и основная а другая просто закомичена
        // Строка подключения Данила
        const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=LAPTOP-Q4M9643N";
        // Строка подключения Влада
        //const string connectionString = "";

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
            return vernyt;
        }
    }
}
