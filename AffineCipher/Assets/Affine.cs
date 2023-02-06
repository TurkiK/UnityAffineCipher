using UnityEngine;
using TMPro;
using System;

public class Affine : MonoBehaviour
{
    private char[] characters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
    'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    private int keyA, keyB;
    public TMP_Text inverseKey;
    public TMP_InputField input, output, inputA, inputB;
    public TMP_Dropdown selection;

    public void convertion()
    {
        if (input.text != "" && inputA.text != "" && inputB.text != "")
        {
            keyA = Int32.Parse(inputA.text);
            keyB = Int32.Parse(inputB.text);
            if (FindGCD() == 1)
            {
                input.text = input.text.ToUpper();
                if (selection.value == 0)
                    Encrypt();
                else
                    Decrypt();
            }
            else {
                output.text = "Something went wrong.\nGCD(A, M) must equal 1 (M is 26)";
            }
        }
        else
            output.text = "Something went wrong.\n1) Text must not be empty!\n2) Keys must not be empty!";
    }

    private int FindGCD()
    {
        int a = keyA, b = 26;
        while (a != 0 && b != 0){
            if (a == 1 || b == 1)
                return (int)MathF.Min(a, b);

            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return -1;
    }

    private void Decrypt()
    {
        output.text = "";

        int x = 0;

        for (int i = 0; i < input.text.Length; i++){
            if (input.text[i] >= 'A' && input.text[i] <= 'Z'){
                x = input.text[i] - 'A';
                int ans = (keyA * (x - keyB)) % 26;
                if(ans < 0)
                    output.text += characters[ans+26];
                else
                    output.text += characters[ans];
            }
            else
                output.text += input.text[i];
        }
        inverseKey.text = "";
    }

    private int findInverse()
    {
        for( int i = 1; i <= 26; i++){
            if (((keyA % 26) * (i % 26)) % 26 == 1)
                return i;
        }
        return -1;
    }

    private void Encrypt()
    {
        output.text = "";

        int x = 0;

        for (int i = 0; i < input.text.Length; i++){
            if (input.text[i] >= 'A' && input.text[i] <= 'Z') {
                x = input.text[i] - 'A';
                output.text += characters[((keyA * x + keyB) % 26)];
            }
            else
                output.text += input.text[i];
        }

        inverseKey.text = "Decryption key \"A\": \n"+findInverse();
    }
}