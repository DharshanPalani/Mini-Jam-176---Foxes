using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchManager : MonoBehaviour
{
    public LevelData currentLevelData; 
    private int currentStageIndex = 0; 

    
    public void LoadNextStage()
    {
        currentStageIndex++;
        
        LoadScene(currentLevelData.stageSceneNames[currentStageIndex]);

        //if (currentStageIndex < currentLevelData.stageSceneNames.Count)
        //{
        //}
        //else
        //{
        //    Debug.Log("Level complete! Use CompleteStage() to load the next level.");
        //}
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
