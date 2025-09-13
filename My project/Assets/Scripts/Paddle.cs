using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public int id;
    public float moveSpeed = 2f;
    private Vector3 startPosition;
    
    public void Start(){
        startPosition = transform.position;
        GameManager.instance.onReset += ResetPosition;
    }

    public void ResetPosition(){
        transform.position = startPosition;
    }

    public void Update(){
        float movement =ProcessInput();
        Move(movement);
    }

    public float ProcessInput(){
        float movement = 0f;

        switch (id){
            case 0:
                movement = Input.GetAxis("MovePlayer1");
                break;
            case 1:
                movement = Input.GetAxis("MovePlayer2");
                break;
        }
        return movement;
    }

    public void Move(float movement){
        Vector2 velo = rb2d.linearVelocity;
        velo.y = moveSpeed * movement;
        rb2d.linearVelocity = velo;
    }
}
