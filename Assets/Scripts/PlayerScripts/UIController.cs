using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    /// <summary>
    ///TMP hates me and doesnt want to install without some weird manipulations
    /// </summary>
    public Image crosshair;
    public Color enemyColor;
    public Color defaultColor;

    [SerializeField] private Animator _animator;
    [SerializeField] private List<string> _tags; 
    void Start()
    {
        EnemyPointerCheck.Instance.onPointingAtEnemyChanged.AddListener(SetColor);
        FindObjectsOfType<AreaEntered>().ForEach(x => x.onLevelRestart.AddListener(AreaEntered));
        crosshair.color = defaultColor;
    }
    
    //These four should be in a separate class

    private void AreaEntered(bool isWin)
    {
        if (isWin) PlayerWon();
        else PlayerDied();
    }

    private void PlayerDied()
    {
        _animator.SetTrigger(_tags[0]);
        StartCoroutine(Restart());
    }

    private void PlayerWon()
    {
        _animator.SetTrigger(_tags[1]);
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetColor(bool isEnemy)
    {
        crosshair.color = isEnemy ? enemyColor : defaultColor;
    }
}
