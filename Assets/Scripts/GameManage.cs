using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    public static int currentLevelIndex;
    int index;
    [SerializeField] AudioSource music;
    [SerializeField] PlayerControls playerControls;
    private void Awake()
    {
        music = GetComponent<AudioSource>();
        index = SceneManager.GetActiveScene().buildIndex;
    }
    private void Start()
    {
        music.Play();
    }
    private void Update()
    {
        if (playerControls.driveMode != 1)
        {
            music.volume -= 1.0f * Time.deltaTime * 3.0f;
        }
    }
    public enum State
    {
        Playing,
        Loss,
    }
    public State currentState { get; private set; }
    public void OnPlayerDied()
    {
        if (currentState != State.Playing) return;
        currentState = State.Loss;
    }
    public int LevelIndex
    {
        get => PlayerPrefs.GetInt("LevelIndex", 0);
        private set
        {
            PlayerPrefs.SetInt(LevelIndexKey, value);
            PlayerPrefs.Save();
        }
    }
    private const string LevelIndexKey = "LevelIndex";
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    public void Quit()
    {
        Application.Quit();
    }
}

