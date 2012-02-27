using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM
{
    class Transition : ITransition
    {
        public List<IAction> actions;
        public State targetState;
        public ICondition condition;

        public bool isTriggered()
        {
            return condition.test();
        }

        public State getTargetState()
        {
            return targetState;
        }

        public List<IAction> getActions()
        {
            return actions;
        }
    }
}
