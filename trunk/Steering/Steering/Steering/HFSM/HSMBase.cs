using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.FSM;

namespace Steering.HFSM
{
    public class HSMBase
    {
        public struct UpdateResult
        {
            public List<IAction> actions;
            public ITransition transition;
            public int level;
        }

        public List<IAction> Actions
        {
            get { return new List<IAction>(); }
        }

        public UpdateResult Update()
        {
            UpdateResult result = new UpdateResult();
            result.actions = Actions;
            result.transition = null;
            result.level = 0;
            return result;
        }

        public virtual List<HState> GetStates()
        {
            throw new NotImplementedException();
        }
    }
}
