using System;
using UnityEngine;

public class CoinPickuper : MonoBehaviour
{
    [SerializeField] private AudioSource _coinAudio;

    private Wallet _wallet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            coin.PickUp();
            _coinAudio.Play();
            _wallet.AddCoin();
        }
    }

    public void Init(Wallet wallet)
    {
        _wallet = wallet != null ? wallet : throw new ArgumentNullException(nameof(wallet));
    }
}
