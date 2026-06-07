using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace tp_sintaxys
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string wordChain = "";
            string message = "";
            bool isValidWord = false;
            Console.WriteLine("Seleccione un automata");
            Console.WriteLine("1- automata 1");
            Console.WriteLine("2- automata 2");
            Console.WriteLine("3- automata 3");
            Console.WriteLine("4- salir");
            string automateOption = Console.ReadLine();
            if (automateOption == "1" || automateOption == "2" || automateOption == "3")
            {
                Console.WriteLine("Palabra a buscar");
                wordChain = Console.ReadLine();
                switch (automateOption) {
                    case "1":
                        isValidWord = AutomateA(wordChain,out message);
                        break;
                    case "2":
                        isValidWord = AutomateB(wordChain,out message);
                        break;
                    case "3":
                        isValidWord = AutomateC(wordChain,out message);
                        break;
                    default :
                        break;
                }
                if (isValidWord)
                {
                    Console.WriteLine("Palabra valida");
                }
                else
                {
                    Console.WriteLine(message);
                }
            }
        }

        static bool AutomateA(string word,out string error)
        {
            bool result = false;
            error = "";
            node q0 = new node("q0", false);
            node q1 = new node("q1", true);
            q0.addTransition('a', q0);
            q0.addTransition('b', q1);
            result = testWord(word, q0,out error);
            return result;
        }

        static bool AutomateB(string word, out string error)
        {
            bool result = false;
            error = "";
            node q0 = new node("q0", false);
            node q1 = new node("q1", false);
            node q2 = new node("q2", true);
            node q3 = new node("q3", true);
            node q4 = new node("q4", true);
            q0.addTransition('a', q1);
            q1.addTransition('b', q2);
            q2.addTransition('a', q1);
            q2.addTransition('c', q3);
            q2.addTransition('b', q4);
            q3.addTransition('c', q3);
            result = testWord(word, q0, out error);
            return result;
        }

        static bool AutomateC(string word, out string error)
        {
            bool result = false;
            error = "";
            node q0 = new node("q0", false);
            node q1 = new node("q1", true);
            node q2 = new node("q2", false);
            node q3 = new node("q3", false);
            q0.addTransition('a', q0);
            q0.addTransition('c', q1);
            q0.addTransition('b', q3);
            q1.addTransition('b', q1);
            q1.addTransition('c', q2);
            q2.addTransition('c', q1);
            q3.addTransition('b', q3);
            q3.addTransition('c', q1);
            result = testWord(word, q0, out error);
            return result;
        }

        static bool testWord(string word, node actualState, out string error)
        {
            error = "";
            bool isValidWord = true;
            try
            {
                int i = 0;
                while (isValidWord && i< word.Length)
                {
                    isValidWord = actualState.nextState(word[i], out actualState);
                    i++;
                }
                /*foreach (char letter in word)
                {
                    if (isValidWord)
                    { 
                        if (!actualState.nextState(letter, out actualState))
                        {
                            isValidWord = false;
                            break;
                        }
                    }
                }*/
                if (!isValidWord)
                {
                    error = "Palabra no valida";
                    return false;
                }
                else if (actualState.isFinal)
                {
                    return true;
                }
                else
                {
                    error = "El nodo donde finalizo no es un nodo terminal";
                    return false;
                }
            }
            catch (Exception e)
            {
                error = "Error no contemplado";
                return false;
            }
        }

    }
}
