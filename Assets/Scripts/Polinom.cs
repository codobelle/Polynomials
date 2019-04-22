using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Polinom : MonoBehaviour
{
    [SerializeField]
    private PolynomialResult polynomialResult = null;
    [HideInInspector]
    public int[] coefficientsValue;
    [SerializeField]
    private GameObject graphPanel = null;
    [SerializeField]
    private Graph graph = null;
    [SerializeField]
    private InputField inputFieldCoefficients = null;
    [SerializeField]
    private Text polinomText = null;
    [SerializeField]
    private Text polinomValueText = null;
    [SerializeField]
    private InputField inputFieldXValue = null;
    [SerializeField]
    private Text polinomResultText = null;
    private int degreeOfPolynomial = 0;
    private List<int> result = new List<int>();

    //Called functions on Button Press

    //Show polynomial in Math form
    public void ShowPolinom()
    {
        if (!string.IsNullOrEmpty(inputFieldCoefficients.text))
        {
            string[] coefficients = inputFieldCoefficients.text.Split(' ');

            coefficientsValue = new int[coefficients.Length];
            degreeOfPolynomial = coefficientsValue.Length - 1;

            for (int i = 0; i < coefficients.Length; i++)
            {
                coefficientsValue[i] = int.Parse(coefficients[i]);
            }

            string polinom = "";

            for (int i = 0; i < coefficientsValue.Length; i++)
            {
                string coefficientSign = "";
                if (i > 0 && coefficientsValue[i] > 0)
                {
                    coefficientSign = "+";
                }
                string exponent = "";
                if (i != coefficientsValue.Length - 1)
                {
                    exponent = "X^" + degreeOfPolynomial--.ToString();
                }
                if (coefficientsValue[i] != 0)
                {
                    polinom += coefficientSign + coefficientsValue[i].ToString() + exponent;
                }
            }
            polinomText.text = polinom;
        }
    }

    //Derivative Of Polynomial
    public void DerivativeOfPolynomial()
    {
        List<float> result = new List<float>();
        for (int i = 0; i < coefficientsValue.Length; i++)
        {
            result.Add(coefficientsValue[i] * (coefficientsValue.Length - 1 - i));
            print(coefficientsValue.Length - 1 - i);
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
        polynomialResult.coefficientsValueDerivative = new float[result.Count];
        polynomialResult.coefficientsValueDerivative = result.ToArray();
    }

    //Integration of polynomial
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
        if (!string.IsNullOrEmpty(polinom))
        {
            polinomResultText.text = polinom + " + C";
        }
        polynomialResult.coefficientsValueDerivative = new float[result.Count];
        polynomialResult.coefficientsValueDerivative = result.ToArray();
    }

    // Calculates polynomial value on given value
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
    // Opens panel with graph
    public void OpenGraph()
    {
        graphPanel.SetActive(true);
        if (coefficientsValue.Length != 1)
        {
            graph.DrawGraph(coefficientsValue);
        }
    }

}
