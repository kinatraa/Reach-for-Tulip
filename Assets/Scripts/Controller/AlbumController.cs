using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumController : MonoBehaviour
{
    public Sprite emptyDisc;
    public Sprite[] discs;
    public SpriteRenderer[] spriteRenderers;

    private bool[] discIsFilled = new bool[10];

    void Start()
    {
        for (int i = 0; i < discIsFilled.Length; i++)
        {
            discIsFilled[i] = false;
        }
    }

    void Update()
    {
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
}
