﻿using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class PageController : MonoBehaviour
{
	[SerializeField] ScenarioView scenarioView;

	[SerializeField] MakeData makeData;


	void FinishStepPage()
	{
		if (makeData.i.Value == makeData.dictionary[makeData.j.Value].scenario.Length - 1)
		{
			if (makeData.j.Value == makeData.scenario_id_max - 1)
			{
				scenarioView.DestroyButton();
			}
		}
	}


	void ControlPage(Dictionary<int, JsonStructure.Item> dictionary, int i, int j)
	{
		Debug.Log(i + ":" + j);

		//
		if (i == 0 && j == makeData.scenario_id_max)        //シナリオが最後まで行ったら
		{
			//return;                                             //何もしない
		}

		if (i == dictionary[j].scenario.Length - 1)               //iがscenarioの数の最大ならば
		{
			//Debug.Log("i:Length=" + i + ":" + dictionary[j].scenario.Length);
			if (dictionary[j].separate[0].separate_next != 0)       //separate_nextが0でないなら
			{
				scenarioView.ChangeButtonActive(scenarioView.SeparateButton1);
				scenarioView.ChangeButtonActive(scenarioView.SeparateButton2);

				scenarioView.SetButtonText(dictionary, j);         //ボタンにテキストを代入
			}
		}


		if (i == dictionary[j].scenario.Length - 1)
		{
			if (dictionary[j].separate[0].separate_next == 0 && j != makeData.scenario_id_max - 1)   //separate_nextが0ならば
			{
				makeData.j.Value = dictionary[j].next;            //nextをjに代入
				makeData.i.Value = 0;                             //iを初期化
			}
		}
		Debug.Log(i + ":" + j);

		if (i < dictionary[j].scenario.Length - 1)
		{
			makeData.i.Value++;                                       //ページを１つ進める
		}

	}


	public void StepNextPage()
	{
		ControlPage(makeData.dictionary, makeData.i.Value, makeData.j.Value);
	}


	public void Start(){
		scenarioView.GetPageButton.onClick.AddListener(StepNextPage);

		makeData.i
			.Subscribe(_ =>{
				scenarioView.UpdatePage(makeData.dictionary, makeData.i.Value, makeData.j.Value);
				FinishStepPage();
			});
			

		//scenarioView.MakePage(makeData.dictionary, makeData.i, makeData.j);
		//scenarioView.GetPageButton.ObserveEveryValueChanged
			
		
	}

}
