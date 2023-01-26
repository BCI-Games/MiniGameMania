using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BCIEssentials.Controllers;

namespace BCIEssentials.Networking
{
    public class InitializeResponseStream : MonoBehaviour
    {
        //TODO Add in initialization of response stream only when we want it. But for now use update
        private int rstream_int;
        private bool rec_markers;

        public int QueryResponseStream()
        {
            Debug.Log("Going to query the stream...");
            rstream_int = BCIController.Instance.ActiveBehavior.QueryResponseStream();
            return rstream_int;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                int rs_available = QueryResponseStream();
                Debug.Log("The query stream is: " + rs_available);

                rec_markers = BCIController.Instance.ActiveBehavior.QueryReceivingMarkers();
                Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Currently Receiving Markers: " + rec_markers);
            }

            if(Input.GetKeyDown(KeyCode.K))
            {
                Debug.Log("Time to kill these streams...");
                BCIController.Instance.CloseLSLMarkerStream();
                BCIController.Instance.CloseLSLResponseStream();
            }
        }
    }
}