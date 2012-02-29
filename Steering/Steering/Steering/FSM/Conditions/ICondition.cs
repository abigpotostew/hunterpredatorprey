using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM
{
    public interface ICondition
    {
        bool test(Game g, Entity e);
    }
}
