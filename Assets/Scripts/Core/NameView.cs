using TMPro;
using UnityEngine;

public class NameView : MonoBehaviour
{
    [SerializeField] private Conquestor _conquestor;
    [SerializeField] private TextMeshProUGUI _textField;

    private void Start()
    {
        _textField.text = _conquestor.Name;
    }
}
