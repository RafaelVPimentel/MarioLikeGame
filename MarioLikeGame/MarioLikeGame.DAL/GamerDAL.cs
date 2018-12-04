using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using MarioLike.Model;

namespace MarioLikeGame.DAL
{
    public class GamerDAL
    {
        private SqlConnection conexao;

        public string MensagemErro { get; set; }

        public GamerDAL()
        {
            LeitorConfiguracao leitor = new LeitorConfiguracao();

            conexao = new SqlConnection();
            conexao.ConnectionString = leitor.LerConexao();
        }

        public bool Inserir(Placar placar)
        {
            bool resultado = false;
            MensagemErro = "";

            //Declarar comando SQL
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "INSERT INTO JOGADOR(Nome_Jogador,Score_Jogador,Data_Score_Jogador,Tempo_Jogador)" +
                "VALUES (@Nome,@Score,@Data,@Tempo);";

            //Criar os parâmetros
            comando.Parameters.AddWithValue("@Nome", placar.Jogador);
            comando.Parameters.AddWithValue("@Score", placar.Score);
            comando.Parameters.AddWithValue("@Data", placar.Data);
            comando.Parameters.AddWithValue("@Tempo", placar.Tempo);

            //Executar o comando
            try
            {
                //Abrir a conexão
                conexao.Open();
                //Executar o comando
                comando.ExecuteNonQuery();
                //Se chegou até aqui, então funcionou! :)
                resultado = true;
            }
            catch (Exception ex)
            {
                //Se entrou aqui, então deu pau!
                MensagemErro = ex.Message;
            }
            finally
            {
                //Finalizar fechando a conexão
                conexao.Close();
            }
            return resultado;
        }

        public List<Placar> Listar()
        {
            //instanciar a lista
            List<Placar> resultado = new List<Placar>();

            //declarar o comando
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "SELECT TOP 10 Id_Jogador, Nome_Jogador, Score_Jogador, Data_Score_Jogador , Tempo_Jogador " +
                "FROM JOGADOR ORDER BY Score_Jogador DESC,Tempo_Jogador,Data_Score_Jogador";

            //executar o comando
            try
            {
                //abrir a conexão
                conexao.Open();

                //executar o comando e receber o resultado
                SqlDataReader leitor = comando.ExecuteReader();

                //verificar se encontrou algo
                while (leitor.Read() == true)
                {
                    //instanciar o objeto
                    Placar placar = new Placar();
                    placar.IdJogador = Convert.ToInt32(leitor["Id_Jogador"]);
                    placar.Jogador = leitor["Nome_Jogador"].ToString();
                    placar.Score = Convert.ToInt32(leitor["Score_Jogador"]);
                    placar.Data = Convert.ToDateTime(leitor["Data_Score_Jogador"]);
                    placar.Tempo = leitor["Tempo_Jogador"].ToString();

                    //adicionar na lista 
                    resultado.Add(placar);
                }

                //fechar leitor 
                leitor.Close();
            }
            catch (Exception ex)
            {
                string mensagem = ex.Message;

            }
            finally
            {
                //finalizar fechando a conexão
                conexao.Close();
            }
            return resultado;
        }
    }
    //public class GamerDAL
    //{
    //    private SqlConnection conexao;

    //    public string MensagemErro { get; set; }

    //    public GamerDAL()
    //    {
    //        LeitorConfiguracao leitor = new LeitorConfiguracao();

    //        conexao = new SqlConnection();
    //        conexao.ConnectionString = leitor.LerConexao();
    //    }

    //    public bool Inserir(Placar placar)
    //    {
    //        bool resultado = false;
    //        MensagemErro = "";

    //        //Declarar comando SQL
    //        SqlCommand comando = new SqlCommand();
    //        comando.Connection = conexao;
    //        comando.CommandText = "INSERT INTO JOGADOR(Nome_Jogador,Score_Jogador,Data_Score_Jogador,Tempo_Jogador)" +
    //            "VALUES (@Nome,@Score,@Data,@Tempo);";

    //        //Criar os parâmetros

    //        comando.Parameters.AddWithValue("@Nome", placar.Jogador);
    //        comando.Parameters.AddWithValue("@Score", placar.Score);
    //        comando.Parameters.AddWithValue("@Data", placar.Data);
    //        comando.Parameters.AddWithValue("@Tempo", placar.Tempo);

    //        //Executar o comando
    //        try
    //        {
    //            //Abrir a conexão
    //            conexao.Open();
    //            //Executar o comando
    //            comando.ExecuteNonQuery();
    //            //Se chegou até aqui, então funcionou! :)
    //            resultado = true;
    //        }
    //        catch (Exception ex)
    //        {
    //            //Se entrou aqui, então deu pau!
    //            MensagemErro = ex.Message;
    //        }
    //        finally
    //        {
    //            //Finalizar fechando a conexão
    //            conexao.Close();
    //        }
    //        return resultado;
    //    }

    //    public List<Placar> Listar()
    //    {
    //        //instanciar a lista
    //        List<Placar> resultado = new List<Placar>();

    //        //declarar o comando
    //        SqlCommand comando = new SqlCommand();
    //        comando.Connection = conexao;
    //        comando.CommandText = "SELECT TOP 10 Id_Jogador, Nome_Jogador, Score_Jogador, Data_Score_Jogador , Tempo_Jogador " +
    //            "FROM JOGADOR ORDER BY Score_Jogador DESC,Tempo_Jogador, Data_Score_Jogador;";

    //        //executar o comando
    //        try
    //        {
    //            //abrir a conexão
    //            conexao.Open();

    //            //executar o comando e receber o resultado
    //            SqlDataReader leitor = comando.ExecuteReader();

    //            //verificar se encontrou algo
    //            while (leitor.Read() == true)
    //            {
    //                //instanciar o objeto
    //                Placar placar = new Placar();
    //                placar.IdJogador = Convert.ToInt32(leitor["Id_Jogador"]);
    //                placar.Jogador = leitor["Nome_Jogador"].ToString();
    //                placar.Score = Convert.ToInt32(leitor["Score_Jogador"]);
    //                placar.Data = Convert.ToDateTime(leitor["Data_Score_Jogador"]);
    //                placar.Tempo = leitor["Tempo_Jogador"].ToString();

    //                //adicionar na lista 
    //                resultado.Add(placar);
    //            }

    //            //fechar leitor 
    //            leitor.Close();
    //        }
    //        catch (Exception ex)
    //        {
    //            string mensagem = ex.Message;

    //        }
    //        finally
    //        {
    //            //finalizar fechando a conexão
    //            conexao.Close();
    //        }
    //        return resultado;
    //    }
    //}
}
