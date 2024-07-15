using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    private float startPosX, startPosY;
    private Vector3 mousePos;
    private bool isDragging = false;
    private static bool isDraggingStatic = false;
    private DinoController dinoController;
    private TreeController treeController;
    private FinnController finnController;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        dinoController = GetComponent<DinoController>();
        treeController = GetComponent<TreeController>();
        finnController = GetComponent<FinnController>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            objectWidth = sr.bounds.size.x / 2;
            objectHeight = sr.bounds.size.y / 2;
        }
        else
        {
            objectWidth = 0.5f;
            objectHeight = 0.5f;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);

            newPosition.x = Mathf.Clamp(newPosition.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
            newPosition.y = Mathf.Clamp(newPosition.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

            this.gameObject.transform.position = newPosition;
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosX = mousePos.x - this.transform.position.x;
            startPosY = mousePos.y - this.transform.position.y;
            isDragging = true;
            isDraggingStatic = true;
            if (dinoController != null)
            {
                dinoController.IsDragged(true);
            }
            if (treeController != null)
            {
                treeController.SetInteraction(true);
            }
            if (finnController != null)
            {
                finnController.SetDrag(true);
            }
        }
        /*else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("right");
            if (!this.gameObject.CompareTag("Fountain"))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                startPosX = mousePos.x - this.transform.position.x;
                startPosY = mousePos.y - this.transform.position.y;
                Destroy(gameObject);
            }
        }*/
    }

    private void OnMouseUp()
    {
        isDragging = false;
        isDraggingStatic = false;
        if (dinoController != null)
        {
            dinoController.IsDragged(false);
        }
        if (treeController != null)
        {
            treeController.SetInteraction(false);
        }
        if (this.gameObject.transform.position.y <= -2.2f)
        {
            SellItem();
        }
        if (finnController != null)
        {
            finnController.SetDrag(false);
        }
    }

    private void SellItem()
    {
        if (gameObject.CompareTag("Dino"))
        {
            PlayerManager.money += Constants.Value.GetValue("Dino");
        }
        else if (gameObject.CompareTag("Gem"))
        {
            PlayerManager.money += Constants.Value.GetValue("Gem");
        }
        else
        {
            PlayerManager.money += Constants.Value.GetValue(gameObject.name);
        }
        Destroy(gameObject);
    }

    public static bool IsDragging()
    {
        return isDraggingStatic;
    }
}