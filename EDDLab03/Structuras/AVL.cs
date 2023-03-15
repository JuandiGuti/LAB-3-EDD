using System;
using System.Collections.Generic;

namespace Structuras
{
    using System;

    public class NODO<T> where T : IComparable<T>
    {
        public T Valor;
        public NODO<T> Izquierdo;
        public NODO<T> Derecho;
        public int Altura;

        public NODO(T valor)
        {
            Valor = valor;
            Altura = 1;
        }
    }

    public class AVL<T> where T : IComparable<T>
    {
        private NODO<T> Raiz;
        public int Rotaciones { get; set; }
        public bool Buscar(T valor)
        {
            NODO<T> actual = Raiz;

            while (actual != null)
            {
                int comparacion = valor.CompareTo(actual.Valor);

                if (comparacion == 0)
                {
                    return true;
                }
                else if (comparacion < 0)
                {
                    actual = actual.Izquierdo;
                }
                else
                {
                    actual = actual.Derecho;
                }
            }

            return false;
        }

        public void Insertar(T valor)
        {
            Raiz = InsertarRecursivo(Raiz, valor);
        }

        private NODO<T> InsertarRecursivo(NODO<T> nodo, T valor)
        {
            if (nodo == null)
            {
                return new NODO<T>(valor);
            }

            if (valor.CompareTo(nodo.Valor) < 0)
            {
                nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, valor);
            }
            else
            {
                nodo.Derecho = InsertarRecursivo(nodo.Derecho, valor);
            }

            nodo.Altura = 1 + Math.Max(Altura(nodo.Izquierdo), Altura(nodo.Derecho));

            int balance = Balance(nodo);

            if (balance > 1 && valor.CompareTo(nodo.Izquierdo.Valor) < 0)
            {
                Rotaciones++;
                return RotarDerecha(nodo);
            }

            if (balance > 1 && valor.CompareTo(nodo.Izquierdo.Valor) > 0)
            {
                nodo.Izquierdo = RotarIzquierda(nodo.Izquierdo);
                Rotaciones++;
                return RotarDerecha(nodo);
            }

            if (balance < -1 && valor.CompareTo(nodo.Derecho.Valor) > 0)
            {
                Rotaciones++;
                return RotarIzquierda(nodo);
            }

            if (balance < -1 && valor.CompareTo(nodo.Derecho.Valor) < 0)
            {
                nodo.Derecho = RotarDerecha(nodo.Derecho);
                Rotaciones++;
                return RotarIzquierda(nodo);
            }

            return nodo;
        }

        public int AlturaAVL()
        {
            return AlturaRecursiva(Raiz);
        }

        private int AlturaRecursiva(NODO<T> nodo)
        {
            if (nodo == null)
            {
                return 0;
            }

            return 1 + Math.Max(AlturaRecursiva(nodo.Izquierdo), AlturaRecursiva(nodo.Derecho));
        }

        private int Altura(NODO<T> nodo)
        {
            return nodo == null ? 0 : nodo.Altura;
        }

        private int Balance(NODO<T> nodo)
        {
            return nodo == null ? 0 : Altura(nodo.Izquierdo) - Altura(nodo.Derecho);
        }

        private NODO<T> RotarIzquierda(NODO<T> nodo)
        {
            NODO<T> nuevoNodo = nodo.Derecho;
            NODO<T> subNodo = nuevoNodo.Izquierdo;

            nuevoNodo.Izquierdo = nodo;
            nodo.Derecho = subNodo;

            nodo.Altura = 1 + Math.Max(Altura(nodo.Izquierdo), Altura(nodo.Derecho));
            nuevoNodo.Altura = 1 + Math.Max(Altura(nuevoNodo.Izquierdo), Altura(nuevoNodo.Derecho));

            return nuevoNodo;
        }

        private NODO<T> RotarDerecha(NODO<T> nodo)
        {
            NODO<T> nuevoNodo = nodo.Izquierdo;
            NODO<T> subNodo = nuevoNodo.Derecho;

            nuevoNodo.Derecho = nodo;
            nodo.Izquierdo = subNodo;

            nodo.Altura = 1 + Math.Max(Altura(nodo.Izquierdo), Altura(nodo.Derecho));
            nuevoNodo.Altura = 1 + Math.Max(Altura(nuevoNodo.Izquierdo), Altura(nuevoNodo.Derecho));

            return nuevoNodo;
        }
        public String Validez()
        {
            if (Raiz == null)
            {
                return ("Vacia");
            }
            else
            {
                if (Raiz.Izquierdo == null)
                {
                    if (Raiz.Derecho == null)
                    {
                        return (Convert.ToString(Raiz.Valor));
                    }
                    return (Convert.ToString(Raiz.Valor) + " / " + Recorrido(Raiz.Derecho, Raiz));
                }
                if (Raiz.Derecho == null)
                {
                    return (Recorrido(Raiz.Izquierdo, Raiz));
                }
                return (Recorrido(Raiz.Izquierdo, Raiz) + " / " + Recorrido(Raiz.Derecho, Raiz));
            }
        }

        public String Recorrido(NODO<T> actual, NODO<T> padre)
        {
            if (actual.Izquierdo == null)
            {
                if (actual.Derecho == null)
                {
                    return (Convert.ToString(actual.Valor));
                }
                return (Convert.ToString(actual.Valor) + " / " + Recorrido(actual.Derecho, actual));
            }

            if (actual.Derecho == null)
            {
                return (Recorrido(actual.Izquierdo, actual) + " / " + Convert.ToString(padre.Valor));
            }
            return (Recorrido(actual.Izquierdo, actual) + " / " + Convert.ToString(padre.Valor) + " / " + Recorrido(actual.Derecho, actual));
        }

    }
}
