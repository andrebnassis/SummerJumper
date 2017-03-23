using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scripts.Core
{
    public class DeadPlayerHealthState : BasePlayerHealthState
    {
        public DeadPlayerHealthState(Player player) : base(player)
        {
        }

        public override void SetupState()
        {
            _player.menina.SetActive(true);
            _player.parteCorpor1.SetActive(true);
            _player.parteCorpor2.SetActive(true);
            _player.parteCorpor3.SetActive(true);
            _player.parteCorpor4.SetActive(true);
            _player.ChangeActionState(new DeadPlayerActionState(_player));
        }

        public override void UpdatelHealthState()
        {

        }

    }
}
