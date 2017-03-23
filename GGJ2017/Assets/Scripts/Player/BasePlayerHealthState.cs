﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripts.Core
{
    public abstract class BasePlayerHealthState : IPlayerHealthState
    {
        protected Player _player;
        
        public BasePlayerHealthState(Player player)
        {
            _player = player;
        }

        public abstract void SetupState();

        public abstract void UpdatelHealthState();

    }
}
