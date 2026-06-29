namespace SintaxisTP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Automata> automatas = CatalogoAutomatas.CrearTodos();

            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                mostrarMenu(automatas);

                if (obtenerOpcionMenu(automatas.Count, out int opcionSeleccionada))
                {
                    if (opcionSeleccionada == 0)
                    {
                        continuar = false;
                    }
                    else
                    {
                        Automata elegido = automatas[opcionSeleccionada - 1];
                        evaluarCadena(elegido);
                    }
                }
                else
                {
                    Console.WriteLine("Opción inválida. Intente nuevamente...");
                    Console.ReadKey();
                }
            }
        }

        static void mostrarMenu(List<Automata> automatas)
        {
            Console.WriteLine("*************** AUTÓMATAS FINITOS ***************");
            Console.WriteLine("Elija un autómata para evaluar una cadena:");
            Console.WriteLine();

            for (int i = 0; i < automatas.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {automatas[i].Nombre}  ->  {automatas[i].Descripcion}");
            }

            Console.WriteLine("  0. Salir");
            Console.WriteLine();
            Console.Write("Opción: ");
        }

        static bool obtenerOpcionMenu(int cantidad, out int opcion)
        {
            bool todoBien = false;
            opcion = -1;

            string? opcionEscrita = Console.ReadLine();

            if (int.TryParse(opcionEscrita, out opcion))
            {
                if (opcion >= 0 && opcion <= cantidad)
                {
                    todoBien = true;
                }
            }

            return todoBien;
        }

        static void evaluarCadena(Automata automata)
        {
            Console.Clear();
            Console.WriteLine($"Autómata {automata.Nombre}: {automata.Descripcion}");
            Console.WriteLine();
            Console.Write("Escriba la cadena a evaluar: ");
            string cadena = Console.ReadLine() ?? "";

            ResultadoEvaluacion resultado = automata.Evaluar(cadena);

            Console.WriteLine();

            if (resultado.Aceptada)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"La cadena \"{cadena}\" SÍ pertenece al lenguaje {automata.Nombre}.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"La cadena \"{cadena}\" NO pertenece al lenguaje {automata.Nombre}.");

                switch (resultado.Motivo)
                {
                    case MotivoRechazo.SimboloSinTransicion:
                        Console.WriteLine($"Motivo: no hay transición para el símbolo '{resultado.Simbolo}' " +
                                          $"(posición {resultado.Posicion + 1}). La cadena no es válida desde ese punto.");
                        break;

                    case MotivoRechazo.EstadoNoFinal:
                        Console.WriteLine($"Motivo: la cadena se consumió por completo, pero el autómata terminó " +
                                          $"en el estado '{resultado.EstadoAlcanzado}', que no es un estado final.");
                        break;
                }
            }
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para volver al menú...");
            Console.ReadKey();
        }
    }
}
