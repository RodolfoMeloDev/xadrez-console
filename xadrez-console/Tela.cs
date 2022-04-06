using System;
using System.Collections.Generic;
using xadrez_console.tabuleiro;
using xadrez_console.tabuleiro.Enums;
using xadrez_console.xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {                    
                    ImprimirPeca(tab.Peca(i, j));                    
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool [,] possicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if(possicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPeca(tab.Peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        private static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();

            if (s.Length == 0)
            {
                throw new TabuleiroException("Não foi informada a posição da jogada a ser realizada!");
            }

            if (s.Length == 1 || s.Length >= 3)
            {
                throw new TabuleiroException("A posição informada é inválida!");
            }

            char coluna = s[0];
            int linha = int.Parse(s[1] + "");

            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirTabuleiro(partida.Tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine("Turno: " + partida.Turno);
            
            if (!partida.Terminada)
            {
                Console.WriteLine("Jogador Atual: " + partida.JogadorAtual);
                if (partida.Xeque)
                {
                    Console.WriteLine();
                    Console.WriteLine("XEQUE !");
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + partida.JogadorAtual);
            }
        }

        private static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças Capturadas:");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.RetornarPecasCapturadas(Cor.Branca));
            Console.WriteLine();

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Pretas: ");
            ImprimirConjunto(partida.RetornarPecasCapturadas(Cor.Preto));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        private static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (var x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
    }
}
