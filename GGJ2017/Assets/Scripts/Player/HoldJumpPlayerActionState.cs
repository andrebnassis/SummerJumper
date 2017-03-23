using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class HoldJumpPlayerActionState : BasePlayerActionState
    {
        public HoldJumpPlayerActionState(Player player) : base(player)
        {
        }

        public override void SetupAction()
        {
            _player.Acumulate = 0;
            _player.animator.SetBool("LoadJump", true);
        }

        public override void UpdateAction()
        {
            if (_player.Acumulate <= 0)
            {
                _player.audioFx.PlayCharging();
            }

            if (_player.MaxAcumulate > _player.Acumulate)
            {
                _player.Acumulate += Time.fixedDeltaTime * _player.MaxAcumulate / 2;
            }
        }
    }
}
