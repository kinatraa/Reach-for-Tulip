using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameMethods
{
    public static List<int> discRemaining = new List<int>();

    public static void Initialize()
    {
        for(int i = 0; i < GameMethods.FindRootGameObject("Gramophone").transform.Find("Songs").transform.childCount; i++)
        {
            discRemaining.Add(i);
        }
    }

    public static GameObject FindRootGameObject(string name)
    {
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }
}
