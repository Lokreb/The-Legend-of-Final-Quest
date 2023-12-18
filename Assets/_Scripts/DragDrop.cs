using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    public GameObject objectDrag;
    public GameObject[] ObjectsDragToPos;
    public GameObject[] ObjectsDraggable;
    public float DropDistance;
    public bool isLocked;

    private Dictionary<GameObject, DraggableObject> draggableObjects = new Dictionary<GameObject, DraggableObject>();
    private GameObject audioSourcesContainer;
    private MusicManager musicManager;
    private Vector3 objectInitPos;
    private Transform originalParent;


    void Start()
    {
        objectInitPos = objectDrag.transform.position;
        originalParent = objectDrag.transform.parent;
        audioSourcesContainer = new GameObject("AudioSourcesContainer");
        musicManager = FindObjectOfType<MusicManager>();
        if (musicManager == null)
        {
            Debug.LogError("MusicManager not found in the scene.");
        }

        SyncAudioLoops();

    }

    void SyncAudioLoops()
    {
        foreach (var obj in ObjectsDraggable)
        {
            DraggableObject draggableObject = obj.GetComponent<DraggableObject>();
            if (draggableObject != null && draggableObject.audioClip != null)
            {
                draggableObject.audioSource = audioSourcesContainer.AddComponent<AudioSource>();
                draggableObject.audioSource.clip = draggableObject.audioClip;
                draggableObject.audioSource.loop = true;
                draggableObjects[obj] = draggableObject;
            }
        }
    }


        public void DragObject()
    {
        if (!isLocked)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            objectDrag.transform.position = new Vector3(mousePosition.x, mousePosition.y, objectInitPos.z);
        }
    }

    public void DropObject()
    {
        if (!isLocked)
        {
            float minDistance = float.MaxValue;
            Transform nearestSlot = null;

            foreach (GameObject dropTarget in ObjectsDragToPos)
            {
                // Vérifiez si le slot est occupé
                if (dropTarget.transform.childCount > 0)
                {
                    // Récupérez l'enfant du slot
                    Transform child = dropTarget.transform.GetChild(0);

                    // Vérifiez si l'enfant est l'objet initial
                    if (child == objectDrag.transform)
                    {
                        // Désengager l'enfant du slot
                        child.SetParent(null);
                        StopMusicForObject(objectDrag);
                       
                    }
                    else
                    {
                        continue; // Slot occupé par un autre objet, passez au suivant
                    }
                }

                // Convertissez les coordonnées du monde 3D des emplacements de slot
                Vector3 slotPosition = dropTarget.transform.position;

                float distance = Vector3.Distance(objectDrag.transform.position, slotPosition);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestSlot = dropTarget.transform;
                }
            }

            if (nearestSlot != null && minDistance < DropDistance)
            {
                objectDrag.transform.position = new Vector3(nearestSlot.position.x, nearestSlot.position.y, objectInitPos.z);
                // Mettez l'objet en tant qu'enfant du slot pour le verrouiller en place
                objectDrag.transform.SetParent(nearestSlot);
                PlayMusicForObject(objectDrag);
            }
            else
            {
                objectDrag.transform.position = objectInitPos;
                // Rétablissez le parent d'origine de l'objet
                objectDrag.transform.SetParent(originalParent);
               
            }
        }
    }

    void PlayMusicForObject(GameObject obj)
    {
        DraggableObject draggableObject;
        if (draggableObjects.TryGetValue(obj, out draggableObject))
        {
            musicManager.PlayMusic(draggableObject.audioSource.clip);
        }
    }

    void StopMusicForObject(GameObject obj)
    {
        DraggableObject draggableObject;
        if (draggableObjects.TryGetValue(obj, out draggableObject))
        {
            draggableObject.audioSource.Stop();
            musicManager.RemoveAudioSource(draggableObject.audioSource);
        }
    }


}
