using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Hilos
{
    class Program
    {
        public delegate void DelegadoParametros(int numero, long otro);

        static void Main(string[] args)
        {
            HilosConObjetoThread();
            HilosConObjetoAction();
            HilosConObjetoTask();


            //Hilo principal
            for (int i = 0; i < 80; i++)
            {
                Console.WriteLine(string.Format("Hilo Principal {0}", i));
                //Thread.Sleep(1000); el Sleep solo se ejecuta en el hilo q se esta ejecutando no bloque el resto de hilos
            }


            Console.ReadLine();
        }

        private static void HilosConObjetoTask()
        {
            //Sin parametros con TASK
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 58; i++)
                {
                    Console.WriteLine(string.Format("Hilo en landa Task.Factory {0}", i));
                }
            });

            ListarTareasConEspera();
        }

        private static void HilosConObjetoThread()
        {
            //Sin parametros con Thread
            ThreadStart metodoHilo1 = new ThreadStart(RecorrerNumeros100);
            Thread hilo1 = new Thread(metodoHilo1);
            hilo1.Start();


        }

        private static void HilosConObjetoAction()
        {
            Action accioSimple = new Action(RecorrerNumeros100);
            accioSimple.Invoke();

            //Con parametros con Action
            Action<int> accion = new Action<int>(RecorrerNumero);
            Action<int, string> accionParametros = new Action<int, string>(VariosParametros);
            accion.Invoke(58);
            accionParametros.Invoke(1, "Erik");
        }

        private static void ListarTareasConEspera()
        {
            //Lista de tareas con Task y esperando q terminen todos los hilos de la lista
            List<Task> tareas = new List<Task>();
            tareas.Add(Task.Factory.StartNew(() =>
            {

                for (int i = 0; i < 18; i++)
                {
                    Console.WriteLine(string.Format("Como una lista de tareas tarea 1!! {0}", i));
                }
            }));

            tareas.Add(Task.Factory.StartNew(() =>
            {

                for (int i = 0; i < 18; i++)
                {
                    Console.WriteLine(string.Format("Como una lista de tareas tarea 2!! {0}", i));
                }
            }));
            //esta Hace q nunca se ejecute por q no se inicia y por ende nunca continuaria la ejecucion
            //tareas.Add(new Task(() => { Console.WriteLine("Otra tarea de la lista"); }));

            //Si no esta esta linea se ejecuta en diferentes hilos y no espera q terminen todas
            Task.WaitAll(tareas.ToArray());
            Console.WriteLine("Termino la lista de Tareas en Hilos!!!");
        }



        private static void RecorrerNumeros100()
        {
            for (int i = 0; i < 58; i++)
            {
                Console.WriteLine(string.Format("Metodo 1 {0}", i));
            }
        }

        private static void RecorrerNumero(int numero)
        {
            for (int i = 0; i < numero; i++)
            {
                Console.WriteLine(string.Format("Metodo 2 con parametro {0}", i));
            }
        }
        

        private static void VariosParametros(int uno, string dos)
        {
            Console.WriteLine(string.Format("{0} {1}", uno, dos));
        }
    }
}
