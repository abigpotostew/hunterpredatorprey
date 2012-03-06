using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Conditions
{
    class OrCondition : ICondition
    {
        ICondition a, b;
        public OrCondition(ICondition a, ICondition b)
        {
            this.a = a;
            this.b = b;
        }

        public bool test(Game g, Entity e)
        {
            return a.test(g, e) || b.test(g, e);
        }
    }
}
