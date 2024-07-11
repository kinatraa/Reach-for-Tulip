using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainController : MonoBehaviour
{
    public GameObject[] gems;

    private Animator animator;
    private int activeCoroutines = 0;
    private float dropRadius = 1.2f;

    private GameObject gemContainer;

    void Start()
    {
        animator = GetComponent<Animator>();
        gemContainer = GameObject.Find("Gems");
    }

    public void DropGem()
    {
        StartCoroutine(WaitForGem());
    }

    public IEnumerator WaitForGem()
    {
        ++activeCoroutines;
        animator.SetBool("HasGem", true);

        yield return new WaitForSeconds(Constants.Cooldown.fountain);

        Vector3 newPosition = GenerateRandomPosition();
        int randomId = Random.Range(0, gems.Length);

        GameObject newObject = Instantiate(gems[randomId], newPosition, gems[randomId].transform.rotation);
        newObject.transform.SetParent(gemContainer.transform, true);

        activeCoroutines--;
        if (activeCoroutines == 0)
        {
            animator.SetBool("HasGem", false);
        }

    }

    private Vector3 GenerateRandomPosition()
    {
        float randomAngle = Random.Range(-Mathf.PI, 0f);

        float offsetX = Mathf.Cos(randomAngle) * dropRadius;
        float offsetY = Mathf.Sin(randomAngle) * dropRadius;

        Vector3 fountainPosition = transform.position;
        Vector3 newPosition = new Vector3(fountainPosition.x + offsetX, fountainPosition.y + offsetY, fountainPosition.z);

        return newPosition;
    }
}
