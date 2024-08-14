using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public TreeController treeController;
    public DinoManager dinoManager;
    public GameObject menuStart;
    public GameObject shop;
    public GameObject game;
    public GameObject album;
    public GameObject menuGame;
    public GameObject setting;
    public GameObject tutorial;

    private float moveDuration = 0.2f;
    private float moveDistance = 600f;
    private RectTransform title;
    private GameObject startButton;
    private GameObject creditsButton;
    private GameObject quitButton;

    void Start()
    {
        title = menuStart.transform.Find("Image").transform.Find("Title").GetComponent<RectTransform>();
        startButton = menuStart.transform.Find("Image").transform.Find("StartButton").gameObject;
        creditsButton = menuStart.transform.Find("Image").transform.Find("Credits").gameObject;
        quitButton = menuStart.transform.Find("Image").transform.Find("Quit").gameObject;

        SetMenuStart(true);
        SetGame(false);

        treeController.BuyTree();
        dinoManager.BuyDino();
        dinoManager.BuyDino();
    }

    public void SetMenuStart(bool check)
    {
        if (check)
        {
            menuStart.SetActive(check);
        }
        else
        {
            StartCoroutine(MoveMenuUp());
        }
    }

    private IEnumerator MoveMenuUp()
    {
        startButton.SetActive(false);
        creditsButton.SetActive(false);
        quitButton.SetActive(false);

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

        menuStart.SetActive(false);
        tutorial.SetActive(true);
    }

    public void CloseTutorial()
    {
        tutorial.SetActive(false);
        SetGame(true);
    }

    public void SetGame(bool check)
    {
        MouseDrag.DisableDrag(!check);
        game.SetActive(check);
        shop.SetActive(check);
        album.SetActive(check);
    }

    public void SetMenuGame(bool check)
    {
        menuGame.SetActive(check);
        SetGame(!check);
    }

    public void SetSetting(bool check)
    {
        setting.SetActive(check);
        menuGame.SetActive(!check);
    }

    public bool IsSettingOpen()
    {
        return setting.activeSelf;
    }

    public bool IsMenuGameOpen()
    {
        return menuGame.activeSelf;
    }
}
