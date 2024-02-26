using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenHandler : MonoBehaviour
{
    [SerializeField]
    private Vector2 offset;
    private Vector2 startPos;
    private static GameObject pointer;
    private static Transform options;
    public static int mouseValue;
    private static int value;
    private bool isPressed;
    public static bool animDone;
    public static bool isSelected;
    public static int incremental = 150;
    // Start is called before the first frame update
    void Start()
    {
        mouseValue = -1;
        pointer = transform.Find("Pointer").gameObject;
        options = transform.Find("Options");
        OptionHandler.Redirect += Redirection;
    }

    // Update is called once per frame
    void Update()
    {
        startPos = options.transform.position;
        OptionDetection();
        ClickDetection();
    }

    private void OptionDetection()
    {
        if (!isSelected)
        {
            if (mouseValue >= 0)
            {
                ChangingPos(mouseValue);
            }
            if (!isPressed && Input.anyKeyDown)
            {
                isPressed = true;
                StartCoroutine(ChangePos());
            }
        }
    }

    private void ClickDetection()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Z))
        {
            isSelected = true;
            Redirection();
            switch (options.GetChild(value).name)
            {
                case "play":
                    Play();
                    break;
                case "quit":
                    Quit();
                    break;
            }
        }
    }
    private static void Redirection()
    {
        var anim = pointer.GetComponent<Animator>();
        anim.SetTrigger("Selected");
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
    }
    public void Play()
    {
        StartCoroutine(SwitchScene());
    }
    public void Quit()
    {
        StartCoroutine(QuitGame());
    }
    private IEnumerator SwitchScene()
    {
        print("play");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainLevel", LoadSceneMode.Single);
    }
    private IEnumerator QuitGame()
    {
        print("quit");
        yield return new WaitForSeconds(0.5f);
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private IEnumerator ChangePos()
    {
        while (Input.anyKey)
        {
            var input = (int) -Input.GetAxisRaw("Vertical");
            options.GetChild(value).GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            value = Mathf.Clamp(value + input, 0, options.childCount-1);
            options.GetChild(value).GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Underline;
            ChangingPos(value);
            yield return new WaitForSeconds(0.2f);
        }
        isPressed = false;
    }   

    private void ChangingPos(int value)
    {
        pointer.transform.position = new Vector2(startPos.x + offset.x, startPos.y + offset.y - value * incremental);
    }

}
