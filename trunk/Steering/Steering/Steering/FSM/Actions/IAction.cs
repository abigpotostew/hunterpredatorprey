using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM
{
    public interface IAction
    {
        SteeringOutput execute(Game game, Entity character);
    }
}
