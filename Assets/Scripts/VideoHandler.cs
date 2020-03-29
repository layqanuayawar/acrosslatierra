using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoHandler : MonoBehaviour
{
    public Material videoMaterial;
    public UnityEngine.Video.VideoPlayer videoPlayer;

    private Material defaultSkybox;
    // TODO: Issue #1 Trying to create skybox material dynamically
    // in script instead of in hierarchy.
    // private Material newSkybox;
    
    private GameObject floor;
    private GameObject teleportation;
    private GameObject[] deactivate;

    private bool initialGrab = true;
    private Coroutine co;

    public void InitiateVideo()
    {
        // on initial grab, need to set/store original values
        if (initialGrab)
        {
            gameObject.tag = "Untagged";

            defaultSkybox = RenderSettings.skybox;
            // newSkybox = new Material(Shader.Find("Skybox/Panoramic"));
            // newSkybox.SetTexture("_MainTex", videoPlayer.targetTexture);

            floor = GameObject.Find("MainFloor");
            teleportation = GameObject.Find("Teleportation");
            deactivate = GameObject.FindGameObjectsWithTag("Timeline");
            
            initialGrab = false;
        }

        if (co != null) { StopCoroutine(co); }
        co = StartCoroutine(StartVideoCoroutine());
    }

    IEnumerator StartVideoCoroutine()
    {
        // On swap, check if video already playing so no need to update anything
        if (!videoPlayer.isPlaying)
        {
            // as long as coroutine isn't stopped (object is held on to) load scene occurs
            // min of 2 seconds before video played. Prepare() may make wait time longer.
            videoPlayer.Prepare();
            yield return new WaitForSeconds(2);
            while (!videoPlayer.isPrepared) { yield return null; }

            // once ready, then update everything
            // RenderSettings.skybox = newSkybox;
            RenderSettings.skybox = videoMaterial;
            videoPlayer.Play();

            foreach (GameObject element in deactivate)
            {
                element.SetActive(false);
            }

            floor.GetComponent<MeshRenderer>().enabled = false;
            teleportation.SetActive(false);
        }
    }

    public void TerminateVideo()
    {
        if (co != null) { StopCoroutine(co); }
        co = StartCoroutine(StopVideoCoroutine());
    }

    IEnumerator StopVideoCoroutine()
    {
        yield return new WaitForSeconds(1);

        // stop video, return everything to previous state
        RenderSettings.skybox = defaultSkybox;
        videoPlayer.Stop();

        gameObject.tag = "Timeline";
        foreach (GameObject element in deactivate)
        {
            element.SetActive(true);
        }

        floor.GetComponent<MeshRenderer>().enabled = true;
        teleportation.SetActive(true);

        // finally destroy material created for skybox
        // Destroy(newSkybox);
    }
}
