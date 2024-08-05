using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GramophoneController : MonoBehaviour
{
    private AudioSource[] songs;
    private KeyValuePair<float, float> limitX = new KeyValuePair<float, float>(-8f, 8f), limitY = new KeyValuePair<float, float>(-1.6f, 4f);
    private AudioSource playingSong;
    private float timer = 0f;
    private float interval = 5f;

    void Start()
    {
        playingSong = GetComponent<AudioSource>();
        songs = GameMethods.FindRootGameObject("Gramophone").transform.Find("Songs").GetComponentsInChildren<AudioSource>();
    }

    void Update()
    {
        if (playingSong != null)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                timer = 0;
                PlayerManager.money += 2;
            }
        }
    }

    public void BuyGramophone()
    {
        float randomX = Random.Range(limitX.Key, limitX.Value);
        float randomY = Random.Range(limitY.Key, limitY.Value);
        Vector3 newPosition = new Vector3(randomX, randomY, 0);

        GameObject parent = GameObject.Find("Gramophone");

        GameObject newGramophone = Instantiate(this.gameObject, newPosition, this.gameObject.transform.rotation);
        newGramophone.transform.SetParent(parent.transform, true);
    }

    public void PlayMusic(int id)
    {
        if (playingSong.isPlaying)
        {
            playingSong.Stop();
        }

        playingSong.clip = songs[id].clip;
        playingSong.Play();
    }

    public void StopMusic()
    {
        if (playingSong.isPlaying)
        {
            playingSong.Stop();
            playingSong.clip = null;
        }
    }

    public bool HasDisc()
    {
        return playingSong.clip != null;
    }
}
