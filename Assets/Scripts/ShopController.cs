using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private bool canSell = false;
    private GameObject interactingObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactingObject = collision.gameObject;
        if (collision.transform.position.y > -2.2f)
        {
            canSell = false;
        }
        else
        {
            canSell = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canSell = false;
    }

    private void OnMouseUp()
    {
        if(interactingObject == null) return;
        if(!canSell){
            Vector3 newPosition = interactingObject.transform.position;
            newPosition.y = -3f;
            interactingObject.transform.position = newPosition;
        }
    }
}
