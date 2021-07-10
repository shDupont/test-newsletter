using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Text;



using System.Threading.Tasks;

namespace Newsletter.Models
{

    public class Clientes
    {
        [Key]
        public int id { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        [DisplayName("Nome Completo:")]
        [StringLength(50, MinimumLength = 5)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Caracteres não permitidos")]
        public string nomeCompleto { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        [DisplayName("Endereço de Email:")]
        [RegularExpression("^(?=.{1,64}@)[A-Za-z0-9_-]+(\\.[A-Za-z0-9_-]+)*@[^-][A-Za-z0-9-]+(\\.[A-Za-z0-9-]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Valid Charactors include (A-Z) (a-z) (' space -)")]
        public string email { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        [DisplayName("Ativo para receber emails:")]
        public bool ativo { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        [DisplayName("Data de Cadastro:")]
        [RegularExpression("(\\d\\d\\/\\d\\d\\/\\d\\d\\d\\d) ", ErrorMessage = "Formato: DD/MM/AAAA")]
        public string dataCadastro { get; set; }

        [Column(TypeName = "varchar(250)")]
        [DisplayName("Data do cancelamento do email:")]
        [DisplayFormat(DataFormatString ="{0:d}", ApplyFormatInEditMode = true)]
        [RegularExpression("(\\d\\d\\/\\d\\d\\/\\d\\d\\d\\d)", ErrorMessage = "Formato: DD/MM/AAAA")]
        public string dataRevogação { get; set; }


    }

   
    public class ShowDetail
    {
        public List<Clientes> Search(List<string> Information)
        {
            StringBuilder Buildsql = new StringBuilder();
            Buildsql.Append("use Newsletter select * from [clientes] where ");
            
            foreach (string value in Information)
            {
                Buildsql.AppendFormat("nomeCompleto like '%{0}%' or email like '%{0}%' or ativo like '%{0}%' or dataCadastro like '%{0}%' or dataRevogação like '%{0}%' and ", value);
            }
            string datasql = Buildsql.ToString(0, Buildsql.Length - 5);
            return QueryList(datasql);
        }
        protected List<Clientes> QueryList(string text)
        {
            List<Clientes> lst = new List<Clientes>();
            SqlCommand cmd = GenerateSqlCommand(text);
            using (cmd.Connection)
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lst.Add(ReadValue(reader));
                    }
                }
            }
            return lst;
        }
        protected SqlCommand GenerateSqlCommand(string cmdText)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand(cmdText, con);
            cmd.Connection.Open();
            return cmd;
        }
        protected Clientes ReadValue(SqlDataReader reader)
        {
            Clientes dt = new Clientes();
            dt.id = (int)reader["id"];
            dt.nomeCompleto = (string)reader["Nome Completo:"];
            dt.email = (string)reader["Email"];
            dt.ativo = (bool)reader["Ativo"];
            dt.dataCadastro = (string)reader["Data Cadastro"];
            dt.dataRevogação = (string)reader["Data Revogação"];
            return dt;
        }
    }
    }
