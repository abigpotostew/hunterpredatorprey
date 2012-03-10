using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Actions
{
    class DebugPrintAction : IAction
    {
        string debugString;

        public DebugPrintAction(string s)
        {
            this.debugString = s;
        }
        public SteeringOutput execute(Game game, Entity character)
        {
            Console.WriteLine(debugString);
            return new SteeringOutput();
        }
    }
}
