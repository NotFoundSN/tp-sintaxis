using System;
using System.Collections.Generic;
using System.Text;

namespace tp_sintaxys
{
    internal class transition
    {
        private node _state;
        private char _character;

        public transition(node state, char character)
        {
            this._state = state;
            this._character = character;
        }
        public transition()
        {
            this._state = new node();
            this._character = ' ';
        }

        public node state { 
            get { return _state; } 
        }

        public char character { 
            get { return _character; } 
        }
    }
}
