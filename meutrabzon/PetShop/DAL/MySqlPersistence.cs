using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.DAL
{
    public class MySqlPersistence
    {
        MySqlConnection _conexao = null;
        int _ultimoIdInserido = 0;

        public int UltimoIdInserido
        {
            get => _ultimoIdInserido;
            set => _ultimoIdInserido = value;
        }

        public MySqlPersistence()
        {            
            _conexao = DAL.Singleton.GetSingleton().GetConnection();
        }

        public void Open()
        {
            _conexao.Open();

        }

        public void Close()
        {
            _conexao.Close();

        }
        public DataTable ExecutarSelect(string select, out string msgErro)
        {
            msgErro = "";

            DataTable dt = new DataTable();
            MySqlCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = select;
            try
            {
                Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                msgErro = ex.Message;
            }
            finally
            {
                Close();
            }

            return dt;

        }

        /// <summary>
        /// Executa um SELECT
        /// </summary>
        /// <param name="select">Select para execução</param>
        /// <param name="parametros">Lista de parâmetros para o select</param>
        /// <returns>Um datatable com os dados.</returns>
        public DataTable ExecutarSelect(string select, Dictionary<string, string> parametros, out string msgErro)
        {
            msgErro = "";
            MySqlCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = select;

            foreach (string key in parametros.Keys)
            {
                cmd.Parameters.AddWithValue(key, parametros[key]);
            }

            DataTable dt = new DataTable();

            try
            {
                Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {

                msgErro = ex.Message;

            }
            finally { Close(); }
            return dt;

        }



        /// <summary>
        /// Executa Insert, Delete e Update
        /// </summary>
        /// <param name="sql">Comando Insert, delete ou update</param>
        /// <returns>Quantidade de linhas afetadas.</returns>
        public int ExecutarNonQuery(string sql, out string msgErro)
        {
            msgErro = "";
            int linhasAfetadas = 0;
            MySqlCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = sql;
            try
            {
                Open();

                linhasAfetadas = cmd.ExecuteNonQuery();
                _ultimoIdInserido = Convert.ToInt32(cmd.LastInsertedId);
            }
            catch (Exception ex)
            {

                msgErro = ex.Message;

            }
            finally
            {
                Close();
            }

            return linhasAfetadas;

        }


        public int ExecutarNonQuery(string sql, Dictionary<string, string> parametros, out string msgErro)
        {
            msgErro = "";
            MySqlCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = sql;

            foreach (string key in parametros.Keys)
            {
                cmd.Parameters.AddWithValue(key, parametros[key]);
            }

            int linhasAfetadas = 0;

            try
            {
                Open();
                linhasAfetadas = cmd.ExecuteNonQuery();
                _ultimoIdInserido = (int)cmd.LastInsertedId;

            }
            catch (Exception ex)
            {

                msgErro = ex.Message;

            }
            finally
            {
                Close();
            }
            return linhasAfetadas;

        }



        public object ExecutarScalar(string select, out string msgErro)
        {
            msgErro = "";

            MySqlCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = select;
            object obj = null;
            try
            {
                Open();
                obj = cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {

                msgErro = ex.Message;

            }
            finally
            {
                Close();
            }

            return obj;

        }

        public object ExecutarScalar(string select, Dictionary<string, string> parametros, out string msgErro)
        {
            msgErro = "";
            MySqlCommand cmd = _conexao.CreateCommand();
            cmd.CommandText = select;

            object obj = null;
            try
            {
                Open();

                foreach (string key in parametros.Keys)
                {
                    cmd.Parameters.AddWithValue(key, parametros[key]);
                }

                obj = cmd.ExecuteScalar();


            }
            catch (Exception ex)
            {

                msgErro = ex.Message;

            }
            finally
            {
                Close();
            }

            return obj;
        }

    }
}
