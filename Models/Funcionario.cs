using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_empresa.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string cpf {  get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public int FilialId { get; set; }
        public int idade {  get; set; }
        public string senha { get; set; }
        public string nomeFilial { get; set; }
    }
}
