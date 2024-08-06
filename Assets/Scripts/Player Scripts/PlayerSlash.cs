using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    [SerializeField]
    private GameObject slashPrefab;

    [SerializeField]
    private float attackCoolDown = 0.3f;

    private float attackTimer;

    private AudioSource audioSource;

    private Camera mainCamera;

    private Vector3 spawnPosition;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > attackTimer)
        {
            Slash();
            audioSource.Play();
            attackTimer = Time.time + attackCoolDown;
        }
    }

    void Slash()
    {
        spawnPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0f;

        Instantiate(slashPrefab, spawnPosition, Quaternion.identity);


    }



}//class
