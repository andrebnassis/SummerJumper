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

        public override void SetupState()
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

        public override void UpdatelHealthState()
        {
            if (_player.life > 1)
            {
                SetInvunerableState();

                _periodInvulnerable += Time.fixedDeltaTime;


                if (_periodInvulnerable > _player.timeInvunerable)
                {
                    SetVunerableState();
                    _player.life--;
                    _player.ChangeHealthState( new NormalPlayerHealthState(_player) );
                    return;
                }
            }
            else
            {
                _player.life--;
                _player.ChangeHealthState( new DeadPlayerHealthState(_player) );
                return;
            }
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
