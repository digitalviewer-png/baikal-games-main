using UnityEngine;

namespace BaikalGames.UnderwaterSealAdvancher
{

    public class SplitScreenPlayerMovement : PlayerMovement
    {
        private bool isRight;
        private void Start()
        {
            screenSizeMultiplier = (float)Screen.width / 1920f;

            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

            Vector3 toEnemy = GetTargetPosition(new Vector2(Screen.width / 4 * 3, Screen.height / 3)) - mainCamera.transform.position + transform.forward;
            Ray ray = new Ray(mainCamera.transform.position + transform.forward, toEnemy);
            Debug.DrawRay(mainCamera.transform.position, toEnemy, Color.red, 5);

            float rayMinDistance = Mathf.Infinity;
            int index = 0;

            for (int p = 0; p < 4; p++)
            {
                if (planes[p].Raycast(ray, out float distance))
                {
                    if (distance < rayMinDistance)
                    {
                        rayMinDistance = distance;
                        index = p;
                    }
                }
            }

            rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toEnemy.magnitude);
            Vector3 worldPosition = ray.GetPoint(rayMinDistance);
            Vector3 position = mainCamera.WorldToScreenPoint(worldPosition);

            if (toEnemy.magnitude > rayMinDistance)
            {
                isRight = false;
            }
            else
            {
                isRight = true;
            }
            if (isRight)
            {
                mousePosition = new Vector2(Screen.width / 4 * 3, Screen.height / 2);
            }
            else
            {
                mousePosition = new Vector2(Screen.width / 4, Screen.height / 2);
            }
        }
        protected override void Update()
        {
            base.Update();
        }
        protected override void FixedUpdate()
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Vector2 coordinates = Input.GetTouch(i).position;
                if (isRight)
                {
                    if (coordinates.x < Screen.width / 2) continue;
                }
                else
                {
                    if (coordinates.x > Screen.width / 2) continue;
                }
                mousePosition = coordinates;
            }
            if (isRight)
            {
                mousePosition = mousePosition + (GetPlayerArrowInputVector() * keyboardModifier * screenSizeMultiplier);
                mousePosition = new Vector2(Mathf.Clamp(mousePosition.x, Screen.width / 2, Screen.width), Mathf.Clamp(mousePosition.y, 0, Screen.height));
            }
            else
            {
                mousePosition = mousePosition + (GetPlayerWASDInputVector() * keyboardModifier * screenSizeMultiplier);
                print(GetPlayerWASDInputVector() * keyboardModifier * screenSizeMultiplier);
                mousePosition = new Vector2(Mathf.Clamp(mousePosition.x, 0, Screen.width / 2), Mathf.Clamp(mousePosition.y, 0, Screen.height));
            }
        }
        protected Vector2 GetPlayerArrowInputVector()
        {
            int x = 0;
            int y = 0;

            if (Input.GetKey(KeyCode.UpArrow)) y += 1;
            if (Input.GetKey(KeyCode.DownArrow)) y += -1;
            if (Input.GetKey(KeyCode.RightArrow)) x += 1;
            if (Input.GetKey(KeyCode.LeftArrow)) x += -1;

            return new Vector2(x, y);
        }
    }
}
