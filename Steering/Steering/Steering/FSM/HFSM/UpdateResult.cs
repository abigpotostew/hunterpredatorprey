using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.HFSM
{
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
}
