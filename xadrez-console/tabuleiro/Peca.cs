using xadrez_console.tabuleiro.Enums;

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

        public void DecrementarQtdeMovimento()
        {
            QtdeMovimentos--;
        }

        public bool ValidarSeExisteMovimentosPossiveis()
        {
            bool[,] mat = PegarMovimentosPossiveis();

            for (int i = 0; i < Tab.Linhas; i++)
            {
                for (int j = 0; j < Tab.Colunas; j++)
                {
                    if(mat[i, j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool PermiteMoverPara(Posicao pos)
        {
            return PegarMovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool [,] PegarMovimentosPossiveis();
    }
}
