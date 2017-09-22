using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexao_MySQL
{
    class clientes //camada de negócios (Model)
    {
        //metodos privados para proteção e encapsulamento das informações
        private int _id;
        private string _nome;
        private string _endereco;
        private string _cep;
        private DateTime _data_cadastro;

        //metodo construtor com sobrecarga
        public clientes(){ }

        public clientes(string nome, string endereco, string cep, DateTime data_cadastro) //para novos registros
        {
            //atribuindo os valores
            this.Nome = nome;
            this.Endereco = endereco;
            this.Cep = cep;
            this.Data_cadastro = data_cadastro;
        }
        public clientes(int id, string nome, string endereco, string cep) //para edicao de registros
        {
            //atribuindo os valores
            this.Id = id;
            this.Nome = nome;
            this.Endereco = endereco;
            this.Cep = cep;
        }

        //encapsulando:
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string Nome
        {
            get
            {
                return _nome;
            }

            set
            {
                _nome = value;
            }
        }

        public string Cep
        {
            get
            {
                return _cep;
            }

            set
            {
                _cep = value;
            }
        }

        public DateTime Data_cadastro
        {
            get
            {
                return _data_cadastro;
            }

            set
            {
                _data_cadastro = value;
            }
        }

        public string Endereco
        {
            get
            {
                return _endereco;
            }

            set
            {
                _endereco = value;
            }
        }
    }    
}
