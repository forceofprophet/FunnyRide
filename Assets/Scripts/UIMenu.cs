using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMenu : MonoBehaviour
{
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameObject SettingsMenuUI;
    [SerializeField] GameObject InGameMenuUI;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject LossMenuUI;
    [SerializeField] GameObject RecordMenuUI;
    [SerializeField] GameObject WastedMenuUI;
    [SerializeField] PlayerControls playerControls;
    public Text score;
    public Text highScore;
    public Text newRecord;

    private void Start()
    {
        Main();
        Time.timeScale = 0f;
        
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        newRecord.text = PlayerPrefs.GetInt("NewRecord", 0).ToString();
    }
    private void Update()
    {
        score.text = playerControls.score.ToString();
        if (playerControls.totalScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", playerControls.totalScore);
            highScore.text = playerControls.totalScore.ToString();
        }
        if (PlayerPrefs.GetInt("HighScore", 0) >= PlayerPrefs.GetInt("NewRecord", 0))
        {
            RecordMenuUI.SetActive(true);
            PlayerPrefs.SetFloat("NewRecord", playerControls.totalScore);
            newRecord.text = playerControls.totalScore.ToString();
        }
    }
    public void Loss()
    {
        StartCoroutine("GameOver");
        score.text = playerControls.totalScore.ToString();
    }
    public void Main()
    {
        MainMenuUI.SetActive(true);
        SettingsMenuUI.SetActive(false);
        InGameMenuUI.SetActive(false);
        PauseMenuUI.SetActive(false);
        LossMenuUI.SetActive(false);
        RecordMenuUI.SetActive(false);
        WastedMenuUI.SetActive(false);
    }
    public void Settings()
    {
        MainMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(true);
        InGameMenuUI.SetActive(false);
        PauseMenuUI.SetActive(false);
        LossMenuUI.SetActive(false);
        RecordMenuUI.SetActive(false);
        WastedMenuUI.SetActive(false);
    }
    public void Pause()
    {
        MainMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        InGameMenuUI.SetActive(true);
        PauseMenuUI.SetActive(true);
        LossMenuUI.SetActive(false);
        RecordMenuUI.SetActive(false);
        WastedMenuUI.SetActive(false);
        Time.timeScale = 0f;
    }
    public void BackToGame()
    {
        MainMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        InGameMenuUI.SetActive(true);
        PauseMenuUI.SetActive(false);
        LossMenuUI.SetActive(false);
        RecordMenuUI.SetActive(false);
        WastedMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Wasted()
    {
        MainMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        InGameMenuUI.SetActive(true);
        PauseMenuUI.SetActive(false);
        LossMenuUI.SetActive(false);
        RecordMenuUI.SetActive(false);
        WastedMenuUI.SetActive(true);
    }
    IEnumerator GameOver()
    {
        MainMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        InGameMenuUI.SetActive(true);
        PauseMenuUI.SetActive(false);
        LossMenuUI.SetActive(false);
        RecordMenuUI.SetActive(false);
        WastedMenuUI.SetActive(true);
        yield return new WaitForSeconds(4.0f);
        MainMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        InGameMenuUI.SetActive(true);
        PauseMenuUI.SetActive(false);
        LossMenuUI.SetActive(true);
        RecordMenuUI.SetActive(false);
        WastedMenuUI.SetActive(false);
        yield break;
    }
}
