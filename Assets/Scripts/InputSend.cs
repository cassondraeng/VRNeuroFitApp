using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//same order as data_saving
public enum HeaderType : int
{
    ID, Played_Before, Computer_Distance, DOB, Sex, Race, RaceOther,
    Ethnicity, Year_in_School, Handedness, Vision, Major, Minor, GPA,
    QPA, SAT, ACT, Colorblind, TypeColorblind, Disorder, TypeDisorder,
    Exercise, TypeExercise, StartHrs, StartMins, StartAPM, StopHrs,
    StopMins, StopAPM, HoursSlept
}                         

[CreateAssetMenu(menuName ="holds_input_data")]
public class InputSend : ScriptableObject
{
    //int size = Enum.GetNames(typeof(HeaderType)).Length;
    public string[] happy;

    private void OnEnable()
    {
        //happy = new string[size];
    }

    public void set_happy(HeaderType headerType,object value)
    {
        happy[(int)headerType] = value.ToString();
    }
}
