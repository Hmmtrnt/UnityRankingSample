using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// �l�b�g���[�N�p.
using System.Net;
using UnityEngine.Networking;


public class RankingManager : MonoBehaviour
{
    /*Http�ʐM*/
    // hostURL.
    const string _hostPass = @"http://localhost/UnityRankingSample/";
    // filePass.
    const string _getPass = @"GetRanking.php";
    const string _postPass = @"PostRanking.php";
    const string _createPass = @"CreateTable.php";
    const string _resetPass = @"ResetDB.php";

    /*uGUI*/
    // hostURL
    InputField _hostUrlField;

    // �e�[�u���̍쐬
    InputField _createTableField;
    InputField _colum1TableField;
    InputField _colum2TableField;
    InputField _colum3TableField;
    // �e�[�u���̎�M
    Text _rankingLabel;
    InputField _getTableField;
    // �e�[�u���̍X�V
    InputField _updateTableField;
    InputField _nameField;
    InputField _scoreField;
    // �f�[�^�x�[�X�̏�����


    // Start is called before the first frame update
    void Start()
    {
        /*uGUI*/
        //hostURL
        _hostUrlField = GameObject.Find("ServerUrlField").GetComponent<InputField>();
        // �e�[�u���쐬
        _createTableField = GameObject.Find("CreateTableField").GetComponent<InputField>();
        _colum1TableField = GameObject.Find("CreateColum1Field").GetComponent<InputField>();
        _colum2TableField = GameObject.Find("CreateColum2Field").GetComponent<InputField>();
        _colum3TableField = GameObject.Find("CreateColum3Field").GetComponent<InputField>();
        // �e�[�u���̎�M
        _rankingLabel = GameObject.Find("TextRanking").GetComponent<Text>();
        _getTableField = GameObject.Find("GetRankingTableField").GetComponent<InputField>();

        // �e�[�u���̍X�V
        _updateTableField = GameObject.Find("TextRanking").GetComponent<InputField>();
        _nameField = GameObject.Find("NameField").GetComponent<InputField>();
        _scoreField = GameObject.Find("ScoreField").GetComponent<InputField>();

        /*input������*/
        _createTableField.text = @"Ranking";
        _colum1TableField.text = @"rank";
        _colum2TableField.text = @"name";
        _colum3TableField.text = @"score";
        _getTableField.text = @"Ranking";
        _hostUrlField.text = @"http://localhost/UnityRankingSample/CreateTable.php";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*�{�^�����\�b�h*/
    public void GetRanking()
    {
        StartCoroutine(GetRequest(_hostPass, _getPass));
    }

    public void UpdateRanking()// �����L���O�̍X�V
    {
        StartCoroutine(PostRequest(_hostUrlField.text, _postPass));
    }

    public void CreateTable()// �e�[�u���̍쐬
    {
        // �񓯊��ʐM������A���̂��ߕʃX���b�h�Ƀ��\�b�h�����s������
        StartCoroutine(PostCreate(_hostPass, _createPass));
    }

    public void ResetDB()// �f�[�^�x�[�X�̏�����
    {
        // 
        StartCoroutine(PostRequest(_hostUrlField.text, _resetPass));
    }

    /*Http���\�b�h*/
    // �R���[�`���ŏ��������邽�߂ɂ́AIEnumerator��Ԃ�l�Ƃ��Ă����\�b�h
    IEnumerator GetRequest(string hostUrl, string filePass)
    {
        /*URL�쐬*/
        string _getUrl = hostUrl + filePass;

        /*�N�G�X�g�����O�̕t�^*/
        _getUrl += @"?" + @"table=" + _getTableField.text;

        /*GetRequest�̍쐬*/
        using UnityWebRequest _getRequest = UnityWebRequest.Get(_getUrl);
        /*yield return*/
        yield return _getRequest.SendWebRequest();
        /*�G���[����*/
        if (_getRequest.isNetworkError)
        {
            // �ʐM���s
            Debug.Log(_getRequest.error);
            Debug.Log(_getUrl);
        }
        else
        {
            // �ʐM����
            _rankingLabel.text = _getRequest.downloadHandler.text;
            Debug.Log(_getUrl);
        }
    }

    IEnumerator PostRequest(string hostUrl, string postPass)
    {
        /*URL�̍쐬*/
        string _postUrl = hostUrl + postPass;

        /*�t�H�[���̍쐬*/
        WWWForm form = new WWWForm();
        form.AddField("table", _getTableField.text);
        form.AddField("name", _nameField.text);
        form.AddField("score", _scoreField.text);

        /*PostRequest*/
        using UnityWebRequest _postRequest = UnityWebRequest.Post(_postUrl, form);

        /*yield return*/
        yield return _postRequest.SendWebRequest();

        /*�G���[����*/
        if (_postRequest.isNetworkError)
        {
            // �ʐM���s
            Debug.Log(_postRequest.error);
            Debug.Log(_postUrl);
        }
        else
        {
            // �ʐM����
            Debug.Log(_postRequest.downloadHandler.text);
            Debug.Log (_postUrl);
        }
    }

    IEnumerator PostCreate(string hostUrl, string filePass)
    {
        /*URL�쐬*/
        string _postUrl = hostUrl + filePass;

        /*�t�H�[���̍쐬*/
        // �|�X�g��URL�݂̂̒ʐM�ł͂Ȃ��AURL�ɑ΂���From��n�����ƂŒʐM����B
        // �Ȃ̂Ńt�H�[�����쐬����K�v������B
        WWWForm form = new WWWForm();
        // �t�H�[���Ƀf�[�^��}��
        form.AddField("table", _createTableField.text);
        form.AddField("colum1", _colum1TableField.text);
        form.AddField("colum2", _colum2TableField.text);
        form.AddField("colum3", _colum3TableField.text);

        /*PostRequest*/
        // using => �X�R�[�v�������玩���I�Ƀ������J��
        using UnityWebRequest _postRequest = UnityWebRequest.Post(_postUrl, form);

        /*yield return*/
        yield return _postRequest.SendWebRequest();
        /*�G���[����*/
        if(_postRequest.isNetworkError)
        {
            // �ʐM���s
            Debug.Log(_postRequest.error);
            Debug.Log(_postUrl);
        }
        else
        {
            // �ʐM����
            Debug.Log(_postRequest.downloadHandler.text);
            Debug.Log(_postUrl);
        }
    }
}
