using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILinks : MonoBehaviour
{
    static UILinks i;
    private void Awake()
    {
        if (!i) i = this;
        else
        {
            Destroy(this);
            return;
        }
    }

    [SerializeField] GameObject _settingsPanel;
    public static GameObject SettingsPanel { get => i._settingsPanel; }
    [SerializeField] Text _playerHealthText;
    public static Text PlayerHealthText { get => i._playerHealthText; }
    [SerializeField] Text _enemyLeftText;
    public static Text EnemyLeftText { get => i._enemyLeftText; }
}
