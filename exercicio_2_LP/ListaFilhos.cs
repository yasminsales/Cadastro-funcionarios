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
    public partial class ListaFilhos : Form
    {
        private int idFuncionario;
        private Form listaFuncionarios;

        public ListaFilhos(Form listaFuncionarios, int idFuncionario)
        {
            this.idFuncionario = idFuncionario;
            this.listaFuncionarios = listaFuncionarios;
            InitializeComponent();
        }

        private void criar_Click(object sender, EventArgs e)
        {
            var cadastroF = new FormularioFilhos(this, 0, idFuncionario);
            cadastroF.Show();
            this.Hide();
        }

        private void PopularDataGrid(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(Globals.ConnString);
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "Select * from Filhos where idFuncionario = " + idFuncionario;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            //adaptar dados pro DatatGrid 
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            DataTable filhos = new DataTable();
            adapter.Fill(filhos);
            dataGridView1.DataSource = filhos;
            con.Close();
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            {
                // Pegar célula selecionada do Datagrid 
                var selectedCells = this.dataGridView1.SelectedCells;
                if (selectedCells.Count == 0)
                {
                    return;
                }
                var selectedRowIndex = selectedCells[0].RowIndex;
                var rowData = this.dataGridView1.Rows[selectedRowIndex];
                var id = (int)rowData.Cells[0].Value;

                var formulario = new FormularioFilhos(this, id, idFuncionario);
                formulario.Show();
                this.Hide();
            }
        }

        private void btn_gerenciarFilhos_Click(object sender, EventArgs e)
        {
            // Pegar célula selecionada do Datagrid 
            var selectedCells = this.dataGridView1.SelectedCells;
            if (selectedCells.Count == 0)
            {
                return;
            }
            var selectedRowIndex = selectedCells[0].RowIndex;
            var rowData = this.dataGridView1.Rows[selectedRowIndex];
            var id = (int)rowData.Cells[0].Value;

            var formulario = new ListaFilhos(this, id);
            formulario.Show();
            this.Hide();
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(Globals.ConnString);
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            var selectedCells = this.dataGridView1.SelectedCells;
            if (selectedCells.Count == 0)
            {
                return;
            }

            var selectedRowIndex = selectedCells[0].RowIndex;
            var rowData = this.dataGridView1.Rows[selectedRowIndex];
            var id = (int)rowData.Cells[0].Value;
            cmd.CommandText = "DELETE from Filhos WHERE id = " + id;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            int rowAffected = cmd.ExecuteNonQuery();
            if (rowAffected == 0)
            {
                MessageBox.Show("Nenhuma linha encontrada.");
            }
            else
            {
                MessageBox.Show("Dados excluidos com sucesso");
            }

            PopularDataGrid(null, null);
            con.Close();

        }

        private void btn_voltar_Click(object sender, EventArgs e)
        {
            this.listaFuncionarios.Show();
            this.Close();
        }
    }
}
