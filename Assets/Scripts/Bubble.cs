using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 1.5f;
    [SerializeField] private float driftStrength = 0.5f;
    [SerializeField] private float lifetime = 3f;

    private float driftDirection;
    private Coroutine disableCoroutine;

    private void OnEnable()
    {
        driftDirection = Random.Range(-1f, 1f);

        if (disableCoroutine != null)
            StopCoroutine(disableCoroutine);

        disableCoroutine = StartCoroutine(AutoDisable());
    }

    private void Update()
    {
        Vector3 drift = new Vector3(driftDirection * driftStrength, floatSpeed, 0f);
        transform.Translate(drift * Time.deltaTime);

        if (transform.position.y > 1.5f || transform.position.x > 1.5f || transform.position.x < -1.5f)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
}
