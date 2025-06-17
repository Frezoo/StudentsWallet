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
    const float DefaultScaleX = 3.9964f;
    const float MaxScaleX = 3.9964f;
    private float currentScaleX;

    private Camera mainCamera;

    void Start()
    {
        player = gameObject;
        currentScaleX = DefaultScaleX;
        mainCamera = Camera.main;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        player.transform.Translate(direction * Speed * Time.deltaTime);

        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp(viewPos.x, 0f, 1f);
        transform.position = mainCamera.ViewportToWorldPoint(viewPos);

        player.transform.localScale = new Vector3(GetScaleX(), player.transform.localScale.y, player.transform.localScale.z);
    }

    float GetScaleX()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float targetScaleX;

        if (horizontalAxis != 0)
        {
            targetScaleX = Mathf.Sign(horizontalAxis) * DefaultScaleX;
        }
        else
        {
            targetScaleX = currentScaleX; 
        }

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
        else if (collision.CompareTag("BadEat"))
        {
            Debug.Log("Get BadEat");
            BadEatCount++;
            Health--;
            Destroy(collision.gameObject);
        }
    }
}