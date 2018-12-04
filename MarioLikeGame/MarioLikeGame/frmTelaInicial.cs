using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarioLikeGame.DAL;

namespace MarioLikeGame
{
    public partial class frmTelaInicial : Form
    {

        public frmTelaInicial()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            txtNome.MaxLength = 15;

        }

        private void PreencheGrid()
        {
            //Declarar a DAL
            GamerDAL gamerDAL;

            //Instanciando a DAL na construção do formulário
            gamerDAL = new GamerDAL();

            //Limpando o DataSource
            dgvListaRecorde.DataSource = null;

            //Listando a DAL
            dgvListaRecorde.DataSource = gamerDAL.Listar();

            //Removendo uma coluna
            dgvListaRecorde.Columns.Remove("IdJogador");

        }

        private void pbMario2_Click(object sender, EventArgs e)
        {

        }

        private void lblNomeJogo_Click(object sender, EventArgs e)
        {

        }

        private void EstilizarGrid()
        {

            //Fonte e tamanho
            this.dgvListaRecorde.DefaultCellStyle.Font = new Font("Swis721 BlkCn BT", 30);
            //Cor letra
            this.dgvListaRecorde.DefaultCellStyle.ForeColor = Color.Goldenrod;
            //Cor Fundo
            this.dgvListaRecorde.DefaultCellStyle.BackColor = Color.DarkBlue;

            dgvListaRecorde.AutoSize = true;
            //Cor Fundo Selecionado
            dgvListaRecorde.DefaultCellStyle.SelectionBackColor = Color.OrangeRed;
            //Alinhamento
            dgvListaRecorde.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //Fonte e tamanho coluna
            dgvListaRecorde.ColumnHeadersDefaultCellStyle.Font = new Font("Swis721 BlkCn BT", 30);
            //Alinhamento coluna
            dgvListaRecorde.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //cursor not
            dgvListaRecorde.EnableHeadersVisualStyles = false;


            for (int i = 0; i < dgvListaRecorde.Columns.Count; i++)
            {
                dgvListaRecorde.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }


        }




        private void frmTelaInicial_Load(object sender, EventArgs e)
        {
            PreencheGrid();
            txtNome.Focus();          
            
            EstilizarGrid();
            
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Digite um nome!","MarioLike - Game");
            }

            else
            {
                this.Visible = false;

                var frm = new frmTelaJogo();

                frm.nomeGamer = txtNome.Text;

                frm.ShowDialog();

                PreencheGrid();
            }
            

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlListaRecord_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvListaRecorde_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblJogador_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
