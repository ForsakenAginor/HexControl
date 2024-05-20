using UnityEngine;
using UnityEngine.UI;

public class MenuRoot : MonoBehaviour
{
    [Header("Player data")]
    [SerializeField] private PlayerDataView _playerDataView;
    [SerializeField] private int _playerBoostSpeedCost;
    [SerializeField] private float _playerBoostSpeedValue;
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _costMultiplier;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private SpeedBuyButtonHandler _buyButtonHandler;

    [Header("Skin system")]
    [SerializeField] private HatModelApplier _skinApplier;
    [SerializeField] private HatSkinChoser _skinChoser;
    [SerializeField] private Button _skinGetButton;
    [SerializeField] private HatsCollection _skinCollection;

    private void Start()
    {
        PlayerDataChanger playerDataChanger = new(_playerBoostSpeedValue, _playerBoostSpeedCost, _baseSpeed, _costMultiplier, _maxSpeed);
        _playerDataView.Init(playerDataChanger);
        _buyButtonHandler.Init(playerDataChanger);

        Hatter hatter = new(_skinCollection);
        _skinApplier.Init(hatter);
        _skinChoser.Init(hatter);
        SkinGetter skinGetter = new(hatter, _skinGetButton);
    }
}
