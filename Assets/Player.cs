using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float maxXMovement = 1;
    [SerializeField] float width = 1;

    Vector2 movement;
    Vector3 startPos;

    float currentSpeed;

    Rigidbody2D rBody;
    Transform myTransform;

    private void Awake()
    {
        myTransform = transform;
        startPos = myTransform.position;
    }

    private void OnEnable()
    {
        currentSpeed = speed;
        OnGamePlayStatae(GamePlayManager.instance.GamePlayState);
        GamePlayManager.instance.OnGamePlayState += OnGamePlayStatae;
    }
    private void OnDisable()
    {
        GamePlayManager.instance.OnGamePlayState -= OnGamePlayStatae;
    }

    // Start is called before the first frame update
    void Start()
    {
        movement = Vector3.zero;
        rBody = GetComponent<Rigidbody2D>();

        width *= 0.5F;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x += Input.GetAxis("Horizontal") * Time.deltaTime * currentSpeed;
    }

    private void FixedUpdate()
    {
        movement = rBody.position + movement;
        movement.x = Mathf.Clamp(movement.x, -maxXMovement, maxXMovement);
        rBody.position = movement;
        movement = Vector2.zero;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Ball") return;

        Vector2 hitPoint;
        hitPoint = collision.contacts[0].point;
        hitPoint -= rBody.position;


        hitPoint.x /= width;
        hitPoint.y = 1;
        hitPoint.Normalize();

        Ball ball = collision.gameObject.GetComponent<Ball>();
        ball.AddDirection(hitPoint);

        Debug.Log(hitPoint);

    }

    public void OnGamePlayStatae(GamePlayState gamePlayState)
    {
        switch (gamePlayState)
        {
            case GamePlayState.Ready:
                myTransform.position = startPos;
                currentSpeed = 0F;
                break;
            case GamePlayState.Playing:
                currentSpeed = speed;
                break;
            default:
                currentSpeed = 0F; 
                break;
        }
    }
}
