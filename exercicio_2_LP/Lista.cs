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
    public partial class Lista : Form
    {
        public Lista()
        {
            InitializeComponent();
        }

        private void criar_Click(object sender, EventArgs e)
        {
            var cadastroF = new FormularioFuncionario(this, 0);
            cadastroF.Show();
            this.Hide();
        }

        private void PopularDataGrid(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(Globals.ConnString);
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "Select * from CadastroFuncionario";
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            //adaptar dados pro DatatGrid 
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            DataTable funcionarios = new DataTable();
            adapter.Fill(funcionarios);
            dataGridView1.DataSource = funcionarios;
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

                var formulario = new FormularioFuncionario(this, id);
                formulario.Show();
                this.Hide();
            }
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
            cmd.CommandText = "DELETE from CadastroFuncionario WHERE id = " + id;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            int rowAffected = cmd.ExecuteNonQuery();
            if (rowAffected == 0)
            {
                MessageBox.Show("Nenhuma linha encontrada.");
            }
            else
            {
                MessageBox.Show("Dados excluídos com sucesso");
            }

            PopularDataGrid(null, null);
            con.Close();

        }

        private void btn_gerenciarFilhos_Click(object sender, EventArgs e)
        {
            var selectedCells = this.dataGridView1.SelectedCells;
            if (selectedCells.Count == 0)
            {
                return;
            }
            var selectedRowIndex = selectedCells[0].RowIndex;
            var rowData = this.dataGridView1.Rows[selectedRowIndex];
            var id = (int)rowData.Cells[0].Value;

            var formulario = new ListaFilhos (this, id);
            formulario.Show();
            this.Hide();
        }
    }
}
