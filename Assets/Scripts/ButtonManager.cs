using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public DinoManager dinoManager;
    public TreeController treeController;
    public ChompController chompController;
    public GramophoneController gramophoneController;
    public FinnController finnController;

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

    public void BuyGramophone()
    {
        gramophoneController.BuyGramophone();
    }

    public void BuyFinn()
    {
        finnController.BuyFinn();
    }

    public void InfoDisplay(GameObject button)
    {
        string name = button.name;
        if (name == "Chomp")
        {
            Transform info = button.transform.Find("Info");
            TextMeshProUGUI text = info.gameObject.GetComponent<TextMeshProUGUI>();
            text.text = Constants.Cost.chomp.ToString();
            info.gameObject.SetActive(true);
        }
    }
}
