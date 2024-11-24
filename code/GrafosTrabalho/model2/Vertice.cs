using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTrabalho.model2
{
    internal class Vertice
    {
        private static int _ultimoId = 0;
        private int _id;
        private List<Aresta> _arestasVertice;
        private dynamic _objeto;

        //Construtor Padrao Classe Vertice
        public Vertice()
        {
            _id = ++_ultimoId;
            _arestasVertice = new List<Aresta>();
            _objeto = null;
        }

        //Construtor com Instacia de Objeto Dinamico
        public Vertice(dynamic objeto)
        {
            _id = ++_ultimoId;
            _arestasVertice = new List<Aresta>();
            _objeto = objeto;
        }

        public List<Vertice> Vizinhanca()
        {
            List<Vertice> vizinhanca = new List<Vertice>();

            foreach (Aresta aresta in _arestasVertice)
            {
                vizinhanca.Add(aresta.VerticeOrigem);
            }

            return vizinhanca;
        }

        public int GrauVertice() { return _arestasVertice.Count; }

        public override string ToString()
        {
            return "v" + Id;
        }

    }
}
