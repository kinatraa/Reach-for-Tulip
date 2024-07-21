using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private FountainController fountainController;
    private WitchController witchController;
    private bool onCollision = false;
    private bool hasBeenEaten = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.CompareTag("Gem"))
        {
            if (collision.CompareTag("Witch"))
            {
                witchController = collision.GetComponent<WitchController>();
                onCollision = true;
            }
        }
        else if (this.name == "flame(Clone)")
        {
            if (collision.CompareTag("Witch"))
            {
                witchController = collision.GetComponent<WitchController>();
                onCollision = true;
            }
        }
        else if (this.name == "basic gem(Clone)")
        {
            if (collision.CompareTag("Fountain"))
            {
                fountainController = collision.gameObject.GetComponent<FountainController>();
                onCollision = true;
            }
        }
        else if (this.CompareTag("Fruit"))
        {
            if (hasBeenEaten)
            {
                return;
            }

            if (collision.CompareTag("Dino"))
            {
                DinoController dino = collision.GetComponent<DinoController>();
                if (dino != null)
                {
                    if (!dino.IsEating())
                    {
                        hasBeenEaten = true;
                        dino.EatFruit();
                        MouseDrag mouseInput = GetComponent<MouseDrag>();
                        mouseInput.OnMouseUp();
                        Destroy(gameObject);
                    }
                }
            }
            else if (collision.CompareTag("Finn"))
            {
                FinnController finn = collision.GetComponent<FinnController>();
                if (finn.GetAteFruits() < 2 && finn.IsWaiting())
                {
                    hasBeenEaten = true;
                    finn.EatFruit();
                    MouseDrag mouseInput = GetComponent<MouseDrag>();
                    mouseInput.OnMouseUp();
                    if (finn.GetAteFruits() == 2)
                    {
                        finn.LetsGoFinn();
                    }
                    Destroy(gameObject);
                }
            }
        }
        else if (this.CompareTag("Orb"))
        {
            if (collision.CompareTag("Tulip"))
            {
                TulipController tulip = collision.GetComponent<TulipController>();
                if (!tulip.IsLoved())
                {
                    tulip.SetLove(true);
                    MouseDrag mouseInput = GetComponent<MouseDrag>();
                    mouseInput.OnMouseUp();
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.CompareTag("Gem"))
        {
            if (collision.CompareTag("Witch"))
            {
                onCollision = false;
            }
        }
        else if (this.name == "flame(Clone)")
        {
            if (collision.CompareTag("Witch"))
            {
                onCollision = false;
            }
        }
        else if (this.name == "basic gem(Clone)")
        {
            if (collision.CompareTag("Fountain"))
            {
                onCollision = false;
            }
        }
    }

    private void OnMouseUp()
    {
        if (onCollision)
        {
            if (this.name == "basic gem(Clone)")
            {
                fountainController.DropGem();
                Destroy(gameObject);
            }
            else if ((this.CompareTag("Gem") && witchController.GetGemAmount() < 2)
                    || (this.name == "flame(Clone)" && witchController.GetFlameAmount() < 1))
            {
                if (!witchController.IsCasting())
                {
                    witchController.TakeObject(gameObject);
                    Destroy(gameObject);
                }
            }

        }
    }
}
