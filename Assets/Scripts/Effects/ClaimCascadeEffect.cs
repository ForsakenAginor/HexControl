using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ClaimCascadeEffect : MonoBehaviour
{
    private readonly List<MeshFilter> _columns = new();
    private TextureAtlasReader _textureAtlasReader;
    private MeshFilter _prefab;
    private float _effectHeight;
    private float _effectDuration;
    private HexGridXZ<CellSprite> _grid;
    private EffectCreator _meshUpdater;
    private Conquestor _conquestor;
    private CellSprite _color;

    private bool isPlaying = false;
    private Vector3 _effectStartPosition;

    private void OnDestroy()
    {
        _conquestor.CellsClaimed -= OnCellsClaimed;
    }

    public void Init(HexGridXZ<CellSprite> grid, Conquestor conquestor, MeshFilter prefab, TextureAtlasReader atlas, float height, float duration)
    {
        _grid = grid != null ? grid : throw new ArgumentNullException(nameof(grid));
        _conquestor = conquestor != null ? conquestor : throw new ArgumentNullException(nameof(conquestor));
        _textureAtlasReader = atlas != null ? atlas : throw new ArgumentNullException(nameof(atlas));
        _prefab = prefab != null ? prefab : throw new ArgumentNullException(nameof(prefab));
        _effectHeight = height;
        _effectDuration = duration;
        _effectStartPosition = new Vector3(0, _effectHeight, 0);
        _color = conquestor.CellSprite;
        //
        //
        _meshUpdater = new(_textureAtlasReader.GetUVDictionary());

        for (int i = 0; i < _grid.Width; i++)
        {
            var column = Instantiate(_prefab, transform);
            column.name = i.ToString();
            _columns.Add(column);
        }

        _conquestor.CellsClaimed += OnCellsClaimed;
    }


    private void OnCellsClaimed(IEnumerable<Vector2Int> cells)
    {
        if (isPlaying)
            return;

        isPlaying = true;
        Dictionary<MeshFilter, Vector3[]> splittedCells = new();

        for (int i = 0; i < _columns.Count; i++)
        {
            var column = cells.Where(o => o.x == i).Select(o => _grid.GetCellWorldPosition(o) + _effectStartPosition).ToArray();

            if (column != null && column.Count() > 0)
                splittedCells.Add(_columns[i], column);
        }

        StartCoroutine(PlayAnimation(splittedCells));
    }

    private IEnumerator PlayAnimation(Dictionary<MeshFilter, Vector3[]> columns)
    {
        float delay = _effectDuration / columns.Count;
        WaitForSeconds waitBeetweenColumns = new(delay);
        WaitForSeconds waitAnimationEnd = new(_effectDuration);

        foreach (var column in columns.Keys)
        {
            column.transform.DOMove(column.transform.position - _effectStartPosition, _effectDuration).SetEase(Ease.Linear);
            _meshUpdater.UpdateMesh(column.mesh, columns[column], _color, _grid.CellSize);
            yield return waitBeetweenColumns;
        }

        yield return waitAnimationEnd;

        foreach (var column in columns.Keys)
        {
            column.mesh = new();
            column.transform.position = Vector3.zero;
        }

        isPlaying = false;
    }
}

