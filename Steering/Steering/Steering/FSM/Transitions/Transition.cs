﻿using System;
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
        public int level;
        //Deer deer;
        //Game game;

        public Transition(/*Game game, Deer deer,*/ ICondition condition, State targetState, int level)
        {
            /*this.game = game;
            this.deer = deer;*/
            this.condition = condition;
            this.targetState = targetState;
            actions = new List<IAction>();
            this.level = level;
        }

        public bool isTriggered(Game g, Entity e)
        {
            return condition.test(g,e);
        }

        public State getTargetState()
        {
            return targetState;
        }
        public void addActions(IAction a)
        {
            actions.Add(a);
        }
        public List<IAction> getActions()
        {
            return actions;
        }

        public int getLevel()
        {
            return level;
        }
    }
}
