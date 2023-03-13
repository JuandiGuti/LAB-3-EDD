using System;
using System.Collections.Generic;

namespace EDDLab03
{
	public class ArbolAVL<T> where T : IComparable
	{
		public int Altura { get; set; }
		public T valor { get; set; }
		public ArbolAVL<T> SubArbolDerecho { get; set; }
		public ArbolAVL<T> SubArbolIzquierdo { get; set; }

		public ArbolAVL()
		{

		}

		public ArbolAVL( T valor, ArbolAVL<T> subArbolDerecho, ArbolAVL<T> subArbolIzquierdo)
		{
			this.valor = valor;
			SubArbolDerecho = subArbolDerecho;
			SubArbolIzquierdo = subArbolIzquierdo;
		}

		private ArbolAVL<T> Rotacion(ArbolAVL<T> subarbol, bool RotDerecha)
		{
			var temp= new ArbolAVL<T>();

			if (RotDerecha)
			{
				temp = subarbol.SubArbolDerecho;
				subarbol.SubArbolDerecho = temp.SubArbolIzquierdo;
				temp.SubArbolIzquierdo = subarbol;
			}
			else
			{
				temp = subarbol.SubArbolIzquierdo;
				subarbol.SubArbolIzquierdo = temp.SubArbolDerecho;
				temp.SubArbolDerecho = subarbol;
			}
			return temp;
		}
		private int MayorAltura(int altura1, int altura2)
		{
			if (altura1 > altura2)
				return altura1;
			else return altura2;
		}

		private void Nueva_Altura(ArbolAVL<T> subarbol)
		{
			if (subarbol != null)
				subarbol.Altura = MayorAltura(subarbol.SubArbolDerecho.Altura, subarbol.SubArbolIzquierdo.Altura);
			Altura = -1;
			
		}
	}
}
