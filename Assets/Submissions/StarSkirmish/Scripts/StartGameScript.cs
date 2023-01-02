using UnityEngine;

public class StartGameScript : MonoBehaviour {
    [SerializeField] Transform _gameplayRoot;
    [SerializeField] Transform _menuRoot;
	[SerializeField] GameObject _gameplayPrefab;

    private GameObject _activeGamePlay;
    
	public void StartGameplay() {
        if (_activeGamePlay != null)
        {
            StopGamePlay();
        }

        _menuRoot.gameObject.SetActive(false);
        _gameplayRoot.gameObject.SetActive(true);
        _activeGamePlay = Instantiate(_gameplayPrefab, _gameplayRoot);
    }

    public void StopGamePlay()
    {
        _menuRoot.gameObject.SetActive(true);
        _gameplayRoot.gameObject.SetActive(false);
        Destroy(_activeGamePlay);
    }
}
