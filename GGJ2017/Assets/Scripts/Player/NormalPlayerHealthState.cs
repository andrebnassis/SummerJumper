using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripts.Core
{
    public class NormalPlayerHealthState : BasePlayerHealthState
    {
        public NormalPlayerHealthState(Player player) : base(player)
        {
        }

        public override void UpdatelHealthState()
        {
            
        }

        public override void ChangeState(IPlayerHealthState newState)
        {
            if (newState is InvulnerablePlayerHealthState)
            {
                if (_player.life == 4)
                {
                    _player.boiaPato.SetActive(false);
                }
                else if (_player.life == 3)
                {
                    _player.boiaL.SetActive(false);
                }
                else if (_player.life == 2)
                {
                    _player.boiaR.SetActive(false);
                }
            }

            base.ChangeState(newState);
        }
    }
}
