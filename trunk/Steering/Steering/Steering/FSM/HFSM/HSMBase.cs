using System.Collections.Generic;
using Steering.FSM;
using Steering.FSM.HFSM;

namespace Steering.HFSM
{
    public class HSMBase
    {

        public HierarchicalStateMachine parent;

        public struct UpdateResult
        {
            public List<IAction> actions;
            public ITransition transition;
            public int level;
            public UpdateResult(List<IAction> actions, ITransition trans, int lvl)
            {
                this.actions = actions;
                this.transition = trans;
                this.level = lvl;
            }
            public UpdateResult(List<IAction> actions)
            {
                this.actions = actions;
                this.transition = null;
                this.level = 0;
            }
        }

        public List<IAction> getActions()
        {
            return new List<IAction>(); 
        }

        public virtual UpdateResult Update()
        {
            UpdateResult result = new UpdateResult();
            result.actions = getActions();
            result.transition = null;
            result.level = 0;
            return result;
        }

        public virtual List<State> GetStates()
        {
            return new List<State>();
        }
    }
}
