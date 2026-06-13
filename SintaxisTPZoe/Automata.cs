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

        public ResultadoEvaluacion Evaluar(string cadena)
        {
            Estado actual = EstadoInicial;

            for (int i = 0; i < cadena.Length; i++)
            {
                char simbolo = cadena[i];
                Estado? siguiente = actual.Transicionar(simbolo);

                if (siguiente == null)
                {
                    
                    return ResultadoEvaluacion.SinTransicion(i, simbolo);
                }

                actual = siguiente;
            }

            return actual.EsFinal
                ? ResultadoEvaluacion.Exito()
                : ResultadoEvaluacion.EstadoNoFinal(actual.Nombre);
        }

    }
}
