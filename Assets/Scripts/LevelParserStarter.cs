using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParserStarter : MonoBehaviour
{
    public string filename;

    public GameObject Rock;

    public GameObject Brick;

    public GameObject QuestionBox;

    public GameObject Stone;

    public Transform parentTransform;
    // Start is called before the first frame update
    void Start()
    {
        RefreshParse();
    }


    private void FileParser()
    {
        string fileToParse = string.Format("{0}{1}{2}.txt", Application.dataPath, "/Resources/", filename);
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            //row is our y value, we will decrement it each pass
            float row = 14.5f;

            while ((line = sr.ReadLine()) != null)
            {
                //column is our x value, it will increment each pass
                float column = 0.5f;
                char[] letters = line.ToCharArray();
                foreach (var letter in letters)
                {
                    //location is set to the x and y we've calculated during our loop
                    Vector3 location = new Vector3(column, row);
                    //spawn the object based on what object it should be and the location we calculated
                    SpawnPrefab(letter, location);
                    //increment column by 1
                    column++;
                }
                //decrement row by 1
                row--;
            }
            sr.Close();
        }
    }

    private void SpawnPrefab(char spot, Vector3 positionToSpawn)
    {
        GameObject ToSpawn;

        switch (spot)
        {
            case 'b': ToSpawn=Brick; break;
            case '?': ToSpawn=QuestionBox; break;
            case 'x': ToSpawn=Stone; break;
            case 's': ToSpawn=Rock; break;
            //default: Debug.Log("Default Entered"); break;
            default: return;
                //ToSpawn = //Brick;       break;
        }
        ToSpawn = GameObject.Instantiate(ToSpawn, parentTransform);
        //spawn at the location we came up with
        ToSpawn.transform.localPosition = positionToSpawn;
    }

    public void RefreshParse()
    {
        GameObject newParent = new GameObject();
        newParent.name = "Environment";
        newParent.transform.position = parentTransform.position;
        newParent.transform.parent = this.transform;
        
        if (parentTransform) Destroy(parentTransform.gameObject);

        parentTransform = newParent.transform;
        FileParser();
    }
}
