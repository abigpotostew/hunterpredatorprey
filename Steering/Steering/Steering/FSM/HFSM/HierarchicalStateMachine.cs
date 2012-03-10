using System.Collections.Generic;
using Steering.HFSM;
using System;

namespace Steering.FSM.HFSM
{
    public class HierarchicalStateMachine : State
    {
        List<State> states;
        State initialState;
        protected State currentState;

        Game game;

        public HierarchicalStateMachine(Game g)
            :base()
        {
            this.game = g;
        }

        public HierarchicalStateMachine(Game g, State initialState, params State[] states)
            :this(g)
        {
            this.initialState = initialState;
            this.states = new List<State>();
            this.states.Add(initialState);
            initialState.parent = this;
            for (int i = 0; i < states.Length; ++i)
            {
                this.states.Add(states[i]);
                states[i].parent = this;
            }
        }

        public HierarchicalStateMachine(Game g, State initialState)
            : this(g)
        {
            this.initialState = initialState;
            this.states = new List<State>();
            this.states.Add(initialState);
            initialState.parent = this;
        }

        public override string ToString()
        {
            if (currentState != null)
            {
                if (currentState is SubMachineState)
                    return ((SubMachineState)currentState).ToString();
                else return currentState.name;
            }
            else return "NULL";
        }

        public override List<State> GetStates()
        {
            if ( currentState != null )
                return currentState.GetStates();
            else return new List<State>();
        }

        public override UpdateResult Update()
        {
            UpdateResult result;

            if (currentState == null)
            {
                
                currentState = initialState;
                result = new UpdateResult(currentState.getEntryAction(), null, 0);
                return result;
            }

            Transition triggeredTransition = null;
            foreach ( Transition t in currentState.getTransitions() )
            {
                if (t.isTriggered(game, game.lion))
                {
                    triggeredTransition = t;
                    break;
                }
            }

            if (triggeredTransition != null)
            {
                result = new UpdateResult();
                result.actions = new List<IAction>();
                result.transition = triggeredTransition;
                result.level = triggeredTransition.getLevel();
            }

            else
            {
                if (currentState is SubMachineState)
                    result = ((SubMachineState)currentState).Update();
                else 
                    result = currentState.Update();
            }

            if (result.transition != null)
            {
                State targetState;
                if (result.level == 0)
                {
                    targetState = result.transition.getTargetState();
                    foreach (IAction a in currentState.getExitAction())
                        result.actions.Add(a);
                    foreach (IAction a in result.transition.getActions())
                        result.actions.Add(a);
                    foreach (IAction a in targetState.getEntryAction())
                        result.actions.Add(a);

                    currentState = targetState;

                    foreach (IAction a in getActions())
                        result.actions.Add(a);

                    result.transition = null;
                }
                else if (result.level > 0)
                {
                    foreach (IAction a in currentState.getExitAction())
                        result.actions.Add(a);
                    currentState = null;

                    result.level -= 1;
                }
                else
                {
                    targetState = result.transition.getTargetState();
                    HierarchicalStateMachine targetMachine = targetState.parent;
                    foreach (IAction a in result.transition.getActions())
                        result.actions.Add(a);
                    foreach (IAction a in targetMachine.UpdateDown(targetState, -result.level-1))
                        result.actions.Add(a);

                    result.transition = null;

                }
            }
            else
            {
                //do normal action if there's no transition
                foreach (IAction a in getActions())
                    result.actions.Add(a);
                
            }


            return result;
        }

        public List<IAction> UpdateDown( State state, int level)
        {
            List<IAction> actions;
            if (level > 0)
                actions = parent.UpdateDown(this, level - 1);
            else actions = new List<IAction>();

            if (currentState != null)
                foreach (IAction a in currentState.getExitAction())
                    actions.Add(a);

            currentState = state;
            foreach (IAction a in state.getEntryAction())
                actions.Add(a);

            return actions;

        }

        
    }
}
