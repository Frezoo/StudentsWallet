using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    [Header("Configuration")]
    public float DistanceBetweenDots;
    public float MaxSpeed;
    public float CurrentSpeed;
    private Manager mn;
   
    [Header("Borders")]
    public float LeftBorder;
    public float RightBorder;
    public float UpperBorder;
    public float FloorBorder;

    [Header("Other to Movement")]
    private Vector3 targertPosition;
    private bool isDeliving;
    private bool isMoving;


    private void Start()
    {
        mn = GameObject.Find("CourierManager").GetComponent<Manager>();
    }


    public void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3 tempPosition = transform.position + new Vector3(DistanceBetweenDots, 0, 0);

            if (!isMoving && TryToMove(tempPosition))
            {
                targertPosition = tempPosition;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                isMoving = true;
            }

        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 tempPosition = transform.position + new Vector3(-DistanceBetweenDots, 0, 0);
            if (!isMoving && TryToMove(tempPosition))
            {
                targertPosition = transform.position + new Vector3(-DistanceBetweenDots, 0, 0);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                isMoving = true;
            }

        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 tempPosition = transform.position + new Vector3(0, DistanceBetweenDots, 0);
            if (!isMoving && TryToMove(tempPosition))
            {
                targertPosition = transform.position + new Vector3(0, DistanceBetweenDots, 0);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                isMoving = true;
            }

        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 tempPosition = transform.position + new Vector3(0, -DistanceBetweenDots, 0);
            if (!isMoving && TryToMove(tempPosition))
            {
                targertPosition = transform.position + new Vector3(0, -DistanceBetweenDots, 0);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                isMoving = true;
            }

        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targertPosition, CurrentSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targertPosition) < 0.01f)
            {
                transform.position = targertPosition;
                isMoving = false;
            }
        }
    }

    public bool TryToMove(Vector3 trgP)
    {
        if (trgP.x <= LeftBorder || trgP.x >= RightBorder || trgP.y >= UpperBorder || trgP.y <= FloorBorder)
        {
            return false;
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Road"))
        {
            Color roadColor = collision.GetComponent<SpriteRenderer>().color;
            Debug.Log(roadColor);
            if (roadColor == Color.green)
            {
                CurrentSpeed = MaxSpeed;
            }
            if (roadColor == Color.yellow)
            {
                CurrentSpeed = MaxSpeed * 0.75f;
            }
            if (roadColor == Color.red)
            {
                CurrentSpeed = MaxSpeed * 0.5f;
            }


        }
        if (collision.CompareTag("Restourant"))
        {
            isDeliving = true;
            mn.Hint.text = "Доставьте заказ клиенту";
        }
        if (collision.CompareTag("Client") && isDeliving)
        {
            isDeliving = false;
            mn.SumbitOrder();
        }
    }

}