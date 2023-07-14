using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int _enemyLeft;
    public int EnemyLeft {
        get { return _enemyLeft; }
        private set
        {
            _enemyLeft = value;
            UILinks.EnemyLeftText.text = "Left: " + _enemyLeft;
        }
    }

    [SerializeField] Transform _zoneLineSprite;


    static GameManager i;
    private void Awake()
    {
        if (!i) i = this;
        else
        {
            Destroy(this);
            return;
        }
        //var all = FindObjectsOfType<MonoBehaviour>().OfType<IUnit>().ToArray();

        Init();
    }

    void Init()
    {
        _zoneLineSprite.position = new Vector2(0, Settings.ZONE_LINE);
        ActionsService.Win += Win;
        ActionsService.GameOver += GameOver;
        ActionsService.EnemyKill += EnemyKill;
        ActionsService.RestartGame += RestartGame;
        ActionsService.ValuesUpdate += () => { EnemyLeft = Random.Range(1, (int)Settings.settingsValues[SettingType.EnemiesQuantity]); };
    }

    private void Start()
    {
        ActionsService.RestartGame.Invoke();
    }

    async void Win()
    {
        await Task.Delay(1000);

        Time.timeScale = 0;
        MessageBox.Show("Win!", () => ActionsService.RestartGame.Invoke(), "Restart");
    }

    async void GameOver()
    {
        await Task.Delay(1000);

        Time.timeScale = 0;
        MessageBox.Show("Game Over!", () => ActionsService.RestartGame.Invoke(), "Restart");
    }

    void EnemyKill()
    {
        EnemyLeft--;

        if (EnemyLeft <= 0)
        {
            Win();
        }
    }

    void RestartGame()
    {
        ActionsService.ValuesUpdate.Invoke();
        Time.timeScale = 1;
        UILinks.SettingsPanel.SetActive(false);
    }
}
