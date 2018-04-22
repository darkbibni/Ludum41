using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitrin : MonoBehaviour {

    [SerializeField] private Interactable interactable;
    [SerializeField] private Animator anim;
    [SerializeField] private Item item;
    [SerializeField] private ItemData itemData; // TODO Scriptable object.

    [SerializeField] private HookingMicrogame hookingMg;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSrc;

    private PlayerInventory playerInventory;
    private PlayerInput playerInput;

    private bool isStealing;
    private bool vitrinLocked;
    private bool vitrinEmpty;

    private void Awake()
    {
        vitrinEmpty = false;
        vitrinLocked = false;
        isStealing = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player)
        {
            playerInventory = player.GetComponent<PlayerInventory>();
            playerInput = player.GetComponent<PlayerInput>();
        }

        item.Data = itemData;

        interactable.OnInteract += OnCutGlass;
        interactable.OnDiscreteInteract += OnUnlockVitrin;
    }

    private void OnCutGlass()
    {
        if(isStealing || vitrinEmpty)
        {
            return;
        }

        isStealing = true;
        
        anim.SetTrigger("CutGlass");

        audioSrc.clip = AudioManager.instance.GetSound("SFX_CUT_GLASS");
        audioSrc.Play();

        StartCoroutine(WaitCutGlassAction());
    }

    private IEnumerator WaitCutGlassAction()
    {
        yield return new WaitUntil(() => (anim.GetBool("End")));
        anim.SetBool("End", false);

        StealItem();
    }

    private void OnUnlockVitrin()
    {
        if (isStealing || vitrinLocked || vitrinEmpty)
        {
            return;
        }
        
        isStealing = true;
        playerInput.InputLocked = true;

        StartCoroutine(UnlockAction());
    }

    private IEnumerator UnlockAction()
    {
        hookingMg.StartMg();

        audioSrc.clip = AudioManager.instance.GetSound("SFX_HOOKING_PROGRESS");
        audioSrc.Play();

        yield return new WaitUntil(() => hookingMg.IsFinished);

        if (hookingMg.HasSucceed)
        {
            anim.SetTrigger("Open");
            
            audioSrc.PlayOneShot(AudioManager.instance.GetSound("SFX_HOOKING_SUCCEED"));

            audioSrc.clip = AudioManager.instance.GetSound("SFX_OPEN_VITRIN");
            audioSrc.Play();

            yield return new WaitUntil(() => (anim.GetBool("End")));
            anim.SetBool("End", false);

            StealItem();
        }

        else
        {
            vitrinLocked = true;

            audioSrc.PlayOneShot(AudioManager.instance.GetSound("SFX_HOOKING_FAILED"));
        }

        playerInput.InputLocked = false;
    }

    private void StealItem()
    {
        playerInventory.AddItem(item);

        // TODO Fade ! Then Display on UI !
        item.gameObject.SetActive(false);

        vitrinEmpty = true;

        isStealing = false;
    }
}
