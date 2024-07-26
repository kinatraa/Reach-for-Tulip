using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    public GameObject itemIcon;


    private string pulledItemTag = "";
    private List<GameObject> pulledItems = new List<GameObject>();
    private List<bool> isMoving = new List<bool>();
    private KeyValuePair<float, float> limitX = new KeyValuePair<float, float>(-8f, 8f), limitY = new KeyValuePair<float, float>(-1.6f, 4f);
    private float targetWidth = 5.0f;
    private float targetHeight = 5.0f;

    void Update()
    {
        if(pulledItemTag != "")
        {
            FindTheSameItem(pulledItemTag);
            AttractItems(0.5f);
            /*int cnt = pulledItems.Count(obj => obj == null);
            Debug.Log(pulledItems.Count + " " + cnt);*/
        }
    }

    public void SetItem(GameObject item)
    {
        if (item.CompareTag("Magnet"))
        {
            return;
        }
        if (pulledItemTag != "")
        {
            if (item.CompareTag(pulledItemTag))
            {
                return;
            }
        }
        ResetPulledList();
        pulledItemTag = item.tag;
        pulledItems.Clear();
        isMoving.Clear();
        /*AttractItems(0.5f);*/

        SpriteRenderer iconSprite = itemIcon.GetComponent<SpriteRenderer>();
        SpriteRenderer itemSprite = item.GetComponent<SpriteRenderer>();
        iconSprite.sprite = itemSprite.sprite;
        AdjustSize(iconSprite);
    }

    private void AdjustSize(SpriteRenderer spriteRenderer)
    {
        if (spriteRenderer == null) return;

        Sprite sprite = spriteRenderer.sprite;
        if (sprite == null) return;

        float spriteWidth = sprite.bounds.size.x;
        float spriteHeight = sprite.bounds.size.y;

        float widthScale = targetWidth / spriteWidth;
        float heightScale = targetHeight / spriteHeight;

        float scale = Mathf.Min(widthScale, heightScale);

        Vector3 newScale = new Vector3(scale, scale, 1);

        spriteRenderer.transform.localScale = newScale;
    }

    private void FindTheSameItem(string iconTag)
    {
        GameObject[] allItems = GameObject.FindGameObjectsWithTag(iconTag);
        foreach (GameObject item in allItems)
        {
            if (!pulledItems.Contains(item))
            {
                pulledItems.Add(item);
                if (item.CompareTag("Dino"))
                {
                    DinoController dino = item.GetComponent<DinoController>();
                    dino.SetPull(true);
                }
                else if (item.CompareTag("Finn"))
                {
                    FinnController fc = item.GetComponent<FinnController>();
                    fc.SetPull(true);
                }
                isMoving.Add(false);
            }
        }
    }

    public void AttractItems(float speed)
    {
        for (int i = 0; i < pulledItems.Count; i++)
        {
            if(pulledItems[i] != null)
            {
                if(isMoving[i] == false)
                {
                    if(Vector3.Distance(pulledItems[i].transform.position, this.transform.position) > 1f)
                    {
                        isMoving[i] = true;
                        StartCoroutine(MoveTowards(pulledItems[i], speed));
                    }
                }
                else
                {
                    if (Vector3.Distance(pulledItems[i].transform.position, this.transform.position) <= 1f)
                    {
                        isMoving[i] = false;
                    }
                }
            }
        }
    }

    private IEnumerator MoveTowards(GameObject item, float speed)
    {
        while (true)
        {
            if(item == null)
            {
                yield break;
            }
            if(!item.CompareTag(pulledItemTag))
            {
                yield break;
            }
            if(Vector3.Distance(item.transform.position, this.transform.position) <= 1f)
            {
                yield break;
            }
            item.transform.position = Vector3.MoveTowards(item.transform.position, this.transform.position, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void BuyMagnet()
    {
        float randomX = Random.Range(limitX.Key, limitX.Value);
        float randomY = Random.Range(limitY.Key, limitY.Value);
        Vector3 newPosition = new Vector3(randomX, randomY, 0);

        GameObject parent = GameObject.Find("Magnets");

        GameObject newChomp = Instantiate(this.gameObject, newPosition, Quaternion.identity);
        newChomp.transform.SetParent(parent.transform, true);
    }

    private void OnDestroy()
    {
        ResetPulledList();
    }

    private void ResetPulledList()
    {
        if(pulledItemTag.Length == 0)
        {
            return;
        }
        if (pulledItemTag == "Dino")
        {
            foreach (GameObject dino in pulledItems)
            {
                if (dino != null)
                {
                    DinoController dc = dino.GetComponent<DinoController>();
                    dc.SetPull(false);
                }
            }
        }
        else if(pulledItemTag == "Finn")
        {
            foreach (GameObject finn in pulledItems)
            {
                if (finn != null)
                {
                    FinnController fc = finn.GetComponent<FinnController>();
                    fc.SetPull(false);
                }
            }
        }
    }
}
