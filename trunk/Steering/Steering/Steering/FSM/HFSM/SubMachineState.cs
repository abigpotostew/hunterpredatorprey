using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.HFSM
{
    class SubMachineState : HierarchicalStateMachine
    {
        protected State state;

        public SubMachineState(State initialState)
            : base(initialState)
        {
            this.state = new State();
        }

        public SubMachineState(State initialState, params State[] states)
            : base(initialState, states)
        {
            this.state = new State();
            foreach (State s in states)
                s.parent = this;
        }

        public List<IAction> getActions()
        {
            return state.getAction();
        }

        public List<State> getStates()
        {
            List<State> states = new List<State>();
            if (currentState != null)   
            {
                states.Add(state);
                foreach (State s in currentState.GetStates())
                    states.Add(s);
            }
            else
                states.Add(state);
                return states;

        }
    }
}
