using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// ネットワーク用.
using System.Net;
using UnityEngine.Networking;


public class RankingManager : MonoBehaviour
{
    /*Http通信*/
    // hostURL.
    //const string _hostPass = @"http://localhost/UnityRankingSample/";
    // filePass.
    const string _getPass = @"GetRanking.php";
    const string _postPass = @"PostRanking.php";
    const string _createPass = @"CreateTable.php";
    const string _resetPass = @"ResetDB.php";

    /*uGUI*/
    // hostURL
    InputField _hostUrlField;

    // テーブルの作成
    InputField _createTableField;
    InputField _colum1TableField;
    InputField _colum2TableField;
    InputField _colum3TableField;
    // テーブルの受信
    Text _rankingLabel;
    InputField _getTableField;
    // テーブルの更新
    InputField _updateTableField;
    InputField _nameField;
    InputField _scoreField;


    // Start is called before the first frame update
    void Start()
    {
        /*uGUI*/
        //hostURL
        _hostUrlField = GameObject.Find("ServerUrlField").GetComponent<InputField>();
        // テーブル作成
        _createTableField = GameObject.Find("CreateTableField").GetComponent<InputField>();
        _colum1TableField = GameObject.Find("CreateColum1Field").GetComponent<InputField>();
        _colum2TableField = GameObject.Find("CreateColum2Field").GetComponent<InputField>();
        _colum3TableField = GameObject.Find("CreateColum3Field").GetComponent<InputField>();
        // テーブルの受信
        _rankingLabel = GameObject.Find("TextRanking").GetComponent<Text>();
        _getTableField = GameObject.Find("GetRankingTableField").GetComponent<InputField>();

        // テーブルの更新
        _updateTableField = GameObject.Find("TextRanking").GetComponent<InputField>();
        _nameField = GameObject.Find("NameField").GetComponent<InputField>();
        _scoreField = GameObject.Find("ScoreField").GetComponent<InputField>();

        /*input初期化*/
        _createTableField.text = @"Ranking";
        _colum1TableField.text = @"rank";
        _colum2TableField.text = @"name";
        _colum3TableField.text = @"score";
        _getTableField.text = @"Ranking";
        _hostUrlField.text = @"http://localhost/UnityRankingSample/";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*ボタンメソッド*/
    public void GetRanking()
    {
        StartCoroutine(GetRequest(_hostUrlField.text, _getPass));
    }

    public void UpdateRanking()// ランキングの更新
    {
        StartCoroutine(PostRequest(_hostUrlField.text, _postPass));
    }

    public void CreateTable()// テーブルの作成
    {
        // 非同期通信がいる、そのため別スレッドにメソッドを実行させる
        StartCoroutine(PostCreate(_hostUrlField.text, _createPass));
    }

    public void ResetDB()// データベースの初期化
    {
        // 
        StartCoroutine(PostRequest(_hostUrlField.text, _resetPass));
    }

    /*Httpメソッド*/
    // コルーチンで処理させるためには、IEnumeratorを返り値としてもつメソッド
    IEnumerator GetRequest(string hostUrl, string filePass)
    {
        /*URL作成*/
        string _getUrl = hostUrl + filePass;

        /*クエストリングの付与*/
        _getUrl += @"?" + @"table=" + _getTableField.text;

        /*GetRequestの作成*/
        using UnityWebRequest _getRequest = UnityWebRequest.Get(_getUrl);
        /*yield return*/
        yield return _getRequest.SendWebRequest();
        /*エラー処理*/
        if (_getRequest.isNetworkError)
        {
            // 通信失敗
            Debug.Log(_getRequest.error);
            Debug.Log(_getUrl);
        }
        else
        {
            // 通信成功
            _rankingLabel.text = _getRequest.downloadHandler.text;
            Debug.Log(_getUrl);
        }
    }

    IEnumerator PostRequest(string hostUrl, string postPass)
    {
        /*URLの作成*/
        string _postUrl = hostUrl + postPass;

        /*フォームの作成*/
        WWWForm form = new WWWForm();
        form.AddField("table", _getTableField.text);
        form.AddField("name", _nameField.text);
        form.AddField("score", _scoreField.text);

        /*PostRequest*/
        using UnityWebRequest _postRequest = UnityWebRequest.Post(_postUrl, form);

        /*yield return*/
        yield return _postRequest.SendWebRequest();

        /*エラー処理*/
        if (_postRequest.isNetworkError)
        {
            // 通信失敗
            Debug.Log(_postRequest.error);
            Debug.Log(_postUrl);
        }
        else
        {
            // 通信成功
            Debug.Log(_postRequest.downloadHandler.text);
            Debug.Log (_postUrl);
        }
    }

    IEnumerator PostCreate(string hostUrl, string filePass)
    {
        /*URL作成*/
        string _postUrl = hostUrl + filePass;

        /*フォームの作成*/
        // ポストはURLのみの通信ではなく、URLに対してFromを渡すことで通信する。
        // なのでフォームを作成する必要がある。
        WWWForm form = new WWWForm();
        // フォームにデータを挿入
        form.AddField("table", _createTableField.text);
        form.AddField("colum1", _colum1TableField.text);
        form.AddField("colum2", _colum2TableField.text);
        form.AddField("colum3", _colum3TableField.text);

        /*PostRequest*/
        // using => スコープ抜けたら自動的にメモリ開放
        using UnityWebRequest _postRequest = UnityWebRequest.Post(_postUrl, form);

        /*yield return*/
        yield return _postRequest.SendWebRequest();
        /*エラー処理*/
        if(_postRequest.isNetworkError)
        {
            // 通信失敗
            Debug.Log(_postRequest.error);
            Debug.Log(_postUrl);
        }
        else
        {
            // 通信成功
            Debug.Log(_postRequest.downloadHandler.text);
            Debug.Log(_postUrl);
        }
    }
}
