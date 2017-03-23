using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class InvulnerablePlayerHealthState : BasePlayerHealthState
    {
        private float _periodInvulnerable;
        public InvulnerablePlayerHealthState(Player player) : base(player)
        {
            _periodInvulnerable = 0;
        }

        public override void UpdatelHealthState()
        {
            if (_player.life > 1)
            {
                SetInvunerableState();

                _periodInvulnerable += Time.fixedDeltaTime;


                if (_periodInvulnerable > _player.timeInvunerable)
                {
                    SetVunerableState();
                    _player.HealthState.ChangeState( new NormalPlayerHealthState(_player) );
                    return;
                }
            }
            else
            {
                _player.HealthState.ChangeState( new DeadPlayerHealthState(_player) );
                return;
            }
        }

        public override void ChangeState(IPlayerHealthState newState)
        {
            _player.life--;

            base.ChangeState(newState);
        }

        private void SetInvunerableState()
        {
            if (_player.menina.activeSelf)
            {
                _player.menina.SetActive(false);
                _player.parteCorpor1.SetActive(false);
                _player.parteCorpor2.SetActive(false);
                _player.parteCorpor3.SetActive(false);
                _player.parteCorpor4.SetActive(false);
            }
            else
            {
                _player.menina.SetActive(true);
                _player.parteCorpor1.SetActive(true);
                _player.parteCorpor2.SetActive(true);
                _player.parteCorpor3.SetActive(true);
                _player.parteCorpor4.SetActive(true);
            }
        }

        private void SetVunerableState()
        {
            _player.parteCorpor1.SetActive(true);
            _player.parteCorpor2.SetActive(true);
            _player.parteCorpor3.SetActive(true);
            _player.parteCorpor4.SetActive(true);
            _player.menina.SetActive(true);
        }
    }
}
