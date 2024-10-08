using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private bool collideShop = false;

    private GameObject hintText;
    private GameObject sellText;
    private RaycastHit2D hit;
    private bool isHitted = false;

    private SpriteRenderer spriteRenderer;
    private int lastOrder = 0;

    private static bool disableDrag = false;

    void Start()
    {
        dinoController = GetComponent<DinoController>();
        treeController = GetComponent<TreeController>();
        finnController = GetComponent<FinnController>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        lastOrder = spriteRenderer.sortingOrder;

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

        GetTextObject();
    }

    void Update()
    {
        /*Debug.Log(disableDrag);*/
        if (disableDrag)
        {
            return;
        }

        /*Debug.Log(hit.collider);*/
        if (isDragging)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);

            newPosition.x = Mathf.Clamp(newPosition.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
            newPosition.y = Mathf.Clamp(newPosition.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

            this.gameObject.transform.position = newPosition;
        }
        if (isHitted && hit.collider != null)
        {
            if(this.transform.position.y <= -2.2f)
            {
                ShowSellInfo();
            }
            else
            {
                sellText.SetActive(false);
            }
            ShowHint();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDragging)
        {
            if (collision.CompareTag("Magnet"))
            {
                MagnetController magnet = collision.GetComponent<MagnetController>();
                magnet.SetItem(this.gameObject);
            }
            else if(collision.name == "ShopBackground")
            {
                if (this.transform.position.y > -2.2f)
                {
                    collideShop = true;
                }
                else
                {
                    collideShop = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "ShopBackground")
        {
            collideShop = false;
        }
    }

    private void OnMouseDown()
    {
        if (disableDrag)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            spriteRenderer.sortingOrder = 500;
            SetSortingOrderForAllChildren(spriteRenderer.transform, 500);

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosX = mousePos.x - this.transform.position.x;
            startPosY = mousePos.y - this.transform.position.y;
            isDragging = true;
            isDraggingStatic = true;
            if (dinoController != null)
            {
                dinoController.SetDrag(true);
            }
            if (treeController != null)
            {
                treeController.SetInteraction(true);
            }
            if (finnController != null)
            {
                finnController.SetDrag(true);
            }

            CheckHit();
        }
    }

    private void SetSortingOrderForAllChildren(Transform parent, int order)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder += order;
            }
            SetSortingOrderForAllChildren(child, order);
        }
    }

    private void GetTextObject()
    {
        Transform[] allChildren = GameMethods.FindRootGameObject("UIGame").transform.Find("Text").GetComponentsInChildren<Transform>(true);
        foreach (Transform child in allChildren)
        {
            if (child.name == "Hint")
            {
                hintText = child.gameObject;
            }
            if(child.name == "Sell Info")
            {
                sellText = child.gameObject;
            }
        }
    }

    private void ShowSellInfo()
    {
        TextMeshProUGUI text = sellText.GetComponent<TextMeshProUGUI>();
        text.text = "Sell for: " + Constants.Value.GetValue(gameObject).ToString() + "$";
        
        sellText.SetActive(true);
    }

    private void CheckHit()
    {
        Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(rayPos, Vector2.zero);

        if (hit.collider != null)
        {
            isHitted = true;
        }
    }

    private void ShowHint()
    {
        Vector3 worldPosition = hit.collider.bounds.center;
        worldPosition.y += hit.collider.bounds.extents.y + 0.2f;

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        hintText.transform.position = screenPosition;

        TextMeshProUGUI text = hintText.GetComponent<TextMeshProUGUI>();

        text.text = CheckText(hit.collider.gameObject);

        hintText.SetActive(true);
    }

    private string CheckText(GameObject obj)
    {
        if (obj.CompareTag("Dino"))
        {
            return Constants.Hint.GetHint("Dino");
        }
        else if (obj.CompareTag("Gem"))
        {
            return Constants.Hint.GetHint("Gem");
        }
        else if (obj.CompareTag("Disc"))
        {
            return Constants.Hint.GetHint("Disc");
        }
        else
        {
            return Constants.Hint.GetHint(obj.name);
        }
    }

    public void OnMouseUp()
    {
        if (disableDrag)
        {
            return;
        }

        spriteRenderer.sortingOrder = lastOrder;
        SetSortingOrderForAllChildren(spriteRenderer.transform, lastOrder);
        isDragging = false;
        isDraggingStatic = false;
        if (dinoController != null)
        {
            dinoController.SetDrag(false);
        }
        if (treeController != null)
        {
            treeController.SetInteraction(false);
        }
        if (finnController != null)
        {
            finnController.SetDrag(false);
        }
        if (isHitted)
        {
            isHitted = false;
            hintText.SetActive(false);
            sellText.SetActive(false);
        }

        if (this.transform.position.y <= -2.2f)
        {
            SellItem();
        }

        if (collideShop)
        {
            Vector3 newPosition = this.transform.position;
            newPosition.y = -1f;
            this.transform.position = newPosition;
            collideShop = false;
        }
    }

    private void SellItem()
    {
        PlayerManager.money += Constants.Value.GetValue(gameObject);
        if (gameObject.CompareTag("Gramophone"))
        {
            GramophoneController gramophone = gameObject.GetComponent<GramophoneController>();
            gramophone.MoveDisc();
        }
        else if (gameObject.CompareTag("Disc"))
        {
            string name = gameObject.name;
            name = name.Substring(12);
            name = name.Substring(0, name.Length - 7);
            int id = int.Parse(name) - 1;

            if (!GameMethods.discRemaining.Contains(id))
            {
                GameMethods.discRemaining.Add(id);
            }
        }
        Destroy(gameObject);
    }

    public static bool IsDragging()
    {
        return isDraggingStatic;
    }

    public static void DisableDrag(bool check)
    {
        disableDrag = check;
    }
}