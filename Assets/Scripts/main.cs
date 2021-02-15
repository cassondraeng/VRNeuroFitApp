using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//Struct stroop holds onto text and color values yayyyy
public class Stroop
{
    public string Word { get; set; }
    public Color Color { get; set; }
    public bool isCon { get; set; }

}

//This struct is 100% for data recording! Yayyyyyy
public struct trialInfo
{
    public string w;
    public string keyPress;
    public string color;
    public float responseTime;
    public bool correct;
    public bool noResponse;
    public bool isCongruent;
}


public class main : MonoBehaviour
{

    //Objects interacted with to present stroop words
    public GameObject Cross;
    public Text WordManager;
    public GameObject Correct;
    public GameObject Wrong;
    public Animator Anims;
    public GameObject RestScreen;
    public GameObject NextScreen;
    public InputSend inputSend;
    public InputSend NotAdrians;
    public InputSend MediaData; // new
    public questionaire_strings questions;
    public questionaire_strings MediaQuestions; // new

    //Objects interacted with to record experiment info (credentials, user info)
    public GameObject ExperimenterInput;
    public GameObject PlayerInput;

    //Bool telling us if rightWrong is running or not
    private bool rwRun = false;

    //Which trial we are currently on (0 indexing)
    public int trialCount;

    //Our array of blocks
    public List<trialInfo[]> blocks = new List<trialInfo[]>();

    //Time Values
    public readonly float rwTime = 0.5f;

    private List<Stroop> trial = new List<Stroop>();
    private Color[] colors = { Color.red, Color.green, Color.cyan, Color.yellow };
    private string[] words = { "RED", "GREEN", "BLUE", "YELLOW" };

    private string RGBYtoString(Color c)
    {
        if (c == Color.red) return "red";
        else if (c == Color.cyan) return "blue";
        else if (c == Color.green) return "green";
        else if (c == Color.yellow) return "yellow";
        else return "";
    }


    // Start is called before the first frame update
    void Start()
    {
        WordManager.text = "";
        DontDestroyOnLoad(this);
    }

    private void buildTrial(int con, int incon)
    {
        System.Random rand = new System.Random();

        //choose 48 random Texts from text list
        //32 texts will be congruent, 16 texts will be incongruent
        for (int i = 0; i < (con+incon); i++)
        {
            int index = rand.Next(4);
            //For our congruent
            if (i < con) trial.Add(new Stroop { Word = words[index], Color = colors[index] , isCon = true});
            else //For when (32 <= i < 48)
            {
                int[] indices = new int[3]; //indicies will exclude the index of the color we would want at index
                //When we find the index we don't want to repeat, skip over it ;)
                int loc = 0;
                for (int k = 0; k < 3; k++)
                {
                    if (loc == index) loc++;
                    indices[k] = loc;
                    loc++;
                }
                trial.Add(new Stroop { Word = words[index], Color = colors[indices[rand.Next(3)]] , isCon = false});
            }
        }
    }

