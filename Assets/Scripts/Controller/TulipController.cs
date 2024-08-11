using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TulipController : MonoBehaviour
{
    public Sprite[] tulips;

    private KeyValuePair<float, float> limitX = new KeyValuePair<float, float>(-8f, 8f), limitY = new KeyValuePair<float, float>(-1.6f, 4f);
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;
    private bool isLoved = false;
    private bool isBig = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        StartCoroutine(GrowingProcess());
    }

    private IEnumerator GrowingProcess()
    {
        for(int i = 0; i < tulips.Length; i++)
        {
            spriteRenderer.sprite = tulips[i];
            UpdateCollider();
            Constants.Hint.tulip = Constants.Hint.tulipHints[i];
            if(i == tulips.Length - 1)
            {
                isBig = true;
            }
            if (i == 1)
            {
                while (!isLoved)
                {
                    yield return null;
                }
                Constants.Hint.tulip = "yeah!";
            }
            if (i == 0)
            {
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return new WaitForSeconds(Constants.Cooldown.tulip);
            }
        }

        yield return null;
    }

    private void UpdateCollider()
    {
        Destroy(polygonCollider);

        polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
    }

    public void SetLove(bool check)
    {
        isLoved = check;
    }

    public bool IsLoved()
    {
        return isLoved;
    }

    public bool IsBig()
    {
        return isBig;
    }

    public void BuyTulip()
    {
        float randomX = Random.Range(limitX.Key, limitX.Value);
        float randomY = Random.Range(limitY.Key, limitY.Value);
        Vector3 newPosition = new Vector3(randomX, randomY, 0);

        GameObject parent = GameObject.Find("Tulips");

        GameObject newChomp = Instantiate(this.gameObject, newPosition, Quaternion.identity);
        newChomp.transform.SetParent(parent.transform, true);
    }
}
