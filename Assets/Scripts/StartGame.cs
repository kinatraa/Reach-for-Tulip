using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public TreeController treeController;
    public DinoManager dinoManager;

    void Awake()
    {
        treeController.BuyTree();
        dinoManager.BuyDino();
        dinoManager.BuyDino();
    }
}
