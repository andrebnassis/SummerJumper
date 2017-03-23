using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Input
{
    public interface IGameInput
    {
        bool HoldJump();
        bool ReleaseJump();
    }
}
