using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChompController : MonoBehaviour
{
    private KeyValuePair<float, float> limitX = new KeyValuePair<float, float>(-8f, 8f), limitY = new KeyValuePair<float, float>(-1.6f, 4f);
    private GameObject rangeObject;
    private Animator animator;
    private float range;
    private int cntFruit = 0;
    private bool isTired = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rangeObject = this.transform.GetChild(0).gameObject;
        CalculateChompRange();
    }

    private void CalculateChompRange()
    {
        SpriteRenderer rangeSpriteRenderer = rangeObject.GetComponent<SpriteRenderer>();

        Vector3 spriteSize = rangeSpriteRenderer.bounds.size;
        range = spriteSize.x / 2;
    }

    public bool IsTreeInRange(Vector3 treePosition)
    {
        if(Vector3.Distance(transform.position, treePosition) <= range)
        {
            ++cntFruit;
            if(cntFruit == 10)
            {
                SetTired(true);
                animator.SetBool("IsTired", isTired);
                cntFruit = 0;
            }
            return true;
        }
        return false;
    }

    public void BuyChomp()
    {
        float randomX = Random.Range(limitX.Key, limitX.Value);
        float randomY = Random.Range(limitY.Key, limitY.Value);
        Vector3 newPosition = new Vector3(randomX, randomY, 0);

        GameObject parent = GameObject.Find("Chomps");

        GameObject newChomp = Instantiate(this.gameObject, newPosition, this.gameObject.transform.rotation);
        newChomp.transform.SetParent(parent.transform, true);
    }

    public void SetTired(bool check)
    {
        isTired = check;
    }

    public bool IsTired()
    {
        return isTired;
    }

    private void OnMouseDown()
    {
        rangeObject.SetActive(true);
    }

    private void OnMouseUp()
    {
        rangeObject.SetActive(false);
    }

    public void InfoDisplay()
    {
        Debug.Log("OnPointerEnter method called!");
    }
}
