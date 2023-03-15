using System.Drawing;

namespace Structuras
{
	public class Nodo<T> where T : IComparable<T>
	{
		public T Valor { get; set; }
		public Nodo<T> Izquierda { get; set; }
		public Nodo<T> Derecha { get; set; }

		public Nodo(T valor)
		{
			Valor = valor;
		}
	}

	public class ABB<T> where T : IComparable<T>
	{
		private Nodo<T> raiz;

		public ABB()
		{
			raiz = null;
		}
        public Nodo<T> Buscar(T valor)
        {
            Nodo<T> actual = raiz;

            while (actual != null)
            {
                int comparacion = valor.CompareTo(actual.Valor);

                if (comparacion == 0)
                {
                    return actual;
                }
                else if (comparacion < 0)
                {
                    actual = actual.Izquierda;
                }
                else
                {
                    actual = actual.Derecha;
                }
            }
            return null;
        }

        public void Insertar(T valor)
		{
			Nodo<T> nuevoNodo = new Nodo<T>(valor);
			if (raiz == null)
			{
				raiz = nuevoNodo;
			}
			else
			{
				Nodo<T> actual = raiz;
				Nodo<T> padre;
				while (true)
				{
					padre = actual;
					if (valor.CompareTo(actual.Valor) < 0)
					{
						actual = actual.Izquierda;
						if (actual == null)
						{
							padre.Izquierda = nuevoNodo;
							break;
						}
					}
					else
					{
						actual = actual.Derecha;
						if (actual == null)
						{
							padre.Derecha = nuevoNodo;
							break;
						}
					}
				}
			}
		}
	}
}