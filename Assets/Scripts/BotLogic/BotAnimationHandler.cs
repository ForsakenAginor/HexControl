using Assets.Scripts.BotLogic.StateMachine;
using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.BotLogic
{
    [RequireComponent(typeof(Bot))]
    internal class BotAnimationHandler : MonoBehaviour, IWinner
    {
        private const string IsMoving = nameof(IsMoving);
        private const string IsWon = nameof(IsWon);
        private const string IsDead = nameof(IsDead);

        [SerializeField] private Transform _modelTransform;
        [SerializeField] private Animator _animator;

        private Vector3 _previousPosition;
        private Bot _bot;

        private void Awake()
        {
            _bot = GetComponent<Bot>();
        }

        private void OnEnable()
        {
            _bot.StateChanged += OnStateChanged;
        }

        private void FixedUpdate()
        {
            if (_modelTransform.position != _previousPosition)
            {
                Vector3 lookPoint = _modelTransform.position - _previousPosition;
                _modelTransform.LookAt(_modelTransform.position + lookPoint);
            }

            _previousPosition = _modelTransform.position;
        }

        private void OnDisable()
        {
            _bot.StateChanged -= OnStateChanged;
        }

        public void BecomeWinner()
        {
            _animator.SetBool(IsWon, true);
        }

        private void OnStateChanged(State state)
        {
            switch (state)
            {
                case ReturningState:
                case ContestState:
                case MovingState:
                    _animator.SetBool(IsMoving, true);
                    break;
                case FinalState:
                    _animator.SetBool(IsMoving, false);
                    break;
                case DyingState:
                    _animator.SetBool(IsDead, true);
                    break;
                default:
                    break;
            }
        }
    }
}