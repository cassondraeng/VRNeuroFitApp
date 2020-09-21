using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//same order as data_saving
public enum HeaderType : int
{
    ID, Played_Before, Computer_Distance, DOB, Sex,
    Race, TypeRace, Ethnicity, Year_in_School, Major_Minor, GPA_QPA, Handedness,
    Vision, StartHrs, StartMins, StartAPM, StopHrs, StopMins, StopAPM, Colorblind,
    TypeColorblind, Disorder, TypeDisorder, Physical, TypePhysical
}                         

[CreateAssetMenu(menuName ="holds_input_data")]
public class InputSend : ScriptableObject
{
    int size = Enum.GetNames(typeof(HeaderType)).Length;
    public string[] happy;

    private void OnEnable()
    {
        happy = new string[size];
    }

    public void set_happy(HeaderType headerType,object value)
    {
        happy[(int)headerType] = value.ToString();
    }
}
