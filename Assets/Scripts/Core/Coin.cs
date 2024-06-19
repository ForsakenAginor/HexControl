using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Coin : MonoBehaviour
    {
        private void Start()
        {
            Vector3 rotation = new (180, 180, 0);
            float duration = 1f;
            int infinity = -1;

            transform.DORotate(rotation, duration).SetLoops(infinity).SetEase(Ease.Linear);
        }

        public void PickUp()
        {
            gameObject.SetActive(false);
        }
    }
}