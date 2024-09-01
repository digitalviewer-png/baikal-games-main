using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BaikalGames.CleanBaikal
{
    public class hslChanger : MonoBehaviour
    {
        private bool _isFinded = false;
        private float _adjustMaxEffect;
        private float _originalAdjustMaxEffect;
        private float _interpolator = 0.2f;
        private Material _material;

        /// <summary>
        /// ������ ��������� �� �������, ���������� ����������� ������������ � � ����� ��� ���������
        /// </summary>
        public void ChangeSaturation()
        {
            _isFinded = true;
        }

        /// <summary>
        /// ����������� ����������� ������������ ��������� ��� ������
        /// </summary>
        public void DefaultSaturation()
        {
            _isFinded = false;
            _material.SetFloat("_HSLRangeMax", _originalAdjustMaxEffect);
        }

        private void Start()
        {
            _material = GetComponent<Image>().material;
            _originalAdjustMaxEffect = _material.GetFloat("_HSLRangeMax");
            _adjustMaxEffect = _originalAdjustMaxEffect;
        }

        /// <summary>
        /// ������� ����� ������������ ����� Math.Lerp
        /// </summary>
        private void Update()
        {
            if (!_isFinded) return;
            _material.SetFloat("_HSLRangeMax", Mathf.Lerp(_adjustMaxEffect, 0, _interpolator));
            _interpolator += 0.5f * Time.deltaTime;
        }
    }
}
