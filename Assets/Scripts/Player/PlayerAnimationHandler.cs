using System;
using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationHandler : MonoBehaviour, IWinner
    {
        private const string IsMoving = nameof(IsMoving);
        private const string IsWon = nameof(IsWon);
        private const string IsDead = nameof(IsDead);

        private Animator _animator;
        private Mover _mover;
        private Claimer _claimer;
        private IPlayerInput _playerInput;

        private void Awake()
        {
            //_playerInput = new PlayerInput();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_playerInput.GetDirection() == Vector3.zero)
                _animator.SetBool(IsMoving, false);
            else
                _animator.SetBool(IsMoving, true);
        }

        private void OnDestroy()
        {
            _claimer.Died -= OnDied;
            _playerInput.DisposeDisposable();
        }

        public void Init(Claimer claimer, Mover mover, IPlayerInput input)
        {
            _claimer = claimer != null ? claimer : throw new ArgumentNullException(nameof(claimer));
            _mover = mover != null ? mover : throw new ArgumentNullException(nameof(mover));
            _playerInput = input != null ? input : throw new ArgumentNullException(nameof(input));
            _claimer.Died += OnDied;
        }

        public void BecomeWinner()
        {
            _animator.SetBool(IsWon, true);
            _mover.enabled = false;
        }

        private void OnDied()
        {
            _animator.SetBool(IsDead, true);
        }
    }
}