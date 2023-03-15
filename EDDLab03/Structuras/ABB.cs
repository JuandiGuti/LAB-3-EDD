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
        public int Altura()
        {
            return Altura(raiz);
        }

        private int Altura(Nodo<T> nodo)
        {
            if (nodo == null)
            {
                return -1;
            }
            int alturaIzquierda = Altura(nodo.Izquierda);
            int alturaDerecha = Altura(nodo.Derecha);

            return 1 + Math.Max(alturaIzquierda, alturaDerecha);
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

        public String Validez()
        {
            if (raiz == null)
            {
                return ("Vacia");
            }
            else
            {
                if (raiz.Izquierda == null)
                {
                    if (raiz.Derecha == null)
                    {
                        return (Convert.ToString(raiz.Valor));
                    }
                    return (Convert.ToString(raiz.Valor) + Recorrido(raiz.Derecha, raiz));
                }
                if (raiz.Derecha == null)
                {
                    return (Recorrido(raiz.Izquierda, raiz));
                }
                return (Recorrido(raiz.Izquierda, raiz) + Recorrido(raiz.Derecha, raiz));
            }
        }

        public String Recorrido(Nodo<T> actual, Nodo<T> padre)
        {
            if (actual.Izquierda == null)
            {
                if (actual.Derecha == null)
                {
                    return (Convert.ToString(actual.Valor));
                }
                return (Convert.ToString(actual.Valor) + " - " + Recorrido(actual.Derecha, actual));
            }

            if (actual.Derecha == null)
            {
                return (Recorrido(actual.Izquierda, actual) + " - " + Convert.ToString(padre.Valor));
            }
            return (Recorrido(actual.Izquierda, actual) + " - " + Convert.ToString(padre.Valor) + " - " + Recorrido(actual.Derecha, actual));
        }
    }
}