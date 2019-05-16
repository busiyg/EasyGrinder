using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtility
{
    public static string NumConversion(float num) {
        string str_num = num.ToString("f0");
        int length = str_num.Length;
        if (length < 4)
        {
   
        }
        else {
            if (length < 7)
            {
                UnityEngine.MonoBehaviour.print(length);
                //System.Console.WriteLine(length);
                int point_pos = length - 3;
                str_num = str_num.Substring(0,4);
                str_num = str_num.Insert(point_pos,".");
                str_num = str_num + "K";
            }
            else
            {
                if (length < 10)
                {
                    UnityEngine.MonoBehaviour.print(length);
                    System.Console.WriteLine(length);
                    int point_pos = length - 6;
                    str_num = str_num.Substring(0, 4);
                    str_num = str_num.Insert(point_pos, ".");
                    str_num = str_num + "M";
                }
                else
                {
                    if (length < 13)
                    {
                        UnityEngine.MonoBehaviour.print(length);
                        System.Console.WriteLine(length);
                        int point_pos = length - 9;
                        str_num = str_num.Substring(0, 4);
                        str_num = str_num.Insert(point_pos, ".");
                        str_num = str_num + "B";
                    }
                    else
                    {
                        if (length < 16)
                        {
                            UnityEngine.MonoBehaviour.print(length);
                            System.Console.WriteLine(length);
                            int point_pos = length - 12;
                            str_num = str_num.Substring(0, 4);
                            str_num = str_num.Insert(point_pos, ".");
                            str_num = str_num + "T";
                        }
                        else
                        {
                            if (length < 19)
                            {
                                UnityEngine.MonoBehaviour.print(length);
                                System.Console.WriteLine(length);
                                int point_pos = length - 17;
                                str_num = str_num.Substring(0, 4);
                                str_num = str_num.Insert(point_pos, ".");
                                str_num = str_num + "AA";
                            }
                            else
                            {
                                if (length < 22)
                                {
                                    UnityEngine.MonoBehaviour.print(length);
                                    System.Console.WriteLine(length);
                                    int point_pos = length - 20;
                                    str_num = str_num.Substring(0, 4);
                                    str_num = str_num.Insert(point_pos, ".");
                                    str_num = str_num + "BB";
                                }
                                else
                                {
                                    if (length < 25)
                                    {
                                        UnityEngine.MonoBehaviour.print(length);
                                        System.Console.WriteLine(length);
                                        int point_pos = length - 23;
                                        str_num = str_num.Substring(0, 4);
                                        str_num = str_num.Insert(point_pos, ".");
                                        str_num = str_num + "CC";
                                    }
                                    else
                                    {
                                        if (length < 28)
                                        {
                                            UnityEngine.MonoBehaviour.print(length);
                                            System.Console.WriteLine(length);
                                            int point_pos = length - 26;
                                            str_num = str_num.Substring(0, 4);
                                            str_num = str_num.Insert(point_pos, ".");
                                            str_num = str_num + "DD";
                                        }
                                        else
                                        {
                                            if (length < 31)
                                            {
                                                UnityEngine.MonoBehaviour.print(length);
                                                System.Console.WriteLine(length);
                                                int point_pos = length - 29;
                                                str_num = str_num.Substring(0, 4);
                                                str_num = str_num.Insert(point_pos, ".");
                                                str_num = str_num + "EE";
                                            }
                                            else
                                            {
                                                str_num = "Big Num!!!";
                                            }
                                        }
                                    }

                                }

                            }

                        }
                    }
                }
            }
        }
        return str_num;
    }
}
