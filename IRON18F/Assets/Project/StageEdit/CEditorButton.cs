using UnityEngine;
using System.Collections;

public enum EEditorButton
{
    ArrowL,
    ArrowR,
    Save,
    Delete,
    Block,
    Sponer1,
    Sponer2,
    Sponer3,
    Sponer4,
}
public class CEditorButton : MonoBehaviour
{
    public EEditorButton m_button;
    public EStageBlocks m_block;
    public bool m_isSelect;
}
