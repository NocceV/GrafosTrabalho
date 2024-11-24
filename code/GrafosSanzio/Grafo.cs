using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosSanzio
{
    class Grafo
    {
        private Dictionary<int, Dictionary<int, int>> adj;
        public Grafo()
        {
            adj = new Dictionary<int, Dictionary<int, int>>();
        }

        public bool AdicionaVertice(int vertice)  
        {
            bool result = false;
            if (!adj.TryGetValue(vertice, out Dictionary<int, int> vertices))
            {
                adj.Add(vertice, new Dictionary<int, int>());
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool AdicionarAresta(int vertice, int destino, int peso) { 
            bool result = false;

            if (adj.TryGetValue(vertice, out Dictionary<int, int> vertices))
            {
                adj[vertice].Add(destino, peso);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public Dictionary<int, Dictionary<int, int>> GetAdj()
        {
            return adj;
        }
    }
}
