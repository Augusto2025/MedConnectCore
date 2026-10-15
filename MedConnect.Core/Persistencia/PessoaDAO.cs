using System;
using System.Data.SqlClient;
using MedConnect.Dominio;

namespace MedConnect.Persistencia
{
    public static class PessoaDAO
    {
        // String de conexão genérica para o SQL Server
        private static string stringConexao = "Server=localhost;Database=MedConnectDB;Trusted_Connection=True;";

        public static bool Inserir(Pessoa pessoa)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                SqlTransaction transacao = conexao.BeginTransaction();

                try
                {
                    // Inserção na tabela PESSOA com retorno do ID gerado
                    string queryPessoa = "INSERT INTO PESSOA (nome, cpf, data_nascimento) OUTPUT INSERTED.id VALUES (@nome, @cpf, @dataNascimento)";
                    long idPessoaGerado;

                    using (SqlCommand cmd = new SqlCommand(queryPessoa, conexao, transacao))
                    {
                        cmd.Parameters.AddWithValue("@nome", pessoa.Nome);
                        cmd.Parameters.AddWithValue("@cpf", pessoa.Cpf);
                        cmd.Parameters.AddWithValue("@dataNascimento", pessoa.DataNascimento);
                        idPessoaGerado = (long)cmd.ExecuteScalar();
                    }

                    // Inserção dos Endereços associados
                    foreach (var endereco in pessoa.Enderecos)
                    {
                        string queryEndereco = "INSERT INTO ENDERECO (logradouro, numero, bairro, cidade, estado, cep, id_pessoa) VALUES (@logradouro, @numero, @bairro, @cidade, @estado, @cep, @idPessoa)";
                        using (SqlCommand cmd = new SqlCommand(queryEndereco, conexao, transacao))
                        {
                            cmd.Parameters.AddWithValue("@logradouro", endereco.Logradouro);
                            cmd.Parameters.AddWithValue("@numero", endereco.Numero);
                            cmd.Parameters.AddWithValue("@bairro", endereco.Bairro);
                            cmd.Parameters.AddWithValue("@cidade", endereco.Cidade);
                            cmd.Parameters.AddWithValue("@estado", endereco.Estado);
                            cmd.Parameters.AddWithValue("@cep", endereco.Cep);
                            cmd.Parameters.AddWithValue("@idPessoa", idPessoaGerado);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transacao.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transacao.Rollback();
                    return false;
                }
            }
        }

        public static Pessoa BuscarPorCpf(long cpf)
        {
            Pessoa pessoa = null;
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                string query = "SELECT id, nome, cpf, data_nascimento FROM PESSOA WHERE cpf = @cpf";
                using (SqlCommand cmd = new SqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    conexao.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pessoa = new Pessoa
                            {
                                Id = reader.GetInt64(0),
                                Nome = reader.GetString(1),
                                Cpf = reader.GetInt64(2),
                                DataNascimento = reader.GetDateTime(3)
                            };
                        }
                    }
                }
            }
            return pessoa;
        }

        public static bool Atualizar(Pessoa pessoa)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                string query = "UPDATE PESSOA SET nome = @nome, data_nascimento = @dataNascimento WHERE cpf = @cpf";
                using (SqlCommand cmd = new SqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", pessoa.Nome);
                    cmd.Parameters.AddWithValue("@dataNascimento", pessoa.DataNascimento);
                    cmd.Parameters.AddWithValue("@cpf", pessoa.Cpf);
                    conexao.Open();
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
        }

        public static bool Excluir(long id)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                SqlTransaction transacao = conexao.BeginTransaction();
                try
                {
                    // Exclusão das dependências antes do registro principal
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM ENDERECO WHERE id_pessoa = @id", conexao, transacao))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM PESSOA WHERE id = @id", conexao, transacao))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }

                    transacao.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transacao.Rollback();
                    return false;
                }
            }
        }
    }
}