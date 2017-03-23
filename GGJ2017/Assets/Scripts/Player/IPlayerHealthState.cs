using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripts.Core
{
    public interface IPlayerHealthState
    {
        void ChangeState(IPlayerHealthState newState);
        void UpdatelHealthState();
    }
}
