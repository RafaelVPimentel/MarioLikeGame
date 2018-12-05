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
using MarioLike.Model;
using MarioLikeGame;
using System.Configuration;
using System.Data.SqlClient;

namespace MarioLikeGame
{
    public partial class frmTelaJogo : Form
    {
        
        
        //Declarando a DAL
        GamerDAL gamerDAL;
        

        //Criar um atributo para pegar o nome do jogador
        public string nomeGamer { get; set; }


        //Atributos para controle da movimentação do personagem
        private bool paraCima;
        private bool paraBaixo;
        private bool paraDireita;
        private bool paraEsquerda;

        //Variavel de condição de vitoria/derrota
        private bool vitoria = false;

        //Variavel de pontos
        private int pontos = 0;

        //Variavel de tempo
        private int segundos = 0;
        private int minutos = 0;

        //Atributo responsável pela velocidade de locomoção do personagem
        private int velocidade = 10;

        //Biblioteca do windows media player
        //WMPLib.WindowsMediaPlayer Tocador = new WMPLib.WindowsMediaPlayer();
        List<System.Windows.Media.MediaPlayer> sounds = new List<System.Windows.Media.MediaPlayer>();

        public frmTelaJogo()
        {

            InitializeComponent();

            pcbGameOver1.Visible = false;
            pcbVitoria.Visible = false;
            personagem.Width = 94;
            personagem.Height = 84;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void frmTelaJogo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmTelaJogo_Load(object sender, EventArgs e)
        {
            //Audio("Musica 1.mp3", "Play");
            playSound("Musica 1.mp3");

        }

        //Movimentar o personagem quando pressiono a tecla
        private void KeyisDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                paraEsquerda = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                paraDireita = true;
            }

            if (e.KeyCode == Keys.Up)
            {
                paraCima = true;
            }

            if (e.KeyCode == Keys.Down)
            {
                paraBaixo = true;
            }
        }

        //Parar o movimento do personagem quando soltar a tecla
        private void KeyisUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //Setas
                case Keys.Left:
                    paraEsquerda = false;
                    break;
                case Keys.Right:
                    paraDireita = false;
                    break;
                case Keys.Up:
                    paraCima = false;
                    break;
                case Keys.Down:
                    paraBaixo = false;
                    break;

                ////WASD
                //case Keys.A:
                //    paraEsquerda = false;
                //    break;
                //case Keys.D:
                //    paraDireita = false;
                //    break;
                //case Keys.W:
                //    paraCima = false;
                //    break;
                //case Keys.S:
                //    paraBaixo = false;
                //    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //pcbGameOverl.Visible = false;
            //pcbVitoria.Visible = false;
            lblPontos.Text = "Pontos: " + pontos;
            timer2.Enabled = true;

            if (paraEsquerda)
            {
                //Movimenta o personagem para esquerda
                personagem.Left -= velocidade;
            }

            if (paraDireita)
            {
                //Movimenta o personagem para Direita
                personagem.Left += velocidade;
            }

            //Movimenta o personagem para cinma
            if (paraCima)
            {
                personagem.Top -= velocidade;
            }

            //Movimenta o personagem para baixo
            if (paraBaixo)
            {
                personagem.Top += velocidade;
            }

            //Posicionamento do personagem dentro da área do formulário (tela)
            if (personagem.Left < 0)
            {
                personagem.Left = 0;
            }

            if (personagem.Left > 1800)
            {
                personagem.Left = 1800;
            }

            if (personagem.Top < 142)
            {
                personagem.Top = 142;
            }

            if (personagem.Top > 970)
            {
                personagem.Top = 970;
            }


