using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace PetShop.DAL
{
    public class Singleton
    {
        private static readonly Singleton instancia = new Singleton();
        private Singleton()
        {
        }
        public static Singleton GetSingleton()
        {
            return instancia;
        }
        public MySqlConnection GetConnection()
        {
            string _connect = "Server=den1.mysql4.gear.host; Port=3306;Database=primedb;Uid=primedb;Pwd=pr!meweb;SslMode=none";
            return new MySqlConnection(_connect);
        }
        public void Open()
        {
            GetSingleton().GetConnection().Open();
        }
        public void Close()
        {
            GetSingleton().GetConnection().Close();
        }
    }
}
