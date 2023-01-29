using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float speed = 5;


    float currentSpeed;

    Vector2 movementDirection;
    float dot;

    Vector3 startPos;

    Transform myTransform;
    Rigidbody2D rBody;


    private void Awake() => startPos = transform.position;
    private void OnEnable()
    {
        movementDirection = Vector3.up;
        myTransform = transform;
        rBody = GetComponent<Rigidbody2D>();

        rBody.velocity = Vector3.zero; 
        rBody.angularVelocity = 0;


        OnGamePlayChange(GamePlayManager.instance.GamePlayState);
        GamePlayManager.instance.OnGamePlayState += OnGamePlayChange;
    }

    private void OnDisable() => GamePlayManager.instance.OnGamePlayState -= OnGamePlayChange;

    private void FixedUpdate()
    {
        movementDirection = rBody.velocity.normalized;
        dot = Vector3.Dot(Vector3.right, movementDirection);

        if(dot > 0.5F || dot < -0.5)
        {
            if (movementDirection.y >= 0)
                movementDirection.y = 1F;
            else
                movementDirection.y = -1F;

            movementDirection.Normalize();
            rBody.velocity = movementDirection;
        }

        rBody.velocity = rBody.velocity.normalized * currentSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null) damagable.Damage();
            return;
        }

        if (collision.gameObject.CompareTag("GameOverBlock"))
        {
            GamePlayManager.instance.GamePlayState = GamePlayState.GameOver;
            return;
        }
    }

    private void OnGamePlayChange(GamePlayState gamePlayState)
    {
        switch(gamePlayState)
        {
            case GamePlayState.Ready:
                myTransform.position = startPos;
                break;
            case GamePlayState.Playing:
                currentSpeed = speed;
                rBody.velocity = Vector3.up;
                break;
            default:
                currentSpeed = 0F;
                break;
        }
    }

    public void AddDirection(Vector2 direction) => rBody.velocity = direction * speed;
}
