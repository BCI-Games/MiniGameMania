using System;
using System.Collections.Generic;
using UnityEngine;
using static Gtec.Chain.Common.Nodes.InputNodes.ChannelQuality;

public class SQBrain : MonoBehaviour
{
    public Sprite SQGood;
    public Sprite SQBad;

    private SpriteRenderer[] _gameObjectRenderers = null;
    private GameObject[] _gameObjects = null;
    private List<ChannelStates> _channelSQ = null;
    private List<ChannelStates> _prevchannelSQ = null;
    private bool _sqChanged = false;
    // Start is called before the first frame update
    void Start()
    {
        BCIManager.Instance.SignalQualityAvailable += OnSignalQualityAvailable;
        int children = transform.childCount;
        _gameObjects = new GameObject[children];
        for (int i = 0; i < children; i++)
        {
            _gameObjects[i] = transform.GetChild(i).gameObject;
        }
        _gameObjectRenderers = new SpriteRenderer[_gameObjects.Length];
        for (int i = 0; i < _gameObjects.Length;i++)
        {
            _gameObjectRenderers[i] = _gameObjects[i].GetComponentsInChildren<SpriteRenderer>()[0];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_sqChanged)
        {
            for (int i = 0; i < _channelSQ.Count; i++)
            {
                if(_channelSQ[i]== ChannelStates.Stable)
                    _gameObjectRenderers[i].sprite = SQGood;
                else
                    _gameObjectRenderers[i].sprite = SQBad;
            }
            _sqChanged = false;
        }
    }

    private void OnDestroy()
    {
        BCIManager.Instance.SignalQualityAvailable -= OnSignalQualityAvailable;
    }

    private void OnApplicationQuit()
    {
        BCIManager.Instance.SignalQualityAvailable -= OnSignalQualityAvailable;
    }

    private void OnSignalQualityAvailable(object sender, EventArgs e)
    {
        ChannelStatesUpdateEventArgs ea = (ChannelStatesUpdateEventArgs)e;
        if(_prevchannelSQ == null && _channelSQ == null)
        {
            _prevchannelSQ = new List<ChannelStates>(ea.ChannelStates);
            _channelSQ = new List<ChannelStates>(ea.ChannelStates);
            _sqChanged = true;
        }   
        else
        {
            _prevchannelSQ = new List<ChannelStates>(_channelSQ);
            _channelSQ = new List<ChannelStates>(ea.ChannelStates);
        }

        for(int i = 0; i < _channelSQ.Count;i++)
        {
            if (!_prevchannelSQ[i].Equals(_channelSQ[i]))
                _sqChanged = true;
        }
    }
}
