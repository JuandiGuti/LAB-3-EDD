using System.Drawing;

namespace Structuras
{
	public class Nodo<T>
	{
		public T Valor { get; set; }
		public Nodo<T> HijoIzquierdo { get; set; }
		public Nodo<T> HijoDerecho { get; set; }

		public Nodo(T valor)
		{
			Valor = valor;
			HijoIzquierdo = null;
			HijoDerecho = null;
		}
	}

	public class Arbol<T>
	{
		public Nodo<T> Raiz { get; set; }

		public Arbol()
		{
			Raiz = null;
		}

		
		public void Colocar(Nodo<T> un_nodo, T valor)
		{
            if (un_nodo.Valor <= valor)
			{
				if(un_nodo.HijoIzquierdo == null)
				{
                    un_nodo.HijoIzquierdo.Valor = valor;
                }
                else
				{
                    Colocar(un_nodo.HijoIzquierdo, valor);
                }
            }
            else
            {
                if (un_nodo.HijoDerecho == null)
                {
                    un_nodo.HijoDerecho.Valor = valor;
                }
                else
                {
                    Colocar(un_nodo.HijoDerecho, valor);
                }
            }
        }
    }
}