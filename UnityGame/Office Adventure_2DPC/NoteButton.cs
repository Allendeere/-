using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteButton : MonoBehaviour
{
    public bool used;
    public int NoteData;
    public int KeyData;
    public void ClickNote()
    {
        used = !used;
        if (used)
        {
            rhythm.ClickNote(KeyData, NoteData);
        }
        else
        {
            rhythm.ClickNote(0, NoteData);
        }

    }
}
