using System;
using System.Collections.Generic;
using System.Text;

namespace tp_sintaxys
{
    internal class transition
    {
        private node _state;
        private string _character;

        public transition(node state, string character)
        {
            this.state = state;
            this.character = character;
        }
        public transition()
        {
            this.state = new node();
            this.character = "";
        }

        public node state { 
            get { return _state; } 
            set { _state = value; } 
        }

        public string character { 
            get { return _character; } 
            set { _character = value; } 
        }
    }
}
