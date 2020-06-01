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
        private List<Licitacao> User_Licitacoes;
        private String username; // "Artur Filipe"

        public AuctionHouse(String first_last)
        {
            username = first_last;
            Leiloes = new List<Leilao>();
        }

        public void addLeilao(Leilao l)
        {
            Leiloes.Add(l);
        }

        public String getUsername()
        {
            return username;
        }

        public Leilao[] getUserLeiloes(String user) // leiloes onde user licitou
        {
            List<Leilao> userLeiloes = new List<Leilao>();
            foreach(Leilao lei in Leiloes)
            {
                foreach(Licitacao lic in lei.Licitacoes)
                {
                    if(lic.user == user)
                    {
                        userLeiloes.Add(lei);
                        break;
                    }  
                }
            }

            return userLeiloes.ToArray();
        }

        public Leilao[] getLeiloes(String Keywords, String Categoria, int order, String user) // order=1 crescente order=-1 decrescente
        {
            List<Leilao> res = new List<Leilao>();
            foreach (Leilao l in Leiloes)
            {
                if (user == null)
                {
                    if (l.Categoria == Categoria | Categoria == "Todos")
                    {
                        if (Keywords != "")
                        {
                            if (l.Nome.ToLower().Contains(Keywords.ToLower()))
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
                else
                {
                    if (l.Owner == user)
                    {
                        if (l.Categoria == Categoria | Categoria == "Todos")
                        {
                            if (Keywords != "")
                            {
                                if (l.Nome.ToLower().Contains(Keywords.ToLower()))
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
                }
            }

            res.Sort(delegate (Leilao x, Leilao y) { return x.getCurrentValue().CompareTo(y.getCurrentValue()); });

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
        public String Owner;
        public List<Licitacao> Licitacoes;
        public double currentValue;

        public Leilao(String n, String desc, String e, String categ, DateTime fim, String img, String Own, double startValue)
        {
            Nome = n;
            Descricao = desc;
            Estado = e;
            Categoria = categ;
            DataFim = fim;
            imgPath = img;
            Owner = Own;
            currentValue = startValue;
            Licitacoes = new List<Licitacao>();
        }

        public Boolean addLicitacao(Licitacao l)
        {
            if( Licitacoes.Count > 0)
            {
                if( l.value > currentValue)
                {
                    currentValue = l.value;
                    Licitacoes.Add(l);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                currentValue = l.value;
                Licitacoes.Add(l);
            }

            return true;
        }

        public String timeToEnd()
        {
            var now = DateTime.Now;

            if (DataFim < now)
            {
                return "";
            }

            var diff = DataFim.Subtract(now);
            return String.Format("{0}d {1}:{2}:{3}h", diff.Days, diff.Hours, diff.Minutes, diff.Seconds);
        } 

        public double getCurrentValue()
        {
            return currentValue;
        }

        public bool hasLicitacoes()
        {
            return Licitacoes.Count > 0;
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