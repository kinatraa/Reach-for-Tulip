using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FinnController : MonoBehaviour
{
    public GameObject flower;
    public GameObject flame;
    public GameObject[] musicDiscs;

    private float speed = 5.0f;

    private KeyValuePair<float, float> limitX = new KeyValuePair<float, float>(-6f, -4f), limitY = new KeyValuePair<float, float>(-1.6f, 4f);
    private Animator animator;
    private bool isDragging = false;
    private bool isMoving = false;
    private bool hasItem = false;
    private float screenLeft, screenRight, lastX;
    private Collider2D coll;
    private Vector3 newPosition;
    private GameObject parentObject;

    void Start()
    {
        if(parentObject == null)
        {
            parentObject = GameObject.Find("Finn and his items");
        }
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        lastX = transform.position.x;
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - 1;
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x + 1;
        StartCoroutine(FinnMovement());
    }

    private IEnumerator FinnMovement()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            if(lastX - transform.position.x <= 0.1 && hasItem)
            {
                coll.enabled = true;
                isMoving = false;
                animator.SetBool("IsMoving", isMoving);
                animator.SetBool("IsCheering", true);
                yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("cheer"));
                DropItem();
                yield return new WaitForSeconds(2f);
                animator.SetBool("IsCheering", false);
                yield return new WaitForSeconds(1f);
                lastX = transform.position.x;
                hasItem = false;
                while (isDragging)
                {
                    lastX = transform.position.x;
                    yield return null;
                }
            }

            while (isDragging)
            {
                isMoving = false;
                animator.SetBool("IsMoving", isMoving);
                yield return null;
            }
            
            isMoving = true;

            if (isMoving)
            {
                coll.enabled = false;
                newPosition = transform.position;
                newPosition.x += speed * Time.deltaTime;

                if (newPosition.x > screenRight)
                {
                    yield return new WaitForSeconds(5f);
                    newPosition.x = screenLeft;
                    hasItem = true;
                }

                transform.position = newPosition;

                animator.SetBool("IsMoving", isMoving);
                yield return null;
            }

            yield return null;
        }
    }

    private void DropItem()
    {
        Vector3 newPosition = this.transform.position;
        newPosition.x += 0.2f;
        newPosition.y += 0.4f;

        GameObject newItem = null;
        int randomNumber = Random.Range(0, 100);
        
        if(randomNumber >= 0 && randomNumber < 50)
        {
            newItem = Instantiate(flower, newPosition, Quaternion.identity);
        }
        else if(randomNumber >= 50 && randomNumber < 90)
        {
            newItem = Instantiate(flame, newPosition, Quaternion.identity);
        }
        else
        {
            int randomId = Random.Range(0, 10);
            newItem = Instantiate(musicDiscs[randomId], newPosition, Quaternion.identity);
        }

        newItem.transform.SetParent(parentObject.transform, true);
    }

    public void BuyFinn()
    {
        float randomX = Random.Range(limitX.Key, limitX.Value);
        float randomY = Random.Range(limitY.Key, limitY.Value);
        Vector3 newPosition = new Vector3(randomX, randomY, 0);

        if (parentObject == null)
        {
            parentObject = GameObject.Find("Finn and his items");
        }

        GameObject newFinn = Instantiate(this.gameObject, newPosition, this.gameObject.transform.rotation);
        newFinn.transform.SetParent(parentObject.transform, true);
    }

    public void SetDrag(bool check)
    {
        isDragging = check;
    }
}
