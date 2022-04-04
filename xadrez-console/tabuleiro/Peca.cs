﻿using xadrez_console.tabuleiro.Enums;

namespace xadrez_console.tabuleiro
{
    abstract  class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdeMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            Posicao = null;
            Tab = tab;
            Cor = cor;
            QtdeMovimentos = 0;
        }

        public void IncrimentarQtdeMovimento()
        {
            QtdeMovimentos++;
        }

        public abstract bool [,] PegarMovimentosPossiveis();
    }
}
