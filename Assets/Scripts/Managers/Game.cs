using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Game : MonoBehaviour
{
    public TextMeshProUGUI txtScore;

    public Player player;

    public static Game current;

    public GameObject readyOverlay;
    public GameObject playOverlay;
    public GameObject gameOverOverlay;
    public GameObject victoryOverlay;
    public GameObject highScoreTitle;
    public GameObject highScoreTitleWin;

    public FloatingText prefabFloater;

    AudioSource _audio;

    const string prefsBestScore = "best_score_v0.1";
    int _score = 0;
    int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            txtScore.text = $"Damage: {value}";
        }
    }
    int _best;

    private void Awake()
    {
        current = this;
        _audio = GetComponent<AudioSource>();

        State = GameState.GetReady;
        highScoreTitle.SetActive(false);
    }

    void Start()
    {
        _best = PlayerPrefs.GetInt(prefsBestScore, 0);
        Score = 0;
    }

    public void AddScore(int value)
    {
        Score += value;
    }

    public void PlayAudio(AudioClip clip)
    {
        _audio.PlayOneShot(clip);
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver(bool isWin)
    {
        bool isRecord = SaveScore();

        if(isWin)
        {
            State = GameState.Win;
            highScoreTitleWin.SetActive(isRecord);
        }
        else
        {
            State = GameState.GameOver;
            highScoreTitle.SetActive(isRecord);
        }
    }

    public void StartPlaying()
    {
        State = GameState.Playing;
        player.OnTap();
    }

    // returns true if set highscore
    bool SaveScore()
    {
        if(_score > PlayerPrefs.GetInt(prefsBestScore, 0))
        {
            PlayerPrefs.SetInt(prefsBestScore, _score);
            return true;
        }

        return false;
    }

    public FloatingText ShowFloater(string msg, Vector3 position, Transform parent = null)
    {
        return FloatingText.Create(prefabFloater, msg, position, parent);
    }

    GameState _state;
    public GameState State
    {
        set
        {
            _state = value;
            
            readyOverlay.SetActive(value == GameState.GetReady);
            playOverlay.SetActive(value == GameState.Playing);
            gameOverOverlay.SetActive(value == GameState.GameOver);
            victoryOverlay.SetActive(value == GameState.Win);

            Time.timeScale = (value == GameState.GetReady) ? 0f:1f;
        }

        get
        {
            return _state;
        }
    }
}

public enum GameState
{
    GetReady,
    Playing,
    GameOver,
    Win,
}

