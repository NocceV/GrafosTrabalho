﻿using System;
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

        /// <summary>
        /// Adiciona aresta informada pelo user
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <param name="peso">Peso da aresta.</param>
        /// <returns>True se a adição a foi realizada com sucesso, False caso contrário.</returns>
        public bool AdicionarAresta(int vertice, int destino, int peso)
        {
            if (vertice < listaAdj.Length && vertice >= 0)
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
                sb.Append(i + " -> ");
                foreach (Adjacencia aresta in listaAdj[i])
                {
                    sb.Append(aresta.getDestino() + " Peso: " + aresta.getPeso());
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
            if (aresta.getDestino() <= listaAdj.Length && aresta.getOrigem() > 0)
            {
                List<Adjacencia> adj = new List<Adjacencia>();
                adj.AddRange(listaAdj[aresta.getDestino()]);
                adj.AddRange(listaAdj[aresta.getOrigem()]);
                return adj;
            }
            return null;
        }

        /// <summary>
        /// Pega as arestas incidentes de um vértice v informado pelo usuário.
        /// </summary>
        /// <param name="vertice">Vértice v</param>
        /// <returns>Retorna uma lista com as arestas incidentes ao vértice.</returns>
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

        /// <summary>
        /// Pega os vértices incidentes a uma aresta a, informada pelo usuário.
        /// </summary>
        /// <param name="origem">Vértice de origem.</param>
        /// <param name="destino">Vértice de destino.</param>
        /// <returns>Retorna uma lista com os vertices incidentes.</returns>
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

        /// <summary>
        /// Verifica o grau de determinado vértice.
        /// </summary>
        /// <param name="vertice">Vértice que irá ser avaliado.</param>
        /// <returns>Retorna o grau do vértice.</returns>
        public int GrauVertice(int vertice)
        {
            if (vertice >= 0 && vertice <= listaAdj.Length)
            {
                return listaAdj[vertice].Count();
            }
            return 0;
        }

        /// <summary>
        /// Determinar se dois vértices são adjacentes.
        /// </summary>
        /// <param name="origeme">Vértice 1.</param>
        /// <param name="destino">Vértice 2.</param>
        /// <returns>True se são vizinhos, False caso contrário.</returns>
        public bool VerificarVizinhos(int vertice, int vertice2)
        {
            if ((vertice >= 0 && vertice <= listaAdj.Length) && (vertice >= 0 && vertice <= listaAdj.Length))
            {
                return listaAdj[vertice].Any(l => l.getDestino() == vertice2) || listaAdj[vertice2].Any(l => l.getDestino() == vertice);
            }
            return false;
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
            if (origem >= 0 && origem < listaAdj.Length && destino >= 0 && destino < listaAdj.Length)
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
        

        /// <summary>
        /// Trocar dois vértices
        /// </summary>
        /// <param name="vertice1">Vértice 1.</param>
        /// <param name="vertice2">Vértice 2.</param>
        /// <returns>True se a troca foi realizada com sucesso e False caso contrário.</returns>
        public bool TrocarAdjacencias(int vertice, int vertice2)
        {
            if ((vertice >= 0 && vertice <= listaAdj.Length) && (vertice2 >= 0 && vertice2 <= listaAdj.Length))
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

        public List<int> VerticesAdjacentes(int vertice)
        {
            if(vertice >= 0 && vertice <= listaAdj.Length)
            {
                List<int> adj = new List<int>();
                listaAdj[vertice].ForEach(a => adj.Add(a.getDestino()));
                return adj;
            }
            return null;
        }
    }
}
