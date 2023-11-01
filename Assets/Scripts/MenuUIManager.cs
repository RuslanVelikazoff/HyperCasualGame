using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public Button[] levelButton = new Button[2];

    public Button musicButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    public Button soundButton;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private void Start()
    {
        SetMusicSprite();
        SetSoundSprite();

        InitializeButton();
    }

    private void InitializeButton()
    {
        if (levelButton[0] != null)
        {
            levelButton[0].onClick.RemoveAllListeners();
            levelButton[0].onClick.AddListener(() =>
            {
                SceneManager.LoadScene(1);
            });
        }

        if (levelButton[1] != null)
        {
            levelButton[1].onClick.RemoveAllListeners();
            levelButton[1].onClick.AddListener(() =>
            {
                SceneManager.LoadScene(2);
            });
        }

        if (musicButton != null)
        {
            musicButton.onClick.RemoveAllListeners();
            musicButton.onClick.AddListener(() =>
            {
                if (PlayerPrefs.GetFloat("MusicVolume") == 0)
                {
                    AudioManager.instance.OnMusic();
                    SetMusicSprite();
                }
                else
                {
                    AudioManager.instance.OffMusic();
                    SetMusicSprite();
                }
            });
        }

        if (soundButton != null)
        {
            soundButton.onClick.RemoveAllListeners();
            soundButton.onClick.AddListener(() =>
            {
                if (PlayerPrefs.GetFloat("SoundVolume") == 0)
                {
                    AudioManager.instance.OnSound();
                    SetSoundSprite();
                }
                else
                {
                    AudioManager.instance.OffSound();
                    SetSoundSprite();
                }
            });
        }
    }

    private void SetSoundSprite()
    {
        if (PlayerPrefs.GetFloat("SoundVolume") == 0)
        {
            soundButton.GetComponent<Image>().sprite = soundOffSprite;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = soundOnSprite;
        }
    }

    private void SetMusicSprite()
    {
        if (PlayerPrefs.GetFloat("MusicVolume") == 0)
        {
            musicButton.GetComponent<Image>().sprite = musicOffSprite;
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = musicOnSprite;
        }
    }
}
