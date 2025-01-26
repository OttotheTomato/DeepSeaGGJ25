using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    [SerializeField]
    private List<AudioClip> AudioList = new List<AudioClip>();

    [SerializeField]
    private AudioClip[] StepAudio = { };

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnSoundAtLocation(string audio, Transform location)
    {
        foreach (AudioClip clip in AudioList)
        {
            if (clip.name == audio)
            {
                AudioSource.PlayClipAtPoint(clip, location.position);
            }
        }
    }

    public void PlayerSounds(AudioSource playerSource, string audio)
    {
        foreach (AudioClip clip in AudioList)
        {
            if (clip.name == audio)
            {
                playerSource.PlayOneShot(clip);
            }
        }

    }

    public void Footstep(AudioSource playerSource)
    {
        PlayerSounds(playerSource, "WalkSound");

        int randomInt = Random.Range(0, StepAudio.Length);
        playerSource.PlayOneShot(StepAudio[randomInt], 0.5f);

        //PlayerSounds(playerSource, "SnowStep");
    }

}
