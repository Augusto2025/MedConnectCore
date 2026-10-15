using System;
using MedConnect.Dominio;
using MedConnect.Persistencia;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== TESTANDO O SISTEMA MEDCONNECT ===");

        // 1. Criando uma nova pessoa de teste
        Pessoa novaPessoa = new Pessoa();
        novaPessoa.Nome = "Carlos Alberto";
        novaPessoa.Cpf = 12345678901;
        novaPessoa.DataNascimento = new DateTime(1985, 10, 15);

        // 2. Adicionando um endereço a essa pessoa
        Endereco end = new Endereco();
        end.Logradouro = "Av. Paulista";
        end.Numero = "1000";
        end.Bairro = "Bela Vista";
        end.Cidade = "São Paulo";
        end.Estado = "SP";
        end.Cep = "01310100";

        novaPessoa.Enderecos.Add(end);

        // 3. Testando a inserção via DAO
        Console.WriteLine("Tentando inserir a pessoa no banco de dados...");
        
        try
        {
            bool sucesso = PessoaDAO.Inserir(novaPessoa);
            if (sucesso)
            {
                Console.WriteLine("Sucesso! Pessoa e endereço cadastrados com êxito.");
            }
            else
            {
                Console.WriteLine("A operação falhou (a transação fez rollback).");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro durante a inserção: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Nota: Se você não tiver o SQL Server rodando localmente na sua máquina, a conexão vai falhar — o que é normal para fins acadêmicos se o foco for apenas a estrutura do código.");
        }

        Console.WriteLine("Fim do teste.");
    }
}