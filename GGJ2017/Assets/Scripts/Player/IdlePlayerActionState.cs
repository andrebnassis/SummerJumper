using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class IdlePlayerActionState : BasePlayerActionState
    {
        private float _idleTime;

        public IdlePlayerActionState(Player player) : base(player)
        {
        }

        public override void SetupAction()
        {
            _player.animator.SetBool("IsGround", true);
        }

        public override void UpdateAction()
        {
            if (_player.IsGrounded())
            {
                ChangeIdle();
            }
        }

        private void ChangeIdle()
        {
            _idleTime += Time.fixedDeltaTime;
            if (_idleTime > _player.Idle1MaxTime)
            {
                _idleTime = 0;
                _player.audioFx.PlayIdle();
                _player.animator.SetBool("Idle1", true);
            }
            else
            {
                _player.animator.SetBool("Idle1", false);
            }
        }
    }
}
