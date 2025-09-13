using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float maxInitialAngle = 0.67f;
    public float moveSpeed = 1f;

    public void Start(){
        InitialPush();
    }

    private void InitialPush(){
        Vector2 dir = Vector2.left;
        dir.y = Random.Range(-maxInitialAngle, maxInitialAngle);
        rb2d.linearVelocity = dir * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if (scoreZone){
            Debug.Log("Score Zone");
        }
    }
}
