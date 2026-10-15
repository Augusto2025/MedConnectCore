using System;
using System.Collections.Generic;

namespace MedConnect.Dominio
{
    public class Pessoa
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public long Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        
        public List<Endereco> Enderecos { get; set; }
        public List<Telefone> Telefones { get; set; }

        public Pessoa()
        {
            Enderecos = new List<Endereco>();
            Telefones = new List<Telefone>();
        }
    }
}