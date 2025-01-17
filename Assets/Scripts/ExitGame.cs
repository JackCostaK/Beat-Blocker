using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    private bool esc_text = false;
    private bool fade_out = false;
    private int timer = 0;
    public CanvasGroup cg;

    public GameStart sceneChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && esc_text)
        {
            sceneChange.SceneToGame(1);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            esc_text = true;
        }
        if (esc_text)
        {
            
            if (fade_out)
            {
                cg.alpha -= 0.005f;
            }
            else
            {
                cg.alpha += 0.005f;
            }
            if (cg.alpha >= 1)
            {
                fade_out = true;
            }
            else if (cg.alpha == 0)
            {
                timer += 1;
                fade_out = false;
            }
            if (timer >= 6)
            {
                timer = 0;
                esc_text = false;
            }

        }
        
    }
}
