using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeArt.Core;
using WeArt.Components;
using Texture = WeArt.Core.Texture;
using System;

public class CustomEffectTutorial : MonoBehaviour
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
    private float _currentForce = 0.5f;

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
        float indexClosing = _indexThimbleTrackingObject.Closure.Value;
        float thumbClosing = _thumbThimbleTrackingObject.Closure.Value;

        Debug.Log("Index closing: " + indexClosing);
        Debug.Log("Thumb closing: " + thumbClosing);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_leftIndexHapticObject.ActiveEffect == null)
            {
                _leftIndexHapticObject.AddEffect(_effect);
            }

            if (_leftThumbHapticObject.ActiveEffect == null)
            {
                _leftThumbHapticObject.AddEffect(_effect);
            }

            _currentForce += 0.1f;

            Force force = Force.Default;
            force.Active = true;
            force.Value = _currentForce;

            Temperature temperature = Temperature.Default;
            temperature.Active = true;
            temperature.Value = 0.0f;

            _effect.Set(temperature, force, Texture.Default);

        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            _leftIndexHapticObject.RemoveEffect(_effect);
            _leftThumbHapticObject.RemoveEffect(_effect);
        }
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
