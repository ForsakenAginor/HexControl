using UnityEngine;

namespace Assets.Scripts.UI.Menu.Profile.Skins
{
    [CreateAssetMenu(fileName = "Hat")]
    public class Hat : ScriptableObject
    {
        [field: SerializeField] public GameObject Model { get; private set; }

        [field: SerializeField] public Sprite Image { get; private set; }

        [field: SerializeField] public Hats Type { get; private set; }
    }
}