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

            if (isEating)
            {
                yield return new WaitForSeconds(Random.Range(Constants.Cooldown.dino.Key, Constants.Cooldown.dino.Value));
                ResetTrigger();
            }

            float randomX = Random.Range(limitX.Key, limitX.Value);
            float randomY = Random.Range(limitY.Key, limitY.Value);

            targetPosition = new Vector3(randomX, randomY, 0);

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
        animator.SetBool("IsEating", isEating);
    }

    public void ResetTrigger()
    {
        isEating = false;
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
