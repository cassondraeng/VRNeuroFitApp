using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public enum TrialType {Pretest, Posttest, DelayedTest, Delayed2Test, Showcase};


public static class DataSaving {
	static string DataFilePath = @"ResearchData/BatteryTestData.csv"; // If you change this, make sure to adjust the writeData method accordingly
	// static string DataFilePathPost = @"ResearchData/BatteryPostTestData.csv"; // If you change this, make sure to adjust the writeData method accordingly
	static string BackupStroopDataPath = @"ResearchData/BackupStroopData/";
	// static string BackupStroopDataPathPost = @"ResearchData/PostBackupStroopData/";
  static string LearningCurveDataPath = @"ResearchData/LearningCurve/";

  // Static strings declared here to ensure consistency for GainsHeader calculations
	//static string GNG_TotalAccuracy = "TotalAccuracy";

	//static string changesInHR = "changesInHR";
	//static string DN_gains = "DN_gains";
	//static string GNG_gains = "GNG_gains";
	//static string F_gains = "F_gains";

	// Task Data Headers -- NO IN-STRING COMMAS ALLOWED (And NO DASHES)
    //This is for the player info hahahahahaha
	public static string[] InfoHeaderPre = {
        "ID",                       "Played Before",    "Computer Distance"};

	public static string[] InfoHeaderPost = {
        "DOB",
        "Sex",                      "Race",             "RaceOther",             "Ethnicity",
        "Year in School",           "Handedness",       "Vision",				"Major",
		"Minor",					"GPA",				"QPA",					"SAT",
		"ACT",						"Colorblind",       "TypeColorblind",		"Disorder",
		"TypeDisorder",				"Exercise",         "TypeExercise",			"SleepTime",
		"WakeTime",					"HoursSlept"};


	// Revised data headers
    //Make one of these for SaveFullTest
	public static string[] StoopHeader = {					
		"S_Congruent_Correct",		"S_Congruent_Errors",			"S_Congruent_Accuracy",		"S_Congruent_RT",
		"S_Incongruent_Correct",		"S_Incongruent_Errors",       "S_Incongruent_Accuracy",		"S_Incongruent_RT",
        "S_n_c_trials",               "S_n_i_trials"
	};

  static string[] LearningCurveHeader = { "word name", "ink color", "c/ic", "tablerow number", "pressed key",
                                            "response (1=correct. 2=wrong. 3=timeout)", "RT (ms)" };
  static string learningCurveHeaderString = string.Join(",", LearningCurveHeader);


  // Don't change this - CSV uses commas by definition
  static char[] fieldSeparator = { ',' };

	// Column pre/post/delayed test indices
	static int SPreIndex;
	static int SPostIndex;
	static int SDelayIndex;
	static int SDelay2Index;

	public static TrialType CurrentTrialType;

	public static string version = "";
	public static string birthdate = "";
	public static string sex = "";
	public static string computerDistance = "";


	// Call this to save trial data into the .csv file
	public static void SaveData(string ID, string[] infoData, string[] newData) {
		if (CurrentTrialType == TrialType.Showcase) {
			return;
		}

		if (string.Equals (ID, "")) {
			ID = "(No ID entered)";
		}

		//string[] newInfoArray = createInfoArray (ID);

		// Save backup of the data
		SaveBackup (ID, infoData, newData);

		// Read the current data and make sure the header is correct
		string[][] AllData = readCSVFile (DataFilePath);
		makeDataHeader().CopyTo (AllData, 0);

		// Get ID
		int lineIndex = findID (AllData, ID);

		// If ID not found in the previous data, save in a new line
		// and save infoData
		// NOTE: copies pretest data here
		if (lineIndex < 0) {
			string[][] newAllData = new string[AllData.Length + 1][];
			AllData.CopyTo (newAllData, 0);

			string[] newLine = new string[ AllData[0].Length ];
			lineIndex = newAllData.Length - 1;
			newAllData [lineIndex] = newLine;
			infoData.CopyTo (newAllData [lineIndex], 0);

			AllData = newAllData;
		}


		// Find the column index for the data, and check if the newData is the right length
		int colIndex = 0;
		
		if (newData.Length != StoopHeader.Length) {
			Debug.LogError ("Unexpected data array length for Stroop task data saving");
		}
	    if (CurrentTrialType == TrialType.Pretest) {
		    colIndex = SPreIndex;
	    } else if (CurrentTrialType == TrialType.Posttest) {
		    colIndex = SPostIndex;
			// copies posttest DEMOGRAPHIC data here
			infoData.CopyTo (AllData [lineIndex], InfoHeaderPre.Length);
	    } // else if (CurrentTrialType == TrialType.DelayedTest) {
		//     colIndex = SDelayIndex;
	    // } else if (CurrentTrialType == TrialType.Delayed2Test) {
		//     colIndex = SDelay2Index;
	    // }

		

		// Copy into the data array
		newData.CopyTo (AllData [lineIndex], colIndex);


		// Write it to the file
		string dataString = convert2DToString(AllData);
		writeData(dataString);
	}


	// Read the data from a .csv and return it as a string[][]
	private static string[][] readCSVFile (string path) {
		string[][] allData;
		if (File.Exists (path)) {
			string[] lines = File.ReadAllLines (path);
			if (lines.Length > 1) {
				allData = new string[lines.Length][];

				for (int i=0; i < lines.Length; i++) {
					string[] fields = lines [i].Split (fieldSeparator);
					allData [i] = new string[fields.Length];
					fields.CopyTo (allData [i], 0);
				}
				return allData;
			}
		}

		// File has not been initialized
		allData = new string[1][];
		return allData;
	}

