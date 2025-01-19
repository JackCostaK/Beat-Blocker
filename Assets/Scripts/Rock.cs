using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Rock : MonoBehaviour
{

    double timeInstantiated;
    public float assignedTime;
    public GameObject hitParticle;
    GameObject hitInstance;
    public GameObject hitSFXPrefab;
    GameObject hitSFXInstance;


    bool active = true;




    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
    }
    public void DestroyGlow(GameObject gameObject)
    {
        // compare children of game object
        for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            // only destroy tagged object
            if (gameObject.transform.GetChild(i).gameObject.tag == "glow")
                Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Shield") && active) {
            ScoreManager.Hit();

            hitInstance = Instantiate(hitParticle, transform);
            hitInstance.GetComponent<ParticleSystem>().Play();

            DestroyGlow(gameObject);


            transform.DetachChildren();

            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            active = false;

            
            




        }
        else if (other.gameObject.CompareTag("Player") && active)
        {
            ScoreManager.Miss();
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player") && active == false)
        {
            Destroy(gameObject);
            hitSFXInstance = Instantiate(hitSFXPrefab);
            hitSFXInstance.GetComponent<AudioSource>().PlayDelayed(0f);
            Destroy(hitSFXInstance, 3);
        }



    }
    // Update is called once per frame
    void Update()
    {

        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));


        if (t > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            var parentGameObject = this.transform.parent.gameObject;
            transform.localPosition = Vector3.Lerp(new Vector3(parentGameObject.GetComponent<Lane>().noteSpawnX, parentGameObject.GetComponent<Lane>().noteSpawnY,0f), new Vector3(parentGameObject.GetComponent<Lane>().noteDespawnX, parentGameObject.GetComponent<Lane>().noteDespawnY, 0f), t);

            if (active)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponentInChildren<SpriteRenderer>().enabled = true;
                GetComponentInChildren<Light2D>().enabled = true;
            }

        }
    }
}