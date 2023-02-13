using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Mathematics.math;
using WeArt.Core;
using WeArt.Components;
using Texture = WeArt.Core.Texture;
using System;

public class CustomEffect : MonoBehaviour
{
    [SerializeField]
    private WeArtHapticObject _leftIndexHapticObject;

    [SerializeField]
    private WeArtThimbleTrackingObject _indexThimbleTrackingObject;

    [SerializeField]
    private WeArtHapticObject _leftThumbHapticObject;

    [SerializeField]
    private WeArtThimbleTrackingObject _thumbThimbleTrackingObject;

    private MyEffect _effect;
    public float _currentForce = 0f;

    private float magnitude;        // Magnitude of the ConstantForce component
    public float magNorm;           // Magnitude of the force vector clamped between 0 - 200
    public float maxForce;          // The force value that corresponds to the maximum force feedback

    private float effectDuration = 2.0f;
    private float timer = 0.0f;

    Texture texture = Texture.Default;
    Force force = Force.Default;

    // Start is called before the first frame update
    void Start()
    {
        _effect = new MyEffect();

        _effect.Set(Temperature.Default, Force.Default, Texture.Default);

        _leftIndexHapticObject.AddEffect(_effect);
        _leftThumbHapticObject.AddEffect(_effect);

    }

    // Update is called once per frame
    void Update()
    {
        //magnitude = (float)GameObject.Find("Simulation Target Cube").GetComponent<RosSharp.RosBridgeClient.GripperJointStateSubscriber>().averageEffort;
        magnitude = gameObject.GetComponent<ConstantForce>().force.magnitude;
        magNorm = Math.Clamp(magnitude, 0f, 200);
        float indexClosing = _indexThimbleTrackingObject.Closure.Value;
        float thumbClosing = _thumbThimbleTrackingObject.Closure.Value;

        _currentForce = remap(5f, maxForce, 0f, 1f, magNorm);

        
        force.Active = true;
        force.Value = _currentForce;


        Temperature temperature = Temperature.Default;
        temperature.Active = true;
        //temperature.Value = 0.2f;
        
        
        /* if (GameObject.Find("Target Cube").GetComponent<RosSharp.RosBridgeClient.GraspActionResultSubscriber>().isMessageReceived == true)
            timer += Time.deltaTime;
            texture.Active = true;
            texture.TextureType = TextureType.CrushedRock;
            texture.Volume = 100f;
            texture.VelocityZ = 1.0f;
            if(timer > effectDuration)
                timer -= effectDuration;
                texture = Texture.Default;
                GameObject.Find("Target Cube").GetComponent<RosSharp.RosBridgeClient.GraspActionResultSubscriber>().success = false; */
        
        
        _effect.Set(temperature, force, texture);

    }

    private class MyEffect : IWeArtEffect
    {
        #region Fields

        #endregion

        #region Events

        /// <summary>
        /// Defines the OnUpdate.
        /// </summary>
        public event Action OnUpdate;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Temperature.
        /// </summary>
        public Temperature Temperature { get; private set; } = Temperature.Default;

        /// <summary>
        /// Gets the Force.
        /// </summary>
        public Force Force { get; private set; } = Force.Default;

        /// <summary>
        /// Gets the Texture.
        /// </summary>
        public Texture Texture { get; private set; } = Texture.Default;

        #endregion

        #region Methods

        /// <summary>
        /// The Set.
        /// </summary>
        /// <param name="temperature">The temperature<see cref="Temperature"/>.</param>
        /// <param name="force">The force<see cref="Force"/>.</param>
        /// <param name="texture">The texture<see cref="Texture"/>.</param>
        /// <param name="impactInfo">The impactInfo<see cref="ImpactInfo"/>.</param>
        public void Set(Temperature temperature, Force force, Texture texture)
        {
            // Need to clone these, or the internal arrays will point to the same data
            force = (Force)force.Clone();
            texture = (Texture)texture.Clone();

            bool changed = false;

            // Temperature
            changed |= !Temperature.Equals(temperature);
            Temperature = temperature;

            // Force
            changed |= !Force.Equals(force);
            Force = force;

            // Texture
            changed |= !Texture.Equals(texture);
            Texture = texture;

            if (changed)
                OnUpdate?.Invoke();
        }

        #endregion
    }
}
