using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoManager : MonoBehaviour
{
    public GameObject[] dinos;
    public GameObject gemOutput;

    private KeyValuePair<float, float> limitX = new KeyValuePair<float, float>(-8f, 8f), limitY = new KeyValuePair<float, float>(-1.6f, 4f);

    public void BuyDino()
    {
        float randomX = Random.Range(limitX.Key, limitX.Value);
        float randomY = Random.Range(limitY.Key, limitY.Value);
        Vector3 newPosition = new Vector3(randomX, randomY, 0);

        int randomId = Random.Range(0, dinos.Length);

        GameObject newObject = Instantiate(dinos[randomId], newPosition, dinos[randomId].transform.rotation);
        newObject.transform.SetParent(this.transform, true);
    }

    public void DropGem(GameObject dino)
    {
        GameObject parent = GameObject.Find("Gems");

        Vector3 newPosition = dino.transform.position + new Vector3(0, -0.3f, 0);

        GameObject newGem = Instantiate(gemOutput, newPosition, Quaternion.identity);
        newGem.transform.SetParent(parent.transform, true);
    }
}
