using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PolynomialOperations : MonoBehaviour
{
    [SerializeField]
    private PolynomialResult polynomialResult = null;
    [SerializeField]
    private Text polinomText = null;
    [SerializeField]
    private Polinom polinom1 = null;
    [SerializeField]
    private Polinom polinom2 = null;
    private int[] coefficientsValuePolynom1;
    private int[] coefficientsValuePolynom2;
    private int[] coefficientsValuePolynomResult;
    private int degreeOfPolynomial = 0;

    //Polynomial Addition of two polynomials
    public void Addition()
    {
        int maxLenght = GetPolynomResultLength();
        int coefficientsValuePolynom1Length = coefficientsValuePolynom1.Length - 1;
        int coefficientsValuePolynom2Length = coefficientsValuePolynom2.Length - 1;
        for (int i = maxLenght - 1; i >= 0; i--)
        {
            int term1 = coefficientsValuePolynom1Length >= 0 ? coefficientsValuePolynom1[coefficientsValuePolynom1Length--] : 0;
            int term2 = coefficientsValuePolynom2Length >= 0 ? coefficientsValuePolynom2[coefficientsValuePolynom2Length--] : 0;
            coefficientsValuePolynomResult[i] = term1 + term2;
        }

        SetTextFormOfPolynom();
    }

    //Polynomial Substraction of two polynomials
    public void Substraction()
    {
        int maxLenght = GetPolynomResultLength();
        int coefficientsValuePolynom1Length = coefficientsValuePolynom1.Length - 1;
        int coefficientsValuePolynom2Length = coefficientsValuePolynom2.Length - 1;
        for (int i = maxLenght - 1; i >= 0; i--)
        {
            int term1 = coefficientsValuePolynom1Length >= 0 ? coefficientsValuePolynom1[coefficientsValuePolynom1Length--] : 0;
            int term2 = coefficientsValuePolynom2Length >= 0 ? coefficientsValuePolynom2[coefficientsValuePolynom2Length--] : 0;
            coefficientsValuePolynomResult[i] = term1 - term2;
        }

        SetTextFormOfPolynom();
    }

    private int GetPolynomResultLength()
    {
        coefficientsValuePolynom1 = polinom1.coefficientsValue;
        coefficientsValuePolynom2 = polinom2.coefficientsValue;
        int maxLenght = Mathf.Max(coefficientsValuePolynom1.Length, coefficientsValuePolynom2.Length);
        degreeOfPolynomial = maxLenght - 1;
        coefficientsValuePolynomResult = new int[maxLenght];
        return maxLenght;
    }

    //Polynomial Multiplication of two polynomials
    public void Multiplication()
    {
        coefficientsValuePolynom1 = polinom1.coefficientsValue;
        coefficientsValuePolynom2 = polinom2.coefficientsValue;
        int coefficientsValuePolynom1Length = coefficientsValuePolynom1.Length - 1;
        int coefficientsValuePolynom2Length = coefficientsValuePolynom2.Length - 1;

        Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();

        for (int i = 0; i < coefficientsValuePolynom1.Length; i++)
        {
            for (int j = 0; j < coefficientsValuePolynom2.Length; j++)
            {
                int exponent = coefficientsValuePolynom1Length - i + coefficientsValuePolynom2Length - j;
                int distributedTerm = coefficientsValuePolynom1[i] * coefficientsValuePolynom2[j];
                if (!dictionary.ContainsKey(exponent))
                {
                    dictionary.Add(exponent, new List<int> { distributedTerm });
                }
                else
                {
                    dictionary[exponent].Add(distributedTerm);
                }
            }
        }

        coefficientsValuePolynomResult = new int[dictionary.Count];
        for (int i = 0; i < dictionary.Count; i++)
        {
            coefficientsValuePolynomResult[i] = dictionary[dictionary.Count - 1 - i].Sum();
        }
        SetTextFormOfPolynom();
    }

    //Polynomial Division of two polynomials
    public void Division()
    {
        List<int> numerator = polinom1.coefficientsValue.ToList();
        List<int> denominator = polinom2.coefficientsValue.ToList();

        List<int> answer = new List<int>();
        Dictionary<int, int> dictionaryResult = new Dictionary<int, int>();

        int countObtainedNewPolynomial = 0;
        ObtainNewPolynomial(countObtainedNewPolynomial, numerator, denominator, answer, dictionaryResult);
    }

    private void ObtainNewPolynomial(int indexOfFirstTermNumerator,List<int> numerator, List<int> denominator, List<int> newPolynomial, Dictionary<int, int> dictionaryAnswer)
    {
        if (numerator.Count >= denominator.Count)
        {
            //Divide the first term of the numerator by the first term of the denominator, and put that in the answer

            int resultTerm = denominator[0] != 0 
                            ? numerator[indexOfFirstTermNumerator] / denominator[0] : 0;
            int exponent = numerator.Count - indexOfFirstTermNumerator - denominator.Count;

            if (!dictionaryAnswer.ContainsKey(exponent))
            {
                dictionaryAnswer.Add(exponent, resultTerm);
            }

            newPolynomial = new List<int>();
            //Multiply the denominator by that answer
            for (int j = 0; j < denominator.Count; j++)
            {
                int newPolynomialCoefficient = resultTerm * denominator[j];
                newPolynomial.Add(newPolynomialCoefficient);
                print(newPolynomial[j]);
            }

            //Subtract to create a new polynomial
            for (int i = indexOfFirstTermNumerator; i < numerator.Count; i++)
            {
                numerator[i] =  i < newPolynomial.Count ? numerator[i] - newPolynomial[i] : numerator[i];
            }
            
            if (indexOfFirstTermNumerator < numerator.Count - 1)
            {
                indexOfFirstTermNumerator++;
                ObtainNewPolynomial(indexOfFirstTermNumerator, numerator, denominator, new List<int>(), dictionaryAnswer);
            }
            else
            {
                List<int> answerList = new List<int>();
                for (int i = 0; i < dictionaryAnswer.Count; i++)
                {
                    if (dictionaryAnswer.ContainsKey(i))
                    {
                        answerList.Add(dictionaryAnswer[i]);

                    }
                    else
                    {
                        answerList.Add(0);
                    }
                }

                coefficientsValuePolynomResult = new int[answerList.Count];

                for (int i = 0; i < answerList.Count; i++)
                {
                    coefficientsValuePolynomResult[i] = answerList[answerList.Count - 1 - i];
                }
                SetTextFormOfPolynom();

                coefficientsValuePolynomResult = new int[newPolynomial.Count];
                for (int i = 0; i < newPolynomial.Count; i++)
                {
                    coefficientsValuePolynomResult[i] = newPolynomial[i];
                    print(coefficientsValuePolynomResult[i]);
                }
                SetTextFormOfPolynom(coefficientsValuePolynomResult);
            }
        }
    }

    //Check if 2 polynomials are equals
    public void Equality()
    {
        coefficientsValuePolynom1 = polinom1.coefficientsValue;
        coefficientsValuePolynom2 = polinom2.coefficientsValue;
        List<int> coefficientsValuePolynom1List = coefficientsValuePolynom1.ToList();
        List<int> coefficientsValuePolynom2List = coefficientsValuePolynom2.ToList();

        for (int i = 0; i < coefficientsValuePolynom1List.Count; i++)
        {
            if (coefficientsValuePolynom1List[0] == 0)
            {
                coefficientsValuePolynom1List.RemoveAt(0);
            }
            else
            {
                break;
            }
        }

        for (int i = 0; i < coefficientsValuePolynom2List.Count; i++)
        {
            if (coefficientsValuePolynom2List[0] == 0)
            {
                coefficientsValuePolynom2List.RemoveAt(0);
            }
            else
            {
                break;
            }
        }
        bool isEqual = coefficientsValuePolynom1List.SequenceEqual(coefficientsValuePolynom2List);
        polinomText.text = isEqual ? "Polinom1 = Polinom2" : "Polinom1 != Polinom2";
    }

    private void SetTextFormOfPolynom()
    {
        degreeOfPolynomial = coefficientsValuePolynomResult.Length - 1;
        string polinom = "";
        for (int i = 0; i < coefficientsValuePolynomResult.Length; i++)
        {
            string coefficientSign = "";
            if (i > 0 && coefficientsValuePolynomResult[i] > 0)
            {
                coefficientSign = "+";
            }
            string exponent = "";
            if (i != coefficientsValuePolynomResult.Length - 1)
            {
                exponent = "X^" + degreeOfPolynomial--.ToString();
            }
            if (coefficientsValuePolynomResult[i] != 0)
            {
                polinom += coefficientSign + coefficientsValuePolynomResult[i].ToString() + exponent;
            }
            if (coefficientsValuePolynomResult[i] == 0 && coefficientsValuePolynomResult.Length - 1 == 1)
            {
                polinom = coefficientsValuePolynomResult[i].ToString();
            }
        }
        polinomText.text = polinom;
        polynomialResult.coefficientsValue = new int[coefficientsValuePolynomResult.Length];
        polynomialResult.coefficientsValue = coefficientsValuePolynomResult;
    }

    private void SetTextFormOfPolynom(int[] coefficientsValuePolynom)
    {
        degreeOfPolynomial = coefficientsValuePolynom.Length - 1;
        string polinom = "";
        for (int i = 0; i < coefficientsValuePolynom.Length; i++)
        {
            string coefficientSign = "";
            if (i > 0 && coefficientsValuePolynom[i] > 0)
            {
                coefficientSign = "+";
            }
            string exponent = "";
            if (i != coefficientsValuePolynom.Length - 1)
            {
                exponent = "X^" + degreeOfPolynomial--.ToString();
            }
            if (coefficientsValuePolynom[i] != 0)
            {
                polinom += coefficientSign + coefficientsValuePolynom[i].ToString() + exponent;
            }
        }
        polinomText.text += "\nRest" + polinom;
    }
}
