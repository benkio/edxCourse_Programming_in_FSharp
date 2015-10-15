#load "Assignment.fs"

open System
open Assignment

let inputValues =
  [ let mutable run = true
    while run do
        Console.WriteLine("Do you want to insert more values (y/n)? ")
        if Console.ReadLine().ToLower() = "y" then
            Console.WriteLine("Enter another value: ")
            let newValue = float (Console.ReadLine())
            yield (newValue, newValue*goldenRatio)
        else run <- false ]
for i=0 to inputValues.Length - 1 do
    printfn "A tuple: %A" inputValues.[i]
0
