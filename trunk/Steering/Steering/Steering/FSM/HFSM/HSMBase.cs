using System.Collections.Generic;
using Steering.FSM;
using Steering.FSM.HFSM;

namespace Steering.HFSM
{
    public class HSMBase
    {

        public HierarchicalStateMachine parent;

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
