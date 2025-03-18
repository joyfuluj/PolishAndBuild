
using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private ParticleSystem brickDestroyEffect;

    [SerializeField] private GameObject[] hearts;
    [SerializeField] private TextMeshProUGUI gameOver;
    private int currentBrickCount;
    private int totalBrickCount;
    private int score;

    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
        score = 0; // Initialize score
        scoreUI.UpdateScore(score); // Set initial score

        //Update the array of hearts
        UpdateHeartsUI();
        gameOver.gameObject.SetActive(false); // Hide the Game Over text initially
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }

    private void FireBall()
    {
        ball.FireBall();
    }

   
        public void OnBrickDestroyed(Vector3 position)
    {
        // fire audio here
        // implement particle effect here
        // add camera shake here
        score++;
        if (scoreUI != null)
        {
            scoreUI.UpdateScore(score); // Update the UI safely
        }
        else
        {
            Debug.Log("ScoreUI is not assigned in GameManager!");
        }
        CameraShake.Shake(0.5f,0.3f);
        currentBrickCount--;
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");
        if (brickDestroyEffect != null)
    {
        ParticleSystem effect = Instantiate(brickDestroyEffect, position, Quaternion.identity);
        effect.Play();
        Destroy(effect.gameObject, effect.main.duration); 
    }


        if  (currentBrickCount == 0) SceneHandler.Instance.LoadNextScene();
    }


    public void KillBall()
    {
        maxLives--;
        // update lives on HUD here
        UpdateHeartsUI();
        // game over UI if maxLives < 0, then exit to main menu after delay
        if (maxLives <= 0)
        {
            // Show Game Over text and return to the main menu after a delay
            StartCoroutine(GameOverSequence());
        }
        else
        {
            ball.ResetBall();
        }
    }

    private void UpdateHeartsUI()
    {
        // Loop through the hearts array and enable/disable hearts based on maxLives
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < maxLives);
        }
    }

     private IEnumerator GameOverSequence()
    {
        Time.timeScale = 0f; //freezing the time

        // Show the Game Over UI
        gameOver.gameObject.SetActive(true);

        // Wait for 1.5 seconds
        yield return new WaitForSecondsRealtime(1.5f);

        // Unfreeze time and transition to the main menu
        Time.timeScale = 1f;
        SceneHandler.Instance.LoadMenuScene();
    }
}
