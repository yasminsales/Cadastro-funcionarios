using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace exercicio_2_LP
{
    public partial class FormularioFuncionario : Form
    {
        private Form cadastroF;
        private int idAtualizacao;

        private string TratarCampoVazio(object valor)
        {
            if (valor is System.DBNull)
            {
                return "";
            }

            return (string)valor;
        }

        public FormularioFuncionario(Lista form_cadastro, int idAtualizacao)
        {
            this.cadastroF = form_cadastro;
            this.idAtualizacao = idAtualizacao;
            InitializeComponent();

            if (idAtualizacao == 0)
            {
                this.Text = "Criar novo contato";
            }
            else
            {
                PreencherDados();
                this.Text = "Editando funcionário " + idAtualizacao;
            }
        }

        private void PreencherDados()
        {
            OleDbConnection con = new OleDbConnection(Globals.ConnString);
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "Select * from CadastroFuncionario where ID = " + idAtualizacao;

            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                this.textBox_nome.Text = TratarCampoVazio(reader["Nome"]);
                this.textBox_genero.Text = TratarCampoVazio(reader["Genero"]);
                this.textBox_cpf.Text = TratarCampoVazio(reader["CPF"]);
                this.textBox_rg.Text = TratarCampoVazio(reader["RG"]);
                this.textBox_estadocivil.Text = TratarCampoVazio(reader["EstadoCivil"]);
                this.textBox_pai.Text = TratarCampoVazio(reader["Pai"]);
                this.textBox_mae.Text = TratarCampoVazio(reader["Mae"]);
                this.textBox_telefone.Text = TratarCampoVazio(reader["Telefone"]);
                this.textBox_endereco.Text = TratarCampoVazio(reader["Endereco"]);
                this.textBox_cidade.Text = TratarCampoVazio(reader["Cidade"]);
                this.textBox_estado.Text = TratarCampoVazio(reader["Estado"]);
                this.textBox_salario.Text = TratarCampoVazio(reader["Salario"]);
            }
            con.Close();
        }


        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.cadastroF.Show();
            this.Close();
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            if (idAtualizacao == 0)
            {
                Criar(); 
            }
            else
            {
                Atualizar(); 
            }
        }

        private void Criar()
        {
            OleDbConnection con = new OleDbConnection(Globals.ConnString);
            OleDbCommand cmd = con.CreateCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO CadastroFuncionario  " +
                    " (Nome, Genero, CPF, RG, EstadoCivil, Pai, Mae, Telefone, Endereco, Cidade, Estado, Salario)" +
                    "VALUES " +
                    " (@Nome, @Genero, @CPF, @RG, @EstadoCivil, @Pai, @Mae, @Telefone, @Endereco, @Cidade, @Estado, @Salario);";

            cmd.Parameters.AddWithValue("Nome", this.textBox_nome.Text);
            cmd.Parameters.AddWithValue("Genero", this.textBox_genero.Text);
            cmd.Parameters.AddWithValue("CPF", this.textBox_cpf.Text);
            cmd.Parameters.AddWithValue("RG", this.textBox_rg.Text);
            cmd.Parameters.AddWithValue("EstadoCivil", this.textBox_estadocivil.Text);
            cmd.Parameters.AddWithValue("Pai", this.textBox_pai.Text);
            cmd.Parameters.AddWithValue("Mae", this.textBox_mae.Text);
            cmd.Parameters.AddWithValue("Telefone", this.textBox_telefone.Text);
            cmd.Parameters.AddWithValue("Endereco", this.textBox_endereco.Text);
            cmd.Parameters.AddWithValue("Cidade", this.textBox_cidade.Text);
            cmd.Parameters.AddWithValue("Estado", this.textBox_estado.Text);
            cmd.Parameters.AddWithValue("Salario", this.textBox_salario.Text);

            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Dados inseridos com sucesso");

            this.cadastroF.Show();
            this.Close();
        }

        private void Atualizar()
        {
            OleDbConnection con = new OleDbConnection(Globals.ConnString);
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE CadastroFuncionario SET" +
                    " Nome = @Nome, Genero = @Genero, CPF = @CPF, RG = @RG, EstadoCivil = @EstadoCivil, " +
                    "Pai = @Pai, Mae = @Mae, Telefone = @Telefone, " +
                    "Endereco = @Endereco, Cidade = @Cidade, Estado = @Estado, Salario = @Salario " +
                    " WHERE Id = " + idAtualizacao;

            cmd.Parameters.AddWithValue("Nome", this.textBox_nome.Text);
            cmd.Parameters.AddWithValue("Genero", this.textBox_genero.Text);
            cmd.Parameters.AddWithValue("CPF", this.textBox_cpf.Text);
            cmd.Parameters.AddWithValue("RG", this.textBox_rg.Text);
            cmd.Parameters.AddWithValue("EstadoCivil", this.textBox_estadocivil.Text);
            cmd.Parameters.AddWithValue("Pai", this.textBox_pai.Text);
            cmd.Parameters.AddWithValue("Mae", this.textBox_mae.Text);
            cmd.Parameters.AddWithValue("Telefone", this.textBox_telefone.Text);
            cmd.Parameters.AddWithValue("Endereco", this.textBox_endereco.Text);
            cmd.Parameters.AddWithValue("Cidade", this.textBox_cidade.Text);
            cmd.Parameters.AddWithValue("Estado", this.textBox_estado.Text);
            cmd.Parameters.AddWithValue("Salario", this.textBox_salario.Text);

            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Dados atualizados com sucesso");

            this.cadastroF.Show();
            this.Close();
        }


    }
}
 
