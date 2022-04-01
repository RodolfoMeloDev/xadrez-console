using System;
using xadrez_console.tabuleiro;
using xadrez_console;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {           
            Tabuleiro tab = new Tabuleiro(8, 8);

            Tela.ImprimirTabuleiro(tab);
        }
    }
}
