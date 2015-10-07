#r "FibonacciLoop.dll"
#r "FibonacciRecursion.dll"

open System
open FibonacciLoop
open FibonacciRecursion

let input = Console.ReadLine()
(*
let input2 = checkvalue [|input|]
let calc = fibonacci input2
Console.WriteLine(calc)
Console.ReadKey() |> ignore
0 // return an integer exit code
*)

let input2 = checkvalue [|input|]
let calc = fibonaccirecursive input2
Console.WriteLine(calc)
Console.ReadKey() |> ignore
0 // return an integer exit code
