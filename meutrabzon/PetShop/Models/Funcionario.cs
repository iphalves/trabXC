using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Models
{
    public class Funcionario
    {
        private int _id;
        private string _nomeCompleto;
        private string _cpf;
        private string _rg;
        private string _sexo;
        private int _senha;

        public string GetNomeCompleto()
        {
            return _nomeCompleto;
        }
        public void SetNomeCompleto(string _nomeCompleto)
        {
            this._nomeCompleto = _nomeCompleto;
        }
        public string GetCpf() { return _cpf; }
        public void SetCpf(string _cpf) { this._cpf = _cpf; }
        public string GetRg() { return _rg; }
        public void SetRg(string _rg) { this._rg = _rg; }
        public string GetSexo() { return _sexo; }
        public void SetSexo(string _sexo) { this._sexo = _sexo; }
        public int GetId() { return _id; }
        public void SetId(int _id) { this._id = _id; }
        public int GetSenha() { return _senha; }
        public void SetSenha(int _senha) { this._senha = _senha; }


        public Funcionario()
        {


        }
        public Funcionario(string nomeCompleto, string cpf, string rg, string sexo, int senha)
        {
            SetNomeCompleto(nomeCompleto);
            SetCpf(cpf);
            SetRg(rg);
            SetSexo(sexo);
            SetSenha(senha);
        }




        public bool Autenticar(string cpf, string senha)
        {
            DAL.MySqlPersistence bd = new DAL.MySqlPersistence();
            string select = @"select count(*) 
                              from funcionario 
                              where cpf = @cpf and
                                    senha = @senha";

            Dictionary<string, string> ps = new Dictionary<string, string>();
            ps.Add("@cpf", cpf);
            ps.Add("@senha", senha.ToString());

            string msg = "";
            object count = bd.ExecutarScalar(select, ps, out msg);

            if (Convert.ToInt32(count) == 0)
                return false;
            else return true;
        }

        public bool Gravar(out string msg)
        {
            msg = "";
            if (_nomeCompleto == "")
            {
                msg = "Forneça o nome.";

                return false;
            }

            if (_id == 0 && _senha == 0)
            {
                msg = "Forneça a senha.";

                return false;
            }


            DAL.MySqlPersistence bd = new DAL.MySqlPersistence();

            string sql = "";
            Dictionary<string, string> ps = new Dictionary<string, string>();
            if (_id == 0)
            {
                sql = @"insert into funcionario (nome, cpf, rg, sexo, senha)
                         values (@nomecompleto, @cpf, @rg, @sexo, @senha)";
                
            }
            else {

                sql = @"update funcionario set 
                                    nome = @nomecompleto, 
                                    rg = @rg,
                                    sexo = @sexo,
                                    cpf = @cpf
                                  where idfunc = @id";

                ps.Add("@id", _id.ToString());
            }

            ps.Add("@nomecompleto", _nomeCompleto);
            ps.Add("@cpf", _cpf);
            ps.Add("@rg", _rg);
            ps.Add("@sexo", _sexo);
            ps.Add("@senha", _senha.ToString());

            int linhas = bd.ExecutarNonQuery(sql, ps, out msg);

            if (_id == 0)
            {
                SetId(bd.UltimoIdInserido);
            }

            return linhas > 0;
        }
        public bool Deletar(int id,out string msg)
        {
            DAL.MySqlPersistence bd = new DAL.MySqlPersistence();
            string sql = "";
            Dictionary<string, string> fs = new Dictionary<string, string>();
            sql = @"delete from  funcionario where idfunc = @idfunc";
            fs.Add("@idFunc", id.ToString());
            int linhas = bd.ExecutarNonQuery(sql, fs, out msg);
            if (linhas > 0)
            {
                return true;
            }
            return false;
        }

        public bool ObterPorId(int id) {

            DAL.MySqlPersistence bd = new DAL.MySqlPersistence();

            string sql = "select * from funcionario where idfunc = " + id;
            string msg = "";
            DataTable dt = bd.ExecutarSelect(sql, out msg);

            if (dt.Rows.Count == 1)
            {
                SetId(Convert.ToInt32(dt.Rows[0]["Id"]));
                SetNomeCompleto(dt.Rows[0]["nome"].ToString());
                SetCpf(dt.Rows[0]["cpf"].ToString());
                SetRg(dt.Rows[0]["rg"].ToString());
                SetSexo(dt.Rows[0]["sexo"].ToString());
                SetSenha(Convert.ToInt32(dt.Rows[0]["Senha"]));
                return true;
            }
            else return false;

        }

        public List<Funcionario> Pesquisar(string nome)
        {
            DAL.MySqlPersistence bd = new DAL.MySqlPersistence();

            string sql = "select * from funcionario where nome like @nome";
            Dictionary<string, string> parametros = new Dictionary<string, string>();
            parametros.Add("@nome", nome + "%");

            string msg = "";
            DataTable dt = bd.ExecutarSelect(sql, parametros, out msg);

            List<Funcionario> fs = new List<Funcionario>();
            foreach (DataRow row in dt.Rows)
            {
                Funcionario f = new Funcionario();
                f.SetId(Convert.ToInt32(row["Id"]));
                f.SetNomeCompleto(row["nome"].ToString());
                f.SetCpf(row["cpf"].ToString());
                f.SetRg(row["rg"].ToString());
                f.SetSexo(row["sexo"].ToString());
                f.SetSenha(Convert.ToInt32(row["Senha"]));
                fs.Add(f);

            }
            return fs;

        }

    }
}
