using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public static class Constants
{
    public static class Cost
    {
        public const int tree = 10;
        public const int dino = 10;
        public const int chomp = 50;
        public const int finn = 50;
        public const int gramophone = 50;
        public const int witch = 50;
        public const int fountain = 100;

        public static int GetCost(string name){
            switch(name){
                case "Tree":
                    return tree;
                case "Dino":
                    return dino;
                case "Chomp":
                    return chomp;
                case "Finn":
                    return finn;
                case "Gramophone":
                    return gramophone;
                case "Witch":
                    return witch;
                case "Water Fountain":
                    return fountain;
            }
            return -1;
        }
    }

    public static class Value
    {
        public const int tree = 5;
        public const int dino = 5;
        public const int fruit = 5;
        public const int chomp = 5;
        public const int finn = 10, gramophone = 10, witch = 10, fountain = 10;
        public const int gem = 15;
        public const int basicGem = 5;

        public static int GetValue(string name){
            switch(name){
                case "Tree(Clone)":
                    return tree;
                case "Dino":
                    return dino;
                case "Chomp(Clone)":
                    return chomp;
                case "Finn(Clone)":
                    return finn;
                case "Gramophone(Clone)":
                    return gramophone;
                case "Witch(Clone)":
                    return witch;
                case "WaterFountain(Clone)":
                    return fountain;
                case "Gem":
                    return gem;
                case "BasicGem(Clone)":
                    return basicGem;
            }
            return -1;
        }
    }

    public static class Cooldown
    {
        public const float tree = 10f;
        public const float dino = 5f;
        public const float witch = 10f;
        public const float fountain = 5f;
    }

    public static class Description
    {
        public const string tree = "Shake it!";
        public const string dino = "Nom nom nom nom nom...";
        public const string chomp = "Auto harvest crops";
        public const string finn = "What time is it? Adventure time!";
        public const string gramophone = "Music?!";
        public const string witch = "All the fish belong to me!";
        public const string fountain = "Make a wish and see your dreams come true!";

        public static string GetDescription(string name){
            switch(name){
                case "Tree":
                    return tree;
                case "Dino":
                    return dino;
                case "Chomp":
                    return chomp;
                case "Finn":
                    return finn;
                case "Gramophone":
                    return gramophone;
                case "Witch":
                    return witch;
                case "Water Fountain":
                    return fountain;
            }
            return "";
        }
    }

    public static class Hint{
        public const string tree = "shake me";
        public const string dino = "feed me";
        public const string fruit = "eatable";
        public const string basicGem = "so f**king dirty";
        public const string gem = "blink blink";
        public const string chomp = "i love flower";
        public const string finn = "i'm hungry";
        public const string flower = "flowererererer";
        public const string blueFlame = "&^#@&#*1&^()";
        public const string disc = "mysterious disc";
        public const string gramophone = "you wanna play music?";
        public const string witch = "two gems and a blue flame";
        public const string fountain = "make the gem shiny";

        public static string GetHint(string name)
        {
            switch (name)
            {
                case "Dino":
                    return dino;
                case "Tree(Clone)":
                    return tree;
                case "Fruit(Clone)":
                    return fruit;
                case "basic gem(Clone)":
                    return basicGem;
                case "Gem":
                    return gem;
                case "Chomp(Clone)":
                    return chomp;
                case "Finn(Clone)":
                    return finn;
                case "PinkFlower(Clone)":
                    return flower;
                case "flame(Clone)":
                    return blueFlame;
                case "Disc":
                    return disc;
                case "Witch(Clone)":
                    return witch;
                case "Gramophone(Clone)":
                    return gramophone;
                case "WaterFountain(Clone)":
                    return fountain;

            }
            return "";
        }
    }
}
