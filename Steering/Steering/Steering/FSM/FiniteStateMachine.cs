using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM
{
    public class FiniteStateMachine
    {
        List<State> states;
        State initalState;
        State currentState;

        public FiniteStateMachine(State intialS, params State[] state)
        {
            this.initalState = intialS;
            currentState = this.initalState;
            states = new List<State>();
            states.Add(intialS);

            foreach (State s in state)
            {
                states.Add(s);
            }
        }

        public List<IAction> UpdateFSM()
        {
            Transition triggeredTransition = null;
            

            foreach (Transition t in currentState.getTransitions())
            {
                if (t.isTriggered())
                {
                    triggeredTransition = t;
                    break;
                }
            }

            if (triggeredTransition != null)
            {
                State targetState = triggeredTransition.getTargetState();
                List<IAction> actions = new List<IAction>();
                foreach (IAction a in currentState.getExitAction())
                {
                    actions.Add(a);
                }
                foreach (IAction a in triggeredTransition.getActions())
                {
                    actions.Add(a);
                }
                foreach (IAction a in targetState.getEntryAction())
                {
                    actions.Add(a);
                }

                currentState = targetState;
                return actions;
            }
            else
            {
                return currentState.getAction();
            }


        }
    }
}
