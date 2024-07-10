using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public GameObject fruit;
    public float shakeDuration = 0.5f;
    public float shakeThreshold = 1f;

    private KeyValuePair<float, float> limitX = new KeyValuePair<float, float>(-8f, 8f), limitY = new KeyValuePair<float, float>(-1.6f, 4f);

    private Animator animator;
    private bool hasFruit = false;
    private bool isShaking = false;
    private float currentShakeTime = 0f;
    private Vector3 lastMousePosition;

    private bool isInteracting = false;

    void Start()
    {
        InitializeTree();
    }

    private void InitializeTree()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(SpawnFruitRoutine());
    }

    private IEnumerator SpawnFruitRoutine()
    {
        while (true)
        {
            while (hasFruit)
            {
                yield return null;
            }

            yield return new WaitForSeconds(10f);
            hasFruit = true;
            animator.SetBool("HasFruit", true);
        }
    }
    void Update()
    {

        if (!isInteracting)
        {
            if (hasFruit)
            {
                CheckChompRange();
            }
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
            isShaking = false;
            currentShakeTime = 0f;
        }
        else if (Input.GetMouseButton(0) && hasFruit)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            if (delta.magnitude > shakeThreshold)
            {
                isShaking = true;
            }
            lastMousePosition = Input.mousePosition;
            if (isShaking)
            {
                currentShakeTime += Time.deltaTime;
                if (currentShakeTime >= shakeDuration)
                {
                    DropFruit();
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isShaking = false;
            currentShakeTime = 0f;
            isInteracting = false;
        }
    }

    private void CheckChompRange()
    {
        GameObject[] chomps = GameObject.FindGameObjectsWithTag("Chomp");

        foreach (GameObject chompObject in chomps)
        {
            ChompController chomp = chompObject.GetComponent<ChompController>();
            if (chomp.IsTired()) return;
            if (chomp.IsTreeInRange(transform.position))
            {
                DropFruit();
                return;
            }
        }
    }

    void DropFruit()
    {
        if (hasFruit)
        {
            GameObject parent = GameObject.Find("Fruits");

            Vector3 newPosition = transform.position + new Vector3(Random.Range(-1f, 1f), -1f, 0);

            GameObject newFruit = Instantiate(fruit, newPosition, Quaternion.identity);
            newFruit.transform.SetParent(parent.transform, true);

            hasFruit = false;
            animator.SetBool("HasFruit", false);
            isShaking = false;
            currentShakeTime = 0f;
        }
    }

    public void BuyTree()
    {
        float randomX = Random.Range(limitX.Key, limitX.Value);
        float randomY = Random.Range(limitY.Key, limitY.Value);
        Vector3 newPosition = new Vector3(randomX, randomY, 0);

        GameObject parent = GameObject.Find("Trees");

        GameObject newTreeObject = Instantiate(this.gameObject, newPosition, this.gameObject.transform.rotation);
        newTreeObject.transform.SetParent(parent.transform, true);
        TreeController newTree = newTreeObject.GetComponent<TreeController>();
        newTree.InitializeTree();
    }

    public void SetInteraction(bool check)
    {
        isInteracting = check;
    }
}
