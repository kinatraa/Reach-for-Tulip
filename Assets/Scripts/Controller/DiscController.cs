using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DiscController : MonoBehaviour
{
    private AlbumController albumController;
    private bool inAlbum = false;
    private GameObject parent;

    void Start()
    {
        parent = GameMethods.FindRootGameObject("Album").transform.Find("openbook").gameObject;
    }

    private void OnMouseUp()
    {
        if (inAlbum == true)
        {
            string name = this.name;
            name = name.Substring(12);
            name = name.Substring(0, name.Length - 7);
            albumController.FillDisc(int.Parse(name) - 1);
            this.transform.SetParent(parent.transform, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Album")
        {
            albumController = collision.GetComponent<AlbumController>();
            inAlbum = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Album")
        {
            albumController = null;
            inAlbum = false;
        }
    }
}
