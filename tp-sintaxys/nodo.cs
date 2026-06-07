using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tp_sintaxys
{
    internal class node
    {
        private bool _isFinal;
        private string _name;
        private List<transition> _transitionList;

        public node ()
        {
            this._name = "";
            this._isFinal = false;
            this._transitionList = new List<transition>();
        }
        public node(string name, bool isFinal)
        { 
            this._name = name;
            this._isFinal = isFinal;
            this._transitionList = new List<transition>();
        }
        public bool isFinal {
            get { return this._isFinal; }
        }

        public string name
        {
            get { return this._name; }
        }

        public List<transition> transitionList {
            get { return this._transitionList.ToList(); }
        }

        public bool addTransition(char character, node state)
        {
            try
            {
                if (character != null && character != ' ')
                {
                    transition newTransition = new transition(state, character);
                    this._transitionList.Add(newTransition);
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                return false;
            }
        }

        public bool nextState(char character, out node nextState)
        {
            bool exist = false;
            nextState = null;
            transition existTransition = this._transitionList.FirstOrDefault(actualTransition => actualTransition.character == character);
            if (existTransition != null)
            {
                nextState = existTransition.state;
                exist = true;
            }
            return exist;
        }
    }
}
