using UnityEngine;

[CreateAssetMenu(fileName = "SoundsArchive", menuName = "ScriptableObjects/SoundsArchive", order = 2)]
public class SoundsArchive : ScriptableObject
{
    public AudioClipData[] clips;
    public AudioClipsRangeData[] clipsRange;
}