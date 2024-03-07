using NecatiAkpinar.PhaseStates;
using UnityEngine;

namespace NecatiAkpinar.Abstracts
{
    public abstract class BasePhaseState
    {
        public abstract void Start(StateInfoTransporter stateInfoTransporter);
        public abstract void End();
    }
}