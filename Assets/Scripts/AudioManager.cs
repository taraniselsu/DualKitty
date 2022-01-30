using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip houseMusic;
    [SerializeField] AudioClip excitingRaceMusic;
    [SerializeField] AudioClip dastardlySchemeMusic;
    [SerializeField] AudioClip groovyJungleMusic;
    [SerializeField] AudioClip roombaMusic;
    [SerializeField] AudioClip sleepyTimeMusic;
    [SerializeField] AudioClip vroomVroomMusic;

    [Space(10)]
    [SerializeField] AudioClip sceneSwitchSwoosh;
    [SerializeField] AudioClip pilotLaser;
    [SerializeField] AudioClip gloriousSFX;
    [SerializeField] AudioClip everythingTheLightTouchesSFX;
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip heavyDrumBeats;
    [SerializeField] AudioClip owClip;
    [SerializeField] AudioClip mischievousDitty;
    [SerializeField] AudioClip reeowSFX;
    [SerializeField] AudioClip reeowShortSFX;

    [Space(10)]
    [SerializeField] AudioSource musicChannel;
    [SerializeField] List<AudioSource> sfxChannels;

    int currentSFXChannel = 0;
    int highestSFXChannel = 0;

    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;

        if (musicChannel == null) Debug.LogError("AudioManager: Music Channel is null");
        foreach (AudioSource sfxChannel in sfxChannels)
        {
            if (sfxChannel == null) Debug.LogError("Audio Manager: One of the SFX Channels is null");
        }

        highestSFXChannel = sfxChannels.Count - 1;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Music
    void PlayMusic(AudioClip music)
    {
        if (music == null)
        {
            Debug.LogWarning("Attempted to play a null music clip.  Not playing music");
            return;
        }
        // This conditional will only allow swapping of the music if the music is changing
        //  - This lets multiple areas with the same music feel contiguous
        if (musicChannel.clip != music)
        {
            musicChannel.Stop();
            musicChannel.clip = music;
            musicChannel.Play();
        }
    }
    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }
    public void PlayGameMusic()
    {
            PlayMusic(gameMusic);
    }
    public void PlayHouseMusic()
    {
        PlayMusic(houseMusic);
    }
    public void PlayExcitingRaceMusic()
    {
        PlayMusic(excitingRaceMusic);
    }
    public void PlayGroovyJungleMusic()
    {
        PlayMusic(groovyJungleMusic);
    }
    public void PlayDastardlySchemeMusic()
    {
        PlayMusic(dastardlySchemeMusic);
    }
    public void PlayRoombaMusic()
    {
        PlayMusic(roombaMusic);
    }
    public void PlaySleepyTimeMusic()
    {
        PlayMusic(sleepyTimeMusic);
    }
    public void PlayVroomVroomMusic()
    {
        PlayMusic(vroomVroomMusic);
    }

    public void StopMusic()
    {
        if(musicChannel.isPlaying) musicChannel.Stop();
        musicChannel.clip = null;
    }
    #endregion

    #region Sound Effects
    void PlaySoundEffect(AudioClip soundEffect)
    {
        if (soundEffect == null)
        {
            Debug.LogWarning("Attempted to play a null sfx clip.  Not playing sfx");
            return;
        }

        NextSFXChannel();
        if (!sfxChannels[currentSFXChannel].isPlaying)
        {
            sfxChannels[currentSFXChannel].Stop();
            sfxChannels[currentSFXChannel].clip = soundEffect;
            sfxChannels[currentSFXChannel].Play();
        }
    }

    public void PlaySceneSwitchSwooshSFX()
    {
        PlaySoundEffect(sceneSwitchSwoosh);
    }
    public void PlayPilotLaserSFX()
    {
        PlaySoundEffect(pilotLaser);
    }
    public void PlayGloriousSFX()
    {
        PlaySoundEffect(gloriousSFX);
        sfxChannels[currentSFXChannel].volume = .25f;
    }
    public void PlayEverythingTheLightTouchesSFX()
    {
        PlaySoundEffect(everythingTheLightTouchesSFX);
        sfxChannels[currentSFXChannel].volume = .85f;
    }
    public void PlayJumpSFX()
    {
        PlaySoundEffect(jumpSFX);
        sfxChannels[currentSFXChannel].volume = .25f;
    }
    public void PlayHeavyDrumBeatsSFX()
    {
        PlaySoundEffect(heavyDrumBeats);
        sfxChannels[currentSFXChannel].volume = 2f;
    }
    public void PlayMischievousDittySFX()
    {
        PlaySoundEffect(mischievousDitty);
        sfxChannels[currentSFXChannel].volume = .5f;
    }
    public void PlayOwSFX()
    {
        PlaySoundEffect(owClip);
        sfxChannels[currentSFXChannel].volume = .5f;
    }
    public void PlayReeowSFX()
    {
        PlaySoundEffect(reeowSFX);
        sfxChannels[currentSFXChannel].volume = .25f;
    }
    public void PlayReeowShortSFX()
    {
        PlaySoundEffect(reeowShortSFX);
        sfxChannels[currentSFXChannel].volume = .25f;
    }
    // This cycles the indices of the sfx channel list and makes "currentSFXChannel" appropriate throughout the class
    // - This is called by PlayMusic() and PlaySoundEffect() before stopping the sound/music, replacing the clip, and playing the new clip
    void NextSFXChannel()
    {
        currentSFXChannel++;
        if (currentSFXChannel > highestSFXChannel)
            currentSFXChannel = 0;

    }
    #endregion
}
