using UnityEngine;

public class LoadingAnimation : MonoBehaviour
{
    public float RotationsPerSecond;

    // Start is called before the first frame update
    private SpriteRenderer _gameObjectRenderer;
    private Vector3 _rotationVector;
    private float _transparency;
    private bool _transparencyIncrement;
    void Start()
    {
        _transparency = 1.0f;
        _transparencyIncrement = false;
        _rotationVector = new Vector3(0, 0, 1);
        _gameObjectRenderer = this.GetComponentsInChildren<SpriteRenderer>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        float fps = (1.0f / Time.unscaledDeltaTime);
        float angle = (360/ fps) * RotationsPerSecond;

        float deltaTransparency = 1.0f / fps;
        if (_transparencyIncrement)
            _transparency += deltaTransparency;
        else
            _transparency -= deltaTransparency;

        if(_transparency >= 1.0f)
        {
            _transparency = 1.0f;
            _transparencyIncrement = false;
        }
        if (_transparency <= 0.0f)
        {
            _transparency = 0.0f;
            _transparencyIncrement = true;
        }

        _gameObjectRenderer.color = new Color(1f, 1f, 1f, _transparency);
        _gameObjectRenderer.transform.Rotate(_rotationVector, angle);
        
    }
}
