using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class ButtonManager : MonoBehaviour
{
    public DinoManager dinoManager;
    public TreeController treeController;
    public ChompController chompController;
    public GramophoneController gramophoneController;
    public FinnController finnController;
    public WitchController witchController;
    public FountainController fountainController;
    public TulipController tulipController;
    public MagnetController magnetController;
    public SceneController sceneController;

    private Transform infoText;
    private Transform descriptionText;

    void Start()
    {
        infoText = transform.Find("Info");
        descriptionText = transform.Find("Description");
    }

    public void StartGame()
    {
        sceneController.SetMenuStart(false);
    }

    public void OpenMenuGame()
    {
        sceneController.SetMenuGame(!sceneController.IsMenuGameOpen());
    }

    public void Resume()
    {
        sceneController.SetMenuGame(!sceneController.IsMenuGameOpen());
    }

    public void OpenSetting()
    {
        sceneController.SetSetting(!sceneController.IsSettingOpen());
    }

    public void Quit()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void BuyDino()
    {
        if (PlayerManager.money < Constants.Cost.dino)
        {
            return;
        }
        dinoManager.BuyDino();
        PlayerManager.money -= Constants.Cost.dino;
    }

    public void BuyTree()
    {
        if (PlayerManager.money < Constants.Cost.tree)
        {
            return;
        }
        treeController.BuyTree();
        PlayerManager.money -= Constants.Cost.tree;
    }

    public void BuyChomp()
    {
        if (PlayerManager.money < Constants.Cost.chomp)
        {
            return;
        }
        chompController.BuyChomp();
        PlayerManager.money -= Constants.Cost.chomp;
    }

    public void BuyGramophone()
    {
        if(GameMethods.FindRootGameObject("Gramophone").transform.childCount >= 2){
            return;
        }
        if (PlayerManager.money < Constants.Cost.gramophone)
        {
            return;
        }
        gramophoneController.BuyGramophone();
        PlayerManager.money -= Constants.Cost.gramophone;
    }

    public void BuyFinn()
    {
        if (PlayerManager.money < Constants.Cost.finn)
        {
            return;
        }
        finnController.BuyFinn();
        PlayerManager.money -= Constants.Cost.finn;
    }

    public void BuyWitch()
    {
        if (PlayerManager.money < Constants.Cost.witch)
        {
            return;
        }
        witchController.BuyWitch();
        PlayerManager.money -= Constants.Cost.witch;
    }

    public void BuyFountain()
    {
        if (GameMethods.FindRootGameObject("WaterFountain").transform.childCount >= 1)
        {
            return;
        }
        if (PlayerManager.money < Constants.Cost.fountain)
        {
            return;
        }
        fountainController.BuyFountain();
        PlayerManager.money -= Constants.Cost.fountain;
    }

    public void BuyTulip()
    {
        if (PlayerManager.money < Constants.Cost.tulip)
        {
            return;
        }
        tulipController.BuyTulip();
        PlayerManager.money -= Constants.Cost.tulip;
    }

    public void BuyMagnet()
    {
        if (PlayerManager.money < Constants.Cost.magnet)
        {
            return;
        }
        magnetController.BuyMagnet();
        PlayerManager.money -= Constants.Cost.magnet;
    }

    public void InfoDisplay(GameObject button)
    {
        if (MouseDrag.IsDragging()) return;

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
        if (MouseDrag.IsDragging()) return;

        infoText.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);
    }
}
