module assignmentProblem =
(*
 * Assignment Problem on Pattern Matching
 * Staring from the Hands on practice idea
*)
    open System
    open System.IO

    (*(Target x coordinate, Target y coordinate, speed, expected distance, name)*)
    type Entity =
        { X : int
          Y : int
          Speed : int
          distanceExprected: int
          name : String }

    let calculateAngleOfReach gravity distance speed = 0.5 * Math.Asin(((gravity * distance)/Math.Pow(speed,2.0)))

    // let calculateDistanceTraveled  = () / 

    let GetFile =
        Console.Write("Enter the full path to the name of the input file: ")
        Console.ReadLine()

    [<EntryPoint>]
    let main argv =
        try
            let filename =
                match argv.Length with
                | 0 -> GetFile
                | _ -> argv.[0]

            let input = new StreamReader(filename)

            let entities =
                [ while not input.EndOfStream do
                      let raw = input.ReadLine()
                      let values = raw.Split(',')
                      yield { X = int values.[0]
                              Y = int values.[1]
                              Speed = int values.[2]
                              distanceExprected = int values.[3]
                              name = values.[4] } ]
                                         
            Console.ReadKey() |> ignore
            0
        with
        | :? System.IO.FileNotFoundException ->
            Console.Write("File Not Found. Press a key to exit")
            Console.ReadKey() |> ignore
            -1
        | _ ->
            Console.Write("Something else happened")
            Console.ReadKey() |> ignore
            -1
