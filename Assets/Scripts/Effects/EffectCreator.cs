using System;
using System.Collections.Generic;
using Assets.Scripts.HexGrid;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class EffectCreator
    {
        private readonly IReadOnlyDictionary<CellSprite, UVCoordinates> _spriteUVDictionary;

        public EffectCreator(IReadOnlyDictionary<CellSprite, UVCoordinates> dictionary)
        {
            _spriteUVDictionary = dictionary != null ? dictionary : throw new ArgumentNullException(nameof(_spriteUVDictionary));
        }

        public void UpdateMesh(Mesh mesh, Vector3[] cells, CellSprite color, float cellSize)
        {
            MeshData meshData = MeshUtils.CreateEmptyMeshArrays(cells.Length);
            Vector3 quadSize = new Vector3(1, 0, 1) * cellSize;

            for (int i = 0; i < cells.Length; i++)
                MeshUtils.AddToMeshArraysXZ(meshData, i, cells[i], 0f, quadSize, _spriteUVDictionary[color]);

            mesh.vertices = meshData.Vertices;
            mesh.triangles = meshData.Triangles;
            mesh.uv = meshData.UVs;
        }
    }
}