using System;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private Wallet _wallet;

    public void Init(Wallet wallet)
    {
        _wallet = wallet != null ? wallet : throw new ArgumentNullException(nameof(wallet));
        OnCoinAdded();
        _wallet.CoinAdded += OnCoinAdded;
    }

    private void OnDisable()
    {
        _wallet.CoinAdded -= OnCoinAdded;
    }

    private void OnCoinAdded()
    {
        _text.text = _wallet.Coins.ToString();
    }
}
