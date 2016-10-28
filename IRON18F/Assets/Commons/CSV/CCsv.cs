using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.IO;

public class CStageCsv
{
    //読み込んだデータサイズ
    private int m_height = 0;    //行数
    private int m_width = 0;    //列数
    //***************************************************************************
    /// <summary> 指定されたファイルを読み込む</summary>
    /// <param name="filePath">ファイル名</param>
    /// <returns>ファイルの内容(2次元配列)</returns>
    //***************************************************************************
    public byte[,] Read(string filePath)
    {
        //返り値の２次元配列
        byte[,] LoadCSVData;

        //読み込み
        //Application.dataPathはプロジェクトデータのAssetフォルダまでのパス
        StreamReader streamReader = new StreamReader(Application.dataPath + "/" + filePath);
        //stringに変換
        string strStream = streamReader.ReadToEnd();

        //StringSplitOptionを設定
        //カンマとカンマの間に何もないときは格納しない
        System.StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;

        string[] lines = strStream.Split(new char[] { '\r', '\n' }, option);

        //区分けする文字の設定
        char[] spliter = new char[1] { ',' };

        //行数設定
        m_height = lines.Length;
        //列数設定
        m_width = lines[0].Split(spliter, option).Length;

        //返り値の2次元配列の要素数を設定
        LoadCSVData = new byte[m_height, m_width];
        //カンマ分
        for (int i = 0; i < m_height; i++)
        {
            for (int j = 0; j < m_width; j++)
            {
                //カンマ分け
                string[] loadData = lines[i].Split(spliter, option);
                //型変換
                LoadCSVData[i,j] = byte.Parse(loadData[j]);
            }
        }
        //返り値
        return LoadCSVData;
    }

    public void Write(string filePath, string[] arrayData)
    {
        //streamwriter初期化
        StreamWriter streamWriter;
        //読み込み
        //Application.dataPathはプロジェクトデータのAssetフォルダまでのパス
        FileInfo fileInfo = new FileInfo(Application.dataPath + "/" + filePath);
        streamWriter = fileInfo.CreateText();
        //書き込み処理
        for (int i = 0; i < arrayData.GetLength(0); i++)
        {
            streamWriter.WriteLine(arrayData[i]);
        }
        //終了処理
        streamWriter.Flush();
        streamWriter.Close();
    }

    public static String[] FileList(string folderPath, string ex)
    {
        string[] fileList = new String[256];

        DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath + "/" + folderPath);
        FileInfo[] fileInfo = directoryInfo.GetFiles("*." + ex);
        int i = 0;
        foreach (FileInfo f in fileInfo)
        {
            fileList[i] = f.Name;
            //Debug.Log(fileList[i]);
            i++;
        }
        Array.Resize(ref fileList, i);//リサイズ
        return fileList;
    }
}