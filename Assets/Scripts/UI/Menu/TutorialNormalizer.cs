using UnityEngine;

public class TutorialNormalizer : MonoBehaviour
{
    [SerializeField] private GameObject _firstPanel;
    [SerializeField] private GameObject _thirdPanel;

    private void OnEnable()
    {
        _firstPanel.SetActive(true);
        _thirdPanel.SetActive(false);
    }
}
