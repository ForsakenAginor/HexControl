using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBuyButtonHandler : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _costTextLabel;

    private void OnEnable()
    {
        _button.onClick.AddListener(BuySpeed);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(BuySpeed);        
    }

    private void BuySpeed()
    {
        PlayerData.Instance.TryBoostSpeed();
    }
}
