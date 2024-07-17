using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    private ChompController chompController;
    private bool inChomp = false;

    private void OnMouseUp()
    {
        if (inChomp)
        {
            chompController.ResetChomp();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chomp"))
        {
            chompController = collision.GetComponent<ChompController>();
            inChomp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Chomp"))
        {
            chompController = null;
            inChomp = false;
        }
    }
}