	// Search for a string[] with the same ID at index 0
	private static int findID(string[][] data, string ID) {
		for (int i = 1; i < data.Length; i++) {
			if (data [i] [0] == ID) {
				return i;
			}
		}

		return -1;
	}

	// Create the info array
	// private static string[] createInfoArray (string ID) {
	// 	string[] infoArray = new string[InfoHeader.Length];

	// 	int i = 0;
	// 	infoArray [i] = ID;					i++;
	// 	infoArray [i] = version;			i++;
	// 	infoArray [i] = birthdate;			i++;

	// 	DateTime result;
	// 	if (DateTime.TryParse (birthdate, out result)) {
	// 		// Compute age
	// 		DateTime today = DateTime.Today;
	// 		TimeSpan dayDiff = today.Subtract (result);
	// 		double years = dayDiff.TotalDays / 365.25;
	// 		infoArray [i] = years.ToString ("##.###");
	// 	} else {
	// 		infoArray [i] = "";
	// 	}
	// 	i++;

	// 	infoArray [i] = sex;				i++;
	// 	infoArray [i] = computerDistance;	i++; 

	// 	// Reset the global strings
	// 	version = "";
	// 	birthdate = "";
	// 	sex = "";
	// 	computerDistance = "";

	// 	// Make SURE there are no commas
	// 	for (int j = 0; j < infoArray.Length; j++) {
			
	// 		Debug.Log($"j = {j}, arr[j]: {infoArray[j]}");
	// 		infoArray[j] = infoArray[j]?.Replace(',', ' ');
	// 	}

	// 	return infoArray;
	// }

	// Set up the header for the data CSV
	private static string[][] makeDataHeader() {
		string[][] header = new string[1][];
		//int lineLength = InfoHeader.Length + GainsHeader.Length + (StoopHeader.Length + GoNoGoHeader.Length + FlankerHeader.Length) * 4;

		// Add length of pretest data and then post test
		int lineLength = InfoHeaderPre.Length + InfoHeaderPost.Length + (StoopHeader.Length) * 1 + (StoopHeader.Length) * 1;
		header[0] = new string[lineLength];

		// Adding prefixes to headers --- No dashes or commas allowed
		string[] DN_PreHeader = addPrefix (StoopHeader, "Pre_");
		string[] DN_PostHeader = addPrefix (StoopHeader, "Post_");
		// string[] DN_DelayHeader = addPrefix (StoopHeader, "S_Delay_");
		// string[] DN_Delay2Header = addPrefix(StoopHeader, "S_Delay2_");

		// Copy the headers at their correct indices, and store the indices along the way
		int index = 0;
		
		// Copy over the pretest info headers
		InfoHeaderPre.CopyTo (header [0], index);
		index += InfoHeaderPre.Length;
		InfoHeaderPost.CopyTo (header [0], index);
		index += InfoHeaderPost.Length;
		SPreIndex = index;
		DN_PreHeader.CopyTo (header [0], index);
		index += StoopHeader.Length;
		SPostIndex = index;
		DN_PostHeader.CopyTo (header [0], index);
		index += StoopHeader.Length;
		
		//index += StoopHeader.Length;            SDelayIndex = index;
		// DN_DelayHeader.CopyTo (header [0], index);			index += StoopHeader.Length;            SDelay2Index = index;
		// DN_Delay2Header.CopyTo(header[0], index);			index += StoopHeader.Length;

		// Output the final result
		return header;
	}


	// Add a prefix to every string in a string[]
	private static string[] addPrefix (string[] array, string prefix) {
		string[] newArray = new string[array.Length];
		for (int i = 0; i < array.Length; i++) {
			newArray[i] = prefix + array[i];
		}
		return newArray;
	}


	// Convert string[][] to string
	private static string convert2DToString(string[][] TwoDArray) {
		string output = "";
		foreach (string[] line in TwoDArray) {
			output = output + string.Join (",", line) + "\n";
		}
		return output;
	}

	// Write the input string to the DataFilePath
	private static void writeData (string dataString) {
		if (!Directory.Exists (@"ResearchData")) {
			Debug.Log ("@\"ResearchData/\" folder does not yet exist. Creating it...");
			Directory.CreateDirectory (@"ResearchData");
		}

		if (!File.Exists (@"ResearchData/BatteryTestData.csv")) {
			Debug.Log ("@\"ResearchData/BatteryTestData.csv\" file does not yet exist. Creating it...");
			FileStream fs = File.Create (@"ResearchData/BatteryTestData.csv");
			fs.Close ();
		}

		File.WriteAllText (DataFilePath, dataString);
		Debug.Log ("Trial data successfully saved");
	}

	// Save the new line of data to the backup folder for the kiddo
	private static void SaveBackup (string ID, string[] infoLine, string[] newData) {
		string infoString = string.Join (",", infoLine);
		string dataString = string.Join (",", newData);

		dataString = string.Concat (infoString, ",", CurrentTrialType.ToString(), ",", dataString, "\n");

		string path = BackupStroopDataPath + ID + ".csv";
		if (!File.Exists (path)) {
			Directory.CreateDirectory (BackupStroopDataPath);
		}
		File.AppendAllText (path, dataString);
		Debug.Log ("Backup Stroop data saved");
  }


  // Append a single trial to the learning curve data
  public static void SaveLearningCurveData(string ID, string dataString) {
    if (!File.Exists(LearningCurveDataPath)) {
      Directory.CreateDirectory(LearningCurveDataPath);
    }

    //string day = "2";
    string path = LearningCurveDataPath + ID + "_LearningCurve.csv";
    if (!File.Exists(path)) {
      File.AppendAllText(path, learningCurveHeaderString);
      //day = "1";
    }

    //dataString = dataString.Replace(learningCurveDayMacro, day);
    File.AppendAllText(path, dataString);
  }
}
