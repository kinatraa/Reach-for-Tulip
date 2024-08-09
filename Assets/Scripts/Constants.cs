using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public static class Constants
{
    public static class Cost
    {
        public const int tree = 10;
        public const int dino = 15;
        public const int chomp = 50;
        public const int finn = 20;
        public const int gramophone = 50;
        public const int witch = 80;
        public const int fountain = 70;
        public const int tulip = 200;
        public const int magnet = 100;

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
                case "Tulip":
                    return tulip;
                case "Magnet":
                    return magnet;
            }
            return -1;
        }
    }

    public static class Value
    {
        public const int tree = 7;
        public const int dino = 10;
        public const int fruit = 3;
        public const int chomp = 35;
        public const int finn = 15, gramophone = 35, witch = 65, fountain = 55;
        public const int gem = 10;
        public const int basicGem = 5;
        public const int flower = 4, flame = 4, disc = 10;
        public const int magnet = 75;
        public const int orb = 35;
        public const int tulip = 30, bigTulip = 350;

        public static int GetValue(GameObject obj){
            if (obj.CompareTag("Dino"))
            {
                return dino;
            }
            else if (obj.CompareTag("Gem"))
            {
                return gem;
            }
            else if (obj.CompareTag("Disc"))
            {
                return disc;
            }
            else if (obj.CompareTag("BasicGem"))
            {
                return basicGem;
            }
            string name = obj.name;
            switch(name){
                case "Fruit(Clone)":
                    return fruit;
                case "Tree(Clone)":
                    return tree;
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
                case "PinkFlower(Clone)":
                    return flower;
                case "flame(Clone)":
                    return flame;
                case "Magnet(Clone)":
                    return magnet;
                case "Love Orb(Clone)":
                    return orb;
            }
            return -1;
        }
    }

    public static class Cooldown
    {
        public const float tree = 10f;
        public static readonly KeyValuePair<float, float> dino = new KeyValuePair<float, float>(5f, 15f);
        public static readonly KeyValuePair<float, float> finn = new KeyValuePair<float, float>(5f, 20f);
        public const float witch = 10f;
        public const float fountain = 5f;
        public const float tulip = 15f;
    }

    public static class Description
    {
        public const string tree = "Shake it!";
        public const string dino = "Nom nom nom nom nom...";
        public const string chomp = "Auto harvest crops";
        public const string finn = "What time is it? Adventure time!";
        public const string gramophone = "Music?! (Only 1)";
        public const string witch = "All the fish belong to me!";
        public const string fountain = "Make a wish and see your dreams come true! (Only 1)";
        public const string tulip = "A seed!";
        public const string magnet = "Attraction is my name!";

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
                case "Magnet Shroom":
                    return magnet;
                case "Tulip":
                    return tulip;
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
        public const string witch = "two gems and a blue matter";
        public const string fountain = "make the gem shiny";
        public const string magnet = "give me something, i can pull it in";
        public const string loveOrb = "love is forever...";
        public static string tulip = "my love!";
        public static string[] tulipHints = { "a seed", "do you love me?", "wait for me", "wait for me", "wait for me", "wait for me", "i love you" };

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
                case "Magnet(Clone)":
                    return magnet;
                case "Tulip(Clone)":
                    return tulip;
                case "Love Orb(Clone)":
                    return loveOrb;
            }
            return "";
        }
    }
}
