using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public enum TrialType {Pretest, Posttest, DelayedTest, Delayed2Test, Showcase};


public static class DataSaving {
	static string DataFilePath = @"ResearchData/PrePostTestData.csv"; // If you change this, make sure to adjust the writeData method accordingly
	static string BackupStroopDataPath = @"ResearchData/BackupStroopData/";
  static string LearningCurveDataPath = @"ResearchData/LearningCurve/";

  // Static strings declared here to ensure consistency for GainsHeader calculations
  static string Heartrate = "Heartrate";
	//static string GNG_TotalAccuracy = "TotalAccuracy";
	static string DN_Incongruent_Accuracy = "Incongruent_Accuracy";
	static string F_Incongruent_Accuracy = "Incongruent_Accuracy";

	//static string changesInHR = "changesInHR";
	//static string DN_gains = "DN_gains";
	//static string GNG_gains = "GNG_gains";
	//static string F_gains = "F_gains";

	// Task Data Headers -- NO IN-STRING COMMAS ALLOWED (And NO DASHES)
    //This is for the player info hahahahahaha
	public static string[] InfoHeader = {
        "ID",                       "Played Before",    "Computer Distance" ,   "DOB",
        "Sex",                      "Race",             "TypeRace",             "Ethnicity",
        "Year in School",           "Major/Minor",      "GPA/QPA",              "Handedness",
        "Vision",                   "MinutesSlept",     "Colorblind",           "TypeColorblind",
        "Disorder",                 "TypeDisorder",     "Physical",             "TypePhysical"};


	// Revised data headers
    //Make one of these for SaveFullTest
	public static string[] StoopHeader = {					
		"Congruent_Correct",		"Congruent_Errors",			"Congruent_Accuracy",		"Congruent_RT",
		"Incongruent_Correct",		"Incongruent_Errors",       DN_Incongruent_Accuracy,	"Incongruent_RT",
        "n_c_trials",               "n_i_trials"
	};

  static string[] LearningCurveHeader = { "word name", "ink color", "c/ic", "tablerow number", "pressed key",
                                            "response (1=correct. 2=wrong. 3=timeout)", "RT (ms)" };
  static string learningCurveHeaderString = string.Join(",", LearningCurveHeader);


  // Don't change this - CSV uses commas by definition
  static char[] fieldSeparator = { ',' };

	// Column pre/post/delayed test indices
	static int DNPreIndex;
	static int DNPostIndex;
	static int DNDelayIndex;
	static int DNDelay2Index;

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
		makeDataHeader ().CopyTo (AllData, 0);

		// Get ID
		int lineIndex = findID (AllData, ID);

		// If ID not found in the previous data, save in a new line
		// and save infoData
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
		    colIndex = DNPreIndex;
	    } else if (CurrentTrialType == TrialType.Posttest) {
		    colIndex = DNPostIndex;
	    } else if (CurrentTrialType == TrialType.DelayedTest) {
		    colIndex = DNDelayIndex;
	    } else if (CurrentTrialType == TrialType.Delayed2Test) {
		    colIndex = DNDelay2Index;
	    }

		

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
	private static string[] createInfoArray (string ID) {
		string[] infoArray = new string[InfoHeader.Length];

		int i = 0;
		infoArray [i] = ID;					i++;
		infoArray [i] = version;			i++;
		infoArray [i] = birthdate;			i++;

		DateTime result;
		if (DateTime.TryParse (birthdate, out result)) {
			// Compute age
			DateTime today = DateTime.Today;
			TimeSpan dayDiff = today.Subtract (result);
			double years = dayDiff.TotalDays / 365.25;
			infoArray [i] = years.ToString ("##.###");
		} else {
			infoArray [i] = "";
		}
		i++;

		infoArray [i] = sex;				i++;
		infoArray [i] = computerDistance;	i++; 

		// Reset the global strings
		version = "";
		birthdate = "";
		sex = "";
		computerDistance = "";

		// Make SURE there are no commas
		for (int j = 0; j < infoArray.Length; j++) {
			
			Debug.Log($"j = {j}, arr[j]: {infoArray[j]}");
			infoArray[j] = infoArray[j]?.Replace(',', ' ');
		}

		return infoArray;
	}


	// Set up the header for the data CSV
	private static string[][] makeDataHeader() {
		string[][] header = new string[1][];
		//int lineLength = InfoHeader.Length + GainsHeader.Length + (StoopHeader.Length + GoNoGoHeader.Length + FlankerHeader.Length) * 4;
		int lineLength = InfoHeader.Length + (StoopHeader.Length) * 4;
		header[0] = new string[lineLength];

		// Adding prefixes to headers --- No dashes or commas allowed
		string[] DN_PreHeader = addPrefix (StoopHeader, "DN_Pre_");
		string[] DN_PostHeader = addPrefix (StoopHeader, "DN_Post_");
		string[] DN_DelayHeader = addPrefix (StoopHeader, "DN_Delay_");
		string[] DN_Delay2Header = addPrefix(StoopHeader, "DN_Delay2_");

		// Copy the headers at their correct indices, and store the indices along the way
		int index = 0;
		
		InfoHeader.CopyTo (header [0], index);				index += InfoHeader.Length;				DNPreIndex = index;

		DN_PreHeader.CopyTo (header [0], index);			index += StoopHeader.Length;			DNPostIndex = index;
		DN_PostHeader.CopyTo (header [0], index);			index += StoopHeader.Length;            DNDelayIndex = index;
		DN_DelayHeader.CopyTo (header [0], index);			index += StoopHeader.Length;            DNDelay2Index = index;
		DN_Delay2Header.CopyTo(header[0], index);			index += StoopHeader.Length;

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

		if (!File.Exists (@"ResearchData/PrePostTestData.csv")) {
			Debug.Log ("@\"ResearchData/PrePostTestData.csv\" file does not yet exist. Creating it...");
			FileStream fs = File.Create (@"ResearchData/PrePostTestData.csv");
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
