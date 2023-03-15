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

        public NODO<T> Buscar(T valor)
        {
            return BuscarRecursivo(Raiz, valor);
        }

        private NODO<T> BuscarRecursivo(NODO<T> nodo, T valor)
        {
            if (nodo == null || nodo.Valor.CompareTo(valor) == 0)
            {
                return nodo;
            }

            if (valor.CompareTo(nodo.Valor) < 0)
            {
                return BuscarRecursivo(nodo.Izquierdo, valor);
            }
            else
            {
                return BuscarRecursivo(nodo.Derecho, valor);
            }
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
                return RotarDerecha(nodo);
            }

            if (balance > 1 && valor.CompareTo(nodo.Izquierdo.Valor) > 0)
            {
                nodo.Izquierdo = RotarIzquierda(nodo.Izquierdo);
                return RotarDerecha(nodo);
            }

            if (balance < -1 && valor.CompareTo(nodo.Derecho.Valor) > 0)
            {
                return RotarIzquierda(nodo);
            }

            if (balance < -1 && valor.CompareTo(nodo.Derecho.Valor) < 0)
            {
                nodo.Derecho = RotarDerecha(nodo.Derecho);
                return RotarIzquierda(nodo);
            }

            return nodo;
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
    }
}
