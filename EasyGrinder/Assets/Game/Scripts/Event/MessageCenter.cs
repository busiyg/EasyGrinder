using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageCenter : MonoBehaviour
{

    private static MessageCenter Instance;
    /// <summary>
    /// 用于储存事件码和
    /// </summary>
    private Dictionary<string, Action<object>> messageCenterDict = new Dictionary<string, Action<object>>();
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
    }

    public static void Dispatcher(string eventCode, object message)
    {
        if (Instance.messageCenterDict.ContainsKey(eventCode))
        {
            Instance.messageCenterDict[eventCode].Invoke(message);
        }
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    public static void AddListener(string eventCode, Action<object> action)
    {
        if (!Instance.messageCenterDict.ContainsKey(eventCode))
        {
            Instance.messageCenterDict.Add(eventCode, action);
        }
    }
    /// <summary>
    /// 移除监听事件
    /// </summary>
    /// <param name="subcode"></param>
    public static void Removelistener(string eventCode)
    {
        if (Instance.messageCenterDict.ContainsKey(eventCode))
        {
            Instance.messageCenterDict.Remove(eventCode);
        }
    }

    public static void RemoveAlllistener()
    {
        Instance.messageCenterDict.Clear();
    }
}