            //Loop para checar todos os controles inseridos no form
            foreach (Control item in this.Controls)
            {

                //Verifica se o jogador colidiu com o inimigo, caso positivo GameOver
                if (item is PictureBox && (string)item.Tag == "inimigo")
                {

                    //Checar colisão com as PictureBox
                    if (((PictureBox)item).Bounds.IntersectsWith(personagem.Bounds))
                    {
                        stopSound();
                        playSound("smb_gameover.wav");
                        vitoria = false; 
                            this.Controls.Remove(item);
                            GameOver(vitoria);

                        GravaHiScore();
                    }
                }

                if (item is PictureBox && (string)item.Tag == "coletaveis")
                {
                    //Checar colisão com as PictureBox
                    if (((PictureBox)item).Bounds.IntersectsWith(personagem.Bounds))
                    {
                        this.Controls.Remove(item);
                        pontos++;
                        playSound("smb_coin.wav");
                        if (pontos >= 20)
                        {

                            stopSound();
                            playSound("Vitoria1.mp3");
                            //Audio("Vitoria1.mp3", "Play");
                            vitoria = true;


                                GameOver(vitoria);
                            GravaHiScore();
                        }

                    }
                }

                if (item is PictureBox && (string)item.Tag == "coletaveisAumento")
                {
                    //Checa colisão com as PictureBox
                    if (((PictureBox)item).Bounds.IntersectsWith(personagem.Bounds))
                    {
                        //toca a música
                        //Audio("smb_coin.wav", "Play");
                        playSound("smb_powerup.wav");
                        //Remove o item coletável
                        this.Controls.Remove(item);

                        personagem.Height += 4;
                        personagem.Width += 4;

                        velocidade-=1;

                        //Incrementar a variável pontos
                        pontos += 2;
                    }
                }

            }
            
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void GameOver(bool ganhou)
        {
            lblPontos.Text = "Pontos: " + pontos;
            personagem.Visible = false;
            btnRestart.Visible = true;
            btnRestart.Focus();
            if (ganhou)
            {
                RemovePictureBox();
                pcbVitoria.Visible = true;
            }
            else
            {
                RemovePictureBox();
                

                pcbGameOver1.Visible = true;
            }
            timer1.Stop();
            timer2.Stop();
            
        }

        private void GravaHiScore()
        {
            gamerDAL = new GamerDAL();

            Placar placar = new Placar();

            var frm = new frmTelaInicial();                     

            if (!this.nomeGamer.Equals(""))
            {
                placar.Jogador = this.nomeGamer;

            }
            else
            {
                placar.Jogador = "Insira seu nick";
            }

            placar.Score = pontos;
            placar.Data = DateTime.Now;
            placar.Tempo = minutos.ToString("00") + ":" + segundos.ToString("00");

            if (!gamerDAL.Inserir(placar))
            {
                MessageBox.Show("Erro ao inserir os dados: \r\r\r\n" +
                    gamerDAL.MensagemErro, "Mario Like Game - Cadastro");
            }

        }

        private void RemovePictureBox()
        {
            foreach (Control item in this.Controls)
            {

                //Verifica se o jogador colidiu com o inimigo, caso positivo GameOver
                if (item is PictureBox && (string)item.Tag != "GameOver")
                {
                    ((PictureBox)item).Image = null;
                   
                }

            }
        }
        private void btnRestart_Click(object sender, EventArgs e)
        {
            stopSound();
            this.Hide();
            frmTelaInicial frmTelaInicial = new frmTelaInicial();
            frmTelaInicial.Closed += (s, args) => this.Close();
            frmTelaInicial.Show();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            segundos++;
            if (segundos == 60)
            {
                    segundos = 0;
                    minutos++;
            }
            lblTempo.Text = "Tempo: " + minutos.ToString("00") + ":" + segundos.ToString("00");
        }

        //private void Audio(string caminho, string estadoMP)
        //{
        //    //Verifica se ocorreu erros ao instanciar o windows media player
        //    Tocador.MediaError += new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Tocador_MediaError);

        //    Tocador.URL = caminho;
        //    if (estadoMP.Equals("Play"))
        //    {
        //        Tocador.controls.play();
        //    }
        //    else if (Tocador.Equals("Stop"))
        //    {
        //        Tocador.controls.stop();
        //    }

        //}

        //private void Tocador_MediaError(object pMediaObject)
        //{
        //    MessageBox.Show("Não é possivel executar o arquivo de midia");
        //    this.Close();
        //}

        private void playSound(string nome)
        {
            string url = Application.StartupPath + @"/" + nome;
            var sound = new System.Windows.Media.MediaPlayer();
            sound.Open(new Uri(url));
            sound.Play();
            sounds.Add(sound);

        }

        private void stopSound()
        {
            for (int i = sounds.Count - 1; i >= 0 ; i--)
            {
                sounds[i].Stop();
                sounds.RemoveAt(i);

            }
        }

        private void lblPontos_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }

        private void lblTempo_Click(object sender, EventArgs e)
        {

        }

        private void lblTempo_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void pcbGameOverl_Click(object sender, EventArgs e)
        {

        }

        private void pcbVitoria_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
