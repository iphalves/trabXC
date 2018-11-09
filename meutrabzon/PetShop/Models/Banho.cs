using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;

namespace PetShop.Models
{
    public class Banho
    {
        private int _id;
        private int _codBanho;
        private string _data;
        private string _valor;
        private string _idAni;
        private string _idCli;
        private string _idFun;
        

    public int Id { get => _id; set => _id = value; }
    public int CodBanho { get => _codBanho; set => _codBanho = value; }
    public string Valor{get => _valor; set => _valor = value;}
    public string IdAni{get => _idAni; set => _idAni = value;}
    public string IdCli{get => _idCli; set => _idCli = value;}
    public string IdFun { get => _idFun; set => _idFun = value; }
    public string Data{get => _data; set => _data = value;}
    public Banho() {}
    public Banho(int codBanho, string valor, string idani,
     string idcli,string idfun, string data){
         CodBanho = codBanho;
         Valor = valor;
         IdAni = idani;
         IdCli = idcli;
         IdFun = idfun;
         Data = data;
    }
    public List<Banho> CarregarBanhos()
    {
        string dataF = ordenaData();
        DAL.MySqlPersistence bd = new DAL.MySqlPersistence();

        string sql = "select * from banho where dataAtual = @dataAtual";
        Dictionary<string, string> parametros = new Dictionary<string, string>();
        parametros.Add("@dataAtual", dataF);

        string msg = "";
        DataTable dt = bd.ExecutarSelect(sql, parametros, out msg);

        List<Banho> bs = new List<Banho>();
        foreach (DataRow row in dt.Rows)
        {
        Banho b = new Banho();
        b.Id = Convert.ToInt32(row["Id"]);
        b.CodBanho = Convert.ToInt32(row["codBanho"]);
        b.Data = row["data"].ToString();
        b.Valor = row["valor"].ToString();
        b.IdAni = row["idAni"].ToString();
        b.IdCli= row["IdCli"].ToString();
        b.IdFun = row["idFunc"].ToString();
        bs.Add(b);

        }
        return bs;
    }
    public string ordenaData()
        {
            DateTime data = DateTime.Today;
            string datE = data.ToString();
            datE = datE.Replace("/", "-");
            string[] datEs = datE.Substring(0).Split(" ");
            string[] datEs2 = datEs[0].Substring(0).Split("-");
            string dataF = datEs2[2] + "-" + datEs2[1] + "-" + datEs2[0];
            return dataF;
        }

    }
}