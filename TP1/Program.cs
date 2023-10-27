using System;
using System.Collections;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace tp1
{
    class Program
    {
        static void Main(string[] args)
        {

            ArbolBinario<int> raiz = new ArbolBinario<int>(10);
            ArbolBinario<int> HijoIzquierdo = new ArbolBinario<int>(4);
            ArbolBinario<int> HijoDerecho = new ArbolBinario<int>(5);

            // Agregamos hijos a la raiz
            raiz.agregarHijoIzquierdo(HijoIzquierdo);
            raiz.agregarHijoDerecho(HijoDerecho);
            //Agrego hijos a hijo izq
            HijoDerecho.agregarHijoDerecho(new ArbolBinario<int>(6));
            HijoIzquierdo.agregarHijoDerecho(new ArbolBinario<int>(10));
            HijoDerecho.agregarHijoIzquierdo(new ArbolBinario<int>(40));
            HijoIzquierdo.agregarHijoIzquierdo(new ArbolBinario<int>(50));

            raiz.RecorridoEntreNiveles(2, 2);



            double f = raiz.CalcularPromedioContenidoHojas(raiz);
            Console.WriteLine(f);


        }


    }


    public class ArbolBinario<T>
    {


        private T dato;
        private ArbolBinario<T> hijoIzquierdo;
        private ArbolBinario<T> hijoDerecho;


        public T Dato
        {
            set { this.dato = value; }
            get { return dato; }
        }
        public ArbolBinario(T dato)
        {
            this.dato = dato;
        }

        public T getDatoRaiz()
        {
            return this.dato;
        }

        public ArbolBinario<T> getHijoIzquierdo()
        {
            return this.hijoIzquierdo;
        }

        public ArbolBinario<T> getHijoDerecho()
        {
            return this.hijoDerecho;
        }

        public void agregarHijoIzquierdo(ArbolBinario<T> hijo)
        {
            this.hijoIzquierdo = hijo;
        }

        public void agregarHijoDerecho(ArbolBinario<T> hijo)
        {
            this.hijoDerecho = hijo;
        }

        public void eliminarHijoIzquierdo()
        {
            this.hijoIzquierdo = null;
        }

        public void eliminarHijoDerecho()
        {
            this.hijoDerecho = null;
        }

        public bool esHoja()
        {
            return this.hijoIzquierdo == null && this.hijoDerecho == null;
        }

        //=================================================== EJERCICIO 4 =====================================================

        public bool Incluye(T dat)
        {
            bool res = false; //bandera flag
            if (this.dato.Equals(dat)) //si el dato en el que estamos es igual al que pasamos por parametro, es true
            {
                return true;
            }
            else //sino recorremos, primero por la parte izquierda
            {

                if (hijoIzquierdo != null)
                {
                    res = hijoIzquierdo.Incluye(dat);
                }
                //si no se encontro en los hijos izquierdos (osea, false) y hay hijo derecho, se revisa si esta
                if (hijoDerecho != null && res == false)
                {
                    res = hijoDerecho.Incluye(dat);
                }

                return res;
            }


        }
        public void inorden()
        {

            if (hijoIzquierdo != null)
            {
                hijoIzquierdo.inorden();
            }
            Console.WriteLine(dato);
            if (hijoDerecho != null)
            {
                hijoDerecho.inorden();
            }

        }

        public void preorden()
        {
            Console.WriteLine(dato);
            if (hijoIzquierdo != null)
            {
                hijoIzquierdo.preorden();
            }
            if (hijoDerecho != null)
            {
                hijoDerecho.preorden();
            }
        }

        public void postorden()
        {
            if (hijoIzquierdo != null)
            {
                hijoIzquierdo.postorden();
            }
            if (hijoDerecho != null)
            {
                hijoDerecho.postorden();
            }
            Console.WriteLine(dato);
        }
        public void recorridoPorNiveles()
        {
            Cola<ArbolBinario<T>> cola = new Cola<ArbolBinario<T>>();
            ArbolBinario<T> ArbolAux;

            cola.encolar(this);
            if (!cola.esVacio())
            {
                ArbolAux = cola.desencolar();
                Console.WriteLine(dato);
                if (ArbolAux.hijoIzquierdo != null)
                {
                    cola.encolar(ArbolAux.hijoIzquierdo);

                }
                if (ArbolAux.hijoDerecho != null)
                {
                    cola.encolar(ArbolAux.hijoDerecho);
                }
            }
        }
        //=============================================================================================================================


        public double CalcularPromedioContenidoHojas(ArbolBinario<int> nodo)
        {
            // Comprobamos si el nodo es nulo (caso base)
            if (nodo == null)
            {
                return 0.0; // Si el nodo es nulo, no hay hojas, por lo que el promedio es 0.
            }

            int cantHojas = 0; // Contador para la cantidad de hojas
            double sumaHojas = 0; // Acumulador para la suma de los valores de las hojas

            // Exploramos el subárbol derecho (si existe)
            if (nodo.hijoDerecho != null)
            {
                cantHojas++; // Incrementamos la cantidad de hojas encontradas
                sumaHojas += CalcularPromedioContenidoHojas(nodo.hijoDerecho); // Recursivamente calculamos el promedio en el subárbol derecho
            }

            // Exploramos el subárbol izquierdo (si existe)
            if (nodo.hijoIzquierdo != null)
            {
                cantHojas++; // Incrementamos la cantidad de hojas encontradas
                sumaHojas += CalcularPromedioContenidoHojas(nodo.hijoIzquierdo); // Recursivamente calculamos el promedio en el subárbol izquierdo
            }

            // Si el nodo es una hoja, sumamos su valor
            if (nodo.hijoDerecho == null && nodo.hijoIzquierdo == null)
            {
                sumaHojas += nodo.Dato; // Sumamos el valor del nodo a la suma
                cantHojas++; // Incrementamos la cantidad de hojas encontradas
            }

            // Calculamos y retornamos el promedio, evitando la división por cero
            if (cantHojas > 0)
            {
                return (double)sumaHojas / cantHojas; // Calculamos el promedio
            }
            else
            {
                return 0.0; // En caso de que no haya hojas, evitamos la división por cero y retornamos 0.
            }
        }

        public int SumarContenidoHojas(ArbolBinario<int> nodo)
        {
            if (nodo == null)
            {
                return 0;
            }
            int cantHojas = 0;
            int sumaHojas = 0;

            if (nodo.hijoDerecho != null)
            {
                cantHojas++;
                sumaHojas += SumarContenidoHojas(nodo.hijoDerecho);
            }

            if (nodo.hijoIzquierdo != null)
            {
                sumaHojas += SumarContenidoHojas(nodo.hijoIzquierdo);
            }

            // Si el nodo es una hoja, retornamos su valor
            if (nodo.hijoDerecho == null && nodo.hijoIzquierdo == null)
            {
                sumaHojas += nodo.Dato;
            }

            return sumaHojas;
        }




        public int contarHojas()
        {
            int cuenta = 0;
            if (this.hijoDerecho == null && this.hijoIzquierdo == null)
            {
                cuenta = 1;
            }
            else
            {
                if (this.hijoDerecho != null)
                {
                    cuenta += hijoDerecho.contarHojas();
                }
                if (this.hijoIzquierdo != null)
                {
                    cuenta += hijoIzquierdo.contarHojas();
                }

            }
            return cuenta;
        }

        public void rerroridoPorNivelesConSeparador()
        {
            // primero generamos la cola y un arbol auxiliar que nos ayudara a desencolar 
            Cola<ArbolBinario<T>> c = new Cola<ArbolBinario<T>>();
            ArbolBinario<T> arbolAux;
            // luego el contador de nivel
            int nivel = 0;
            //encolamos la raiz y luego el separado
            c.encolar(this);
            c.encolar(null);
            // imprimimos el nivel
            Console.WriteLine("Nivel: " + nivel);
            while (!c.esVacio())
            {
                arbolAux = c.desencolar();
                if (arbolAux == null)
                {
                    if (!c.esVacio())
                    {
                        nivel++;
                        Console.WriteLine("\nNivel {0}", nivel);
                        c.encolar(null);
                    }
                }
                else
                {
                    Console.Write(arbolAux.dato + " ");
                    if (arbolAux.hijoIzquierdo != null)
                    {
                        c.encolar(arbolAux.hijoIzquierdo);
                    }
                    if (arbolAux.hijoIzquierdo != null)
                    {
                        c.encolar(arbolAux.hijoDerecho);
                    }
                }
            }
        }
        // ====================== RECORRIDO ENTRE NIVELES ========================================

        public void RecorridoEntreNiveles(int nivelN, int nivelM)
        {
            // Creamos una cola para realizar el recorrido por niveles
            Cola<ArbolBinario<T>> cola = new Cola<ArbolBinario<T>>();
            cola.encolar(this); // Agregamos el árbol raíz a la cola

            int nivelActual = 0; // Inicializamos el nivel actual

            while (!cola.esVacio())
            {
                int nodosEnNivelActual = cola.contar(); // Obtener la cantidad de nodos en el nivel actual

                // Procesar todos los nodos en el nivel actual
                for (int i = 0; i < nodosEnNivelActual; i++)
                {
                    ArbolBinario<T> nodoActual = cola.desencolar(); // Sacar el nodo de la cola

                    // Si el nodo actual está en el rango de niveles [nivelN, nivelM], imprimir su dato
                    if (nivelActual >= nivelN && nivelActual <= nivelM)
                    {
                        Console.Write(nodoActual.dato + " ");
                    }

                    // Agregar los hijos del nodo actual a la cola si existen
                    if (nodoActual.hijoIzquierdo != null)
                    {
                        cola.encolar(nodoActual.hijoIzquierdo);
                    }
                    if (nodoActual.hijoDerecho != null)
                    {
                        cola.encolar(nodoActual.hijoDerecho);
                    }
                }

                // Aumentar el nivel actual después de procesar todos los nodos en el nivel
                nivelActual++;

                // Si el nivel actual es mayor que nivelM, detener el recorrido
                if (nivelActual > nivelM)
                {
                    break;
                }
            }
        }



    }



    public class Cola<T>
    {
        private List<T> datos = new List<T>();

        public void encolar(T dato)
        {
            datos.Add(dato);
        }

        public T desencolar()
        {
            T dat = datos[0];
            datos.RemoveAt(0);
            return dat;
        }

        public T tope(T dato)
        {
            return datos[0];
        }

        public bool esVacio()
        {
            if (datos.Count == 0) return true;
            else return false;
        }

        public int contar()
        {
            return datos.Count;
        }

    }
}

