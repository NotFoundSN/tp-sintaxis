using System.Text;

namespace SintaxisTPZoe
{
    public class Automata
    {
        public string Nombre { get; }
        public string Descripcion { get; }
        public Estado EstadoInicial { get; }

        public Automata(string nombre, string descripcion, Estado estadoInicial)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            EstadoInicial = estadoInicial;
        }

        public bool Evaluar(string cadena)
        {
            Estado? actual = EstadoInicial;

            foreach (char simbolo in cadena)
            {
                actual = actual.Transicionar(simbolo);

                if (actual == null)
                {
                    return false;
                }
            }

            return actual.EsFinal;
        }

    }
}
