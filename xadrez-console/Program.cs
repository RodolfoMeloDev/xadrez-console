﻿using System;
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
            Tabuleiro tab = new Tabuleiro(8, 8);

            tab.ColocarPeca(new Rei(tab, Cor.Preto), new Posicao(0, 2));

            tab.ColocarPeca(new Torre(tab, Cor.Preto), new Posicao(1, 5));

            Tela.ImprimirTabuleiro(tab);

        }
    }
}