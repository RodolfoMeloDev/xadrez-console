using xadrez_console.tabuleiro;
using xadrez_console.tabuleiro.Enums;

namespace xadrez_console.xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool PodeMoverFrente(Posicao pos)
        {
            Peca p = Tab.Peca(pos);

            return p == null;
        }

        private bool PodeMoverAdjacente(Posicao pos)
        {
            Peca p = Tab.Peca(pos);

            return p != null && p.Cor != Cor;
        }

        public override bool[,] PegarMovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            // frente 
            if (QtdeMovimentos == 0)
            {
                // 2 casas brancas
                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && PodeMoverFrente(pos) && Cor == Cor.Branca)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // 2 casas pretas
                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && PodeMoverFrente(pos) && Cor == Cor.Preto)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
            }
            
            // 1 casa branca
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && PodeMoverFrente(pos) && Cor == Cor.Branca)
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // 1 casa preta
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && PodeMoverFrente(pos) && Cor == Cor.Preto)
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // direita branca
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna +1);
            if (Tab.PosicaoValida(pos) && PodeMoverAdjacente(pos) && Cor == Cor.Branca)
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // direita preta
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMoverAdjacente(pos) && Cor == Cor.Preto)
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // esquerda branca
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMoverAdjacente(pos) && Cor == Cor.Branca)
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // esquerda preta
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMoverAdjacente(pos) && Cor == Cor.Preto)
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            return mat;
        }
    }
}
