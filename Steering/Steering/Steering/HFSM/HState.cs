using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.HFSM
{
    public class HState : HSMBase
    {

        public override List<HState> GetStates()
        {
            return this;
        }


    }
}
