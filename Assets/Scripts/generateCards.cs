using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class generateCards : MonoBehaviour {

    private TextAsset csv_cards_data;
    private List<string[]> cards_data=new List<string[]>();


	// Use this for initialization
	void Start () {
        csv_cards_data = Resources.Load("cards_data") as TextAsset;
        StringReader csv_reader = new StringReader(csv_cards_data.text);

        while (csv_reader.Peek() > -1)
            cards_data.Add(csv_reader.ReadLine().Split(','));

        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.Return))
            .Subscribe(_ => {
                var _card= GameObject.FindGameObjectWithTag("Card");
                var _canvas = _card.transform.FindChild("Canvas").gameObject;
                _canvas.GetComponentInChildren<Text>().text = printCard(Random.Range(1,cards_data.Count));

                _card.GetComponent<AudioSource>().Play();
            });
    }

    string printCard(int ar_index)
    {
        string _str = "";
        int _length;
        foreach (var w in cards_data[ar_index])
        {
            _length = (w.Length == Encoding.GetEncoding("shift_jis").GetByteCount(w)) ? (int)(w.Length / 2) :w.Length;

            if (_length > 9) _str += "<size=2>" + w + "</size>" + "\r\n";
            else if (_length > 7) _str += "<size=3>" + w + "</size>" + "\r\n";
            else _str += w + "\r\n";
        }
        return _str;
    }

    // Update is called once per frame
    void Update () {
	}
}
