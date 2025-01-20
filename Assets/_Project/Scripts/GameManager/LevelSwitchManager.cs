using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchManager : MonoBehaviour
{
    public LevelData currentLevelData;
    private int _registeredOrbCount = 0;

    public void RegisterOrbCollected()
    {
        _registeredOrbCount++;
    }

    public void LoadNextStage()
    {
        if (_registeredOrbCount >= currentLevelData.stageOrbsToCollect)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            int currentSceneIndex = currentLevelData.stageSceneNames.IndexOf(currentSceneName);

            if (currentSceneIndex >= 0 && currentSceneIndex < currentLevelData.stageSceneNames.Count - 1)
            {
                string nextSceneName = currentLevelData.stageSceneNames[currentSceneIndex + 1];
                LoadScene(nextSceneName);
            }
            else
            {
                Debug.Log("No more stages to load!");
            }
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
        LoadScene(SceneManager.GetActiveScene().name);
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
