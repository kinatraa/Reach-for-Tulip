using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumController : MonoBehaviour
{
    public Sprite emptyDisc;
    public Sprite[] discs;
    public GameObject[] discSlots;
    public GameObject page;

    private bool[] discIsFilled = new bool[10];
    private bool toggleButton = false;

    void Start()
    {
        for (int i = 0; i < discIsFilled.Length; i++)
        {
            discIsFilled[i] = false;
        }
    }

    void Update()
    {
        page.SetActive(toggleButton);
        /*for(int i = 0; i < discSlots.Length; i++)
        {
            if (discIsFilled[i])
            {
                discSlots[i].sprite = discs[i];
            }
            else
            {
                discSlots[i].sprite = emptyDisc;
            }
        }*/
    }

    private void OnMouseDown() {
        toggleButton = !toggleButton;
    }

    public void FillDisc(GameObject d){
        string name = d.name;
        name = name.Substring(12);
        name = name.Substring(0, name.Length - 7);
        int id = int.Parse(name) - 1;
        d.transform.position = discSlots[id].transform.position;
        discSlots[id].GetComponent<SpriteRenderer>().sprite = discs[id];
    }

    public void TakeDisc(GameObject d)
    {
        string name = d.name;
        name = name.Substring(12);
        name = name.Substring(0, name.Length - 7);
        int id = int.Parse(name) - 1;
        discSlots[id].GetComponent<SpriteRenderer>().sprite = emptyDisc;
    }
}
