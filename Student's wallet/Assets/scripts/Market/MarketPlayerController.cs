using UnityEngine;

public class MarketPlayerController : MonoBehaviour
{
    public float Speed;
    public float LerpSpeed = 10f;

    private Vector2 direction;
    private GameObject player;
    public int Health = 3;


    public float DefaultScaleX = 0.96f;
    public float MaxScaleX = 0.96f;
    private float _currentScaleX;

    void Start()
    {
        player = gameObject;
        _currentScaleX = DefaultScaleX;
    }


    void Update()
    {
        Movement();
    }

    void Movement()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        player.transform.Translate(direction * Speed * Time.deltaTime);
        player.transform.localScale = new Vector3(GetScaleX(), player.transform.localScale.y, player.transform.localScale.z);

    }

    float GetScaleX()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float targetScaleX;


        targetScaleX = Mathf.Sign(horizontalAxis) * DefaultScaleX;


        _currentScaleX = Mathf.Lerp(_currentScaleX, targetScaleX, Time.deltaTime * LerpSpeed);
        return _currentScaleX;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Eat"))
        {
            Debug.Log("Get Eat");
            Destroy(collision.gameObject);
        }
        else
        {
            Debug.Log("Get BadEat");
            Health--;
            Destroy(collision.gameObject);
        }
    }
}

