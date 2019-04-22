using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolynomialResult : MonoBehaviour
{
    [HideInInspector]
    public int[] coefficientsValue;
    [HideInInspector]
    public float[] coefficientsValueDerivative;
    [SerializeField]
    private GameObject graphPanel = null;
    [SerializeField]
    private Graph graph = null;
    [SerializeField]
    private InputField inputFieldXValue = null;
    [SerializeField]
    private Text polinomResultText = null;
    [SerializeField]
    private Text polinomValueText = null;
    private int degreeOfPolynomial = 0;
    private List<int> result = new List<int>();
    
    public void DerivativeOfPolynomial()
    {
        List<float> result = new List<float>();
        for (int i = 0; i < coefficientsValueDerivative.Length; i++)
        {
            result.Add(coefficientsValueDerivative[i] * (coefficientsValueDerivative.Length - 1 - i));
            print(coefficientsValueDerivative.Length - 1 - i);
        }

        degreeOfPolynomial = result.Count - 2;

        string polinom = "";
        for (int i = 0; i < result.Count; i++)
        {
            string coefficientSign = "";
            if (i > 0 && result[i] > 0)
            {
                coefficientSign = "+";
            }
            string exponent = "";
            if (i != result.Count - 1)
            {
                exponent = "X^" + degreeOfPolynomial--.ToString();
            }

            if (result[i] != 0 && i != result.Count - 1)
            {
                //exponent = (degreeOfPolynomial == 0) ? exponent : "";
                polinom += coefficientSign + result[i].ToString() + exponent;
            }
        }
        polinomResultText.text = polinom;
    }

    public void Integration()
    {
        List<float> result = new List<float>();
        for (int i = 0; i < coefficientsValue.Length; i++)
        {
            if (coefficientsValue.Length - 1 - i != 0)
            {
                result.Add((float)coefficientsValue[i] / (coefficientsValue.Length - i));
            }
            else
            {
                result.Add((float)coefficientsValue[i]);
            }
        }

        degreeOfPolynomial = result.Count;

        string polinom = "";
        for (int i = 0; i < result.Count; i++)
        {
            string coefficientSign = "";
            if (i > 0 && result[i] > 0)
            {
                coefficientSign = "+";
            }
            string exponent = "";
            if (i != result.Count - 1)
            {
                exponent = "X^" + degreeOfPolynomial--.ToString();
            }
            else
            {
                exponent = "X";
            }

            if (result[i] != 0)
            {
                polinom += coefficientSign + result[i].ToString() + exponent;
            }
        }
        polinomResultText.text = polinom + " + C";
    }

    public void Value()
    {
        if (int.TryParse(inputFieldXValue.text, out int x))
        {
            float y = 0;

            for (int i = 0; i < coefficientsValue.Length; i++)
            {
                y += coefficientsValue[i] * Mathf.Pow(x, coefficientsValue.Length - 1 - i);
            }
            polinomValueText.text = y.ToString();
        }
    }

    public void OpenGraph()
    {
        graphPanel.SetActive(true);
        if (coefficientsValue.Length != 1)
        {
            graph.DrawGraph(coefficientsValue);
        }
    }
}

