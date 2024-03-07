using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Controllers;
using NecatiAkpinar.Enums;
using NecatiAkpinar.PhaseStates;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NecatiAkpinar.Managers
{
    [RequireComponent(requiredComponent: typeof(BoxCollider))]
    public class PhaseManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private InputState _inputState;
        private DecisionState _decisionState;
        private FillState _fillState;
        private IdleState _idleState;

        private BasePhaseState _currentState;

        private MatchActivatorController _matchActivator;

        private void Start()
        {
            _matchActivator = new MatchActivatorController(this);

            _inputState = new InputState(ChangeGameState);
            _decisionState = new DecisionState(ChangeGameState, _matchActivator, this);
            _fillState = new FillState(ChangeGameState, this);
            _idleState = new IdleState(ChangeGameState, this);

            StateInfoTransporter stateInfoTransporter = new StateInfoTransporter();
            ChangeGameState(PhaseStateType.Input, stateInfoTransporter); // Enable this after game state in in game state!
        }

        private void ChangeGameState(PhaseStateType stateType, StateInfoTransporter stateInfoTransporter)
        {
            switch (stateType)
            {
                case PhaseStateType.Input:
                    _currentState = _inputState;
                    break;
                case PhaseStateType.Decision:
                    _currentState = _decisionState;
                    break;
                case PhaseStateType.Fill:
                    _currentState = _fillState;
                    break;
                case PhaseStateType.Idle:
                    _currentState = _idleState;
                    break;
            }

            _currentState.Start(stateInfoTransporter);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_currentState is IPointerDownHandler handler) handler.OnPointerDown(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_currentState is IPointerUpHandler handler) handler.OnPointerUp(eventData);
        }
    }
}