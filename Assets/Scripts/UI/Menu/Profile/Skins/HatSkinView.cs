using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu.Profile.Skins
{
    public class HatSkinView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _activeLabel;

        public event Action<HatSkinView> Selected;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_activeLabel.activeSelf)
                return;

            _activeLabel.SetActive(true);
            Selected?.Invoke(this);
        }

        public void Init(Sprite sprite)
        {
            _image.sprite = sprite != null ? sprite : throw new ArgumentNullException(nameof(sprite));
        }

        public void Select()
        {
            _activeLabel.SetActive(true);
        }

        public void Deselect()
        {
            _activeLabel.SetActive(false);
        }
    }
}