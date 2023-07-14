using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataArchive", menuName = "ScriptableObjects/EnemyDataArchive", order = 2)]
public class EnemyDataArchive : ScriptableObject
{
    [SerializeField] private List<EnemyData> _listOgre;
    [SerializeField] private List<EnemyData> _listTroll;

    public List<EnemyData> ListOgre => _listOgre;
    public List<EnemyData> ListTroll => _listTroll;
}
