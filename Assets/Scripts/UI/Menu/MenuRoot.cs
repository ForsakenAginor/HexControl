using Assets.Scripts.UI.Menu.Profile;
using Assets.Scripts.UI.Menu.Profile.Skins;
using Lean.Localization;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu
{
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
        [SerializeField] private RectTransform _hatChosePanel;
        [SerializeField] private float _hatChosePanelMinAnchor;
        [SerializeField] private int _hatCost = 30;
        [SerializeField] private TMP_Text _hatCostText;

        [Header("Localization")]
        private readonly string _russian = "Russian";
        private readonly string _english = "English";
        private readonly string _turkish = "Turkish";
        [SerializeField] private Button _toEnglish;
        [SerializeField] private Button _toTurkish;
        [SerializeField] private Button _toRussian;
        

        private void Start()
        {
            Hatter hatter = new (_skinCollection);

            PlayerDataChanger playerDataChanger = new (_playerBoostSpeedValue, _playerBoostSpeedCost,
                _baseSpeed, _costMultiplier, _maxSpeed, hatter, _hatCost);
            _playerDataView.Init(playerDataChanger);
            _buyButtonHandler.Init(playerDataChanger);

            _skinApplier.Init(hatter);
            _skinChoser.Init(hatter);
            SkinGetter skinGetter = new (hatter, _skinGetButton, playerDataChanger);
            SkinMenuExtender skinMenuExtender = new (skinGetter, _hatChosePanel, _hatChosePanelMinAnchor);
            _hatCostText.text = _hatCost.ToString();

            _toEnglish.onClick.AddListener(ChangeLanguageToEnglish);
            _toTurkish.onClick.AddListener(ChangeLanguageToTurkish);
            _toRussian.onClick.AddListener(ChangeLanguageToRussian);
        }

        private void OnDestroy()
        {
            _toEnglish.onClick.RemoveListener(ChangeLanguageToEnglish);
            _toTurkish.onClick.RemoveListener(ChangeLanguageToTurkish);
            _toRussian.onClick.RemoveListener(ChangeLanguageToRussian);
        }

        private void ChangeLanguageToRussian()
        {
            SetLanguage(_russian);
        }

        private void ChangeLanguageToTurkish()
        {
            SetLanguage(_turkish);
        }

        private void ChangeLanguageToEnglish()
        {
            SetLanguage(_english);
        }

        private void SetLanguage(string language)
        {
            LeanLocalization.SetCurrentLanguageAll(language);
            LeanLocalization.UpdateTranslations();
        }
    }
}