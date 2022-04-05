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
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Terminada = false;
            Turno = 1;
            JogadorAtual = Cor.Branca;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        private void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrimentarQtdeMovimento();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);     
            
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
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

        public HashSet<Peca> RetornarPecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var x in capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }

            return aux;
        }

        public HashSet<Peca> RetornarPecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var x in pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }

            aux.ExceptWith(RetornarPecasCapturadas(cor));

            return aux;
        }

        private void ColocarnovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            // brancas
            ColocarnovaPeca('a', 1, new Torre(Tab, Cor.Branca));
            ColocarnovaPeca('b', 1, new Cavalo(Tab, Cor.Branca));
            ColocarnovaPeca('c', 1, new Bispo(Tab, Cor.Branca));
            ColocarnovaPeca('d', 1, new Rei(Tab, Cor.Branca));
            ColocarnovaPeca('e', 1, new Dama(Tab, Cor.Branca));
            ColocarnovaPeca('f', 1, new Bispo(Tab, Cor.Branca));
            ColocarnovaPeca('g', 1, new Cavalo(Tab, Cor.Branca));
            ColocarnovaPeca('h', 1, new Torre(Tab, Cor.Branca));

            ColocarnovaPeca('a', 2, new Peao(Tab, Cor.Branca));
            ColocarnovaPeca('b', 2, new Peao(Tab, Cor.Branca));
            ColocarnovaPeca('c', 2, new Peao(Tab, Cor.Branca));
            ColocarnovaPeca('d', 2, new Peao(Tab, Cor.Branca));
            ColocarnovaPeca('e', 2, new Peao(Tab, Cor.Branca));
            ColocarnovaPeca('f', 2, new Peao(Tab, Cor.Branca));
            ColocarnovaPeca('g', 2, new Peao(Tab, Cor.Branca));
            ColocarnovaPeca('h', 2, new Peao(Tab, Cor.Branca));

            // Pretas
            ColocarnovaPeca('a', 7, new Peao(Tab, Cor.Preto));
            ColocarnovaPeca('b', 7, new Peao(Tab, Cor.Preto));
            ColocarnovaPeca('c', 7, new Peao(Tab, Cor.Preto));
            ColocarnovaPeca('d', 7, new Peao(Tab, Cor.Preto));
            ColocarnovaPeca('e', 7, new Peao(Tab, Cor.Preto));
            ColocarnovaPeca('f', 7, new Peao(Tab, Cor.Preto));
            ColocarnovaPeca('g', 7, new Peao(Tab, Cor.Preto));
            ColocarnovaPeca('h', 7, new Peao(Tab, Cor.Preto));

            ColocarnovaPeca('a', 8, new Torre(Tab, Cor.Preto));
            ColocarnovaPeca('b', 8, new Cavalo(Tab, Cor.Preto));
            ColocarnovaPeca('c', 8, new Bispo(Tab, Cor.Preto));
            ColocarnovaPeca('d', 8, new Rei(Tab, Cor.Preto));
            ColocarnovaPeca('e', 8, new Dama(Tab, Cor.Preto));
            ColocarnovaPeca('f', 8, new Bispo(Tab, Cor.Preto));
            ColocarnovaPeca('g', 8, new Cavalo(Tab, Cor.Preto));
            ColocarnovaPeca('h', 8, new Torre(Tab, Cor.Preto)); 
        }
    }
}
