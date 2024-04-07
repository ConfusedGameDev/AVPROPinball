    using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int Score { get; private set; }
    public int HighScore { get; private set; }

    public TextMeshPro scoreText;
    public TextMeshPro highScoreText;

    private const string highScoreKey = "HighScore";
    public Renderer[] ballsRenderers;

    int ballsRemaining = 3;

    private void Awake()
    {
        
        foreach (var ballRenderer in ballsRenderers)
        {
            ballRenderer.material.color = Color.green;
        }
        
       

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadHighScore();
        UpdateHighScoreText();
    }

    public void onReset()
    {
        ballsRemaining--;
        if (ballsRemaining >= 0)
            ballsRenderers[ballsRemaining].material.color = Color.red;
        else
        {
            foreach (var ballRenderer in ballsRenderers)
            {
                ballRenderer.material.color = Color.green;
            }
            ResetScore();
        }
            
    }

    private void LoadHighScore()
    {
        if (PlayerPrefs.HasKey(highScoreKey))
        {
            HighScore = PlayerPrefs.GetInt(highScoreKey);
        }
        else
        {
            HighScore = 0;
        }
    }
    [ContextMenu("Test")]
    public void testScore()
    {
        UpdateScore(Random.Range(10, 1250));
    }
    public void UpdateScore(int amount)
    {
        Score += amount;
        if (Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt(highScoreKey, HighScore);
            UpdateHighScoreText();
        }

        if (Score > 99999) // Clamp score to 99999
        {
            Score = 99999;
        }

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = Score.ToString("D5");
    }

    private void UpdateHighScoreText()
    {
        highScoreText.text = HighScore.ToString("D5");
    }

    public void ResetScore()
    {
        Score = 0;
        UpdateScoreText();
    }

    public void ResetHighScore()
    {
        HighScore = 0;
        PlayerPrefs.DeleteKey(highScoreKey);
        UpdateHighScoreText();
    }
}
