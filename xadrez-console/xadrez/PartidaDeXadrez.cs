using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_console.tabuleiro;
using xadrez_console.tabuleiro.Enums;

namespace xadrez_console.xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public bool Terminada { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Terminada = false;
            Turno = 1;
            JogadorAtual = Cor.Branca;

            ColocarPecas();
        }

        private void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrimentarQtdeMovimento();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);                        
        }

        public void RelizarJogada(Posicao origem, Posicao destino)
        {
            ExecutarMovimento(origem, destino);
            Turno++;
            MudarJogador();
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem da escolhida!");
            }

            if (JogadorAtual != Tab.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }

            if (!Tab.Peca(pos).ValidarSeExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidaPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).PermiteMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void MudarJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preto;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        private void ColocarPecas()
        {
            Tab.ColocarPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('a', 1).ToPosicao());
            Tab.ColocarPeca(new Cavalo(Tab, Cor.Branca), new PosicaoXadrez('b', 1).ToPosicao());
            Tab.ColocarPeca(new Bispo(Tab, Cor.Branca), new PosicaoXadrez('c', 1).ToPosicao());
            Tab.ColocarPeca(new Rei(Tab, Cor.Branca), new PosicaoXadrez('d', 1).ToPosicao());
            Tab.ColocarPeca(new Dama(Tab, Cor.Branca), new PosicaoXadrez('e', 1).ToPosicao());
            Tab.ColocarPeca(new Bispo(Tab, Cor.Branca), new PosicaoXadrez('f', 1).ToPosicao());
            Tab.ColocarPeca(new Cavalo(Tab, Cor.Branca), new PosicaoXadrez('g', 1).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('h', 1).ToPosicao());

            Tab.ColocarPeca(new Peao(Tab, Cor.Branca), new PosicaoXadrez('a', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Tab, Cor.Branca), new PosicaoXadrez('b', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Tab, Cor.Branca), new PosicaoXadrez('c', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Tab, Cor.Branca), new PosicaoXadrez('d', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Tab, Cor.Branca), new PosicaoXadrez('e', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Tab, Cor.Branca), new PosicaoXadrez('f', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Tab, Cor.Branca), new PosicaoXadrez('g', 2).ToPosicao());
            Tab.ColocarPeca(new Peao(Tab, Cor.Branca), new PosicaoXadrez('h', 2).ToPosicao());

            Tab.ColocarPeca(new Peao(Tab, Cor.Preto), new PosicaoXadrez('a', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Tab, Cor.Preto), new PosicaoXadrez('b', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Tab, Cor.Preto), new PosicaoXadrez('c', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Tab, Cor.Preto), new PosicaoXadrez('d', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Tab, Cor.Preto), new PosicaoXadrez('e', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Tab, Cor.Preto), new PosicaoXadrez('f', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Tab, Cor.Preto), new PosicaoXadrez('g', 7).ToPosicao());
            Tab.ColocarPeca(new Peao(Tab, Cor.Preto), new PosicaoXadrez('h', 7).ToPosicao());

            Tab.ColocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('a', 8).ToPosicao());
            Tab.ColocarPeca(new Cavalo(Tab, Cor.Preto), new PosicaoXadrez('b', 8).ToPosicao());
            Tab.ColocarPeca(new Bispo(Tab, Cor.Preto), new PosicaoXadrez('c', 8).ToPosicao());
            Tab.ColocarPeca(new Rei(Tab, Cor.Preto), new PosicaoXadrez('d', 8).ToPosicao());
            Tab.ColocarPeca(new Dama(Tab, Cor.Preto), new PosicaoXadrez('e', 8).ToPosicao());
            Tab.ColocarPeca(new Bispo(Tab, Cor.Preto), new PosicaoXadrez('f', 8).ToPosicao());
            Tab.ColocarPeca(new Cavalo(Tab, Cor.Preto), new PosicaoXadrez('g', 8).ToPosicao());
            Tab.ColocarPeca(new Torre(Tab, Cor.Preto), new PosicaoXadrez('h', 8).ToPosicao());
        }
    }
}
