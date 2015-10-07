#r "Assignment2Part1.dll"

open System
open Assignment2Part1

Console.Write("insert the value:")
let input = Console.ReadLine()
let inputC = int input
let calc = dosomethingrandom inputC
printf "%i" calc
Console.ReadLine()
