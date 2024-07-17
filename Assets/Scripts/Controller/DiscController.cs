using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DiscController : MonoBehaviour
{
    private AlbumController albumController;
    private bool collideAlbum = false;
    private bool inAlbum = false;
    private GameObject albumObject;
    private GameObject finnObject;

    void Start()
    {
        finnObject = this.transform.parent.gameObject;
        albumObject = GameMethods.FindRootGameObject("Album").transform.Find("openbook").gameObject;
    }

    private void OnMouseDown()
    {
        if (inAlbum)
        {
            albumController.TakeDisc(this.gameObject);
            this.transform.SetParent(finnObject.transform, true);
            inAlbum = false;
        }
    }

    private void OnMouseUp()
    {
        if (collideAlbum == true)
        {
            this.transform.SetParent(albumObject.transform, true);
            albumController.FillDisc(this.gameObject);
            inAlbum = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Album")
        {
            albumController = collision.GetComponent<AlbumController>();
            collideAlbum = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Album")
        {
            collideAlbum = false;
        }
    }
}
