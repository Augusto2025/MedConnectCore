namespace MedConnect.Dominio
{
    public class Telefone
    {
        public long Id { get; set; }
        public int Ddd { get; set; }
        public string Numero { get; set; }
        public long IdPessoa { get; set; }
        public TipoTelefone Tipo { get; set; }
    }
}