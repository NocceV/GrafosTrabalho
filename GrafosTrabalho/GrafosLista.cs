using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTrabalho
{
    internal class GrafosLista : IGrafo
    {
        private List<Adjacencia>[] listaAdj;
        public GrafosLista(int vertices)
        {
            listaAdj = new List<Adjacencia>[vertices];
            for (int i = 0; i < vertices; i++)
            {
                listaAdj[i] = new List<Adjacencia>();
            }
        }

        public GrafosLista(int vertices, List<List<int>> dimic)
        {
            listaAdj = new List<Adjacencia>[vertices];
            for (int i = 0; i < vertices; i++)
            {
                listaAdj[i] = new List<Adjacencia>();
            }

            foreach (List<int> lista in dimic)
            {
                AdicionarAresta(lista[0], lista[1], lista[2]);
            }
        }
        public bool AdicionarAresta(int vertice, int destino, int peso)
        {
            if (vertice <= listaAdj.Length && vertice > 0)
            {
                Adjacencia aresta = new Adjacencia(vertice, destino, peso);
                listaAdj[vertice].Add(aresta);
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < listaAdj.Length; i++)
            {
                sb.Append(i + " -> ");
                foreach (Adjacencia aresta in listaAdj[i])
                {
                    sb.Append(aresta.getDestino() + " Peso: " + aresta.getPeso());
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public List<Adjacencia> ArestasAdjacentes(Adjacencia aresta)
        {
            if (aresta.getDestino() <= listaAdj.Length && aresta.getOrigem() > 0)
            {
                List<Adjacencia> adj = new List<Adjacencia>();
                adj.AddRange(listaAdj[aresta.getDestino()]);
                adj.AddRange(listaAdj[aresta.getOrigem()]);
                return adj;
            }
            return null;
        }
        public List<Adjacencia> ArestasIncidentes(int vertice)
        {
            if (vertice >= 0 && vertice <= listaAdj.Length)
            {
                List<Adjacencia> adj = new List<Adjacencia>();
                adj.AddRange(listaAdj[vertice]);
                return adj;
            }
            return null;
        }
        public List<int> VerticesIncidentes(int origem, int destino)
        {
            if (origem <= listaAdj.Length && destino > 0)
            {
                List<int> adj = new List<int>();
                adj.Add(origem);
                adj.Add(destino);
                return adj;
            }
            return null;
        }
        public int GrauVertice(int vertice)
        {
            if (vertice >= 0 && vertice <= listaAdj.Length)
            {
                return listaAdj[vertice].Count();
            }
            return 0;
        }
        public bool VerificarVizinhos(int vertice, int vertice2)
        {
            if ((vertice >= 0 && vertice <= listaAdj.Length) && (vertice >= 0 && vertice <= listaAdj.Length))
            {
                return listaAdj[vertice].Any(l => l.getDestino() == vertice2) || listaAdj[vertice2].Any(l => l.getDestino() == vertice);
            }
            return false;
        }
        public bool TrocarPeso(int origem, int destino, int peso)
        {
            if (destino <= listaAdj.Length && origem >= listaAdj.Length)
            {
                listaAdj[origem].ForEach(a =>
                {
                    if (a.getDestino() == destino)
                    {
                        a.setPeso(peso);
                    }
                });
                return true;
            }
            return false;
        }
        public bool TrocarAdjacencias(int vertice, int vertice2)
        {
            if ((vertice >= 0 && vertice <= listaAdj.Length) && (vertice >= 0 && vertice <= listaAdj.Length))
            {
                List<Adjacencia> aux = new List<Adjacencia>();
                aux = listaAdj[vertice];

                //Troca de posição e ajuste de origem
                listaAdj[vertice] = listaAdj[vertice2];
                listaAdj[vertice].ForEach(a => a.setOrigem(vertice));
                listaAdj[vertice2] = listaAdj[vertice];
                listaAdj[vertice2].ForEach(a => a.setOrigem(vertice2));

                //Verifica quais vértices são adjacentes antes da troca e altera o destino
                for (int i = 0; i < listaAdj.Length; i++)
                {
                    listaAdj[i].Where(x => x.getDestino() == vertice).ToList().ForEach(a => a.setDestino(vertice2));
                    listaAdj[i].Where(x => x.getDestino() == vertice2).ToList().ForEach(a => a.setDestino(vertice));
                }
                return true;
            }
            return false;
        }
    }
}
