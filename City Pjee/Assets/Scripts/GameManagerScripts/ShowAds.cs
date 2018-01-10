﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ShowAds : MonoBehaviour {

  public void ShowRewardedAd()
	{
                if (Advertisement.IsReady("rewardedVideo"))
                {
                  var options = new ShowOptions { resultCallback = HandleShowResult };
                  Advertisement.Show("rewardedVideo", options);
                }
              }

              private void HandleShowResult(ShowResult result)
              {
                switch (result)
                {
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
                    //
			GameManager.instance.RewardPlayer ();
			GameManager.instance.revived = true;
                    // Give coins etc.
                    break;
                  case ShowResult.Skipped:
                    Debug.Log("The ad was skipped before reaching the end.");
                    break;
                  case ShowResult.Failed:
                    Debug.LogError("The ad failed to be shown.");
                    break;
                }
              }
            }