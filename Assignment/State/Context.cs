using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.State
{
    public class Context
    {
        private IState _state;

        // Constructor to set the initial state
        public Context(IState state)
        {
            this._state = state;
        }

        // Method to set a new state
        public void SetState(IState state)
        {
            this._state = state;
        }

        // Method to request the state to handle itself
        public void Request()
        {
            _state.Handle(this);
        }
    }
}
