﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Player name input field. Let the user input his name, will appear above the player in the game.
/// </summary>
[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    #region Private Constants

    // Store the PlayerPref key to avoid typos
    const string playerNamePrefKey = "PlayerName";
    #endregion

    #region MonoBehaviour Callbacks

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>
    /// 

    void Start()
    {
        string defaultName = string.Empty;
        
        InputField _inputField = this.GetComponent<InputField>();
        if (_inputField.text == "")
        {
            
            if (PlayerPrefs.HasKey("playerNamePrefKey"))
            {
                Debug.Log("name");
                defaultName = PlayerPrefs.GetString("playerNamePrefKey");
                _inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
    /// </summary>
    /// <param name="value">The name of the Player</param>
    /// 
    public void SetPlayerName(string value)
    {
        
        //#important
        if(string.IsNullOrEmpty(value))
        {
            
            Debug.LogError("Player name is null or empty");
            return;
        }
        

        PhotonNetwork.NickName = value;

        PlayerPrefs.SetString("playerNamePrefKey", value);
    }
    #endregion
}
