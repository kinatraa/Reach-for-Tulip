using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public DinoManager dinoManager;
    public TreeController treeController;
    public ChompController chompController;

    public void BuyDino()
    {
        dinoManager.BuyDino();
    }

    public void BuyTree()
    {
        treeController.BuyTree();
    }

    public void BuyChomp()
    {
        chompController.BuyChomp();
    }
}
