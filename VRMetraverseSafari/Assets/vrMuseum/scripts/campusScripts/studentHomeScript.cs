using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vrMuseumCourseware;

namespace VRMuseum
{
    public class studentHomeScript : MonoBehaviour
    {
        [Header("Courseware")]
        [SerializeField] private Transform m_Camera;
        [SerializeField] private float m_RayLength = 500f;
        [SerializeField] private Reticle m_Reticle;

        private bool enable = true;
        public void enableTeacherScripts(bool mode)
        {
            enable = mode;
        }
        private gazeableObject m_CurrentGazeable;
        private gazeableObject m_LastGazeable;

        private void EyeRaycast()
        {
            // Create a ray that points forwards from the camera.
            Ray ray = new Ray(m_Camera.position, m_Camera.forward);
            RaycastHit hit;

            // Do the raycast forweards to see if we hit an interactive item
            if (Physics.Raycast(ray, out hit, m_RayLength))
            {
                gazeableObject interactible = hit.collider.GetComponent<gazeableObject>();
                //attempt to get the VRInteractiveItem on the hit object
                m_CurrentGazeable = interactible;

                // If we hit an interactive item and it's not the same as the last interactive item, then call Over
                if (interactible && interactible != m_LastGazeable)
                {
                    interactible.Over();
                }

                // Deactive the last interactive item 
                if (interactible != m_LastGazeable)
                    DeactiveateLastInteractible();

                m_LastGazeable = interactible;

                if (interactible)
                {
                    m_Reticle.SetOn(true);
                }
                else
                {
                    m_Reticle.SetOn(false);
                }

                // Something was hit, set at the hit position.
                if (m_Reticle)
                    m_Reticle.SetPosition(hit);

            }
            else
            {
                // Nothing was hit, deactive the last interactive item.
                DeactiveateLastInteractible();
                m_CurrentGazeable = null;
                m_Reticle.SetOn(false);

                // Position the reticle at default distance.
                if (m_Reticle)
                    m_Reticle.SetPosition();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!enable) return;
            EyeRaycast();
        }

        private void DeactiveateLastInteractible()
        {
            if (m_LastGazeable == null)
                return;

            m_LastGazeable.Out();
            m_LastGazeable = null;
        }
    }
}
