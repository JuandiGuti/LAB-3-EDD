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
}