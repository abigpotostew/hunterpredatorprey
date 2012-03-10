using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.HFSM;
using Steering.FSM.HFSM;

namespace Steering.FSM
{
     public class State : HSMBase
    {
         public List<IAction> actions, entryActions, exitActions;
         List<ITransition> transitions;
         public String name;
         //Deer deer;
         //Game game;

         public State()
         {
             actions = new List<IAction>();
             entryActions = new List<IAction>();
             exitActions = new List<IAction>();
             transitions = new List<ITransition>();
         }

         public State(/*Game game, Deer deer,*/  String name, IAction entryAction, IAction action, IAction exitAction)
             : this()
         {
             this.name = name;
             entryActions.Add(entryAction);
             actions.Add(action);
             exitActions.Add(exitAction);
         }

         public State(String name, IAction action)
             : this()
         {
             this.name = name;
             actions.Add(action);
         }
         public State(String name, IAction action1, IAction action2)
             : this()
         {
             this.name = name;
             actions.Add(action1);
             actions.Add(action2);
         }
         public override List<State> GetStates()
         {
             List<State> states = base.GetStates();
             states.Add(this);
             return states;
         }
         public void AddActions(params IAction[] actions)
         {
             for (int i = 0; i < actions.Length; ++i)
             {
                 this.actions.Add(actions[i]);
             }
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


         public override UpdateResult Update()
         {
             return new UpdateResult(getAction(), null, 0);
         }
    }
}
