using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class JumpPlayerActionState : BasePlayerActionState
    {
        public JumpPlayerActionState(Player player) : base(player)
        {
        }

        public override void SetupAction()
        {
            _player.audioFx.StopCharging();
            _player.audioFx.PlayJump();
            _player.Jump();
            _player.animator.SetBool("LoadJump", false);
            _player.animator.SetBool("IsGround", false);
        }

        public override void UpdateAction()
        {
            Debug.Log("UpdateAction - "+_player.IsGrounded());
            if (_player.IsGrounded())
            {
                _player.ChangeActionState(new IdlePlayerActionState(_player));
            }
            else
            {
                _player.animator.SetBool("IsHigh", _player.IsHigh());
            }
        }
    }
}
