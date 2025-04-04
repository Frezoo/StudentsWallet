using UnityEngine;

public class EatLogic : MonoBehaviour
{
    public float Speed;
    void Start()
    {
        
    }

    void Update()
    {
        Movemnet();
    }

    void Movemnet()
    {
        transform.Translate(0, -Speed * Time.deltaTime, 0);
    }
}
