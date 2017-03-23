using System;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class DeadPlayerActionState : BasePlayerActionState
    {
        private float timeChange = 0.0f;

        public DeadPlayerActionState(Player player):base(player)
        {
        }

        public override void SetupAction()
        {
            _player.animator.SetBool("isGameOver", true);
            _player.animator.SetBool("IsDead", true);
            timeChange = 0;
        }

        public override void UpdateAction()
        {
            if (timeChange > 0.0f)
            {
                _player.animator.SetBool("IsDead", false);
            }

            timeChange += Time.deltaTime;
        }
    }
}