using BotLogic;
using UnityEngine;

internal class BotAnimationHandler : MonoBehaviour, IWinner
{
    [SerializeField] private Transform _modelTransform;
    [SerializeField] private Animator _animator;

    private const string IsMoving = nameof(IsMoving);
    private const string IsWon = nameof(IsWon);
    private const string IsDead = nameof(IsDead);

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
        if (state is ReturningState || state is ContestState || state is MovingState)
            _animator.SetBool(IsMoving, true);
        else if (state is FinalState)
            _animator.SetBool(IsMoving, false);
        else if (state is DyingState)
            _animator.SetBool(IsDead, true);
    }
}
