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
        public Peca VulneravelEnPassant { get; private set; }
        public bool Xeque { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Terminada = false;
            Turno = 1;
            JogadorAtual = Cor.Branca;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            Xeque = false;
            VulneravelEnPassant = null;
            ColocarPecas();
        }

        private Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrimentarQtdeMovimento();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);     
            
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            // # Jogada Especial Roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);

                Peca T = Tab.RetirarPeca(origemT);
                T.IncrimentarQtdeMovimento();
                Tab.ColocarPeca(T, destinoT);
            }

            // # Jogada Especial Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca T = Tab.RetirarPeca(origemT);
                T.IncrimentarQtdeMovimento();
                Tab.ColocarPeca(T, destinoT);
            }

            // # Jogada Especial En Passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }

                    pecaCapturada = Tab.RetirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        private void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQtdeMovimento();

            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }

            Tab.ColocarPeca(p, origem);

            // # Jogada Especial Roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);

                Peca T = Tab.RetirarPeca(destinoT);
                T.DecrementarQtdeMovimento();
                Tab.ColocarPeca(T, origemT);
            }

            // # Jogada Especial Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca T = Tab.RetirarPeca(destinoT);
                T.DecrementarQtdeMovimento();
                Tab.ColocarPeca(T, origemT);
            }

            // # Jogada Especial En Passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = Tab.RetirarPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    Tab.ColocarPeca(peao, posP);
                }
            }
        }

        public void RelizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca p = Tab.Peca(destino);

            // #Jogada Especial Promoção
            if (p is Peao)
            {
                if ((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preto && destino.Linha == 7))
                {
                    p = Tab.RetirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(Tab, p.Cor);
                    Tab.ColocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TestarSeEstaEmXequemate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudarJogador();
            }
            

            // # Jogada Especial En Passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
            }

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

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preto;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca x in RetornarPecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }

            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);

            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }

            foreach (Peca x in RetornarPecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.PegarMovimentosPossiveis();

                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool TestarSeEstaEmXequemate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca x in RetornarPecasEmJogo(cor))
            {
                bool[,] mat = x.PegarMovimentosPossiveis();

                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazerMovimento(origem, destino, pecaCapturada);

                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
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
            ColocarnovaPeca('d', 1, new Dama(Tab, Cor.Branca));
            ColocarnovaPeca('e', 1, new Rei(Tab, Cor.Branca, this));
            ColocarnovaPeca('f', 1, new Bispo(Tab, Cor.Branca));
            ColocarnovaPeca('g', 1, new Cavalo(Tab, Cor.Branca));
            ColocarnovaPeca('h', 1, new Torre(Tab, Cor.Branca));

            ColocarnovaPeca('a', 2, new Peao(Tab, Cor.Branca, this));
            ColocarnovaPeca('b', 2, new Peao(Tab, Cor.Branca, this));
            ColocarnovaPeca('c', 2, new Peao(Tab, Cor.Branca, this));
            ColocarnovaPeca('d', 2, new Peao(Tab, Cor.Branca, this));
            ColocarnovaPeca('e', 2, new Peao(Tab, Cor.Branca, this));
            ColocarnovaPeca('f', 2, new Peao(Tab, Cor.Branca, this));
            ColocarnovaPeca('g', 2, new Peao(Tab, Cor.Branca, this));
            ColocarnovaPeca('h', 2, new Peao(Tab, Cor.Branca, this));

            // Pretas
            ColocarnovaPeca('a', 7, new Peao(Tab, Cor.Preto, this));
            ColocarnovaPeca('b', 7, new Peao(Tab, Cor.Preto, this));
            ColocarnovaPeca('c', 7, new Peao(Tab, Cor.Preto, this));
            ColocarnovaPeca('d', 7, new Peao(Tab, Cor.Preto, this));
            ColocarnovaPeca('e', 7, new Peao(Tab, Cor.Preto, this));
            ColocarnovaPeca('f', 7, new Peao(Tab, Cor.Preto, this));
            ColocarnovaPeca('g', 7, new Peao(Tab, Cor.Preto, this));
            ColocarnovaPeca('h', 7, new Peao(Tab, Cor.Preto, this));

            ColocarnovaPeca('a', 8, new Torre(Tab, Cor.Preto));
            ColocarnovaPeca('b', 8, new Cavalo(Tab, Cor.Preto));
            ColocarnovaPeca('c', 8, new Bispo(Tab, Cor.Preto));
            ColocarnovaPeca('d', 8, new Dama(Tab, Cor.Preto));
            ColocarnovaPeca('e', 8, new Rei(Tab, Cor.Preto, this));
            ColocarnovaPeca('f', 8, new Bispo(Tab, Cor.Preto));
            ColocarnovaPeca('g', 8, new Cavalo(Tab, Cor.Preto));
            ColocarnovaPeca('h', 8, new Torre(Tab, Cor.Preto)); 
        }
    }
}
