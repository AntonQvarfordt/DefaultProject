using UnityEngine;
using UnityEngine.Audio;
[RequireComponent(typeof(AudioSourcePool))]
[RequireComponent(typeof(BackgroundMusicPlayer))]
public class AudioManager : MonoBehaviour
{
    public AudioMixer AudioMixer;

    public static AudioManager Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<AudioManager>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }

    protected static AudioManager instance;

    private AudioSourcePool _audioSourcePool;
    private BackgroundMusicPlayer _musicPlayer;

    public static AudioManager Create()
    {
        GameObject sceneControllerGameObject = new GameObject("AudioManager");
        instance = sceneControllerGameObject.AddComponent<AudioManager>();

        return instance;
    }

    private void Awake()
    {
        _audioSourcePool = GetComponent<AudioSourcePool>();
        _musicPlayer = GetComponent<BackgroundMusicPlayer>();
    }

    private void Start()
    {
        Debug.Log(_musicPlayer);
    }

    public void PlayClipTrigger (AudioClip clip)
    {
        PlayOneShot(clip);
    }

    public void PlayOneShot (AudioClip clip, AudioMixerGroup mixerGroup = null, float volume = 1, float pitch = 1)
    {
        if (mixerGroup == null)
            mixerGroup = AudioMixer.FindMatchingGroups("SFXMaster/SFX")[0];

        var audioSourceObject = _audioSourcePool.GetAudoSourceObject();
        var audioSource = audioSourceObject.GetAudioSource;


        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.outputAudioMixerGroup = mixerGroup;

        audioSource.Play();

        _audioSourcePool.ReturnToPoolOnNotPlaying(audioSourceObject);
    }
}
