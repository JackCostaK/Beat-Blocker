using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehaviour : MonoBehaviour
{
    public static ShakeBehaviour Instance;

    // Transform of the GameObject you want to shake
    private Transform move;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    [SerializeField]
    private float shakeMagnitude;

    [SerializeField]
    private float dampingSpeed;

    // The initial position of the GameObject
    Vector3 initialPosition;

    private void Start()
    {
        Instance = this;
    }

    void Awake()
    {
        if (move == null)
        {
            move = GetComponent(typeof(Transform)) as Transform;
        }
    }
    void OnEnable()
    {
        initialPosition = move.localPosition;
    }
    public void TriggerShake()
    {
        shakeDuration = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            move.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            move.localPosition = initialPosition;
        }
    }
}
