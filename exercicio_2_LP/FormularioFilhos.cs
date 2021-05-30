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
    public partial class FormularioFilhos : Form
    {
        private Form ListaFilhos;
        private int idAtualizacao;
        private int idFuncionario;

        private string TratarCampoVazio(object valor)
        {
            if (valor is System.DBNull)
            {
                return "";
            }

            return (string)valor;
        }

        public FormularioFilhos(ListaFilhos form_cadastro, int idAtualizacao, int idFuncionario)
        {
            this.ListaFilhos = form_cadastro;
            this.idAtualizacao = idAtualizacao;
            this.idFuncionario = idFuncionario; 
            InitializeComponent();

            if (idAtualizacao == 0)
            {
                this.Text = "Criar novo filho";
            }
            else
            {
                PreencherDados();
                this.Text = "Editando filhos " + idAtualizacao;
            }
        }

        private void PreencherDados()
        {
            OleDbConnection con = new OleDbConnection(Globals.ConnString);
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "Select * from Filhos where ID = " + idAtualizacao;

            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                this.textBox_nome.Text = TratarCampoVazio(reader["Nome"]);


            }
            con.Close();
        }


        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.ListaFilhos.Show();
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
            cmd.CommandText = "INSERT INTO Filhos " +
                    " (Nome, idFuncionario)" +
                    "VALUES " +
                    " (@Nome, @idFuncionario);";

            cmd.Parameters.AddWithValue("Nome", this.textBox_nome.Text);
            cmd.Parameters.AddWithValue("idFuncionario", this.idFuncionario);

            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Dados inseridos com sucesso");

            this.ListaFilhos.Show();
            this.Close();
        }

        private void Atualizar()
        {
            OleDbConnection con = new OleDbConnection(Globals.ConnString);
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Filhos SET" +
                    " Nome = @Nome " +
                    " WHERE Id = " + idAtualizacao;

            cmd.Parameters.AddWithValue("Nome", this.textBox_nome.Text);
           

            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Dados atualizados com sucesso");

            this.ListaFilhos.Show();
            this.Close();
        }


    }
}
 
