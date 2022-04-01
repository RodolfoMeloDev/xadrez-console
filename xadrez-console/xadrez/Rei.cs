using xadrez_console.tabuleiro;
using xadrez_console.tabuleiro.Enums;

namespace xadrez_console.xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) 
            : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
