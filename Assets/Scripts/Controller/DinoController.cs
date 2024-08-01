using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class DinoController : MonoBehaviour
{
    private DinoManager dinoManager;

    private float moveSpeed = 1f;
    private KeyValuePair<float, float> limitX = new KeyValuePair<float, float>(-8f, 8f), limitY = new KeyValuePair<float, float>(-1.6f, 4f);

    private Vector3 targetPosition;
    private Vector3 lastPosition;

    private Animator animator;
    private bool isMoving = false;
    private bool isEating = false;
    private bool isDragging = false;
    private bool isPulling = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
        dinoManager = FindObjectOfType<DinoManager>();
        StartCoroutine(DinoMovement());
    }

    private IEnumerator DinoMovement()
    {
        while (true)
        {
            while (isDragging || isPulling)
            {
                isMoving = false;
                animator.SetBool("IsMoving", isMoving);
                yield return null;
            }

            while (isEating)
            {
                yield return null;
            }

            float randomX = Random.Range(limitX.Key, limitX.Value);
            float randomY = Random.Range(limitY.Key, limitY.Value);
            targetPosition = new Vector3(randomX, randomY, 0);

            int randomCheck = Random.Range(0, 3);
            if (randomCheck != 0)
            {
                GameObject fruits = GameMethods.FindRootGameObject("Fruits");
                Transform[] fruitPos = fruits.GetComponentsInChildren<Transform>();
                if(fruitPos.Length > 0){
                    int randFruit = Random.Range(0, fruitPos.Length);
                    targetPosition = fruitPos[randFruit].position;
                } 
            }

            isMoving = false;
            animator.SetBool("IsMoving", isMoving);
            float waitTime = Random.Range(0.5f, 2f);
            yield return new WaitForSeconds(waitTime);

            isMoving = true;
            animator.SetBool("IsMoving", isMoving);

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f && !isDragging && !isEating && !isPulling)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                transform.position += moveDirection * moveSpeed * Time.deltaTime;

                if (transform.position.x > lastPosition.x)
                {
                    transform.localScale = new Vector3(4, 4, 1);
                }
                else if (transform.position.x < lastPosition.x)
                {
                    transform.localScale = new Vector3(-4, 4, 1);
                }

                lastPosition = transform.position;

                yield return null;
            }
        }
    }

    public bool IsEating()
    {
        return isEating;
    }

    public void EatFruit()
    {
        isEating = true;
        StartCoroutine(NomNomFruit());
    }

    private IEnumerator NomNomFruit()
    {
        animator.SetBool("Bite", isEating);
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Bite"));
        animator.SetBool("IsEating", isEating);
        yield return new WaitForSeconds(Random.Range(Constants.Cooldown.dino.Key, Constants.Cooldown.dino.Value));
        ResetTrigger();
    }

    public void ResetTrigger()
    {
        isEating = false;
        animator.SetBool("Bite", isEating);
        animator.SetBool("IsEating", isEating);
        dinoManager.DropGem(this.gameObject);
    }

    public void SetDrag(bool check)
    {
        isDragging = check;
    }

    public void SetPull(bool check)
    {
        isPulling = check;
    }
}
