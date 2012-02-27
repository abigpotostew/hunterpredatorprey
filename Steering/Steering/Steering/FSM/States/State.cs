using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM
{
     public class State
    {
         List<IAction> actions, entryActions, exitActions;
         List<ITransition> transitions;
         Deer deer;
         Game game;

         public State(Game game, Deer deer, IAction entryAction, IAction action, IAction exitAction)
         {
             this.game = game;
             this.deer = deer;
             actions = new List<IAction>();
             entryActions = new List<IAction>(); 
             exitActions = new List<IAction>();
             transitions = new List<ITransition>();

             entryActions.Add(entryAction);
             actions.Add(action);
             exitActions.Add(exitAction);

            
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
