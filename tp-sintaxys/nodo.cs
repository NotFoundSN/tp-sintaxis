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
            this.name = "";
            this.isFinal = false;
            this._transitionList = new List<transition>();
        }
        public node(string name, bool isFinal)
        { 
            this.name = name;
            this.isFinal = isFinal;
            this._transitionList = new List<transition>();
        }
        public bool isFinal {
            get { return this._isFinal; }
            set { this._isFinal = value; }
        }

        public string name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public List<transition> transitionList {
            get { return this._transitionList.ToList(); }
        }

        public bool addJump(transition newTransition)
        {
            this._transitionList.Add(newTransition);
            return true;
        }

        public bool searchJump(string character, out transition existTransition)
        {
            bool exist = false;
            existTransition = this._transitionList.FirstOrDefault(actualTransition => actualTransition.character == character);
            if (existTransition != null)
            {
                exist = true;
            }
            return exist;
        }
    }
}
