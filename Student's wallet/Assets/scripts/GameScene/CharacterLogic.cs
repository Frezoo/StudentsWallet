using UnityEngine;

public class CharacterLogic : MonoBehaviour
{
    void Update()
    {
        float deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - gameObject.transform.position.x;
        if (deltaX >= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
