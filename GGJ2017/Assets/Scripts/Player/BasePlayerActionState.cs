using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripts.Core
{
    public abstract class BasePlayerActionState : IPlayerActionState, IEquatable<BasePlayerActionState>
    {
        protected Player _player;

        public BasePlayerActionState(Player player)
        {
            _player = player;
        }

        public abstract void SetupAction();
        public abstract void UpdateAction();

        public bool Equals(BasePlayerActionState other)
        {
            return this.GetType().Name.Equals(other.GetType().Name);
        }
    }
}
