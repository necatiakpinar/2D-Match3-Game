using System;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Enums;
using UnityEngine;

namespace NecatiAkpinar.PhaseStates
{
    public class IdleState : BasePhaseState
    {
        private Action<PhaseStateType, StateInfoTransporter> _changeStateCallback;
        private MonoBehaviour _monoBehaviour;

        public IdleState(Action<PhaseStateType, StateInfoTransporter> changeStateCallback, MonoBehaviour monoBehaviour)
        {
            _changeStateCallback = changeStateCallback;
            _monoBehaviour = monoBehaviour;
        }

        public override void Start(StateInfoTransporter stateInfoTransporter)
        {
        }

        public override void End()
        {
        }
    }
}