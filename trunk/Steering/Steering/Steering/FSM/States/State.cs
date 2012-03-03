using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.HFSM;

namespace Steering.FSM
{
     public class State : HSMBase
    {
         List<IAction> actions, entryActions, exitActions;
         List<ITransition> transitions;
         //Deer deer;
         //Game game;

         public State()
         {
             actions = new List<IAction>();
             entryActions = new List<IAction>();
             exitActions = new List<IAction>();
             transitions = new List<ITransition>();
         }

         public State(/*Game game, Deer deer,*/ IAction entryAction, IAction action, IAction exitAction)
             : this()
         {
             entryActions.Add(entryAction);
             actions.Add(action);
             exitActions.Add(exitAction);
         }

         public State(IAction action)
             : this()
         {
             actions.Add(action);
         }

         public override List<State> GetStates()
         {
             List<State> states = base.GetStates();
             states.Add(this);
             return states;
         }


         public List<IAction> getAction()
         {
             return actions;
         }
         public List<IAction> getEntryAction()
         {
             return entryActions;
         }
         public List<IAction> getExitAction()
         {
             return exitActions;
         }

         public List<ITransition> getTransitions()
         {
             return transitions;
         }

         public void addTransition(params ITransition[] t)
         {
             foreach (ITransition tran in t)
             {
                 transitions.Add(tran);
             }
         }
    }
}
