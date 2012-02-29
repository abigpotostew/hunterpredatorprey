using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    class AndCondition : ICondition
    {
        ICondition a, b;

        public AndCondition(ICondition a, ICondition b)
        {
            this.a = a;
            this.b = b;
        }
        public bool test(Game g, Entity e)
        {
            //if fear is less than a certain number then return true
            return a.test(g,e) && b.test(g,e);
        }
    }
}
