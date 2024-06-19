using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Claimer))]
    public class ClaimSoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private Claimer _claimer;

        private void Awake()
        {
            _claimer = GetComponent<Claimer>();
        }

        private void OnEnable()
        {
            _claimer.CellsClimed += OnCellsClaimed;
        }

        private void OnDisable()
        {
            _claimer.CellsClimed -= OnCellsClaimed;
        }

        private void OnCellsClaimed(IEnumerable<Vector2Int> nonmatterValue)
        {
            _audioSource.Play();
        }
    }
}