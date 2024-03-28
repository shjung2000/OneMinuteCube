using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Stack happens if:
Color of BCS5 (Base Cube Side 5) == Color of TCS6 (Top Cube Side 6)

Top Cube Sides that are directly opposite to each other:
Side 1 <-> Side 2
Side 3 <-> Side 4
Side 5 <-> Side 6

All the colors rgb:
Red -> (255 , 0 , 0 , 255)
Blue -> (0, 0 , 255 , 255)
Green -> (2 , 48 , 32 , 255)
Orange -> (255 , 95 , 0 , 255)
Purple -> (128 , 0 , 128 , 255)
White -> (255 , 255 , 255 ,  255)

Top Cube Respawn position:
Position Vector3 (0 , 6.5 , 0)

Base Cube Position:
Position Vector3 (0,0,0)

*/

public class TopCube : MonoBehaviour
{
    private Rigidbody CubeTop;
    private Rigidbody CubeBottom;
    private List<GameObject> CubeTopChildren = new List<GameObject>();
    private List<GameObject> CubeBottomChildren = new List<GameObject>();
    private bool comparison = false;
    public HandleScore scoreHandle;
    public TimeManage timeHandle;

    public GameObject pauseScreen;

    public bool checkTimeUp;

    Dictionary<int, GameObject> colorCubeSideMap;

    Dictionary<int, GameObject> colorBottomCubeSideMap;

    Dictionary<int, List<string>> colorMap = new Dictionary<int, List<string>>()
    {
        { 0, new List<string>{"255" , "95" , "0" , "255"} }, //0 -> Orange
        { 1, new List<string>{"255","0","0" ,"255"}},  //1 -> Red
        { 2, new List<string>{"0","0","255","255"}},   //2 -> Blue
        { 3, new List<string>{"2","48","32","255"}},   //3 -> Green
        { 4, new List<string>{"255","255","255","255"}},  //4 -> White
        { 5, new List<string>{"128","0", "128" , "255"}}  //5 -> Purple
    };

    List<List<string>> colorList = new List<List<string>>();

    // Start is called before the first frame update
    void Start()
    {
        CubeTop = GameObject.Find("TopCube").GetComponent<Rigidbody>();

        CubeBottom = GameObject.Find("BaseCube").GetComponent<Rigidbody>(); 

        colorCubeSideMap = new Dictionary<int, GameObject> {
            { 0, CubeTop.transform.GetChild(0).gameObject },
            { 1, CubeTop.transform.GetChild(1).gameObject },
            { 2, CubeTop.transform.GetChild(2).gameObject },
            { 3, CubeTop.transform.GetChild(3).gameObject },
            { 4, CubeTop.transform.GetChild(4).gameObject },
            { 5, CubeTop.transform.GetChild(5).gameObject }
        };

        colorBottomCubeSideMap = new Dictionary<int, GameObject>
        {
            { 0, CubeBottom.transform.GetChild(0).gameObject },
            { 1, CubeBottom.transform.GetChild(1).gameObject },
            { 2, CubeBottom.transform.GetChild(2).gameObject },
            { 3, CubeBottom.transform.GetChild(3).gameObject },
            { 4, CubeBottom.transform.GetChild(4).gameObject },
            { 5, CubeBottom.transform.GetChild(5).gameObject }
        };

        scoreHandle = GameObject.FindGameObjectWithTag("scoreHandler").GetComponent<HandleScore>();

        timeHandle = GameObject.FindGameObjectWithTag("timeHandler").GetComponent<TimeManage>();

        checkTimeUp = true;

        pauseScreen = GameObject.FindGameObjectWithTag("pauseScreen");
        pauseScreen.SetActive(false);
    }
        

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Rotation Sound
                FindObjectOfType<AudioManager>().Play("rotation");

                //Rotate the cube
                RandomizeCube(CubeTop);               
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Check if top color of bottom cube is same as bottom color of top cube

                //Get access to color of top child of bottom cube
                Color32 CubeBottomTopColor = getColor(CubeBottom, 4);

                //Get access to color of bottom child of top cube
                Color32 CubeTopBottomColor = getColor(CubeTop, 4);

                comparison = compareTwoSides(CubeBottomTopColor, CubeTopBottomColor);


