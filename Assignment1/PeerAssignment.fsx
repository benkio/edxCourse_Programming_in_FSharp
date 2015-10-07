open System

let readFloat () = Console.ReadLine () |> float
printfn "Radius:"
let radius = readFloat ()
printfn "Height:"
let height = readFloat ()
let volume = 2.0 * 3.14 * radius * radius * height
printfn "%A" volume
Console.ReadLine ()
0 // return an integer exit code
