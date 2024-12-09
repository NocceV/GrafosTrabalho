using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTrabalho
{
    internal class GrafosMatriz : IGrafo
    {
        #region Atributos
        private int[,] _matrizGrafo;
        #endregion

        #region Construtores
        //Construtor Inicial Vazio
        public GrafosMatriz(int vertices) { _matrizGrafo = new int[vertices, vertices]; }

        //Construtor Completo entre Vertices e Arestas
        public GrafosMatriz(int vertices, List<List<int>> listaArestas)
        {
            int[,] matrizAdjacencia = new int[vertices, vertices];

            foreach (List<int> aresta in listaArestas)
            {
                matrizAdjacencia[aresta[0], aresta[1]] = aresta[2];
            }
            _matrizGrafo = matrizAdjacencia;
        }
        #endregion

        #region Metodos
        /// <summary>Adiciona aresta informada pelo user</summary>
        /// <param name="vertice">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <param name="peso">Peso da aresta.</param>
        /// <returns>True se a adição a foi realizada com sucesso, False caso contrário.</returns>
        public bool AdicionarAresta(int vertice, int destino, int peso)
        {
            try
            {
                if (vertice < 0 || vertice >= _matrizGrafo.GetLength(0))
                    throw new ArgumentOutOfRangeException(nameof(vertice), "O vértice está fora dos limites da matriz de adjacência.");

                if (destino < 0 || destino >= _matrizGrafo.GetLength(0))
                    throw new ArgumentOutOfRangeException(nameof(destino), "O destino está fora dos limites da matriz de adjacência.");

                if (peso <= 0)
                    throw new ArgumentException("O peso deve ser maior que zero.", nameof(peso));

                _matrizGrafo[vertice, destino] = peso;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}"); return false;
            }
        }



        /// <summary>Pega as arestas adjacentes de uma aresta E informada pelo usuário.</summary>
        /// <param name="aresta">Aresta</param>
        /// <returns>Retorna uma lista de arestas com as arestas adjacentes a aresta E.</returns>
        public List<Adjacencia> ArestasAdjacentes(Adjacencia aresta)
        {
            List<Adjacencia> resultado = new List<Adjacencia>();
            int destino = aresta.getDestino();
            int origem = aresta.getOrigem();

            try
            {
                if (aresta == null)
                    throw new ArgumentNullException(nameof(aresta), "A aresta fornecida é nula.");

                if (origem < 0 || origem >= _matrizGrafo.GetLength(0))
                    throw new ArgumentOutOfRangeException(nameof(origem), "O vértice de origem da aresta está fora dos limites permitidos.");

                if (destino < 0 || destino >= _matrizGrafo.GetLength(0))
                    throw new ArgumentOutOfRangeException(nameof(destino), "O vértice de destino da aresta está fora dos limites permitidos.");

                for (int i = 0; i < _matrizGrafo.GetLength(0); i++)
                {
                    if (_matrizGrafo[destino, i] > 0)
                        resultado.Add(new Adjacencia(destino, i, _matrizGrafo[destino, i]));
                }

                for (int i = 0; i < _matrizGrafo.GetLength(0); i++)
                {
                    if (_matrizGrafo[origem, i] > 0)
                        resultado.Add(new Adjacencia(origem, i, _matrizGrafo[origem, i]));
                }

                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar arestas adjacentes: {ex.Message}"); return null;
            }
        }



        /// <summary> Pega as arestas incidentes de um vértice v informado pelo usuário.</summary>
        /// <param name="vertice">Vértice v</param>
        /// <returns>Retorna uma lista com as arestas incidentes ao vértice.</returns>
        public List<Adjacencia> ArestasIncidentes(int vertice)
        {
            List<Adjacencia> resultado = new List<Adjacencia>();

            try
            {
                if (vertice < 0 || vertice >= _matrizGrafo.GetLength(0))
                    throw new ArgumentOutOfRangeException(nameof(vertice), "O vértice está fora dos limites da lista de adjacência.");

                for (int i = 0; i < _matrizGrafo.GetLength(0); i++)
                {
                    if (_matrizGrafo[vertice, i] > 0)
                        resultado.Add(new Adjacencia(vertice, i, _matrizGrafo[vertice, i]));
                }

                return resultado;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}"); return new List<Adjacencia>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado ao buscar arestas incidentes: {ex.Message}");
                return new List<Adjacencia>();
            }
        }



        /// <summary>Pega os vértices incidentes a uma aresta a, informada pelo usuário.</summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <returns>Retorna uma lista com os vertices incidentes.</returns>
        public List<int> VerticesIncidentes(int origem, int destino)
        {
            try
            {
                if (origem < 0 || origem >= _matrizGrafo.GetLength(0))
                    throw new ArgumentOutOfRangeException(nameof(origem), "O vértice de origem está fora dos limites permitidos.");

                if (destino < 0 || destino >= _matrizGrafo.GetLength(0))
                    throw new ArgumentOutOfRangeException(nameof(destino), "O vértice de destino está fora dos limites permitidos.");

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



        /// <summary>Verifica o grau de determinado vértice.</summary>
        /// <param name="vertice">Vértice que irá ser avaliado.</param>
        /// <returns>Retorna o grau do vértice.</returns>
        public int GrauVertice(int vertice)
        {
            if (vertice < 0 || vertice >= _matrizGrafo.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(vertice), "O vértice está fora do intervalo válido.");

            return Enumerable.Range(0, _matrizGrafo.GetLength(1))
                             .Count(i => _matrizGrafo[vertice, i] > 0);
        }



        /// <summary>Determinar se dois vértices são adjacentes.</summary>
        /// <param name="origeme">Vértice 1.</param>
        /// <param name="destino">Vértice 2.</param>
        /// <returns>True se são vizinhos, False caso contrário.</returns>
        public bool VerificarVizinhos(int vertice, int vertice2)
        {
            if (vertice < 0 || vertice >= _matrizGrafo.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(vertice), $"O vértice {vertice} está fora do intervalo permitido.");

            if (vertice2 < 0 || vertice2 >= _matrizGrafo.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(vertice2), $"O vértice {vertice2} está fora do intervalo permitido.");

            return _matrizGrafo[vertice, vertice2] > 0 || _matrizGrafo[vertice2, vertice] > 0;
        }



        /// <summary>Substitui o peso de uma aresta a, informada pelo usuário.</summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <param name="peso">Peso da aresta.</param>
        /// <returns>True se a troca foi realizada com sucesso, False caso contrário.</returns>
        public bool TrocarPeso(int origem, int destino, int peso)
        {
            bool encontrado = false;

            if (origem < 0 || origem >= _matrizGrafo.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(origem), $"O vértice de origem {origem} está fora do intervalo permitido.");

            if (destino < 0 || destino >= _matrizGrafo.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(destino), $"O vértice de destino {destino} está fora do intervalo permitido.");

            if (_matrizGrafo[origem, destino] > 0)
                _matrizGrafo[origem, destino] = peso; encontrado = true;

            if (!encontrado)
                throw new InvalidOperationException($"A aresta entre {origem} e {destino} não foi encontrada.");

            return true;
        }



        /// <summary>Trocar dois vértices</summary>
        /// <param name="vertice1">Vértice 1.</param>
        /// <param name="vertice2">Vértice 2.</param>
        /// <returns>True se a troca foi realizada com sucesso e False caso contrário.</returns>
        public bool TrocarAdjacencias(int vertice, int vertice2)
        {
            try
            {
                int aux = 0;

                if (vertice < 0 || vertice >= _matrizGrafo.GetLength(0))
                    throw new ArgumentOutOfRangeException(nameof(vertice), $"O vértice {vertice} está fora do intervalo permitido.");

                if (vertice2 < 0 || vertice2 >= _matrizGrafo.GetLength(0))
                    throw new ArgumentOutOfRangeException(nameof(vertice2), $"O vértice {vertice2} está fora do intervalo permitido.");

                for (int i = 0; i < _matrizGrafo.GetLength(0); i++)
                {
                    aux = _matrizGrafo[vertice, i];
                    _matrizGrafo[vertice, i] = _matrizGrafo[vertice2, i];
                    _matrizGrafo[vertice2, i] = aux;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao realizar a troca de adjacências.", ex);
            }
        }



        /// <summary>Imprimir vertices adjacentes</summary>
        /// <param name="vertice">Vértice.</param>
        /// <returns>Lista com vertices adjacentes.</returns>
        public List<int> VerticesAdjacentes(int vertice)
        {
            List<int> adj = new List<int>();

            try
            {
                if (vertice < 0 || vertice >= _matrizGrafo.GetLength(0))
                    throw new ArgumentOutOfRangeException(nameof(vertice), "O vértice está fora dos limites da lista de adjacência.");


                for (int i = 0; i < _matrizGrafo.GetLength(0); i++)
                    if (_matrizGrafo[vertice, i] > 0) { adj.Add(i); }

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



        /// <summary>Get numero de vertices do grafo.</summary>
        /// <returns>Número de vértices.</returns>
        public int numVertices() { return _matrizGrafo.GetLength(0); }



        /// <summary>Método ToString com override.</summary>
        /// <returns>Retorna o ToString da classe.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _matrizGrafo.GetLength(0); i++)
            {
                sb.Append("[");
                for (int j = 0; j < _matrizGrafo.GetLength(1); j++)
                {
                    if (_matrizGrafo[i, j] == 0) { sb.Append($"  " + _matrizGrafo[i, j] + $" ,"); }
                    else if (_matrizGrafo[i, j] > 0) { sb.Append($" +" + _matrizGrafo[i, j] + $" ,"); }
                    else { sb.Append($" " + _matrizGrafo[i, j] + $" ,"); }
                }
                sb.Append("]\n");
            }
            return sb.ToString();
        }
        #endregion
    }
}
