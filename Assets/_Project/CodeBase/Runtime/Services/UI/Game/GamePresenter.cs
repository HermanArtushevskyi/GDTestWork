using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Entities.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.GameLoop.Interfaces;
using _Project.CodeBase.Runtime.Scenes.Interfaces;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using Level;

namespace _Project.CodeBase.Runtime.UI
{
    public class GamePresenter
    {
        private readonly GameView _view;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUpdate _update;
        private readonly IWaveSpawner _waveSpawner;
        private readonly IEnemySpawner _enemySpawner;
        private readonly LevelConfig _config;
        private IPlayer _player;

        public GamePresenter(GameView view, ISceneLoader sceneLoader, IUpdate update, IWaveSpawner waveSpawner, IEnemySpawner enemySpawner, LevelConfig config)
        {
            _view = view;
            _sceneLoader = sceneLoader;
            _update = update;
            _waveSpawner = waveSpawner;
            _enemySpawner = enemySpawner;
            _config = config;
        }

        public void BindButtons(IPlayer player)
        {
            _player = player;
            _view.RestartBtn.onClick.AddListener(Restart);
            _view.AttackBtn.onClick.AddListener(player.Attack);
            _view.SuperAttackBtn.onClick.AddListener(player.SuperAttack);

            _player.OnDeath += OnLose;
            _waveSpawner.AllWavesCleared += OnWin;
            
            _update.OnUpdate += Update;
        }

        private void OnWin()
        {
            _view.WinPanel.SetActive(true);
        }

        private void OnLose(IHasHealth obj)
        {
            _view.LosePanel.SetActive(true);
        }

        private void Update()
        {
            if (_player.CanSuperAttack == false)
            {
                _view.SuperAttackBtn.interactable = false;
            }
            else
            {
                _view.SuperAttackBtn.interactable = true;
            }

            _view.HealthText.text = $"HP: {_player.HP}";
            if (_waveSpawner.CurrentWave == _config.Waves.Length)
            {
                _view.WaveText.text = "All waves cleared!";
                return;
            }
            else
            {
                _view.WaveText.text = $"Wave: {_waveSpawner.CurrentWave + 1}/{_config.Waves.Length}";
            }
        }

        private void Restart()
        {
            _update.OnUpdate -= Update;
            _enemySpawner.AliveEnemies.Clear();
            _sceneLoader.LoadScene("SampleScene");
        }
    }
}