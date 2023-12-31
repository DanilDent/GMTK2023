﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using Vector3 = System.Numerics.Vector3;

public class HeroBehPatternRecorder : MonoBehaviour
{
    private string RECORDINGS_PATH => Application.dataPath + "\\Content\\Config\\_HeroBehPatterns\\_jsons\\Resources";
    private const int _recordFrameRate = 60;
    private float _timerMax;
    private float _timer;

    public void StartRecording()
    {
        _isRecording = true;
        _recording.Data.Add(new Command { CmdType = CommandType.SetCursorPosition, Vec3 = AbsolutePosToRelativePos(UnityVector3ToSystemVector3(Input.mousePosition)) });
        _prevMousePosition = UnityVector3ToSystemVector3(Input.mousePosition);
        _timer = _timerMax;
    }

    public void StopRecording()
    {
        _isRecording = false;
        string path = Path.Combine(RECORDINGS_PATH, $"{_patternName}.json");
        string json = JsonConvert.SerializeObject(_recording);
        File.WriteAllText(path, json);
        Debug.Log($"Recording {_patternName} was saved to {path}");
    }

    [SerializeField] private string _patternName;
    [SerializeField] private bool _isRecording;
    private Recording _recording;
    //
    private Vector3 _prevMousePosition;

    private void Awake()
    {
        _recording = new Recording { Data = new List<Command>() };
    }

    private void Start()
    {
        _timerMax = 1f / _recordFrameRate;
        EventService.Instance.DiagButtonClicked += HandleDialogueButtonClick;
    }

    private void OnDestroy()
    {
        EventService.Instance.DiagButtonClicked -= HandleDialogueButtonClick;
    }

    private void Update()
    {
        if (!_isRecording)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartRecording();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                StopRecording();
            }

            _timer -= Time.deltaTime;

            if (_timer < 0f)
            {
                _timer = _timerMax;


                if (_isRecording)
                {
                    Vector3 mouseDelta = UnityVector3ToSystemVector3(Input.mousePosition) - _prevMousePosition;
                    _prevMousePosition = UnityVector3ToSystemVector3(Input.mousePosition);
                    _recording.Data.Add(new Command { CmdType = CommandType.MoveCursorTo, Vec3 = AbsolutePosToRelativePos(mouseDelta) });
                }
            }
        }
    }

    private void HandleDialogueButtonClick(ButtonType btnType)
    {
        if (_isRecording)
        {
            _recording.Data.Add(new Command { CmdType = CommandType.ClickButtonCommand, BtnType = btnType });
        }
    }

    private static Vector3 UnityVector3ToSystemVector3(UnityEngine.Vector3 vector)
    {
        return new Vector3(vector.x, vector.y, vector.z);
    }

    private static Vector3 AbsolutePosToRelativePos(Vector3 vec)
    {
        return new Vector3(vec.X / Screen.width, vec.Y / Screen.height, vec.Z);
    }
}

public class Recording
{
    public List<Command> Data;
}
