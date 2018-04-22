using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitrin : MonoBehaviour {

    [SerializeField] private Interactable interactable;
    [SerializeField] private Animator anim;
    [SerializeField] private Item item;
    [SerializeField] private ItemData itemData; // TODO Scriptable object.

    [Header("Audio")]
    [SerializeField] private AudioSource audioSrc;

    private PlayerInventory playerInventory;

    private bool IsStealing;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player)
        {
            playerInventory = player.GetComponent<PlayerInventory>();
        }

        item.Data = itemData;

        interactable.OnInteract += OnCutGlass;
    }

    private void OnCutGlass()
    {
        if(IsStealing)
        {
            return;
        }

        IsStealing = true;

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
        if (IsStealing)
        {
            return;
        }

        IsStealing = true;

        // TODO Trigger microgame !!!

        StartCoroutine(UnlockAction());
    }

    private IEnumerator UnlockAction()
    {
        anim.SetTrigger("Open");

        audioSrc.clip = AudioManager.instance.GetSound("SFX_UNLOCK_VITRIN");
        audioSrc.Play();

        yield return new WaitUntil(() => (anim.GetBool("End")));
        anim.SetBool("End", false);

        StealItem();
    }

    private void StealItem()
    {
        playerInventory.AddItem(item);

        // TODO Fade ! Then Display on UI !
        item.gameObject.SetActive(false);
    }
}
