using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.HFSM
{
    class SubMachineState : HierarchicalStateMachine
    {
        protected State state;

        public SubMachineState(Game g, State initialState)
            : base(g, initialState)
        {
            this.state = new State();
            initialState.parent = this;
        }

        public SubMachineState(Game g, State initialState, params State[] states)
            : base(g, initialState, states)
        {
            this.state = new State();
            initialState.parent = this;
            foreach (State s in states)
                s.parent = this;
        }

        public List<IAction> getActions()
        {
            return state.getAction();
            //return state.getAction();
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

        public override string ToString()
        {
            if (currentState != null)
            {
                return base.currentState.name;
            }
            else return "NULL SMS";
        }

        /*public override UpdateResult Update()
        {
            return base.Update();
        }*/
    }
}
