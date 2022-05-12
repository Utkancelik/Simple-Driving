using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float speedGainPerSecond = 0.2f;
    [SerializeField] private float rotationSpeed = 10f;


    private float steerValue;
    private void Update()
    {
        speed += speedGainPerSecond * Time.deltaTime;
        transform.Rotate(0f, steerValue * rotationSpeed * Time.deltaTime, 0f);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

     

    }

    public void Steer(int value)
    {
        steerValue = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene("Scene_MainMenu");
        }
    }

}
