using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimateClick : MonoBehaviour
{

    public Image image;
    private float speed;
    private float setAlpha;
    bool maxAlpha;
    bool minAlpha;
    // Start is called before the first frame update
    void Start()
    {
        image = GameObject.Find("clickImage").GetComponent<Image>();
        speed = 220;
        setAlpha = 255;
        maxAlpha = true;
        minAlpha = false;
        image.color = new Color32(226 , 226 , 226 , (byte)setAlpha ); 
    }

    // Update is called once per frame
    void Update()
    {
        if (maxAlpha && !minAlpha)
        {
            //Have yet to hit 0 for alpha
            setAlpha -= Time.deltaTime * speed;
            image.color = new Color32(226, 226, 226, (byte)setAlpha);

            if(setAlpha < 0)
            {   
                //Reached 0 for alpha
                setAlpha = 0;
                image.color = new Color32(226, 226, 226, (byte)setAlpha);
                maxAlpha = false;
                minAlpha = true;
            }
        }

        if(!maxAlpha && minAlpha)
        {
            //Have yet to hit 255 for alpha
            setAlpha += Time.deltaTime * speed;
            image.color = new Color32(226 , 226 , 226 , (byte)setAlpha );

            if (setAlpha > 255)
            {
                setAlpha = 255;
                image.color = new Color32(226, 226, 226, (byte)setAlpha);
                maxAlpha = true;
                minAlpha = false;
            }
        }
    }

    public void OpenMainMenu()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }

    public void OpenInstruction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }


    
}
