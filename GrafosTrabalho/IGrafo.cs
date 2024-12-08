using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTrabalho
{
    internal interface IGrafo
    {

        /// <summary>
        /// Adiciona uma aresta ao grafo.
        /// </summary>
        /// <param name="vertice">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <param name="peso">Peso da aresta.</param>
        /// <returns>True se a aresta foi adicionada com sucesso, False caso contrário.</returns>
        bool AdicionarAresta(int vertice, int destino, int peso);

        /// <summary>
        /// ToString para o grafo.
        /// </summary>
        /// <returns>String representando o grafo.</returns>
        string ToString();

        /// <summary>
        /// Verifica arestas adjacentes a determinada aresta
        /// </summary>
        /// <param name="aresta">Aresta.</param>
        /// <returns>Lista de arestas adjacentes</returns>
        List<Adjacencia> ArestasAdjacentes(Adjacencia aresta);

        /// <summary>
        /// Verifica vertices adjacentes a determinado vértice
        /// </summary>
        /// <param name="vertice">vertice.</param>
        /// <returns>Lista de verticess adjacentes</returns>
        List<int> VerticesAdjacentes(int vertice);

        /// <summary>
        /// Verifica arestas incidentes a determinado vértice
        /// </summary>
        /// <param name="vertice">vertice.</param>
        /// <returns>Lista de arestas incidentes</returns>
        List<Adjacencia> ArestasIncidentes(int vertice);

        /// <summary>
        /// Verifica vertices incidentes a determinado vértice
        /// </summary>
        /// <param name="origem">origem</param>
        /// /// <param name="destino">destino</param>
        /// <returns>Lista de vetices incidentes</returns>
        List<int> VerticesIncidentes(int origem, int destino);

        /// <summary>
        /// Verifica grau de determinado vértice
        /// </summary>
        /// <param name="vertice">vertice.</param>
        /// <returns>Grau do vértice</returns>
        int GrauVertice(int vertice);

        /// <summary>
        /// Verifica se dois vertices sao vizinhos
        /// </summary>
        /// <param name="vertice">vertice.</param>
        /// /// <param name="vertic2">vertice2.</param>
        /// <returns>Retorna true caso os vpertices seja vizinhos e falso se não</returns>
        bool VerificarVizinhos(int vertice, int vertice2);

        /// <summary>
        /// ATroca o peso de uma aresta.
        /// </summary>
        /// <param name="vertice">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <param name="peso">Peso da aresta.</param>
        /// <returns>True se o novo peso foi adicionado com sucesso, False caso contrário.</returns>
        bool TrocarPeso(int origem, int destino, int peso);

        /// <summary>
        /// Faz uma troca de ajacencia
        /// </summary>
        /// <param name="vertice">vertice.</param>
        /// /// <param name="vertic2">vertice2.</param>
        /// <returns>Retorna true caso a troca foi efetuada com sucesso e falso se não</returns>
        bool TrocarAdjacencias(int vertice, int vertice2);
    }
}
