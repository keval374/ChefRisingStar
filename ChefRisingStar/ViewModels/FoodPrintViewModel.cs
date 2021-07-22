using ChefRisingStar.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    public class FoodPrintViewModel : BaseViewModel
    {
        public FoodPrintCategory categories { get; }
        public FoodPrint foodprintData = new FoodPrint();
        public AnnualFoodPrint annualfoodprintData = new AnnualFoodPrint();
        public OtherFoodPrint otherfoodprintData = new OtherFoodPrint();
        public List<object> foodprintresultsdata = new List<object>();
        public List<object> otherfoodprintresultsdata = new List<object>();

        public List<FoodPrint> foodPrint
        {
            get => foodPrint;
            set
            {
                if (foodPrint == value)
                    return;

            var response = File.ReadAllText("sample/foodprint.json");
            foodPrint = JsonConvert.DeserializeObject<List<FoodPrint>>(response);
            }
        }

        public List<AnnualFoodPrint> annualfoodPrint
        {
            get => annualfoodPrint;
            set
            {
                if (annualfoodPrint == value)
                    return;

                var response = File.ReadAllText("sample/annual_foodprint.json");
                annualfoodPrint = JsonConvert.DeserializeObject<List<AnnualFoodPrint>>(response);
            }
        }

        public List<OtherFoodPrint> otherFoodPrints
        {
            get => otherFoodPrints;
            set
            {
                if (otherFoodPrints == value)
                    return;

                var response = File.ReadAllText("sample/co2.json");
                otherFoodPrints = JsonConvert.DeserializeObject<List<OtherFoodPrint>>(response);
            }
        }

        public ExtendedIngredient[] extendedIngredients
        {
            get => extendedIngredients;
            set
            {
                if (extendedIngredients == value)
                    return;

                extendedIngredients = value;
                OnPropertyChanged();
            }
        }

        public FoodPrintViewModel(Recipe recipe)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            extendedIngredients = recipe.ExtendedIngredients;
            try
            {
                foreach (ExtendedIngredient ingredient in extendedIngredients)
                {
                    var isExist = checkCategories(ingredient);
                    if (isExist != "")
                    {
                        foodprintresultsdata.Add(foodPrint.Find(item => item.Ingredient == ingredient.Name));
                        foodprintresultsdata.Add(annualfoodPrint.Find(item => item.Ingredients.Contains(ingredient.Name)));

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get recipes: {ex.Message}");
                //await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }           

        


        public string checkCategories(ExtendedIngredient ingredient)
        {
            var exist = "";
                foreach (var beans in categories.Bean)
                {
                    if (ingredient.Name.Contains(beans))
                    {
                        return beans;
                    }
                }
                foreach (var item in categories.Cheese)
                {
                    if (ingredient.Name.Contains(item))
                    {
                        return item;
                    }
                }
                foreach (var item in categories.CitrusFruits)
                {
                    if (ingredient.Name.Contains(item))
                    {
                        return item;
                    }
                }
                foreach (var item in categories.Dairy)
                {
                    if (ingredient.Name.Contains(item))
                    {
                        return item;
                    }
                }
                foreach (var item in categories.Drink)
                {
                    if (ingredient.Name.Contains(item))
                    {
                        return item;
                    }
                }
                foreach (var item in categories.Fish)
                {
                    if (ingredient.Name.Contains(item))
                    {
                        return item;
                    }
                }
                foreach (var item in categories.Meat)
                {
                    if (ingredient.Name.Contains(item))
                    {
                        return item;
                    }
                }
                foreach (var item in categories.Nuts)
                {
                    if (ingredient.Name.Contains(item))
                    {
                        return item;
                    }
                }
                foreach (var item in categories.Pasta)
                {
                    if (ingredient.Name.Contains(item))
                    {
                        return item;
                    }
                }
                foreach (var item in categories.Poultry)
                {
                    if (ingredient.Name.Contains(item))
                    {
                        return item;
                    }
                }
                foreach (var item in categories.Seafood)
                {
                    if (ingredient.Name.Contains(item))
                    {
                        return item;
                    }
                }
            
            return exist;
        }

    }

}
