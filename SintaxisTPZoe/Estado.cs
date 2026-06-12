namespace SintaxisTPZoe
{

    public class Estado
    {
        public string Nombre { get; }
        public bool EsFinal { get; set; }

        //es como una lista hacia donde puede ir y con que caracteres podria moverse hacia ese estado
        private readonly Dictionary<char, Estado> transiciones = new Dictionary<char, Estado>();

        public Estado(string nombre, bool esFinal = false)
        {
            Nombre = nombre;
            EsFinal = esFinal;
        }


        public void AgregarTransicion(char simbolo, Estado destino)
        {
            transiciones[simbolo] = destino;
        }


        public Estado? Transicionar(char simbolo)
        {
            return transiciones.TryGetValue(simbolo, out Estado? destino) ? destino : null;
        }
    }
}
