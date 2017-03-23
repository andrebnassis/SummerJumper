using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripts.Core
{
    public abstract class BasePlayerHealthState : IPlayerHealthState, IEquatable<BasePlayerHealthState>
    {
        protected Player _player;
        
        public BasePlayerHealthState(Player player)
        {
            _player = player;
        }

        public abstract void SetupState();

        public abstract void UpdatelHealthState();

        public bool Equals(BasePlayerHealthState other)
        {
            return other.GetType().Name.Equals(this.GetType().Name);
        }
    }
}
