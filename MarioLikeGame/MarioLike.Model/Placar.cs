using System;
using System.Collections.Generic;
using System.Text;

namespace MarioLike.Model
{
    public class Placar
    {
        private int idJogador;
        private string nomeJogador;
        private int score;
        private DateTime dataScore;
        private string tempo;

        public Placar()
        {

        }

        public Placar(int idJogador, string jogador, int score, DateTime data, string tempo)
        {
            IdJogador = idJogador;
            Jogador = jogador;
            Score = score;
            Data = data;
            Tempo = tempo;
        }

        public int IdJogador { get => idJogador; set => idJogador = value; }
        public string Jogador { get => nomeJogador; set => nomeJogador = value; }
        public int Score { get => score; set => score = value; }
        public DateTime Data { get => dataScore; set => dataScore = value; }
        public string Tempo { get => tempo; set => tempo = value; }
    }
}
