namespace tp_sintaxys
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string wordChain = "";
            string message;
            Console.WriteLine("Palabra a buscar");
            wordChain = Console.ReadLine();
            if (AutomateA(wordChain,out message))
            {
                Console.WriteLine("Palabra valida");
            }
            else
            {
                Console.WriteLine(message);
            }
        }

        static bool AutomateA(string word,out string error)
        {
            error = "";
            bool result = false;
            node q0 = new node("q0", false);
            node q1 = new node("q1", true);
            transition q0q0 = new transition(q0,"a");
            transition q0q1 = new transition(q1, "b");
            transition actualTransition = new transition();
            q0.addJump(q0q0);
            q0.addJump(q0q1);
            
            node actual = q0;
            try {
                foreach (char letter in word) {
                    
                    if (actual.searchJump(letter.ToString(),out actualTransition))
                    {
                        actual = actualTransition.state;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (actual.isFinal)
                {
                    return true;
                }
                else
                {
                    error = "El nodo donde finalizo no es un nodo terminal";
                    return false;
                }
            }
            catch (Exception e) {
                error = "Palabra no valida";
                return false;
            }
        }


    }
}
