using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    class NotCondition : ICondition
    {
        ICondition a;
        public NotCondition(ICondition a)
        {
            this.a = a;
        }

        public bool test(Game g, Entity e)
        {
            return !a.test(g, e);
        }
    }
}
