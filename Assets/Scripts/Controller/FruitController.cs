using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dino"))
        {
            DinoController dino = collision.GetComponent<DinoController>();
            if (dino != null)
            {
                if (!dino.IsEating())
                {
                    dino.EatFruit();
                    MouseDrag mouseInput = GetComponent<MouseDrag>();
                    mouseInput.OnMouseUp();
                    Destroy(gameObject);
                }
            }
        }
    }

}
