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
    public WitchController witchController;
    public FountainController fountainController;

    private Transform infoText;
    private Transform descriptionText;

    private bool switchAlbumButton = true;

    void Start() {
        infoText = transform.Find("Info");
        descriptionText = transform.Find("Description");
    }

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

    public void BuyWitch()
    {
        witchController.BuyWitch();
    }

    public void BuyFountain(){
        fountainController.BuyFountain();
    }

    public void OpenAlbum(GameObject page)
    {
        page.SetActive(switchAlbumButton);
        switchAlbumButton = !switchAlbumButton;
    }

    public void InfoDisplay(GameObject button)
    {
        if(MouseDrag.IsDragging()) return;

        string name = button.name;
        TextMeshProUGUI info = infoText.gameObject.GetComponent<TextMeshProUGUI>();
        info.text = name + " " + Constants.Cost.GetCost(name);
        TextMeshProUGUI description = descriptionText.gameObject.GetComponent<TextMeshProUGUI>();
        description.text = Constants.Description.GetDescription(name);

        Vector3 newPosition = button.transform.position;
        newPosition.y += 100;
        infoText.position = newPosition;
        newPosition.y -= 200;
        descriptionText.position = newPosition;

        infoText.gameObject.SetActive(true);
        descriptionText.gameObject.SetActive(true);
    }

    public void TurnOffInfoDisplay()
    {
        if(MouseDrag.IsDragging()) return;

        infoText.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);
    }
}
