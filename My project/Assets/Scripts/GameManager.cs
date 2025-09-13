using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int scorePlayer1, scorePlayer2;
    public ScoreText scoreTextLeft, scoreTextRight;
    public System.Action onReset;
    
    [Header("Game End Settings")]
    public int winningScore = 5;
    public GameObject winMessagePanel;
    public TextMeshProUGUI winMessageText;
    
    private bool gameEnded = false;

    public void Awake(){
        if (instance){
            Destroy(gameObject);
        }
        else{
            instance = this;
        }
    }
    
    void Start(){
        // Hide win message panel at start
        if (winMessagePanel != null){
            winMessagePanel.SetActive(false);
        }
    }

    public void OnScoreZoneReached(int id){
        if (gameEnded) return; // Don't score if game is over
        
        onReset?.Invoke();

        if (id == 1){
            scorePlayer1++; // Player 1 scores when ball hits zone 1
        }
        if (id == 2){
            scorePlayer2++; // Player 2 scores when ball hits zone 2
        }
        
        UpdateScores();
        CheckForWinner();
    }

    public void UpdateScores(){
        scoreTextLeft.SetScore(scorePlayer1);
        scoreTextRight.SetScore(scorePlayer2);
    }
    
    private void CheckForWinner(){
        if (scorePlayer1 >= winningScore){
            EndGame("Player 1 Wins!");
        }
        else if (scorePlayer2 >= winningScore){
            EndGame("Player 2 Wins!");
        }
    }
    
    private void EndGame(string winnerMessage){
        gameEnded = true;
        
        // Stop the ball
        Ball ball = FindFirstObjectByType<Ball>();
        if (ball != null){
            ball.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
        
        // Show win message
        if (winMessagePanel != null){
            winMessagePanel.SetActive(true);
        }
        
        if (winMessageText != null){
            winMessageText.text = winnerMessage;
        }
        
        // Pause the game
        Time.timeScale = 0f;
    }
    
    public void RestartGame(){
        Time.timeScale = 1f;
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        gameEnded = false;
        
        UpdateScores();
        
        if (winMessagePanel != null){
            winMessagePanel.SetActive(false);
        }
        
        onReset?.Invoke();
    }
}
