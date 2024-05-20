using System.Collections.Generic;
using UnityEngine;

public class HatsCollection : MonoBehaviour
{
    [SerializeField] private Hat[] _hats;

    public IEnumerable<Hat> Hats => _hats;
}
