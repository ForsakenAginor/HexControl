using UnityEngine;

public class TutorialShower : MonoBehaviour
{
    [SerializeField] private Transform _holderCanvas;
    [SerializeField] private GameObject _tutorialPrefab;

    private void Start()
    {
        if (TutorialData.Instance.IsTutorialCompleted == false)     
            Instantiate(_tutorialPrefab, _holderCanvas);        
    }
}
