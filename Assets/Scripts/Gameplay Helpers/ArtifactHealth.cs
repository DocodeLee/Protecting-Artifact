using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactHealth : MonoBehaviour
{

    [SerializeField]
    private Slider artifactHealthSlider;

    [SerializeField]
    private Artifact artifact;

    // Start is called before the first frame update
    void Start()
    {
        artifactHealthSlider.maxValue = artifact.maxHealth;
        artifactHealthSlider.value = artifact.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        artifactHealthSlider.value = artifact.health;
    }
}
