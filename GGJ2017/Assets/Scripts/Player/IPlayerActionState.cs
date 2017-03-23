using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripts.Core
{
    public interface IPlayerActionState
    {
        void SetupAction();
        void UpdateAction();
    }
}
