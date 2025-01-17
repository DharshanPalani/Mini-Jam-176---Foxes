using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchManager : MonoBehaviour
{
    public LevelData currentLevelData; 
    private int currentStageIndex = 0;

    private int _registeredOrbCount = 0;

    public void RegisterOrbCollected()
    {
        _registeredOrbCount++;
    }

    
    public void LoadNextStage()
    {
        if(_registeredOrbCount == currentLevelData.stageOrbsToCollect)
        {
            currentStageIndex++;
        
            LoadScene(currentLevelData.stageSceneNames[currentStageIndex]);
        }
        else
        {
            Debug.Log("Not all orbs collected!");
        }
    }

    
    public void CompleteLevel()
    {
        SceneManager.LoadScene(currentLevelData.currentLevel + 1);
    }

    
    public void RestartCurrentStage()
    {
        LoadScene(currentLevelData.stageSceneNames[currentStageIndex]);
    }

    
    private void LoadScene(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scene {sceneName} does not exist or is not added to the build settings!");
        }
    }
}
