using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Level System/Level Data")]
public class LevelData : ScriptableObject
{
    public List<string> stageSceneNames;
    public int stageOrbsToCollect;
    public int currentLevel;
}
