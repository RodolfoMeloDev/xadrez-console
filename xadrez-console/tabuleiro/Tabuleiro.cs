using System;

namespace xadrez_console.tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        public Peca Peca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public Peca Peca(Posicao pos)
        {
            try
            {
                return Pecas[pos.Linha, pos.Coluna];
            }catch (IndexOutOfRangeException)
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }

        public void ColocarPeca(Peca peca, Posicao pos)
        {
            if (ExistePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nesta posição!");
            }

            Pecas[pos.Linha, pos.Coluna] = peca;
            peca.Posicao = pos;
        }

        public Peca RetirarPeca(Posicao pos)
        {
            if (Peca(pos) == null)
            {
                return null;
            }

            Peca aux = Peca(pos);
            aux.Posicao = null;
            Pecas[pos.Linha, pos.Coluna] = null;
            return aux;
        }

        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
            {
                return false;
            }
            return true;
        }

        private void ValidarPosicao(Posicao pos) { 
            if (!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }

        private bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return Peca(pos) != null;
        }

    }
}
