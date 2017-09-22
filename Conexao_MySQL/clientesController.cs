using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration; //para acessarmos arquivo xml de configuração, onde deixamos nossa string de conexao

//bibliotecas MySQL:
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace Conexao_MySQL
{
    class clientesController //classe de camada de acesso aos dados (Controller)
    {
        string Conexao;

        //metodo construtor 
        public clientesController()
        {
            //obtendo a string de conexao do arquivo App.config
            Conexao = ConfigurationManager.AppSettings["ConexaoMySQL"];
        }

        //incluindo um novo registro fazendo conexao manual:
        public void IncluirNovoCliente(clientes cliente)
        {
            MySqlConnection novaConexao = new MySqlConnection(Conexao); //criando um objeto para manipular a conexao com o BD
            MySqlCommand comando = novaConexao.CreateCommand(); //aplica comandos sql atrelado a conexao
            comando.CommandText = "INSERT INTO clientes (nome, endereco, cep, data_cadastro) VALUES (?nome, ?endereco, ?cep, ?data_cadastro)"; //comando que vamos executar na base de dados
            comando.Parameters.AddWithValue("?nome", cliente.Nome); //usando os parametros get dos metodos da classe clientes.cs (parametro, valor)
            comando.Parameters.AddWithValue("?endereco", cliente.Endereco);
            comando.Parameters.AddWithValue("?cep", cliente.Cep);
            comando.Parameters.AddWithValue("?data_cadastro", cliente.Data_cadastro);

            try
            {
                novaConexao.Open(); //abrindo a conexao com a base de dados
                int registros = comando.ExecuteNonQuery(); //executa o comando SQL e retorna o numero de linhas afetadas
            }
            catch (MySqlException erro)
            {
                throw new ApplicationException(erro.ToString()); //se der erro, mostra
            }
            finally
            {
                novaConexao.Close(); //encerrando a conexao com o BD
            }
        }

        //excluindo registro
        public void ExcluirCliente(int id)
        {
            MySqlConnection novaConexao = new MySqlConnection(Conexao); //criando um objeto para manipular a conexao com o BD
            MySqlCommand comando = novaConexao.CreateCommand(); //aplica comandos sql atrelado a conexao
            comando.CommandText = "DELETE FROM clientes WHERE id=?id"; //comando que vamos executar na base de dados
            comando.Parameters.AddWithValue("?id", id);
            try
            {
                novaConexao.Open(); //abrindo a conexao com a base de dados
                int registros = comando.ExecuteNonQuery(); //executa o comando SQL e retorna o numero de linhas afetadas
            }
            catch (MySqlException erro)
            {
                throw new ApplicationException(erro.ToString()); //se der erro, mostra
            }
            finally
            {
                novaConexao.Close(); //encerrando a conexao com o BD
            }
        }

        //incluindo um novo registro fazendo conexao manual:
        public void AtualizarCliente(clientes cliente)
        {
            MySqlConnection novaConexao = new MySqlConnection(Conexao); //criando um objeto para manipular a conexao com o BD
            MySqlCommand comando = novaConexao.CreateCommand(); //aplica comandos sql atrelado a conexao
            comando.CommandText = "UPDATE clientes set nome = ?nome, endereco = ?endereco, cep=?cep WHERE id=?id"; //comando que vamos executar na base de dados
            comando.Parameters.AddWithValue("?id", cliente.Id); //usando os parametros get dos metodos da classe clientes.cs (parametro, valor)
            comando.Parameters.AddWithValue("?nome", cliente.Nome); 
            comando.Parameters.AddWithValue("?endereco", cliente.Endereco);
            comando.Parameters.AddWithValue("?cep", cliente.Cep);

            try
            {
                novaConexao.Open(); //abrindo a conexao com a base de dados
                int registros = comando.ExecuteNonQuery(); //executa o comando SQL e retorna o numero de linhas afetadas
            }
            catch (MySqlException erro)
            {
                throw new ApplicationException(erro.ToString()); //se der erro, mostra
            }
            finally
            {
                novaConexao.Close(); //encerrando a conexao com o BD
            }
        }

        //selecionando os registros da base de dados:
        public DataTable getClientes()
        {
            MySqlConnection novaConexao = new MySqlConnection(Conexao); //criando um objeto para manipular a conexao com o BD
            MySqlCommand comando = novaConexao.CreateCommand(); //aplica comandos sql atrelado a conexao
            MySqlDataAdapter dataAdapter; //vai intermediar a base de dados e a aplicação
            comando.CommandText = "SELECT * FROM clientes"; //comando que vamos executar na base de dados
            try
            {
                novaConexao.Open(); //abrindo a conexao com a base de dados
                comando = new MySqlCommand(comando.CommandText, novaConexao); //instanciando o objeto com o SQL e a String de conexao
                dataAdapter = new MySqlDataAdapter(comando); //instanciando o DataAdapter passando as informações de interação com o BD
                DataTable dtClientes = new DataTable(); //criando um DataTable para alocar as informações na memória
                dataAdapter.Fill(dtClientes); //atualizando o conteudo do DataTable com o que veio da execucao do SQL
                return dtClientes;
            }catch(MySqlException erro)
            {
                throw new ApplicationException(erro.ToString()); //se der erro, mostra
            }
            finally
            {
                novaConexao.Close(); //encerrando a conexao com o BD
            }
        }
    }
}
