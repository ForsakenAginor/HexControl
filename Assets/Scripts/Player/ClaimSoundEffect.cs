using UnityEngine;

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

    private void OnCellsClaimed()
    {
        _audioSource.Play();
    }
}
