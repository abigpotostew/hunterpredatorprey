using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM
{
	public interface ITransition
	{
        bool isTriggered(Game g, Entity e);
        State getTargetState();
        List<IAction> getActions();
	}
}
