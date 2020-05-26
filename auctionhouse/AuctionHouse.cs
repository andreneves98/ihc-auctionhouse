using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace auctionhouse
{
    public class AuctionHouse
    {
        private List<Leilao> Leiloes;

        public AuctionHouse()
        {
            Leiloes = new List<Leilao>();
        }

        public void addLeilao(Leilao l)
        {
            Leiloes.Add(l);
        }

        public Leilao[] getLeiloes(String Keywords, String Categoria, int order) // order=1 crescente order=-1 decrescente
        {
            List<Leilao> res = new List<Leilao>();
            foreach (Leilao l in Leiloes)
            {
                if(l.Categoria == Categoria | Categoria == "Todos")
                {
                    if(Keywords != "")
                    {
                        if (l.Categoria.Contains(Keywords))
                        {
                            res.Add(l);
                        }
                    }
                    else
                    {
                        res.Add(l);
                    }
                }
            }

            res.Sort(delegate (Leilao x, Leilao y) { return x.highestLicitacao().CompareTo(y.highestLicitacao()); });

            if(order  == -1)
            {
                res.Reverse();
            }
           
            return res.ToArray();
        }
    }

    public class Leilao
    {
        public String Nome;
        public String Descricao;
        public String Estado; // aberto/fechado
        public String Categoria;
        public DateTime DataFim;
        public String imgPath;
        public List<Licitacao> Licitacoes;

        public Leilao(String n, String desc, String e, String categ, DateTime fim, String img)
        {
            Nome = n;
            Descricao = desc;
            Estado = e;
            Categoria = categ;
            DataFim = fim;
            imgPath = img;
            Licitacoes = new List<Licitacao>();
        }

        public Boolean addLicitacao(Licitacao l)
        {
            if( Licitacoes.Count > 0)
            {
                if( l.value > Licitacoes[Licitacoes.Count-1].value)
                {
                    Licitacoes.Add(l);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Licitacoes.Add(l);
            }

            return true;
        }

        public String timeToEnd()
        {
            var now = DateTime.Now;
            var diff = DataFim.Subtract(now);
            return String.Format("{0}d {1}:{2}:{3}h", diff.Days, diff.Hours, diff.Minutes, diff.Seconds);
        } 

        public double highestLicitacao()
        {
            if(Licitacoes.Count > 0)
            {
                return Licitacoes[Licitacoes.Count - 1].value;
            }
            else
            {
                return 0;
            }
        }
    }

    public class Licitacao
    {
        public String user;
        public double value; // €

        public Licitacao(String u, Double v)
        {
            user = u;
            value = v;
        }
    }
}