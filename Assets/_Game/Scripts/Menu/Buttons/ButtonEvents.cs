using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour {
    [SerializeField] private Object _mainSchene;
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _creditMusic;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private CanvasGroup _creditsGroup;
    [SerializeField] private CanvasGroup _menuGroup;

    private void Start() {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnPlayButton() {
        SceneManager.LoadScene(_mainSchene.name, LoadSceneMode.Single);
    }

    public void OnExitButton() {
        Application.Quit();
    }

    public void OnCreditButton() {
        float t = _audioSource.time;
        _audioSource.clip = _creditMusic;
        _audioSource.time = t;
        _audioSource.Play();

        _creditsGroup.alpha = 1;
        _menuGroup.alpha = 0;
    }

    public void OnMenuButton() {
        float t = _audioSource.time;
        _audioSource.clip = _menuMusic;
        _audioSource.time = t;
        _audioSource.Play();

        _creditsGroup.alpha = 0;
        _menuGroup.alpha = 1;
    }
}