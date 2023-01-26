using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using LSL;

namespace BCIEssentials.Networking
{
    public class LSLMarkerStream : MonoBehaviour
    {
        private StreamOutlet outlet;

        public string StreamName = "UnityMarkerStream";
        public string StreamType = "LSL_Marker_Strings";
        public string StreamId = "MyStreamID-Unity1234";

        private string[] sample = new string[1];

        void Start()
        {
            StreamInfo streamInfo = new StreamInfo(StreamName, StreamType, 1, 0.0, LSL.channel_format_t.cf_string);

            outlet = new StreamOutlet(streamInfo);
        }

        public void Write(string markerString)
        {
            sample[0] = markerString;
            outlet.push_sample(sample);

            Debug.Log("Sent Marker : " + markerString);
        }

        public string CloseMarkerStream()
        {
            // Try to close the stream
            try
            {
                //Harsh closing right now. May need to have a "wait for consumers" option instead.
                outlet.Close();
                Debug.Log("Closing the marker stream....");
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return "There was an error in closing the marker stream: " + e.Message;
            }

            return "Marker Stream (outlet from unity) now closed";
        }

    }

}