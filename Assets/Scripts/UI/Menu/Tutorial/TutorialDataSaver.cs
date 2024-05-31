using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TutorialDataSaver : MonoBehaviour
{
    [SerializeField] private GameObject _holderPanel;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(SaveData);
    }

    private void OnDisable()
    {
        _button.onClick.AddListener(SaveData);        
    }

    private void SaveData()
    {
        TutorialData.Instance.Save(true);
        _holderPanel.SetActive(false);
    }
}
