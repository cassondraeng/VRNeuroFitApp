using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//same order as data_saving
public enum HeaderType : int
{
    ID, Computer_Distance, DOT, DOB, Age, Sex, Race, RaceOther,
    Ethnicity, Semester, Year_in_School, Vision, Major, Minor, GPA,
    QPA, SAT, ACT, Handedness, Colorblind, TypeColorblind, Disorder, TypeDisorder, ADHD, ADHDPresentation,
    Exercise/*exercise freq*/, ExerciseIntensity, TypeExercise, StartHrs, StartMins, StartAPM, StopHrs,
    StopMins, StopAPM, HoursSlept, Played_Before
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
