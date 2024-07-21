using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    private KeyValuePair<float, float> limitX = new KeyValuePair<float, float>(-8f, 8f), limitY = new KeyValuePair<float, float>(-1.6f, 4f);
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
