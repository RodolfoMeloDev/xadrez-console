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
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.ColocarPeca(new Rei(tab, Cor.Preto), new Posicao(0, 2));
                tab.ColocarPeca(new Torre(tab, Cor.Preto), new Posicao(1, 5));
                tab.ColocarPeca(new Torre(tab, Cor.Preto), new Posicao(5, 6));

                tab.ColocarPeca(new Peao(tab, Cor.Branca), new Posicao(6, 3));
                tab.ColocarPeca(new Peao(tab, Cor.Branca), new Posicao(6, 2));
                tab.ColocarPeca(new Dama(tab, Cor.Branca), new Posicao(7, 4));

                Tela.ImprimirTabuleiro(tab);

                Console.ReadLine();

            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
