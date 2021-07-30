using ChefRisingStar.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ChefRisingStar.Helpers
{
    public class SubstitutionHelper
    {
        private static List<SubstituteIngredient[]> NoSubstitutes;

        private static List<string> measureTerms = new List<string> { "cup", "tbsp", "tsp", "oz", "g", "gr", "gram" };
        public static SubstituteIngredient[] ParseSubstitution(string substituion)
        {
            if (string.IsNullOrEmpty(substituion) || string.IsNullOrWhiteSpace(substituion))
                return null;

            string[] split = substituion.Split('=');
            split[0] = split[0].Trim();
            split[1] = split[1].Trim();

            if (split[1].Contains(" and ") || split[1].Contains(" + "))
            {
                return ProcessMultiSubstitution(split[0], split[1]);
            }

            var sub = ProcessSingleSubstitution(split[0], split[1]);
            SubstituteIngredient[] substitutes = new SubstituteIngredient[] { sub };

            return substitutes;
        }

        public static List<SubstituteIngredient[]> GetNoSubstituteItem()
        {
            if (NoSubstitutes == null)
            {
                SubstituteIngredient sub = new SubstituteIngredient { Amount = string.Empty, Unit = string.Empty, Name = "Sorry no substitute found for this ingredient" };
                NoSubstitutes = new List<SubstituteIngredient[]> { new SubstituteIngredient[] { sub } };
            }

            return NoSubstitutes;
        }

        public static string StringFormat(SubstituteIngredient[] substitutes)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;

            foreach (SubstituteIngredient sub in substitutes)
            {
                if (first)
                {
                    sb.Append(sub);
                    first = false;
                }
                else
                {
                    sb.Append($" and {sub}");
                }
            }

            return sb.ToString();
        }

        private static SubstituteIngredient ProcessSingleSubstitution(string conversion, string substituion)
        {
            string[] split = substituion.Split(' ');

            string amount = null;
            string unit = null;

            StringBuilder ingredientSb = new StringBuilder();

            try
            {
                int foundIndex = 0;

                for (; foundIndex <= split.Length; foundIndex++)
                {
                    if (measureTerms.Contains(split[foundIndex]))
                        break;
                }

                if (foundIndex > 0)
                    amount = split[foundIndex - 1];

                unit = split[foundIndex];

                bool first = true;

                for (int j = foundIndex + 1; j <= split.Length - foundIndex; j++)
                {
                    if (first)
                    {
                        ingredientSb.Append(split[j]);
                        first = false;
                    }
                    else
                    {
                        ingredientSb.Append(" " + split[j]);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error ProcessSingleSubstitution: {ex}");
            }

            SubstituteIngredient sub = new SubstituteIngredient { Conversion = conversion, Amount = amount, Unit = unit, Name = ingredientSb.ToString() };

            return sub;
        }

        private static SubstituteIngredient[] ProcessMultiSubstitution(string conversion, string multiSubstituion)
        {
            string[] split = multiSubstituion.Split('+');

            if (split.Length <= 1)
                split = multiSubstituion.Split(new[] { "and" }, StringSplitOptions.RemoveEmptyEntries);

            //Clean up whitespace
            for (int i = 0; i < split.Length; i++)
            {
                split[i] = split[i].Trim();
            }

            string result = null;

            List<SubstituteIngredient> substitutes = new List<SubstituteIngredient>();

            foreach (string s in split)
            {
                substitutes.Add(ProcessSingleSubstitution(conversion, s));
            }

            return substitutes.ToArray();
        }
    }
}
