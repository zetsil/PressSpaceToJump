using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    // Speed at which the character moves to the left
    public float moveSpeed = 2f;

    // Amplitude of the wave (height of the sine wave)
    public float waveAmplitude = 1f;

    // Frequency of the wave (how often the wave oscillates)
    public float waveFrequency = 2f;

    // Used to track time for the sine wave calculation
    private float time;

    void Update()
    {
        // Increment time based on the wave frequency
        time += Time.deltaTime * waveFrequency;

        // Calculate the vertical offset using the sine function
        float verticalOffset = Mathf.Sin(time) * waveAmplitude;

        // Move the character to the left while applying the vertical offset
        transform.position += new Vector3(moveSpeed * Time.deltaTime, verticalOffset * Time.deltaTime, 0);
    }
}