                if (comparison)
                {
                    //The color matches

                    //Correct Sound Effect
                    FindObjectOfType<AudioManager>().Play("correctSound");

                    //Randomize the top and buttom cube
                    //Increment player score
                    scoreHandle.AddScore();
                    //ReplaceCube();
                    RandomizeCube(CubeTop);
                    RandomizeCube(CubeBottom);
                    comparison = false;
                    
                    
                }
                else
                {
                    //Color doesn't match and user made a mistake

                    //Wrong Sound Effect
                    FindObjectOfType<AudioManager>().Play("wrongSound");
                    
                    //Deduct the player score
                    scoreHandle.MinusScore();
                }
            }

            if(timeHandle.time == 0 && checkTimeUp)
            {
                //Time has up -> Game Over
                //1. Logic for saving the score
                //2. Change the scene to GameOver screen
                int highscore;
                RecordData data = SaveScore.loadHighScore();
                if(data == null)
                {
                    highscore = 0;
                }
                else
                {
                    highscore = data.score;
                }
                int playerScore = scoreHandle.PlayerScore;
                SaveScore.savePlayerScore(playerScore);  //Player Score always saved after the game ends
                if(playerScore > highscore)
                {
                    //Update the new highscore
                    SaveScore.savePlayerHighScore(playerScore, true);  //New High Score
                }
                else
                {
                    //High score doesn't change
                    SaveScore.savePlayerHighScore(highscore, false);
                }
                checkTimeUp = false;

                //Stop the clock sound
                FindObjectOfType<AudioManager>().Stop("clock");

                //Change to GameOver Screen
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

            FindObjectOfType<AudioManager>().Play("gameOver");
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {   
                Time.timeScale = 0;
                pauseGame();
            }
        }
       

        bool compareTwoSides(Color32 topSide, Color32 bottomSide)
        {
            string topR = topSide.r.ToString();
            string topG = topSide.g.ToString();
            string topB = topSide.b.ToString();
            string topA = topSide.a.ToString();

            string bottomR = bottomSide.r.ToString();
            string bottomG = bottomSide.g.ToString();
            string bottomB = bottomSide.b.ToString();
            string bottomA = bottomSide.a.ToString();

            if ((topR == bottomR) && (topG == bottomG) && (topB == bottomB) && (topA == bottomA))
            {
                return true;
            }

            return false;
        }


        void pauseGame()
        {
            pauseScreen.SetActive(true);
            FindObjectOfType<AudioManager>().Stop("clock");
        }

        Color32 getColor(Rigidbody obj, int side)
        {
            //Get access to respective child of respective cube
            GameObject CubeRespectiveSide = obj.transform.GetChild(side).gameObject;

            //Get access to color of respective child of respective cube
            Color32 returnColor = CubeRespectiveSide.GetComponent<MeshRenderer>().material.color;

            return returnColor;
        }

        /*
        void ReplaceCube()
        {
            //Change the color of the side tiles of the base cube to the top cube's

            //Get all the child of Top Cube and Bottom Cube
            for(int i = 0; i < 6; i++)
            {
                CubeTopChildren.Add(CubeTop.transform.GetChild(i).gameObject);
                CubeBottomChildren.Add(CubeBottom.transform.GetChild(i).gameObject);
            }

            for (int i = 0; i < 6; i++)
            {
                Color32 TopCubeSideColor = CubeTopChildren[i].GetComponent<MeshRenderer>().material.color;
                int r = Int32.Parse(TopCubeSideColor.r.ToString());
                int g = Int32.Parse(TopCubeSideColor.g.ToString());
                int b = Int32.Parse(TopCubeSideColor.b.ToString());
                int a = Int32.Parse(TopCubeSideColor.a.ToString());
                CubeBottomChildren[i].GetComponent<MeshRenderer>().material.color = new Color32((byte)r, (byte)g, (byte)b, (byte)a);
            }
            CubeTopChildren.Clear();
            CubeBottomChildren.Clear();
        }
        */

        void RandomizeCube(Rigidbody cube)
        {
            //New Top Cube will replace the old one by having different cube colors (which are randomized)
            System.Random rand = new System.Random();
            int counter = 0;
            Dictionary<int, int> checkDuplicate = new Dictionary<int, int>();
            while (counter < 6)
            {
                int randIndex = rand.Next(0, 6);
                if (!checkDuplicate.ContainsValue(randIndex))
                {
                    //A unique index
                    List<string> colorSet = colorMap[randIndex];
                    int r = Int32.Parse(colorSet[0].ToString());
                    int g = Int32.Parse(colorSet[1].ToString());
                    int b = Int32.Parse(colorSet[2].ToString());
                    int a = Int32.Parse(colorSet[3].ToString());
                    cube.transform.GetChild(counter).gameObject.GetComponent<MeshRenderer>().material.color = new Color32((byte)r, (byte)g, (byte)b, (byte)a);
                    checkDuplicate[counter] = randIndex;
                    counter++;
                }
                else
                {
                    continue;
                }
            }
            checkDuplicate.Clear();
        }
}







