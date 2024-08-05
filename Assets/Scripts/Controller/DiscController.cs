using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DiscController : MonoBehaviour
{
    private GramophoneController gramophoneController;
    private AlbumController albumController;
    private bool collideAlbum = false;
    private bool inAlbum = false;
    private bool collideGramophone = false;
    private bool inGramophone = false;
    private GameObject albumObject;
    private GameObject finnObject;
    private GameObject gramophoneObject;

    void Start()
    {

        finnObject = this.transform.parent.gameObject;
        albumObject = GameMethods.FindRootGameObject("UIGame").transform.Find("Album").transform.Find("openbook").gameObject;
    }

    void Update()
    {
        if (gramophoneController == null)
        {
            if (gramophoneObject == null)
            {
                GameObject tmp = GameMethods.FindRootGameObject("Gramophone");
                if (tmp.transform.childCount == 1)
                {
                    return;
                }
                gramophoneObject = tmp.transform.Find("Gramophone(Clone)").gameObject;
                if (gramophoneObject != null)
                {
                    gramophoneController = gramophoneObject.GetComponent<GramophoneController>();
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (inAlbum)
        {
            albumController.TakeDisc(this.gameObject);
            this.transform.SetParent(finnObject.transform, true);
            inAlbum = false;
        }
        else if (inGramophone)
        {
            gramophoneController.StopMusic();
            this.transform.SetParent(finnObject.transform, true);
            inGramophone = false;
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
        else if (collideGramophone == true)
        {
            if (gramophoneController.HasDisc())
            {
                MoveDiscOut();
                return;
            }
            Vector3 discPosition = gramophoneObject.transform.position;
            discPosition.y -= 0.4f;
            this.transform.position = discPosition;
            this.transform.SetParent(gramophoneObject.transform, true);

            string name = gameObject.name;
            name = name.Substring(12);
            name = name.Substring(0, name.Length - 7);
            int id = int.Parse(name) - 1;

            gramophoneController.PlayMusic(id);

            inGramophone = true;
        }
    }

    private void MoveDiscOut()
    {
        Collider2D gramophoneCollider = gramophoneObject.GetComponent<Collider2D>();
        Bounds bounds = gramophoneCollider.bounds;

        Vector3 newPos = transform.position;
        Vector3 discSize = transform.GetComponent<Collider2D>().bounds.size;

        int direction = Random.Range(0, 8);
        switch (direction)
        {
            case 0: // up
                newPos.y = bounds.max.y + discSize.y / 2 + 0.1f;
                break;
            case 1: // down
                newPos.y = bounds.min.y - discSize.y / 2 - 0.1f;
                break;
            case 2: // left
                newPos.x = bounds.min.x - discSize.x / 2 - 0.1f;
                break;
            case 3: // right
                newPos.x = bounds.max.x + discSize.x / 2 + 0.1f;
                break;
            case 4: // up-left
                newPos.y = bounds.max.y + discSize.y / 2 + 0.05f;
                newPos.x = bounds.min.x - discSize.x / 2 - 0.05f;
                break;
            case 5: // up-right
                newPos.y = bounds.max.y + discSize.y / 2 + 0.05f;
                newPos.x = bounds.max.x + discSize.x / 2 + 0.05f;
                break;
            case 6: // down-left
                newPos.y = bounds.min.y - discSize.y / 2 - 0.05f;
                newPos.x = bounds.min.x - discSize.x / 2 - 0.05f;
                break;
            case 7: // down-right
                newPos.y = bounds.min.y - discSize.y / 2 - 0.05f;
                newPos.x = bounds.max.x + discSize.x / 2 + 0.05f;
                break;
        }
        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Album")
        {
            albumController = collision.GetComponent<AlbumController>();
            collideAlbum = true;
        }
        else if (collision.CompareTag("Gramophone"))
        {
            collideGramophone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Album")
        {
            collideAlbum = false;
        }
        else if (collision.CompareTag("Gramophone"))
        {
            collideGramophone = false;
        }
    }
}
