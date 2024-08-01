using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GramophoneController : MonoBehaviour
{
    public AudioSource[] songs;

    private KeyValuePair<float, float> limitX = new KeyValuePair<float, float>(-8f, 8f), limitY = new KeyValuePair<float, float>(-1.6f, 4f);
    private AudioSource playingSong = null;
    private AudioSource newSong;
    private float timer = 0f;
    private float interval = 5f;

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

        Instantiate(this.gameObject, newPosition, this.gameObject.transform.rotation);
    }

    public void PlayMusic(int id)
    {
        if (playingSong != null)
        {
            playingSong.Stop();
        }
        newSong = Instantiate(songs[id], transform);
        playingSong = newSong;
        playingSong.Play();
    }

    public void StopMusic()
    {
        if (playingSong != null)
        {
            playingSong.Stop();
            playingSong = null;
            Destroy(newSong.gameObject);
        }
    }
}
