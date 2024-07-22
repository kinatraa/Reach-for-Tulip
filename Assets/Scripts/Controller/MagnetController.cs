using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    public GameObject itemIcon;


    private GameObject pulledItem;
    private GameObject[] pulledItems;
    private bool[] isMovingItems;
    private KeyValuePair<float, float> limitX = new KeyValuePair<float, float>(-8f, 8f), limitY = new KeyValuePair<float, float>(-1.6f, 4f);
    private float targetWidth = 5.0f;
    private float targetHeight = 5.0f;

    void Update()
    {
        if (pulledItem != null)
        {
            AttractItems(0.5f);
        }
    }

    public void SetItem(GameObject item)
    {
        pulledItem = item;
        FindTheSameItem(item);
        isMovingItems = new bool[pulledItems.Length];

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

    private void FindTheSameItem(GameObject icon)
    {
        pulledItems = GameObject.FindGameObjectsWithTag(icon.tag);
    }

    public void AttractItems(float speed)
    {
        for (int i = 0; i < pulledItems.Length; i++)
        {
            if(isMovingItems[i] == true){
                
            }
            if (Vector3.Distance(pulledItems[i].transform.position, this.transform.position) > 1f)
            {
                StartCoroutine(MoveTowards(pulledItems[i], this.transform.position, speed));
            }
        }
    }

    private IEnumerator MoveTowards(GameObject item, Vector3 targetPosition, float speed)
    {
        while (Vector3.Distance(item.transform.position, targetPosition) > 1f)
        {
            item.transform.position = Vector3.MoveTowards(item.transform.position, targetPosition, speed * Time.deltaTime);
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
}
