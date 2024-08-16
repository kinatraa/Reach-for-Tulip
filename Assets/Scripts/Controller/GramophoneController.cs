using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GramophoneController : MonoBehaviour
{
    private AudioSource[] songs;
    private KeyValuePair<float, float> limitX = new KeyValuePair<float, float>(-8f, 8f), limitY = new KeyValuePair<float, float>(-1.6f, 4f);
    private AudioSource playingSong;
    private GameObject playingDisc;
    private float timer = 0f;
    private float interval = 5f;

    void Start()
    {
        playingSong = GetComponent<AudioSource>();
        songs = GameMethods.FindRootGameObject("Gramophone").transform.Find("Songs").GetComponentsInChildren<AudioSource>();
    }

    void Update()
    {
        if (playingSong.isPlaying)
        {
            Debug.Log(playingSong.clip);
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

    public void PlayMusic(int id, GameObject d)
    {
        if (playingSong.isPlaying)
        {
            playingSong.Stop();
        }

        playingDisc = d;
        playingSong.clip = songs[id].clip;

        AudioController.playingMusic.Pause();
        AudioController.playingMusic = playingSong;

        playingSong.Play();
    }

    public void StopMusic()
    {
        if(playingSong == null)
        {
            return;
        }
        if (playingSong.isPlaying)
        {
            playingSong.Stop();
            playingSong.clip = null;
            playingDisc = null;
            AudioController.playingMusic = null;
        }
    }

    public bool HasDisc()
    {
        return playingSong.clip != null;
    }

    public void MoveDisc()
    {
        playingDisc.transform.SetParent(GameMethods.FindRootGameObject("Finn and his items").transform);
        
        Vector3 newPos = playingDisc.transform.position;
        newPos.y = -2;
        playingDisc.transform.position = newPos;
    }
}
