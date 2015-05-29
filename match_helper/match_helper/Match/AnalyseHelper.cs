using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 
    class AnalyseHelper
    {
        public static  bool  is_same_name(string input1,string input2)
        {

            ArrayList list = new ArrayList();
            ArrayList list1=new ArrayList();
            ArrayList list2=new ArrayList();
           
            
            string name1 = input1.ToLower().E_REMOVE();
            string name2 = input2.ToLower().E_REMOVE();

           
            string[] list_temp1 = name1.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            string[] list_temp2 = name2.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in list_temp1)
            {
                if (item != "(" && item != ")" && item != "fc") 
                { list1.Add(item); }
            }
            foreach (string item in list_temp2)
            {
                if (item != "(" && item != ")" && item != "fc") 
                { list2.Add(item); }
            } 

            for (int i = 0; i < list1.Count; i++)
            {
                list1[i] = list1[i].ToString().TrimStart().TrimEnd();
            }
            for (int i = 0; i < list2.Count; i++)
            {
                list2[i] = list2[i].ToString().TrimStart().TrimEnd();
            }

            int max = 0;
            int l_min = 0;
            if (list1.Count < list2.Count) { l_min = list1.Count; } else { l_min = list2.Count; }

            int count_all = 0;
            for (int i = 0; i < list1.Count; i++)
            {
                for (int j = 0; j < list2.Count; j++)
                {
                    if (list1[i].ToString() == list2[j].ToString()) count_all = count_all + 1;
                }
            }

            
            int count_contain = 0;
            for (int i = 0; i < list1.Count; i++)
            {
                for (int j = 0; j < list2.Count; j++)
                {
                    if (list1[i].ToString().Contains(list2[j].ToString()) || list2[j].ToString().Contains(list1[i].ToString())) count_contain = count_contain + 1;
                }
            }
            int count_alike = 0;
            for (int i = 0; i < list1.Count; i++)
            {
                for (int j = 0; j < list2.Count; j++)
                {
                    if (is_alike_word(list1[i].ToString(),list2[j].ToString())) count_alike = count_alike + 1;
                }
            }
            if (count_all > max) max = count_all;
            if (count_contain > max) max = count_all;
            if (count_alike > max) max = count_alike;

             
            if (max >= 2) return true;
            if ((max * 1.000) / (l_min * 1.000) > 0.5) return true;
            return false;
        }
        public static bool  is_alike_word(string word1, string word2)
        {
            int l1 = word1.Length;
            int l2 = word2.Length;
            int l_min = 0;
            if (l1 < l2) { l_min = l1; } else { l_min = l2; }

            int max = 0;
            string target="";
            for (int i = 0; i < l1; i++)
            {
                for (int j = i; j < l1 + 1 - i; j++)
                {
                    string temp = word1.Substring(i, j);
                    if (word2.Contains(temp))
                    {
                        if (temp.Length > max)
                        {
                            max = temp.Length;
                            target = temp;
                        } 
                    }

                }
            }

            for (int i = 0; i < l2; i++)
            {
                for (int j = i; j < l2 + 1 - i; j++)
                {
                    string temp = word2.Substring(i, j);
                    if (word1.Contains(temp))
                    {
                        if (temp.Length > max)
                        {
                            max = temp.Length;
                            target = temp;
                        }
                    }

                }
            }
            if (max >= 6) return true;
            if ((max * 1.000) / (l_min * 1.000) > 0.75) return true;
            return false;

        }

    }
 