using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conexao_MySQL
{
    public partial class Form1 : Form
    {
        private void modoNavegacao() // desabilitando os itens de edicao
        {
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            maskCEP.Enabled = false;
            btnLimpar.Enabled = false;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnNovo.Enabled = true;
            btnCancel.Enabled = false;
            btnSair.Enabled = true;
            dateCadastro.Enabled = false;
            DGClientes.Enabled = true;
        }
        private void modoEdicao() // habilitando os itens de edicao
        {
            txtNome.Enabled = true;
            txtEndereco.Enabled = true;
            maskCEP.Enabled = true;
            btnLimpar.Enabled = true;
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnNovo.Enabled = false;
            btnCancel.Enabled = true;
            btnSair.Enabled = false;
            dateCadastro.Enabled = true;
            DGClientes.Enabled = false;
        }

        public void limparCampos()
        {
            clientesController clienteController = new clientesController();
            //limpando campos
            txtID.Clear();
            txtNome.Clear();
            txtEndereco.Clear();
            maskCEP.Clear();
            txtNome.Focus(); //cursor volta ao campo nome
            DGClientes.DataSource = clienteController.getClientes(); //preenchendo o DataGrid
        }

        public Form1()
        {
            InitializeComponent();
            modoNavegacao(); //iniciando o form com os campos bloqueados
            //preenchendo o DataGrid:
            clientesController clienteController = new clientesController();
            DGClientes.DataSource = clienteController.getClientes(); 
        }

        private void btnSalvar_Click(object sender, EventArgs e) //salvando novo registro
        {
            if (txtNome.Text.Equals(""))
            {
                MessageBox.Show("Nome não pode ser vazio", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; //sai do IF se o campo nome estiver vazio (poderia implementar mais validaçoes)
            }
            try
            {
                clientesController clienteController = new clientesController(); //objeto para acessar as funcoes de manuseio do BD

                if (txtID.Text == "") //se o campo ID estiver vazio, é novo registro
                {
                    clientes novoCliente = new clientes(txtNome.Text, txtEndereco.Text, maskCEP.Text, dateCadastro.Value.Date);
                    clienteController.IncluirNovoCliente(novoCliente); //cadastrando novo cliente
                }
                else //aí é salvar alguma edição
                {
                    clientes editarCliente = new clientes(Convert.ToInt32(txtID.Text),txtNome.Text, txtEndereco.Text, maskCEP.Text);
                    clienteController.AtualizarCliente(editarCliente); //efetuando a atualização do registro
                }
                MessageBox.Show("Dados registrados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLimpar.PerformClick(); //chamando o evento click do botao limpar
                modoNavegacao(); //desabilitando os campos apos salvar
            }catch(Exception erro)
            {//se der erro
                MessageBox.Show("Houve o seguinte erro: " + erro.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e) //limpando os campos e o dando um refresh nos dados de memória
        {
            limparCampos();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            //habilitando campos e botoes para cadastro
            modoEdicao();
            txtNome.Focus(); //cursor no txtNome
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //desabilitando campos
            modoNavegacao();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close(); //encerrando a aplicação
        }

        private void DGClientes_Click(object sender, EventArgs e)
        {
            if (DGClientes.CurrentRow.Cells[0].Value.ToString() != null)
            {
                txtID.Text = DGClientes.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = DGClientes.CurrentRow.Cells[1].Value.ToString();
                txtEndereco.Text = DGClientes.CurrentRow.Cells[2].Value.ToString();
                maskCEP.Text = DGClientes.CurrentRow.Cells[3].Value.ToString();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if(txtID.Text == "") //testando se há registro selecionado
            {
                MessageBox.Show("Selecione um registro na tabela.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; //sai do IF
            }
            modoEdicao(); //habilita a edicao dos campos
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") //testando se há registro selecionado
            {
                MessageBox.Show("Selecione um registro na tabela.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; //sai do IF
            }
            clientesController clienteController = new clientesController(); //criando e instanciando o objeto para fazermos a exclusao
            clienteController.ExcluirCliente(Convert.ToInt32(txtID.Text)); //excluindo
            limparCampos();
            MessageBox.Show("Registro excluido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
