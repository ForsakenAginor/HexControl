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

        float longRangeFactor = 0.7f;
        float closeRangeFactor = 0.15f;
        Vector3 longRangePoint = (transform.position + _playerInput.GetDirection() * _grid.CellSize * longRangeFactor);
        Vector3 closeRangePoint = (transform.position + _playerInput.GetDirection() * _grid.CellSize * closeRangeFactor);
        _model.LookAt(longRangePoint);
        Vector2Int longRangeHex = _grid.GetXZ(longRangePoint);
        Vector2Int closeRangeHex = _grid.GetXZ(closeRangePoint);
        bool isLongRangeValidHex = _grid.IsValidGridPosition(longRangeHex) && ColorAssignment.GetEnemyColors(_color).Contains(_grid.GetGridObject(longRangeHex)) == false;
        bool isCloseRangeValidHex = _grid.IsValidGridPosition(closeRangeHex) && ColorAssignment.GetEnemyColors(_color).Contains(_grid.GetGridObject(closeRangeHex)) == false;

        if (isCloseRangeValidHex && isCloseRangeValidHex)
            _characterController.Move(_playerInput.GetDirection() * _speed * Time.deltaTime);
    }
}
