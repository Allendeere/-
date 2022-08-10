using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rhythm : MonoBehaviour
{
    static rhythm Rhythm;
    bool EdittinMode;
    int Mode_;
    public Notes S_notes;
    public List<NoteButton> NoteButton = new List<NoteButton>();
    public Image[] image;
    public int newnum;
    //public int BPM = 0;
    [SerializeField] AudioManger audioManger;
    [SerializeField] bool PlayRhythm;

    [Header("如果是副本 --- 請勾選")]
    [SerializeField] bool Ingame;
    [SerializeField] Transform spawnpoint;
    [SerializeField] GameObject UI_note;
    int anum , index;
    
    [SerializeField] GameObject[] UI_notes;
    //TEST------------
    [Header("傷害加成 / 音符數量")]
    [SerializeField] int DamageBoot;
    [SerializeField] Text BullectDamage;
    [SerializeField] Text MeleeDamage;
    [SerializeField] playerinfo pinfo;

    float tick,time=.5f;
    public void r_Start()
    {
        GetNoteData();
        //InvokeRepeating("amm累加器", 0, .5f);
        tick = time;
        if (Rhythm != null)
            Destroy(this);
        Rhythm = this;
        Debug.Log("STTTTTT");

        if (!Ingame)
        {
            Mode_ = 1;
            Refresh();
            PlayerPrefs.SetInt("DamageBoot", DamageBoot);
        }
        else if (Ingame)
        {
            DamageBoot = PlayerPrefs.GetInt("DamageBoot");
            Debug.Log(PlayerPrefs.GetInt("DamageBoot" ) + " - - - - - -- - - ingame- ");
            
        }
    }

    void Edit(int Mode)
    {
        switch (Mode)
        {
            case 1://正常
                Mode_ = 1;
                break;
            case 2://編輯
                Mode_ = 2;
                break;

        }
        Refresh();
    }
    public void ClickOnEdit()
    {
        
        EdittinMode = !EdittinMode;
        if (EdittinMode) { Edit(2); }
        else { Edit(1); }
    }


    public static void ClickNote(int KeyData,int NoteData)
    {
        if (Rhythm.Mode_ == 2)
        {
            Rhythm.S_notes.MyRhythm[NoteData] = KeyData;
            Refresh();
        }
    }

    public static void Refresh()
    {
        int num = 0;        int num_ = 0;        int count = 0;        int count_ = 4;      Rhythm.DamageBoot = 0;//傷害加成 / 音符數量
        float H = 0;        float G = 0;         float V = 0.88f; //顏色 / 飽和 / 亮度
        
        Rhythm.newnum = 1;
        while (num_ < 32) { 
        if (Rhythm.Mode_ == 1)
        {
            //if (count_ == 4 && num_ < 4) { V = .95f; H = 0; G = .2f; }
              H = .72f; G = .76f; V = .87f;
            while (num_ < count_)
            {
                while (count < 12)
                {
                    if (count == 0)
                    {
                        num = num_ + num;
                        Rhythm.image[num].color = Color.HSVToRGB(H, G, V);
                    }
                    else
                    {
                        Rhythm.image[num].color = Color.HSVToRGB(H, G, V);
                        num = num + 32;
                    }
                    count++;
                }
                if (count >= 12)
                {
                    count = 0;
                    num_++;
                    num = 0;
                }
            }
            count_ = count_ + 4;
            }
        if (Rhythm.Mode_ == 2)
        {
            if (count_ == 4 && num_ < 4) { V = .3f; H = 0; G = 0; }
            while (num_ < count_)
            {
                while (count < 12)
                {
                    if (count == 0)
                    {
                        num = num_ + num;
                        Rhythm.image[num].color = Color.HSVToRGB(H, G, V);
                    }
                    else
                    {
                        Rhythm.image[num].color = Color.HSVToRGB(H, G, V);
                        num = num + 32;
                    }
                    count++;
                }
                if (count >= 12)
                {
                    count = 0;
                    num_++;
                    num = 0;
                }
            }
            if (num_ == 4 || num_ == 12) { V = .38f; }
            else if (num_ == 8) {  V = .3f; }
            else if ( num_ == 16 || num_ == 24) { V = .5f; }
            else if (num_ == 20 || num_ == 28) { V = .6f;  }
            count_ = count_ + 4;
            }
        }

        for (int i = 0; i < Rhythm.NoteButton.Count; i++)
        {
            Rhythm.NoteButton[i].used = false;
        }
        while (Rhythm.newnum < 33)
        {
            for (int i = 0; i < Rhythm.NoteButton.Count; i++)
            {
                if (Rhythm.NoteButton[i].NoteData == Rhythm.newnum && Rhythm.NoteButton[i].KeyData == Rhythm.S_notes.MyRhythm[Rhythm.newnum])
                {
                    Rhythm.NoteButton[i].used = true;
                    Rhythm.image[i].color = Color.HSVToRGB(0, 0, 1);
                    //                    Rhythm.image[i].color = Color.HSVToRGB(.4f, .8f, .88f);
                }
            }
            Rhythm.newnum++;
        }

        for (int i = 0; i < Rhythm.S_notes.MyRhythm.Count; i++)
        {
            if (Rhythm.S_notes.MyRhythm[i] != 0)
            {
                Rhythm.DamageBoot++;
            }
        }
        PlayerPrefs.SetInt("DamageBoot", Rhythm.DamageBoot);

        Rhythm.SaveNoteData();
    }
    void GetNoteData()
    {
        for (int i = 0; i < S_notes.MyRhythm.Count; i++)
        {
            S_notes.MyRhythm[i]= PlayerPrefs.GetInt("note_" + i, S_notes.MyRhythm[i]);
            //Debug.Log(("note_" + i) +PlayerPrefs.GetInt("note_" + i));
        }
    }
    void SaveNoteData()
    {
        //PlayerPrefs.SetInt("Note", S_notes.MyRhythm.Count);
        for (int i = 0; i < S_notes.MyRhythm.Count; i++)
        {
            PlayerPrefs.SetInt("note_" + i, S_notes.MyRhythm[i]);
            //Debug.Log(("note_" + i) + PlayerPrefs.GetInt("note_" + i));
        }
    }
    public void damage_system()
    {
        float basedamage = 0; 
        basedamage = (int)pinfo.Damage;
        float countnote = DamageBoot;
        float BullectBootTotul = 0;
        float MeleeBootTotul = 0;
        pinfo.Bullect = Mathf.RoundToInt(BullectBootTotul = Mathf.Round((basedamage * ((34 - countnote) / 33)) * (((34 - countnote) / 33) + 1)));

        PlayerPrefs.SetInt("BullectDamage", pinfo.Bullect);
        //傷害計算 :  (基本傷害 *((34 - count)  / 33) )* ((34 - count)  / 33)+1 ) 

       

        pinfo.Melee = Mathf.RoundToInt(MeleeBootTotul = Mathf.Round((basedamage / 2) * ((countnote + 3) / 33)));

        PlayerPrefs.SetInt("MeleeDamage", pinfo.Melee);
        //MeleeDamage傷害計算 :  (基本傷害*0.5) * (count +3 / 33) 

        if (!Ingame)
        {
            MeleeDamage.text = MeleeBootTotul.ToString();
            BullectDamage.text = BullectBootTotul.ToString();
        }
        
    }
    public void amm累加器()
    {
        damage_system();
        anum++;
        if (PlayRhythm)
        {

            //if (anum < BPM) return;
            audioManger.Play(S_notes.MyRhythm[index].ToString());
            anum = 0;
            index++;
            if (index >= S_notes.MyRhythm.Count) { index = 1; }
        }

        if (Ingame)
        {

            //if (anum < BPM) return;

            if (S_notes.MyRhythm[index] != 0)
            {

                var notes = Instantiate(UI_notes[S_notes.MyRhythm[index] - 1], Vector3.zero, Quaternion.identity);
                notes.transform.SetParent(spawnpoint, false);
                /*
                var notes = Instantiate(UI_note, Vector3.zero, Quaternion.identity);
                notes.transform.SetParent(spawnpoint, false);
                Debug.Log("dot"); */
            }

            anum = 0;
            index++;
            if (index >= S_notes.MyRhythm.Count) { index = 1; }
        }
    }

    public void StopPlaying()
    {
        PlayRhythm = false;
        Edit(1);
    }
    public void Play()
    {
        PlayRhythm = !PlayRhythm;
        index = 1;
    }
}