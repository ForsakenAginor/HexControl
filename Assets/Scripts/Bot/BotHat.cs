using UnityEngine;

public class BotHat : MonoBehaviour
{
    [SerializeField] private Transform _hatSocket;
    [SerializeField] private GameObject _hatModel;

    private void Start()
    {
        Instantiate(_hatModel, _hatSocket);
    }
}
