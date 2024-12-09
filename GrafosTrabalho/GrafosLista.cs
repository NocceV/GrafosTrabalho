using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTrabalho
{
    internal class GrafosLista : IGrafo
    {
        #region Atributos
        private List<Adjacencia>[] listaAdj;
        #endregion

        #region Contrutores
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
        #endregion

        #region Metodos
        /// <summary>
        /// Adiciona aresta informada pelo user
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <param name="peso">Peso da aresta.</param>
        /// <returns>True se a adição a foi realizada com sucesso, False caso contrário.</returns>
        public bool AdicionarAresta(int vertice, int destino, int peso)
        {
            if (vertice >= 0 && vertice < listaAdj.Length && destino >= 0 && destino < listaAdj.Length)
            {
                Adjacencia aresta = new Adjacencia(vertice, destino, peso);
                listaAdj[vertice].Add(aresta);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Método ToString com override
        /// </summary>
        /// <returns>Retorna o ToString da classe.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < listaAdj.Length; i++)
            {
                sb.Append((i +1) + " -> ");
                foreach (Adjacencia aresta in listaAdj[i])
                {
                    sb.Append((aresta.getDestino()) + " Peso: " + (aresta.getPeso()) + " | ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        /// <summary>
        /// Pega as arestas adjacentes de uma aresta E informada pelo usuário.
        /// </summary>
        /// <param name="aresta">Aresta</param>
        /// <returns>Retorna uma lista de arestas com as arestas adjacentes a aresta E.</returns>
        public List<Adjacencia> ArestasAdjacentes(Adjacencia aresta)
        {
            try
            {
                if (aresta == null)
                {
                    throw new ArgumentNullException(nameof(aresta), "A aresta fornecida é nula.");
                }

                if (aresta.getDestino() >= listaAdj.Length || aresta.getDestino() < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(aresta), "O destino da aresta está fora dos limites da lista de adjacência.");
                }

                if (aresta.getOrigem() >= listaAdj.Length || aresta.getOrigem() < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(aresta), "A origem da aresta está fora dos limites da lista de adjacência.");
                }
                List<Adjacencia> adj = new List<Adjacencia>();
                for(int i = 0; i < listaAdj.Length; i++)
                {
                    foreach(Adjacencia a in listaAdj[i])
                    {
                        if (VerificarPorAdjacencia(a, aresta))
                        {
                            adj.Add(a);
                        }
                    }
                }
                return adj;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar arestas adjacentes: {ex.Message}");
                return null;
            }
        }
        public bool VerificarPorAdjacencia(Adjacencia vertice, Adjacencia vertice2)
        {
            if (vertice.getDestino() == vertice2.getDestino() || 
                vertice.getDestino() == vertice2.getOrigem() || 
                vertice.getOrigem() == vertice2.getDestino() || 
                vertice.getOrigem() == vertice2.getOrigem())
            {
                return true;
            }
            return false;
        }
        public bool VerificarPorVertice(Adjacencia vertice, int vertice2)
        {
            if (vertice.getDestino() == vertice2 ||
                vertice.getOrigem() == vertice2)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Imprimir vertices adjacentes
        /// </summary>
        /// <param name="vertice">Vértice.</param>
        /// <returns>Lista com vertices adjacentes.</returns>
        public List<int> VerticesAdjacentes(int vertice)
        {
            try
            {
                if (vertice < 0 || vertice >= listaAdj.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(vertice), "O vértice está fora dos limites da lista de adjacência.");
                }

                List<int> adj = new List<int>();
                listaAdj[vertice].Where(a => VerificarPorVertice(a,vertice));
                return adj;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new List<int>(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado ao obter vértices adjacentes: {ex.Message}");
                return new List<int>();
            }
        }

        /// <summary>
        /// Pega as arestas incidentes de um vértice v informado pelo usuário.
        /// </summary>
        /// <param name="vertice">Vértice v</param>
        /// <returns>Retorna uma lista com as arestas incidentes ao vértice.</returns>
        public List<Adjacencia> ArestasIncidentes(int vertice)
        {
            try
            {
                if (vertice < 0 || vertice >= listaAdj.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(vertice), "O vértice está fora dos limites da lista de adjacência.");
                }

                List<Adjacencia> adj = new List<Adjacencia>();
                for (int i = 0; i < listaAdj.Length; i++)
                {
                    foreach (Adjacencia a in listaAdj[i])
                    {
                        if (a.getDestino() == vertice)
                        {
                            adj.Add(a);
                        }
                    }
                }
                adj.AddRange(listaAdj[vertice]);
                return adj;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new List<Adjacencia>(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado ao buscar arestas incidentes: {ex.Message}");
                return new List<Adjacencia>();
            }

        }

        /// <summary>
        /// Pega os vértices incidentes a uma aresta a, informada pelo usuário.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <returns>Retorna uma lista com os vertices incidentes.</returns>
        public List<int> VerticesIncidentes(int origem, int destino)
        {
            try
            {
                if (origem < 0 || origem >= listaAdj.Length || destino < 0 || destino >= listaAdj.Length)
                {
                    throw new ArgumentOutOfRangeException("Os vértices de origem ou destino estão fora dos limites permitidos.");
                }

                return new List<int> { origem, destino };
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new List<int>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado ao buscar vértices incidentes: {ex.Message}");
                return new List<int>();
            }
        }

        /// <summary>
        /// Verifica o grau de determinado vértice.
        /// </summary>
        /// <param name="vertice">Vértice que irá ser avaliado.</param>
        /// <returns>Retorna o grau do vértice.</returns>
        public int GrauVertice(int vertice)
        {
            if (vertice < 0 || vertice >= listaAdj.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(vertice), "O vértice está fora do intervalo válido.");
            }

            return listaAdj[vertice]?.Count() ?? 0;
        }

        /// <summary>
        /// Determinar se dois vértices são adjacentes.
        /// </summary>
        /// <param name="origeme">Vértice 1.</param>
        /// <param name="destino">Vértice 2.</param>
        /// <returns>True se são vizinhos, False caso contrário.</returns>
        public bool VerificarVizinhos(int vertice, int vertice2)
        {
            if (vertice < 0 || vertice >= listaAdj.Length)
                throw new ArgumentOutOfRangeException(nameof(vertice), $"O vértice {vertice} está fora do intervalo permitido.");

            if (vertice2 < 0 || vertice2 >= listaAdj.Length)
                throw new ArgumentOutOfRangeException(nameof(vertice2), $"O vértice {vertice2} está fora do intervalo permitido.");

            return listaAdj[vertice]?.Any(l => l.getDestino() == vertice2) == true ||
                   listaAdj[vertice2]?.Any(l => l.getDestino() == vertice) == true;
        }

        /// <summary>
        /// Substitui o peso de uma aresta a, informada pelo usuário.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <param name="peso">Peso da aresta.</param>
        /// <returns>True se a troca foi realizada com sucesso, False caso contrário.</returns>
        public bool TrocarPeso(int origem, int destino, int peso)
        {
            if (origem < 0 || origem >= listaAdj.Length)
                throw new ArgumentOutOfRangeException(nameof(origem), $"O vértice de origem {origem} está fora do intervalo permitido.");

            if (destino < 0 || destino >= listaAdj.Length)
                throw new ArgumentOutOfRangeException(nameof(destino), $"O vértice de destino {destino} está fora do intervalo permitido.");

            if (listaAdj[origem] == null)
                throw new InvalidOperationException($"A lista de adjacência para o vértice de origem {origem} está vazia ou não inicializada.");

            bool encontrado = false;
            listaAdj[origem].ForEach(a =>
            {
                if (a.getDestino() == destino)
                {
                    a.setPeso(peso);
                    encontrado = true;
                }
            });

            if (!encontrado)
                throw new InvalidOperationException($"A aresta entre {origem} e {destino} não foi encontrada.");

            return true;
        }
        

        /// <summary>
        /// Trocar dois vértices
        /// </summary>
        /// <param name="vertice1">Vértice 1.</param>
        /// <param name="vertice2">Vértice 2.</param>
        /// <returns>True se a troca foi realizada com sucesso e False caso contrário.</returns>
        public bool TrocarAdjacencias(int vertice, int vertice2)
        {
            if (vertice < 0 || vertice >= listaAdj.Length)
                throw new ArgumentOutOfRangeException(nameof(vertice), $"O vértice {vertice} está fora do intervalo permitido.");

            if (vertice2 < 0 || vertice2 >= listaAdj.Length)
                throw new ArgumentOutOfRangeException(nameof(vertice2), $"O vértice {vertice2} está fora do intervalo permitido.");

            if (listaAdj[vertice] == null)
                throw new InvalidOperationException($"A lista de adjacência do vértice {vertice} está vazia ou não inicializada.");

            if (listaAdj[vertice2] == null)
                throw new InvalidOperationException($"A lista de adjacência do vértice {vertice2} está vazia ou não inicializada.");
            try
            {
                List<Adjacencia> aux = listaAdj[vertice];
                listaAdj[vertice] = listaAdj[vertice2];
                listaAdj[vertice].ForEach(a => a.setOrigem(vertice));
                listaAdj[vertice2] = aux;
                listaAdj[vertice2].ForEach(a => a.setOrigem(vertice2));

                for (int i = 0; i < listaAdj.Length; i++)
                {
                    listaAdj[i].Where(x => x.getDestino() == vertice).ToList().ForEach(a => a.setDestino(vertice2));
                    listaAdj[i].Where(x => x.getDestino() == vertice2).ToList().ForEach(a => a.setDestino(vertice));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao realizar a troca de adjacências.", ex);
            }
        }

        /// <summary>
        /// Get numero de vertices do grafo
        /// </summary>
        /// <returns>Número de vértices.</returns>
        public int numVertices()
        {
            return listaAdj.Length;
        }
        #endregion
    }
}
