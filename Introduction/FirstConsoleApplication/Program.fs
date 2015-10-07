(*
 * First console application
 * Not actually my real first f# application fortunatly. :)
 *)

open System

let rectangleArea width height = width * height

let cubeVolume x y z = (rectangleArea x  y) * z

[<EntryPoint>]
let main argv =
    Console.Write("Please enter the width: ")
    let width = Console.ReadLine();
    let width = int width
    Console.Write("Please enter the height: ")
    let height = Console.ReadLine();
    let height = int height
    Console.Write("Please enter the depth: ")
    let depth = Console.ReadLine();
    let depth = int depth

    let cubeVolumeResult = cubeVolume width height depth

    printfn "%A" cubeVolumeResult
    Console.Read() |> ignore
    0