    IEnumerator DisplayStroop(int length,trialInfo[] info,bool isTest)
    {
        System.Random rand = new System.Random();
        bool bPressed = false;
        for (int i = 0; i < length; i++)
        {
            //Display the cross for a second.
            int index = rand.Next(length - i);
            Cross.SetActive(true);

            yield return new WaitForSeconds(1);

            Cross.SetActive(false);

            yield return new WaitForSeconds(0.2f);


            Stroop s = trial[index];
            trial.RemoveAt(index);
            //Put word into WordManager
            WordManager.enabled = true;
            WordManager.text = s.Word;
            WordManager.color = s.Color;

            //Update our info array with the word and color
            info[i].w = s.Word;
            info[i].color = RGBYtoString(s.Color);
            info[i].isCongruent = s.isCon;

            //Display the word and check input from the user for a second
            float duration = 1.0f;
            while (duration > 0.0f)
            {
                //Check for key
                //If key press, then update info with response time and correctness
                if (!bPressed && Input.GetKeyDown(KeyCode.R))
                {
                    bPressed = true;
                    info[i].noResponse = false;
                    info[i].keyPress = "R";
                    WordManager.enabled = false;
                    info[i].responseTime = 1.0f - duration;
                    if (WordManager.color == Color.red)
                    {
                        info[i].correct = true;
                        StartCoroutine(rightWrong(true));
                        yield return new WaitWhile(() => rwRun == true);
                    }
                    else
                    {
                        info[i].correct = false;
                        StartCoroutine(rightWrong(false));
                        yield return new WaitWhile(() => rwRun == true);
                    }

                }
                if (!bPressed && Input.GetKeyDown(KeyCode.Y))
                {
                    bPressed = true;
                    info[i].noResponse = false;
                    info[i].keyPress = "Y";
                    WordManager.enabled = false;
                    info[i].responseTime = 1.0f - duration;
                    if (WordManager.color == Color.yellow)
                    {
                        info[i].correct = true;
                        StartCoroutine(rightWrong(true));
                        yield return new WaitWhile(() => rwRun == true);
                    }
                    else
                    {
                        info[i].correct = false;
                        StartCoroutine(rightWrong(false));
                        yield return new WaitWhile(() => rwRun == true);
                    }
                }
                if (!bPressed && Input.GetKeyDown(KeyCode.G))
                {
                    bPressed = true;
                    info[i].noResponse = false;
                    info[i].keyPress = "G";
                    WordManager.enabled = false;
                    info[i].responseTime = 1.0f - duration;
                    if (WordManager.color == Color.green)
                    {
                        info[i].correct = true;
                        StartCoroutine(rightWrong(true));
                        yield return new WaitWhile(() => rwRun == true);
                    }
                    else
                    {
                        info[i].correct = false;
                        StartCoroutine(rightWrong(false));
                        yield return new WaitWhile(() => rwRun == true);
                    }
                }

                if (!bPressed && Input.GetKeyDown(KeyCode.B))
                {
                    bPressed = true;
                    info[i].noResponse = false;
                    info[i].keyPress = "B";
                    WordManager.enabled = false;
                    info[i].responseTime = 1.0f - duration;
                    if (WordManager.color == Color.cyan)
                    {
                        info[i].correct = true;
                        StartCoroutine(rightWrong(true));
                        yield return new WaitWhile(() => rwRun == true);
                    }
                    else
                    {
                        info[i].correct = false;
                        StartCoroutine(rightWrong(false));
                        yield return new WaitWhile(() => rwRun == true);
                    }
                }
                duration -= Time.deltaTime;
                yield return null;
            }

            //If our user never inputs anything, note this
            if (WordManager.enabled == true)
            {
                info[i].correct = false;
                info[i].noResponse = true;
                info[i].keyPress = "N/A";
                WordManager.enabled = false;
                StartCoroutine(rightWrong(false));
                yield return new WaitWhile(() => rwRun == true);
            }
            bPressed = false;
            //SAVE THIS TRIAL HERE AAAAH
            if (!isTest) saveSingleTrial(info[i]);
            yield return new WaitForSeconds(0.2f);
        }
        //If this is not the final task, prepare to reset. Else don't reset.
        if (trialCount < 5)
            Anims.SetTrigger("trialDone");
        else
        {
            Anims.SetTrigger("AllDone");
            //saveFullTest();
        }
        //Add this trial to blocks
        if (!isTest) blocks.Add(info);

        yield return null;
    }

    IEnumerator rightWrong(bool b)
    {
        rwRun = true;
        if (b)
        {
            Correct.SetActive(true);
            yield return new WaitForSeconds(rwTime);
            Correct.SetActive(false);
        }
        else
        {
            Wrong.SetActive(true);
            yield return new WaitForSeconds(rwTime);
            Wrong.SetActive(false);
        }
        yield return new WaitForSeconds(rwTime);
        rwRun = false;

    }


    public void start_test() {
        trialInfo[] test = new trialInfo[2]; //24
        buildTrial(1, 1); //(16,8)
        Anims.SetTrigger("Start Test");
        StartCoroutine(DisplayStroop(2, test,true)); //24
    }

    public void start_trial()
    {
        trialInfo[] tmp = new trialInfo[2]; //48
        buildTrial(1,1); //(32,16)
        trialCount++;
        StartCoroutine(DisplayStroop(2, tmp,false)); //48

    }

  //Writing out pseudo in meantime
  //trialInfo[i] has: [w,color,responseTime, correct, noResponse, isCongruent]

  //Congruent_correct   = [isCongruent=true, correct true]
  //Congruent_errors    = [isCongruent=true, correct false]
  //Congruent_accuracy  = Congruent_correct / trialInfo.Length
  //Congruent_RT        = SUM[isCongruent=true,responseTime] / trialInfo.Length

  //Just a thought,split trialInfo into chunks of congruent and incongruent for storing this data.

  //Incongruent_correct = [isCongruent=false, correct true]
  //Incongruent_errors  = [isCongruent=false, correct false]
  //Incongruent_accuracy= Congruent_false / trialInfo.Length
  //Incongruent_RT      = SUM[isCongruent=true,responseTime] / trialInfo.Length

