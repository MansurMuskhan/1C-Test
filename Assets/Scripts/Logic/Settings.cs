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
    EnemiesQuantity, //���������� ������, ������� ������� ���������� ��� ������ (range int); ��� ������ ������� ���� ���������� ������ ���������� ��������� ������� �� ��������� [min, max];
    SpawnRateTime, //������� � ������� ����� ���������� (range float), ��������� ���� ���������� ����� ��������� ����� ������ �� ��������� [min, max];
    EnemiesSpeed, //�������� �������� ������ (range float), �������� ������� ������ ����� ���������� �������� �� ��������� [min, max];
    EnemiesHealth, //���������� �������� ������ (int);
    RadiusDestruction, //������ ���������(��������) ��������� (float);
    PlayerFireRate, //�������� �������� ��������� (float);
    BulletDamage, //���� �� �������� ��������� (int);
    BulletSpeed, //�������� ���� (float).
}

[System.Serializable]
public struct SettingsData
{
    public SettingType Type;
    public Slider UISlider;
    public Text UIText;
}
