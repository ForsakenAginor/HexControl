using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationHandler : MonoBehaviour, IWinner
{
    private const string IsMoving = nameof(IsMoving);
    private const string IsWon = nameof(IsWon);
    private const string IsDead = nameof(IsDead);

    private Animator _animator;
    private Mover _mover;
    private Claimer _claimer;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_playerInput.Direction == Vector3.zero)
            _animator.SetBool(IsMoving, false);
        else
            _animator.SetBool(IsMoving, true);
    }

    private void OnDestroy()
    {
        _claimer.Died -= OnDied;
    }

    public void Init(Claimer claimer, Mover mover)
    {
        _claimer = claimer != null ? claimer : throw new ArgumentNullException(nameof(claimer));
        _mover = mover != null ? mover : throw new ArgumentNullException(nameof(mover));
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
