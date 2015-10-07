#r "Program.dll"

open System
open Assignment1


Console.WriteLine("Enter the radius of the cylinder: ")
let value1 = Console.ReadLine()
let radius = float value1
Console.WriteLine("Enter the height of the cylinder: ")
let value2 = Console.ReadLine()
let height = float value2
let result = cylinderVolume radius height
printf "%f" result
Console.ReadLine() |> ignore
0
