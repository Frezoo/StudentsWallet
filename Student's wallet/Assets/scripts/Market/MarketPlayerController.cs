using UnityEngine;

public class MarketPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float Speed;
    public float LerpSpeed = 10f;
    private Vector2 direction;
    private GameObject player;

    [Header("Health")]
    public int Health = 3;
    public int MaxHealth = 3;

    [Header("Eating Stats")]
    public int GoodEatCount = 0;
    public int BadEatCount = 0;

    [Header("Scaling")]
    const  float DefaultScaleX = 0.96f;
    const  float MaxScaleX = 0.96f;
    private float currentScaleX;


    void Start()
    {
        player = gameObject;
        currentScaleX = DefaultScaleX;

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


        currentScaleX = Mathf.Lerp(currentScaleX, targetScaleX, Time.deltaTime * LerpSpeed);
        return currentScaleX;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Eat"))
        {
            Debug.Log("Get Eat");
            GoodEatCount++;
            Destroy(collision.gameObject);
        }
        else
        {
            Debug.Log("Get BadEat");
            BadEatCount++;
            Health--;
            Destroy(collision.gameObject);
            
        }
    }
}

