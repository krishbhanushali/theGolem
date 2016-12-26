using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SettingsPopup : MonoBehaviour
{
    [SerializeField]
    private AudioClip sound;
    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private Slider soundSlider;

   // [SerializeField]
    //private Toggle soundToggle;

    //[SerializeField]
    //private Toggle musicToggle;

    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void OnSpeedValueForPlayer(float speed)
    {
        Debug.Log(speed);
        PlayerPrefs.SetFloat("speed", speed);
        Messenger<float>.Broadcast(GameEvent.PLAYER_SPEED_CHANGED, speed);
        Messenger<float>.Broadcast(GameEvent.PLAYER_MOVE_SPEED_CHANGED, speed);
    }

    public void OnSpeedValueForEnemy(float speed)
    {
        PlayerPrefs.SetFloat("speed", speed);
        Messenger<float>.Broadcast(GameEvent.ENEMY_SPEED_CHANGED, speed);
    }

    public void OnSoundToggle()
    {
        Managers.Audio.soundMute = !Managers.Audio.soundMute;
        if (Managers.Audio.soundMute)
        {
            soundSlider.value = 0.0f;
            soundSlider.interactable = false;
        }
        else
        {
            soundSlider.value = 1.0f;
            soundSlider.interactable = true;
        }
        Managers.Audio.PlaySound(sound);
    }
    public void OnSoundValue(float volume)
    {
        Managers.Audio.soundVolume = volume;
    }
    public void OnMusicToggle()
    {
        Managers.Audio.musicMute = !Managers.Audio.musicMute;
        if (Managers.Audio.musicMute)
        {
            musicSlider.value = 0.0f;
            musicSlider.interactable = false;
        }
        else
        {
            musicSlider.value = 1.0f;
            musicSlider.interactable = true;
        }
        Managers.Audio.PlaySound(sound);
    }
    public void OnMusicValue(float volume)
    {
        Managers.Audio.musicVolume = volume;
    }

    public void OnPlayMusic(int selector)
    {
        Managers.Audio.PlaySound(sound);
        switch (selector)
        {
            case 1:
                Managers.Audio.PlayIntroMusic();
                break;
            case 2:
                Managers.Audio.PlayLevel1Music();
                break;
            default:
                Managers.Audio.StopMusic();
                break;
        }
    }

    /*public void OnMusicToggle()
    {
        Managers.Audio.musicMute =
        !Managers.Audio.musicMute;
        Managers.Audio.PlaySound(sound);
    }
    public void OnMusicValue(float volume)
    {
        Managers.Audio.musicVolume = volume;
    }*/
}