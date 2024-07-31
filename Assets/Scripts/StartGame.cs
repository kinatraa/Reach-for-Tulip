using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public TreeController treeController;
    public DinoManager dinoManager;
    public GameObject menu;
    public GameObject game;

    private float moveDuration = 0.2f;
    private float moveDistance = 600f;
    private RectTransform title;
    private GameObject startButton;

    void Start()
    {
        title = menu.transform.Find("Image").transform.Find("Title").GetComponent<RectTransform>();
        startButton = menu.transform.Find("Image").transform.Find("StartButton").gameObject;

        SetMenu(true);
        SetGame(false);

        treeController.BuyTree();
        dinoManager.BuyDino();
        dinoManager.BuyDino();
    }

    public void SetMenu(bool check)
    {
        if (check)
        {
            menu.SetActive(check);
        }
        else
        {
            StartCoroutine(MoveMenuUp());
        }
    }

    private IEnumerator MoveMenuUp()
    {
        startButton.SetActive(false);

        Vector2 startPos = title.anchoredPosition;
        Vector2 endPos = startPos + new Vector2(0, moveDistance);
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            title.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        title.anchoredPosition = endPos;

        menu.SetActive(false);
        SetGame(true);
    }

    public void SetGame(bool check)
    {
        MouseDrag.DisableDrag(!check);
        game.SetActive(check);
    }
}
