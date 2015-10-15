(* Assignment 3 Part 2  *)
module Assignment

    open System
    (*
     * I based this on the reply of 13xforever in the discussion forum
     * This assignment is a little nonsense.
     *)

    let goldenRatio = (1.0 + Math.Sqrt(5.0))/2.0

    [<EntryPoint>]
    let main argv =
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
