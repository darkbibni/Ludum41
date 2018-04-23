using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitrin : MonoBehaviour {

    [SerializeField] private Interactable interactable;
    [SerializeField] private Animator anim;
    [SerializeField] private Item item;
    [SerializeField] private ItemData itemData; // TODO Scriptable object.
    
    [SerializeField] private HookingMicrogame hookingMg;
    [SerializeField] private LockPickingPreset mgDifficulty;
    [SerializeField] private float unlockDuration = 0.5f;
    
    [SerializeField] private LayerMask warnableMask;
    [SerializeField] private Vector3 warnSize = Vector3.one;
    [SerializeField] private ParticleSystem noiseFX;

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

    private void OnValidate()
    {
        //item.Data = itemData;
        hookingMg.Difficulty = mgDifficulty;

        // TODO Rotation of vitrin.
        //transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, (((int)interactable.Orientation)+2) * 90);
    }

    private void OnCutGlass()
    {
        if(isStealing || vitrinEmpty)
        {
            return;
        }

        isStealing = true;
        
        StartCoroutine(CutGlassAction());
    }

    private IEnumerator CutGlassAction()
    {
        anim.SetTrigger("CutGlass");

        audioSrc.clip = AudioManager.instance.GetSound("SFX_CUT_GLASS");
        audioSrc.Play();

        noiseFX.Play();

        yield return new WaitUntil(() => (anim.GetBool("End")));
        anim.SetBool("End", false);
        
        WarnDogs();

        StealItem();
    }

    // Warn all dogs around !!!
    private void WarnDogs()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, warnSize, Quaternion.identity, warnableMask);

        foreach(Collider c in colliders)
        {
            Dog dog = c.GetComponent<Dog>();
            if(dog != null)
            {
                dog.WakeUp();
            }
        }
    }

    private void OnUnlockVitrin()
    {
        if(vitrinLocked)
        {
            audioSrc.clip = AudioManager.instance.GetSound("SFX_LOCKPICK_DENIED");
            audioSrc.Play();

            return;
        }

        if (isStealing || vitrinEmpty)
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

        audioSrc.loop = true;
        audioSrc.clip = AudioManager.instance.GetSound("SFX_LOCKPICK_LOOP");
        audioSrc.Play();

        yield return new WaitUntil(() => hookingMg.IsFinished);
        
        audioSrc.loop = false;

        if (hookingMg.HasSucceed)
        {
            yield return UnlockSuceed();
        }

        else
        {
            vitrinLocked = true;

            audioSrc.clip = AudioManager.instance.GetSound("SFX_LOCKPICK_FAIL");
            audioSrc.Play();
        }

        playerInput.InputLocked = false;
    }

    private IEnumerator UnlockSuceed()
    {
        audioSrc.clip = AudioManager.instance.GetSound("SFX_UNLOCK_VITRIN");
        audioSrc.Play();

        yield return new WaitForSeconds(unlockDuration);

        anim.SetTrigger("Open");

        yield return new WaitUntil(() => (anim.GetBool("End")));
        anim.SetBool("End", false);

        StealItem();
    }

    [ContextMenu("FAKE")]
    private void FakeUnlock()
    {
        StartCoroutine(CutGlassAction());
    }

    private void StealItem()
    {
        playerInventory.AddItem(item);

        // TODO Fade ! Then Display on UI !
        item.Disappear();

        vitrinEmpty = true;

        isStealing = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, warnSize * 2f);
    }
}
