using System.Linq;
using Agava.YandexGames;
using Assets.Scripts.Advertise;
using Assets.Scripts.BotLogic;
using Assets.Scripts.Core;
using Assets.Scripts.Effects;
using Assets.Scripts.Effects.PopupEffect;
using Assets.Scripts.HexGrid;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Menu.LevelSystem;
using Assets.Scripts.UI.Menu.Profile.Skins;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Root : MonoBehaviour
    {
        [Header("Grid")]
        [SerializeField] private int _gridWidth;
        [SerializeField] private int _gridHeight;
        [SerializeField] private float _gridCellSize;
        [SerializeField] private MeshUpdater _meshUpdater;

        [Header("Bots")]
        [SerializeField] private BotStateMachineInitializer[] _bots;

        [Header("Player")]
        [SerializeField] private PlayerInitializer _player;

        [Header("UI")]
        [SerializeField] private GameObject _losingPanel;
        [SerializeField] private GameObject _winingPanel;
        [SerializeField] private WalletView[] _walletViews;
        [SerializeField] private SceneChanger[] _mainMenuButtons;
        [SerializeField] private SceneChanger[] _restartButtons;
        [SerializeField] private SceneChanger _nextLevelButton;

        [Header("VirtualCameras")]
        [SerializeField] private GameObject _followCamera;
        [SerializeField] private GameObject _celebratingCamera;

        [Header("ClaimCascadeEffects")]
        [SerializeField] private MeshFilter _prefab;
        [SerializeField] private float _effectHeight;
        [SerializeField] private float _effectDuration;

        [Header("PopupEffects")]
        [SerializeField] private PopupEffectView _popupEffectPrefab;
        [SerializeField] private Transform _parentCanvas;

        [Header("Skin logic")]
        [SerializeField] private HatsCollection _hatsCollection;
        [SerializeField] private HatModelApplier _hatModelApplier;

        [Header("Other")]
        [SerializeField] private CoinSpawner _coinSpawner;
        [SerializeField] private ConquestMonitor _conquestMonitor;
        [SerializeField] private PlaneCreator _planeCreator;
        [SerializeField] private Silencer _silencer;

        private Claimer _playerClaimer;
        private LevelUnlocker _levelUnlocker;

        private void Awake()
        {
            HexGridXZ<CellSprite> grid = new (_gridWidth, _gridHeight, _gridCellSize, Vector3.zero);
            _meshUpdater.Init(grid);
            TextureAtlasReader atlas = _meshUpdater.GetComponent<TextureAtlasReader>();

            _conquestMonitor.Init(grid);

            foreach (var bot in _bots)
            {
                bot.Init(grid);
                Conquestor conquestor = bot.GetComponent<Conquestor>();
                _conquestMonitor.AddConquestor(conquestor);
                var gameObject = new GameObject();
                gameObject.AddComponent<ClaimCascadeEffect>().Init(grid, conquestor, _prefab, atlas, _effectHeight, _effectDuration);
            }

            Wallet wallet = new ();
            _player.Init(grid, wallet);

            foreach (var walletView in _walletViews)
                walletView.Init(wallet);

            _playerClaimer = _player.GetComponent<Claimer>();
            Conquestor playerConquestor = _player.GetComponent<Conquestor>();
            var effect = new GameObject();
            effect.AddComponent<ClaimCascadeEffect>().Init(grid, playerConquestor, _prefab, atlas, _effectHeight, _effectDuration);
            _conquestMonitor.AddConquestor(playerConquestor);

            PartCalculator partCalculator = new (_playerClaimer, grid.Height * grid.Width);
            gameObject.AddComponent<PopupEffectPool>().Init(partCalculator, _popupEffectPrefab, _parentCanvas);

            CoinsSaver coinSaver = new (wallet, _conquestMonitor, _playerClaimer);

            VirtualCameraSwitcher uselessVariable = new (_followCamera, _celebratingCamera, _conquestMonitor);

            _coinSpawner.Init(grid, _bots.Select(o => o.GetComponent<Bot>()).ToArray());

            _planeCreator.Init(grid);

            Hatter hatter = new (_hatsCollection);
            _hatModelApplier.Init(hatter);

            InitializeUI();

            StickyAd.Show();
            InterstitialAdvertiseShower advertiseShower = new (_silencer);

            if (SceneManager.GetActiveScene().name != Scenes.FirstLevel.ToString())
                advertiseShower.ShowAdvertise();

            Time.timeScale = 0f;
        }

        private void OnEnable()
        {
            _playerClaimer.Died += OnPlayerDied;
            _conquestMonitor.PlayerWon += OnPlayerWon;
            _conquestMonitor.BotWon += OnBotWon;
        }

        private void OnDisable()
        {
            _playerClaimer.Died -= OnPlayerDied;
            _conquestMonitor.PlayerWon -= OnPlayerWon;
            _conquestMonitor.BotWon -= OnBotWon;
        }

        private void OnPlayerWon(int nonmatterValue)
        {
            _levelUnlocker.UnlockNext();
            _winingPanel.SetActive(true);
        }

        private void OnBotWon()
        {
            _losingPanel.SetActive(true);
        }

        private void OnPlayerDied()
        {
            Time.timeScale = 0f;
            _losingPanel.SetActive(true);
        }

        private void InitializeUI()
        {
            _levelUnlocker = new ();

            foreach (var sceneChanger in _mainMenuButtons)
                sceneChanger.Init(Scenes.MainMenu);

            foreach (var sceneChanger in _restartButtons)
                sceneChanger.Init(_levelUnlocker.CurrentScene);

            _nextLevelButton.Init(_levelUnlocker.NextScene);
        }
    }
}