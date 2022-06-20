using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Magnet : MonoBehaviour
{
    #region Singleton class:Magnet
    public static Magnet Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion

    [SerializeField] float magnetForce;
    private float _cacheMagnetForce;
    List<Rigidbody> affectedRigidbodies = new List<Rigidbody>();
    Transform magnet;

    public float MagnetForce { get => magnetForce; set => magnetForce = value; }

    private void Start()
    {
        magnet = transform;
        affectedRigidbodies.Clear();
        _cacheMagnetForce = magnetForce;
    }
    private void FixedUpdate()
    {
        if (!Game.isGameOver && Game.isMoving)
        {
            foreach (var rb in affectedRigidbodies)
            {
                rb.AddForce((magnet.position - rb.position) * magnetForce * Time.fixedDeltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Game.isGameOver && (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Object")))
        {
            AddToMagnetField(other.attachedRigidbody);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!Game.isGameOver && (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Object")))
        {
            RemoveFromMagnetField(other.attachedRigidbody);
        }
    }

    public void ResetMagnetForce()
    {
        MagnetForce = _cacheMagnetForce;
    }

    public void AddToMagnetField(Rigidbody rb)
    {
        affectedRigidbodies.Add(rb);
    }
    public void RemoveFromMagnetField(Rigidbody rb)
    {
        affectedRigidbodies.Remove(rb);
    }
}