  // Save the averaged data over ALL trials and ALL blocks of this test
  public void saveFullTest() {
    // Save the full test data

        //Data to collect throughout running of algorithm
        int c_c = 0;
        int c_e = 0;
        int ic_c = 0;
        int ic_e = 0;

        float cRT_total = 0f;
        float iRT_total = 0f;

        //5*48 (160 congruent, 80 incongruent)
        int n_c_trials = 160;
        int n_i_trials = 80;
        foreach(trialInfo[] t in blocks)
        {
            foreach(trialInfo i in t)
            {
                //Add up correct/incorrect answers. Sum total response times
                if (i.isCongruent)
                {
                    if (i.correct) c_c++;
                    else c_e++;

                    cRT_total += i.responseTime;
                }
                else
                {
                    if (i.correct) ic_c++;
                    else ic_e++;

                    iRT_total += i.responseTime;
                }
            }
        }
        float Congruent_Accuracy = c_c / n_c_trials;
        float Incongruent_Accuracy = ic_c / n_i_trials;
        float CongruentRT = cRT_total / n_c_trials;
        float InCongruentRT = iRT_total / n_i_trials;
        string[] info = {
            c_c.ToString(), c_e.ToString(), Congruent_Accuracy.ToString(), CongruentRT.ToString(),
            ic_c.ToString(), ic_e.ToString(), Incongruent_Accuracy.ToString(), InCongruentRT.ToString(),
            n_c_trials.ToString(), n_i_trials.ToString()
        };
        // System.DateTime.Today.ToString(),
        // etc.
        DateTime timeStart, timeStop = DateTime.Today;

      DataSaving.sex = inputSend.happy[(int) HeaderType.Sex];
      DataSaving.version = "n/a";
      DataSaving.birthdate = inputSend.happy[(int) HeaderType.DOB];
      DataSaving.computerDistance = inputSend.happy[(int) HeaderType.Computer_Distance];
      
      

    var no_commas = questions.questions.Select(s => String.Concat("MIND_",String.Concat(s.Where<char>(c => c != ','))));
    var media_no_commas = MediaQuestions.questions.Select(s => String.Concat(s.Where<char>(c => c != ','))); // new
    DataSaving.StoopHeader = DataSaving.StoopHeader.Concat(no_commas).Concat(media_no_commas).ToArray();

    //Fix up the Mindfulness questions with an additional Prefix
    /*for(int i = 0; i < NotAdrians.happy.Length; i++)
    {
            NotAdrians.happy[i] = String.Concat("MIND_",NotAdrians.happy[i]);
    }*/


    var BetterHappy = CalculateTime(inputSend.happy);

    DataSaving.SaveData(inputSend.happy[(int)HeaderType.ID], BetterHappy,info.Concat(NotAdrians.happy).Concat(MediaData.happy).ToArray());

    // Also save single trial data
    DataSaving.SaveLearningCurveData(inputSend.happy[(int)HeaderType.ID], learningCurveDataString);
    learningCurveDataString = "";
  }

  private string learningCurveDataString = "";
  private void saveSingleTrial(trialInfo TrialInfo) {
    // Learning Curve Header
    //{ "word name", "ink color", "c/ic", "tablerow number", "pressed key",
    //  "response (1=correct. 2=wrong. 3=timeout)", "RT (ms)" };

    string wordName = TrialInfo.w;
    string inkColor = TrialInfo.color;
    string c_ic = TrialInfo.isCongruent ? "1" : "0";
    string tablerow_number = "SHOULD WE REMOVE THIS";
    string pressed_key = TrialInfo.keyPress;
    string response = TrialInfo.noResponse ? "3" : (TrialInfo.correct ? "1" : "2");
    string RT = TrialInfo.responseTime.ToString();

    // Assemble the data array here
    string[] data = {
      wordName, inkColor, c_ic, tablerow_number, pressed_key, response, RT
    };

    learningCurveDataString += "\n" + string.Join(",", data);
  }

  private string index2APM(string index)
  {
        if (index == "0")
            return "AM";
        else
            return "PM";

  }  

  private string[] CalculateTime(string[] ogHappy)
  {
        string startHrs = ogHappy[(int)HeaderType.StartHrs];
        string startMins = ogHappy[(int)HeaderType.StartMins];
        string startAPM = ogHappy[(int)HeaderType.StartAPM];
        string stopHrs = ogHappy[(int)HeaderType.StopHrs];
        string stopMins = ogHappy[(int)HeaderType.StopMins];
        string stopAPM = ogHappy[(int)HeaderType.StopAPM];

        string stAPMA = index2APM(startAPM);
        string stAPMB = index2APM(stopAPM);

        string StartTime = startHrs + ":" + startMins + " " + stAPMA;
        string StopTime = stopHrs + ":" + stopMins + " " + stAPMB;

        float StartCongregate = float.Parse(startHrs) + (float.Parse(startMins) / 60f);
        float StopCongregate = float.Parse(stopHrs) + (float.Parse(stopMins) / 60f);
        float totalSlept;
        if(startAPM == stopAPM)
        {
            totalSlept = StopCongregate - StartCongregate;
        }
        else
        {
            totalSlept = 12f - StartCongregate + StopCongregate;
        }

        string[] newHappy = new string[DataSaving.InfoHeader.Length];
        for(int i = 0; i < newHappy.Length - 3; i++)
        {
            newHappy[i] = ogHappy[i];
        }
        newHappy[newHappy.Length - 3] = StartTime;
        newHappy[newHappy.Length - 2] = StopTime;
        newHappy[newHappy.Length - 1] = totalSlept.ToString();
        return newHappy;
  }
}
