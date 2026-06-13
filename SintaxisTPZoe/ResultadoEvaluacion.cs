namespace SintaxisTPZoe
{
    public enum MotivoRechazo
    {
        Ninguno,
        SimboloSinTransicion,
        EstadoNoFinal         
    }


    public class ResultadoEvaluacion
    {
        public bool Aceptada { get; }
        public MotivoRechazo Motivo { get; }

        public int Posicion { get; }
        public char Simbolo { get; }

        public string? EstadoAlcanzado { get; }

        private ResultadoEvaluacion(bool aceptada, MotivoRechazo motivo, int posicion, char simbolo, string? estadoAlcanzado)
        {
            Aceptada = aceptada;
            Motivo = motivo;
            Posicion = posicion;
            Simbolo = simbolo;
            EstadoAlcanzado = estadoAlcanzado;
        }

        public static ResultadoEvaluacion Exito() =>
            new ResultadoEvaluacion(true, MotivoRechazo.Ninguno, -1, '\0', null);

        public static ResultadoEvaluacion SinTransicion(int posicion, char simbolo) =>
            new ResultadoEvaluacion(false, MotivoRechazo.SimboloSinTransicion, posicion, simbolo, null);

        public static ResultadoEvaluacion EstadoNoFinal(string estadoAlcanzado) =>
            new ResultadoEvaluacion(false, MotivoRechazo.EstadoNoFinal, -1, '\0', estadoAlcanzado);
    }
}
