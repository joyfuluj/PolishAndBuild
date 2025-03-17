using UnityEngine;

public class ButtonHooks : MonoBehaviour
{
    public void LoadNextScene()
    {
        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.gameStartClip);
        }
        SceneHandler.Instance.LoadNextScene();
    }

    public void ExitToMenu()
    {
        SceneHandler.Instance.LoadMenuScene();
    }
}
