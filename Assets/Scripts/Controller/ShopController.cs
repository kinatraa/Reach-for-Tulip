using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private bool check = false;
    private GameObject interactingObject;

    void Update()
    {
        if (interactingObject == null) return;
        if (check)
        {
            Vector3 newPosition = interactingObject.transform.position;
            newPosition.y = -1f;
            interactingObject.transform.position = newPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactingObject = collision.gameObject;

        if (collision.transform.position.y > -2.2f)
        {
            check = true;
        }
        else
        {
            check = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        check = false;
    }
}
