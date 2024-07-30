using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public TreeController treeController;
    public DinoManager dinoManager;
    public GameObject menu;
    public GameObject game;
    void Start()
    {
        menu.SetActive(true);
        game.SetActive(false);

        treeController.BuyTree();
        dinoManager.BuyDino();
        dinoManager.BuyDino();
    }

    public void SetMenu(bool check)
    {
        menu.SetActive(check);
    }

    public void SetGame(bool check)
    {
        game.SetActive(check);
    }
}
