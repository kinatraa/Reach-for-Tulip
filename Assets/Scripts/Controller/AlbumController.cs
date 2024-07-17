using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumController : MonoBehaviour
{
    public Sprite emptyDisc;
    public Sprite[] discs;
    public SpriteRenderer[] spriteRenderers;
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
        for(int i = 0; i < spriteRenderers.Length; i++)
        {
            if (discIsFilled[i])
            {
                spriteRenderers[i].sprite = discs[i];
            }
            else
            {
                spriteRenderers[i].sprite = emptyDisc;
            }
        }
    }

    private void OnMouseDown() {
        toggleButton = !toggleButton;
    }

    public void FillDisc(int i){
        discIsFilled[i] = true;
    }
}
