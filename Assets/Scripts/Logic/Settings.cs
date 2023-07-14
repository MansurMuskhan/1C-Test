using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public const int ZONE_LINE = -4;
    public const int KEY = 5;

    [SerializeField] SettingsData[] settingsData;
    public static Dictionary<SettingType, float> settingsValues = new Dictionary<SettingType, float>();

    private void Awake()
    {
        //settingsData = new SettingsData[8];
        //for (int i = 0; i < settingsData.Length; i++)
        //{
        //    settingsData[i].UISlider = GameObject.Find($"Slider ({i})").GetComponent<Slider>();
        //    settingsData[i].UIText = settingsData[i].UISlider.transform.Find("TextMaximum").GetComponent<Text>();
        //    settingsData[i].Type = (SettingType)Enum.GetValues(typeof(SettingType)).GetValue(i);
        //}

        foreach (var i in settingsData)
        {
            i.UISlider.value = PlayerPrefs.GetFloat(KEY + i.Type.ToString(), i.UISlider.maxValue / 2);
            settingsValues.Add(i.Type, i.UISlider.value);
            i.UISlider.onValueChanged.AddListener((v) =>
            {
                PlayerPrefs.SetFloat(KEY + i.Type.ToString(), v);
                i.UIText.text = v.ToString();
                settingsValues[i.Type] = v;
            });
            i.UIText.text = i.UISlider.value.ToString();
        }
    }
}

public enum SettingType
{
    EnemiesQuantity, // оличество врагов, которое следует уничтожить дл€ победы (range int); при каждом запуске игры количество врагов выбираетс€ случайным образом из диапазона [min, max];
    SpawnRateTime, //“аймаут с которым враги по€вл€ютс€ (range float), следующий враг по€вл€етс€ через случайное число секунд из диапазона [min, max];
    EnemiesSpeed, //—корость движени€ врагов (range float), скорость каждого нового врага выбираетс€ случайно из диапазона [min, max];
    EnemiesHealth, // оличество здоровь€ врагов (int);
    RadiusDestruction, //–адиус поражени€(стрельбы) персонажа (float);
    PlayerFireRate, //—корость стрельбы персонажа (float);
    BulletDamage, //”рон от выстрела персонажа (int);
    BulletSpeed, //—корость пули (float).
}

[System.Serializable]
public struct SettingsData
{
    public SettingType Type;
    public Slider UISlider;
    public Text UIText;
}
