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
        //Deer deer;
        //Game game;

        public FiniteStateMachine(/*Game game, Deer deer,*/ State intialS, params State[] state)
        {
            /*this.game = game;
            this.deer = deer;*/
            this.initalState = intialS;
            currentState = this.initalState;
            states = new List<State>();
            states.Add(intialS);

            foreach (State s in state)
            {
                states.Add(s);
            }
        }
        
        //pass deer into everything
        public List<IAction> UpdateFSM(Game g, Entity e)
        {
            Transition triggeredTransition = null;
            

            foreach (Transition t in currentState.getTransitions())
            {
                if (t.isTriggered(g,e))
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

        public String toString()
        {
            return currentState.name;
        }
    }
}
