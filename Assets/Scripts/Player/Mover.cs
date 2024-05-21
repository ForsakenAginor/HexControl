using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent (typeof(Claimer))]
public class Mover : MonoBehaviour
{
    private float _speed;
    private CellSprite _color;
    private CharacterController _characterController;
    private PlayerInput _playerInput;
    private HexGridXZ<CellSprite> _grid;
    private Claimer _claimer;
    private Transform _model;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _claimer = GetComponent<Claimer>();
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _claimer.Died += OnDied;
    }

    private void FixedUpdate()
    {
        if (_grid == null)
            return;

        Move();
    }

    private void OnDisable()
    {
        _claimer.Died -= OnDied;        
    }

    public void Init(Transform model, HexGridXZ<CellSprite> grid, CellSprite color, float speed)
    {
        _grid = grid != null ? grid : throw new ArgumentNullException(nameof(grid));
        _model = model != null ? model : throw new ArgumentNullException(nameof(model));
        _color = color;
        _speed = speed;
    }

    private void OnDied()
    {
        enabled = false;
    }

    private void Move()
    {
        if (_characterController == null)
            throw new NullReferenceException(nameof(_characterController));

        float hexFactor = 0.7f;
        Vector3 nextPoint = (transform.position + _playerInput.GetDirection() * _grid.CellSize * hexFactor);
        _model.LookAt(nextPoint);
        Vector2Int nextHex = _grid.GetXZ(nextPoint);

        if (_grid.IsValidGridPosition(nextHex) && ColorAssignment.GetEnemyColors(_color).Contains(_grid.GetGridObject(nextHex)) == false)
            _characterController.Move(_playerInput.GetDirection() * _speed * Time.deltaTime);
    }
}
