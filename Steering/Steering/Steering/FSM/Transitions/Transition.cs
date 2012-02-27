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
        Deer deer;
        Game game;

        public Transition(Game game, Deer deer, ICondition condition, State targetState)
        {
            this.game = game;
            this.deer = deer;
            this.condition = condition;
            this.targetState = targetState;
        }

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
