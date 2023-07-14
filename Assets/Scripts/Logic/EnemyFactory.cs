using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyType
{
    orge,
    troll
}

public class EnemyFactory : MonoBehaviour
{
    [SerializeField]
    bool _factoryEnable = false;

    [SerializeField]
    Transform[]
        _spawnPoints;

    float _timer;
    float _rateTime;
    [SerializeField] GameObject _enemyPrefab;

    private Dictionary<EnemyType, Func<int, Vector2, GameObject>> _enemyFactory;

    [SerializeField] EnemyDataArchive _enemyData;
    Quaternion _rot = Quaternion.identity;

    private void Awake()
    {
        ActionsService.ValuesUpdate += ()=> 
        { 
            _timer = Random.Range(1, Settings.settingsValues[SettingType.SpawnRateTime]); ValuesUpdate(); 
        };
    }

    void ValuesUpdate()
    {
        foreach (var item in _enemyData.ListOgre)
        {
            item.MoveSpeed = Random.Range(1, Settings.settingsValues[SettingType.EnemiesSpeed]);
            item.MaxHealth = Settings.settingsValues[SettingType.EnemiesHealth];
        }
        foreach (var item in _enemyData.ListTroll)
        {
            item.MoveSpeed = Random.Range(1, Settings.settingsValues[SettingType.EnemiesSpeed]);
            item.MaxHealth = Settings.settingsValues[SettingType.EnemiesHealth];
        }
    }

    private void Start()
    {
        _enemyFactory = new Dictionary<EnemyType, Func<int, Vector2, GameObject>>()
        {
            { EnemyType.orge, (level, pos) => CreateEnemy(_enemyData.ListOgre[level], pos) },
            { EnemyType.troll, (level, pos) => CreateEnemy(_enemyData.ListTroll[level], pos) },
        };
    }

    void Update()
    {
        if (_factoryEnable)
        {
            if (_rateTime < 0)
            {
                _rateTime = _timer;
                int p = Random.Range(0, _spawnPoints.Length);

                ValuesUpdate();
                Create(EnemyType.orge, 1, _spawnPoints[p].position);
            }

            _rateTime -= Time.deltaTime;
        }
    }

    public GameObject Create(EnemyType type, int level, Vector2 pos)
    {
        return _enemyFactory[type](level, pos);
    }

    public GameObject CreateEnemy(EnemyData enemy, Vector2 position)
    {
        GameObject obj = Instantiate(_enemyPrefab, position, _rot);
        Core.GetUnitByID(obj.transform.GetInstanceID())?.Unit_Init(enemy);
        return obj;
    }
}

[System.Serializable]
public class EnemyData
{
    public float MoveSpeed;
    public float MaxHealth;
}