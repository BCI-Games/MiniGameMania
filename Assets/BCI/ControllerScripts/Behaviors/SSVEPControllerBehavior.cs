using System.Collections;
using UnityEngine;
using System;
using BCIEssentials.Controllers;

public class SSVEPControllerBehavior : BCIControllerBehavior
{
    [SerializeField] private float[] setFreqFlash;
    [SerializeField] private float[] realFreqFlash;

    private int[] frames_on = new int[99];
    private int[] frame_count = new int[99];
    private float period;
    private int[] frame_off_count = new int[99];
    private int[] frame_on_count = new int[99];
    
    private bool voteOnWindows = true;


    public override void PopulateObjectList(SpoPopulationMethod populationMethod = SpoPopulationMethod.Tag)
    {
        base.PopulateObjectList(populationMethod);

        realFreqFlash = new float[objectList.Count];

        var refreshRate = Application.targetFrameRate;
        for (int i = 0; i < objectList.Count; i++)
        {
            
            frames_on[i] = 0;
            frame_count[i] = 0;
            period = refreshRate / setFreqFlash[i];
            // could add duty cycle selection here, but for now we will just get a duty cycle as close to 0.5 as possible
            frame_off_count[i] = (int)Math.Ceiling(period / 2);
            frame_on_count[i] = (int)Math.Floor(period / 2);
            realFreqFlash[i] = (refreshRate / (float)(frame_off_count[i] + frame_on_count[i]));
            
            Debug.Log($"frequency {i + 1} : { realFreqFlash[i]}");
        }
    }

    public override IEnumerator SendMarkers(int trainingIndex = 99)
    {
        // Make the marker string, this will change based on the paradigm
        while (stimOn)
        {
            // Desired format is: ["ssvep", number of options, training target (-1 if n/a), window length, frequencies]
            string freqString = "";
            for (int i = 0; i < realFreqFlash.Length; i++)
            {
                freqString = freqString + "," + realFreqFlash[i].ToString();
            }

            string trainingString;
            if (trainingIndex <= objectList.Count)
            {
                trainingString = trainingIndex.ToString();
            }
            else
            {
                trainingString = "-1";
            }

            string markerString = "ssvep," + objectList.Count.ToString() + "," + trainingString + "," + windowLength.ToString() + freqString;

            // Send the marker
            marker.Write(markerString);

            // Wait the window length + the inter-window interval
            yield return new WaitForSecondsRealtime(windowLength + interWindowInterval);


        }
    }

    public override IEnumerator Stimulus()
    {
        while (stimOn)
        {
            // Add duty cycle
            // Generate the flashing
            for (int i = 0; i < objectList.Count; i++)
            {
                frame_count[i]++;
                if (frames_on[i] == 1)
                {
                    if (frame_count[i] >= frame_on_count[i])
                    {
                        // turn the cube off
                        objectList[i].GetComponent<SPO>().TurnOff();
                        frames_on[i] = 0;
                        frame_count[i] = 0;
                    }
                }
                else
                {
                    if (frame_count[i] >= frame_off_count[i])
                    {
                        // turn the cube on
                        objectList[i].GetComponent<SPO>().TurnOn();
                        frames_on[i] = 1;
                        frame_count[i] = 0;
                    }
                }
            }
            yield return 0;
        }
        for (int i = 0; i < objectList.Count; i++)
        {
            // turn the cube off
            objectList[i].GetComponent<SPO>().TurnOff();
        }
    }

    public override IEnumerator ReceiveMarkers()
    {
        if (receivingMarkers == false)
        {
            //Get response stream from Python
            print("Looking for a response stream");
            int diditwork = response.ResolveResponse();
            print(diditwork.ToString());
            receivingMarkers = true;
        }

        //Set interval at which to receive markers
        float receiveInterval = 1 / Application.targetFrameRate;
        float responseTimeout = 0f;

        //Ping count
        int pingCount = 0;
        // Receive markers continuously
        // Receive markers continuously
        while (receivingMarkers)
        {
            // Receive markers
            // Initialize the default response string
            string[] defaultResponseStrings = { "" };
            string[] responseStrings = defaultResponseStrings;

            // Pull the python response and add it to the responseStrings array
            responseStrings = response.PullResponse(defaultResponseStrings, responseTimeout);

            // Check if there is 
            bool newResponse = !responseStrings[0].Equals(defaultResponseStrings[0]);


            if (responseStrings[0] == "ping")
            {
                pingCount++;
                if (pingCount % 100 == 0)
                {
                    Debug.Log("Ping Count: " + pingCount.ToString());
                }
            }

            else if (responseStrings[0] != "")
            {
                for (int i = 0; i < responseStrings.Length; i++)
                {
                    string responseString = responseStrings[i];
                    //print("WE GOT A RESPONSE");
                    print("response : " + responseString);

                    // If there are square brackets then remove them
                    responseString.Replace("[", "").Replace("]", "").Replace(".", "");
                    responseString = responseString.Replace("[", "");
                    responseString = responseString.Replace("]", "");
                    responseString = responseString.Replace(".", "");

                    // If it is a single value then select that value
                    int n;
                    bool isNumeric = int.TryParse(responseString, out n);
                    if (isNumeric && n < objectList.Count)
                    {
                        //Run on selection
                        objectList[n].GetComponent<SPO>().OnSelection();
                    }
                    Debug.Log("vote on windows: " + voteOnWindows);

                    if (voteOnWindows == true)
                    {
                        // Otherwise split 
                        string[] responses = responseString.Split(" ");
                        Debug.Log("!!!!!!!!!!!!!!! responseString: " + responseString);
                        Debug.Log("!!!!!!!!!!!!!!! responses: " + responses);
                        int[] objectVotes = new int[objectList.Count];

                        foreach (string response in responses)
                        {
                            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!! Current Response: " + response);

                            isNumeric = int.TryParse(response, out n);
                            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!! Out n: " + n);
                            if (isNumeric == true)
                            {
                                //Run the vote
                                objectVotes[n] = objectVotes[n] + 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (isNumeric == false)
                        {
                            continue;
                        }

                        // make a selection based on the vote
                        int voteSelection = 0;
                        for (int v = 1; v < objectList.Count; v++)
                        {
                            if (objectVotes[v] > objectVotes[voteSelection])
                            {
                                voteSelection = v;
                            }
                        }

                        //Run on selection
                        UnityEngine.Debug.Log("Voting selected object " + voteSelection.ToString());
                        objectList[voteSelection].GetComponent<SPO>().OnSelection();
                    }
                }
            }

            // Wait for the next receive interval
            yield return new WaitForSecondsRealtime(receiveInterval);
        }

        Debug.Log("Done receiving markers");
    }
}
