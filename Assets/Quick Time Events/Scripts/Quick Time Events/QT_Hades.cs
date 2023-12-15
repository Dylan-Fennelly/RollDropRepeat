using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace DefaultNamespace
{
    public class QT_Hades : QuickTimeEvent
    {
        private KeyCode[] key = {KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S};
        private List<KeyCode> keys;
        
        private bool wasShown = false;
        private bool toShow = true;
        

        [Header("Keys")]
        [SerializeField]
        private  UnityEngine.UI.Image wImage;
        [SerializeField]
        private UnityEngine.UI.Image aImage;
        [SerializeField]
        private UnityEngine.UI.Image sImage;
        [SerializeField]
        private UnityEngine.UI.Image dImage;
        
        
        

        public override void StartQTE()
        {
            base.StartQTE();
            keys = new List<KeyCode>();
            for (int i = 0; i < data.goal; i++)
            {
                keys.Add(key[Random.Range(0, key.Length)]);
            }
        }

        protected override void ConfirmGuide()
        {
            base.ConfirmGuide();
            if (toShow && IsRunning)
            {
                StartCoroutine(ShowKyes());
                toShow = false;
            }
            
        }

        protected override void HandleInput()
        {
            if (Input.GetKeyDown(keys[Progress]) && wasShown)
            {
                Progress++;
                PlayUISound();
            }
            
        }

        private IEnumerator ShowKyes()
        {
            int i = 0;
            while (i < keys.Count)
            {
                switch (keys[i])
                {
                    case KeyCode.A:
                        StartCoroutine(scaleKeyUp(aImage));
                        break;
                    case KeyCode.D:
                        StartCoroutine(scaleKeyUp(dImage));
                        break;
                    case KeyCode.W:
                        StartCoroutine(scaleKeyUp(wImage));
                        break;
                    case KeyCode.S:
                        StartCoroutine(scaleKeyUp(sImage));
                        break;
                }
                yield return new WaitForSeconds(0.3f);
                switch (keys[i])
                {
                    case KeyCode.A:
                        StartCoroutine(scaleKeyDown(aImage));
                        break;
                    case KeyCode.D:
                        StartCoroutine(scaleKeyDown(dImage));
                        break;
                    case KeyCode.W:
                        StartCoroutine(scaleKeyDown(wImage));
                        break;
                    case KeyCode.S:
                        StartCoroutine(scaleKeyDown(sImage));
                        break;
                }
                yield return new WaitForSeconds(0.3f);
                i++;
            }
            wasShown = true;
        }

        private IEnumerator scaleKeyUp(Image image)
        {
            image.gameObject.transform.localScale = new Vector3(2, 2, 2);
            yield return new WaitForSeconds(0.3f);
        }
        
        private IEnumerator scaleKeyDown(Image image)
        {
            image.gameObject.transform.localScale = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(0.3f);
        }
    }
}