using UnityEngine;
using System.Collections;

public enum EAudioList
{
    BGM_Title,
    BGM_Game1,
    BGM_Game2,
    BGM_Result,

    SE_CandleIgnition,     // ロウソク着火
    SE_CandleVanish,       // ロウソク消える
    SE_CandlePut,          // ロウソク置く(地面)//
    SE_CandlePlacement,    // 燭台にロウソク設置
    SE_HumanBeat,          // 人鼓動
    SE_HumanScream,        // 人悲鳴(お化けに驚かされた(攻撃？))//
    SE_HumanSwoon,         // 人気絶//
    SE_HumanResurrection,  // 人復活//
    SE_HumanGetItem,       // 人物拾う//
    SE_HumanExclamation,   // 人ビックリマーク(つかうかわからん)
    SE_GhostLaugh,         // おばけ驚かし成功
    SE_GhostSurprise,      // おばけパンチ
    SE_DummyGhost,         // ダミーゴースト生成と削除両方で使用
    SE_CandyThrow,         // キャンディ投げた時//
    SE_CandyHit,           // キャンディ何かに当たったとき
    SE_GameGo,             //
    SE_GameReady,          // 
    SE_AwardDrumroll,      // リザルトのドラムロール
    SE_AwardAnnouncement,  // リザルト結果がでたとき
    SE_Start,              // タイトル→マッチングの遷移音
    SE_Finish,             // ゲーム終了音


    //新規追加は途中ではなくここにおねがい
    AudioLast
};

