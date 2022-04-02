using System;
using xadrez_console.tabuleiro;
using xadrez_console;
using xadrez_console.xadrez;
using xadrez_console.tabuleiro.Enums;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida= new PartidaDeXadrez();

                while (!partida.Terminada) {
                    
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tab);                    

                    Console.WriteLine();
                    Console.WriteLine("Turno: " + partida.Turno);
                    Console.WriteLine("Jogador Atual: " + partida.JogadorAtual);
                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                    partida.ExecutarMovimento(origem, destino);
                }

            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
