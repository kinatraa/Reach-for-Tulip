using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGemController : MonoBehaviour
{
    private FountainController fountainController;
    private bool isInFountain = false;

    /*void Start()
    {
        fountainController = FindObjectOfType<FountainController>();
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fountain"))
        {
            fountainController = collision.gameObject.GetComponent<FountainController>();
            isInFountain = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fountain"))
        {
            isInFountain = false;
        }
    }

    private void OnMouseUp()
    {
        if (isInFountain)
        {
            fountainController.DropGem();
            Destroy(gameObject);
        }
    }
}
