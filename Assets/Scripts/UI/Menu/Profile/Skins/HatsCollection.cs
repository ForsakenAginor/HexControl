using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Menu.Profile.Skins
{
    public class HatsCollection : MonoBehaviour
    {
        [SerializeField] private Hat[] _hats;

        public IEnumerable<Hat> Hats => _hats;
    }
}