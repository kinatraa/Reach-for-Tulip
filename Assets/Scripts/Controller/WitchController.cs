using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchController : MonoBehaviour
{
    public GameObject magicOrb;

    private Animator animator;

    private KeyValuePair<float, float> limitX = new KeyValuePair<float, float>(-8f, 8f), limitY = new KeyValuePair<float, float>(-1.6f, 4f);
    private int gemAmount = 0, flameAmount = 0;
    private bool castingSpell = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (gemAmount == 2 && flameAmount == 1 && !castingSpell)
        {
            StartCoroutine(CastSpell());
        }
    }

    private IEnumerator CastSpell()
    {
        castingSpell = true;
        animator.SetBool("CastingSpell", castingSpell);
        yield return new WaitForSeconds(Constants.Cooldown.witch);
        castingSpell = false;
        animator.SetBool("CastingSpell", castingSpell);

        gemAmount = flameAmount = 0;
        DropOrb();
        yield return null;
    }

    private void DropOrb()
    {
        Vector3 orbPosition = this.transform.position;
        orbPosition.y -= 1f;

        GameObject newOrb = Instantiate(magicOrb, orbPosition, Quaternion.identity);
        newOrb.transform.SetParent(this.transform.root, true);
    }

    public void TakeObject(GameObject obj)
    {
        if (obj.CompareTag("Gem"))
        {
            ++gemAmount;
        }
        else if (obj.name == "flame(Clone)")
        {
            ++flameAmount;
        }
    }

    public bool IsCasting()
    {
        return castingSpell;
    }

    public int GetGemAmount()
    {
        return gemAmount;
    }

    public int GetFlameAmount()
    {
        return flameAmount;
    }

    public void BuyWitch()
    {
        float randomX = Random.Range(limitX.Key, limitX.Value);
        float randomY = Random.Range(limitY.Key, limitY.Value);
        Vector3 newPosition = new Vector3(randomX, randomY, 0);

        GameObject parent = GameObject.Find("Witch");

        GameObject newWitch = Instantiate(this.gameObject, newPosition, this.gameObject.transform.rotation);
        newWitch.transform.SetParent(parent.transform, true);
    }
}
