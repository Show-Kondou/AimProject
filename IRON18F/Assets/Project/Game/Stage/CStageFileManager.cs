using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;



//!  CStageFileManager.cs	(シングルトン)
/*!
 * \details CStageFileManager	ステージファイルを管理するクラス
 * \author  Shoki Tsuneyama
 * \date    2016/10/12	新規作成

 */
public class CStageFileManager : CSingleton<CStageFileManager>
{
    public string m_folderPath = "StreamingAssets/Stage/";
    public List<string> m_stageFiles = new List<string>();
    public int m_fileNum = 0;
    public void LoadFileList()
    {
        m_stageFiles.Clear();
        string[] fileList = CStageCsv.FileList(m_folderPath, "csv");
        if (fileList.Length != 0)
        {
            foreach (string f in fileList)
            {
                m_stageFiles.Add(m_folderPath + f);
            }
        }
        else
        {
            m_stageFiles.Add("/StageEditor/Default.csv");
        }
    }
    public byte[,] GetStageData(int stageNo)
    {
        CStageCsv csv = new CStageCsv();
        return csv.Read(m_stageFiles[stageNo]);
    }
    public void SetStageData(byte[,] arr, int stageNo)
    {
        CStageCsv csv = new CStageCsv();
        csv.Write(m_stageFiles[stageNo], ToStringArry(arr,","));
    }
    //Byte型配列をString型配列に変換
    public static string[] ToStringArry(byte[,] arrayData, string split)
    {
        string[] ret = new string[arrayData.GetLength(0)];
        for (int y = 0; y< arrayData.GetLength(0); y++)
        for (int x = 0; x< arrayData.GetLength(1); x++)
            ret[y] += arrayData[y,x] + split;
        return ret;
    }
    //public void Save(byte[][] arrayData)
    //{
    //    CTextIO text = new CTextIO();
    //    // ファイルパス
    //    string filePath;
    //    filePath = System.IO.Path.ChangeExtension(CStageFileManeger.Instance.StageFile[CStageFileManeger.Instance.FileNum], "csv");
    //    //ファイル書き込み
    //    text.Write(filePath, CByteConverter.ToString(arrayData, ","));
    //    CPlayer _player = GameObject.Find("Player").GetComponent<CPlayer>();
    //    CStageDataManeger.Instance.SetStageData(new PositionData(_player.Position.m_x, _player.Position.m_z));
    //    CStageDataManeger.Instance.SetStageData(m_data.ClearNum);
    //    CStageDataManeger.Instance.Save();
    //    Debug.Log("セーブ!");
    //}
}
