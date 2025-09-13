using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float maxInitialAngle = 0.67f;
    public float moveSpeed = 1f;
    public float maxStartY = 4f;
    public GameManager gameManager;
    public float speedMultiplier = 1.1f;
    public float maxSpeed = 15f;

    private float startX = 0f;

    public void Start(){
        InitialPush();
        GameManager.instance.onReset += ResetBall;
    }

    private void ResetBall(){
        ResetBallPosition();
        InitialPush();
    }

    private void InitialPush(){
        Vector2 dir = Random.value < 0.5f ? Vector2.left : Vector2.right;

        dir.y = Random.Range(-maxInitialAngle, maxInitialAngle);
        rb2d.linearVelocity = dir * moveSpeed;
    }

    private void ResetBallPosition(){
        float posY = Random.Range(-maxStartY, maxStartY);
        Vector2 position = new Vector2(startX, posY);
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if (scoreZone){
            GameManager.instance.OnScoreZoneReached(scoreZone.id);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        Paddle paddle = collision.collider.GetComponent<Paddle>();
        if(paddle){
            // Calculate bounce direction based on where ball hits paddle
            Vector2 ballPos = transform.position;
            Vector2 paddlePos = collision.transform.position;
            
            // Calculate hit position (-1 to 1, where 0 is center of paddle)
            float hitFactor = (ballPos.y - paddlePos.y) / collision.collider.bounds.size.y;
            
            // Determine bounce direction (left or right based on which paddle)
            Vector2 bounceDirection;
            if (collision.transform.position.x < 0) {
                // Left paddle - bounce right
                bounceDirection = new Vector2(1, hitFactor).normalized;
            } else {
                // Right paddle - bounce left  
                bounceDirection = new Vector2(-1, hitFactor).normalized;
            }
            
            // Apply consistent speed with slight increase, capped at maxSpeed
            float newSpeed = Mathf.Min(moveSpeed * speedMultiplier, maxSpeed);
            rb2d.linearVelocity = bounceDirection * newSpeed;
        }
    }
}
