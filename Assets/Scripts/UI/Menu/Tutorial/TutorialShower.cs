using UnityEngine;

public class TutorialShower : MonoBehaviour
{
    [SerializeField] private Transform _holderCanvas;
    [SerializeField] private GameObject _tutorialPrefab;

    private void Start()
    {
        TutorialData tutorialData = new();

        if (tutorialData.IsTutorialCompleted == false)     
            Instantiate(_tutorialPrefab, _holderCanvas);        
    }
}
