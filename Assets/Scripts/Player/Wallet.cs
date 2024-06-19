using System;

namespace Assets.Scripts.Player
{
    public class Wallet
    {
        private int _coins;

        public event Action CoinAdded;

        public int Coins => _coins;

        public void AddCoin()
        {
            _coins++;
            CoinAdded?.Invoke();
        }
    }
}