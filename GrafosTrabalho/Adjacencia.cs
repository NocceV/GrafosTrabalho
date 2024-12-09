using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafosTrabalho
{
    internal class Adjacencia
    {
        #region Atributos
        private int origem;
        private int destino;
        private int peso;
        #endregion

        #region Contrutor
        public Adjacencia(int origem, int destino, int peso)
        {
            this.origem = origem;
            this.destino = destino;
            this.peso = peso;
        }
        #endregion

        #region Get/Set
        public int getDestino()
        {
            return destino;
        }
        public int getPeso()
        {
            return peso;
        }
        public int getOrigem()
        {
            return origem;
        }
        public void setOrigem(int origem)
        {
            this.origem = origem;
        }
        public void setDestino(int destino)
        {
            this.destino = destino;
        }
        public void setPeso(int peso)
        {
            this.peso = peso;
        }
        #endregion

        #region Metodos
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("----------------------------");
            str.AppendLine("Origem - "+origem);
            str.AppendLine("Destino - "+destino);
            str.AppendLine("Peso - "+peso);
            str.AppendLine("----------------------------");
            return str.ToString();
        }
        #endregion
    }
}
