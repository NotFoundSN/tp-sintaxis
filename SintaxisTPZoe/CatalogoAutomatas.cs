namespace SintaxisTPZoe
{
    public static class CatalogoAutomatas
    {
        /*No agregue un archivo tipo json o demas porque no vimos nada de eso como para 
         * cargar los automatas de alguna otra manera. Ademas con objetos quedaba lindo.
        */
        public static List<Automata> CrearTodos()
        {
            return new List<Automata>
            {
                CrearL3(),
                CrearL4(),
                CrearL5()
            };
        }

        public static Automata CrearL3()
        {
            Estado q0 = new Estado("q0");
            Estado q1 = new Estado("q1", esFinal: true);

            q0.AgregarTransicion('a', q0);
            q0.AgregarTransicion('b', q1); 
            
            return new Automata("L3", "{ aⁿb | n ≥ 0 }", q0);
        }

        public static Automata CrearL4()
        {
            Estado q0 = new Estado("q0");                 
            Estado q1 = new Estado("q1");                 
            Estado q2 = new Estado("q2", esFinal: true);  
            Estado q3 = new Estado("q3");                 
            Estado q4 = new Estado("q4", esFinal: true);  

            q0.AgregarTransicion('a', q1);
            q1.AgregarTransicion('b', q2);

            q2.AgregarTransicion('a', q1); 
            q2.AgregarTransicion('c', q3); 
            q2.AgregarTransicion('b', q4); 

            q3.AgregarTransicion('c', q3);


            return new Automata("L4", "{ (ab)ⁿ · (cᵖ | b) | n ≥ 1, p ≥ 0 }", q0);
        }

        public static Automata CrearL5()
        {
            Estado q0 = new Estado("q0");
            Estado q1 = new Estado("q1", true);
            Estado q2 = new Estado("q2");
            Estado q3 = new Estado("q3");

            q0.AgregarTransicion('a', q0);
            q0.AgregarTransicion('c', q1);
            q0.AgregarTransicion('b', q3);

            q1.AgregarTransicion('b', q1);
            q1.AgregarTransicion('c', q2);

            q2.AgregarTransicion('c', q1);

            q3.AgregarTransicion('b', q3);
            q3.AgregarTransicion('c', q1);


            return new Automata("L5", "{ aⁿ·w | w ∈ {b,c}*, w con impar de 'c'; n ≥ 0 }", q0);
        }
    }
}
