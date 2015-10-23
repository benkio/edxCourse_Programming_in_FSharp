module handsOnPractice =
    (*
 * Hands on practice on Pattern Matching
*)
    open System
    open System.IO

    type Entity =
        { X : int
          Y : int
          Name : string }

    let (|Quadrant1|Quadrant2|Quadrant3|Quadrant4|Origin|Undefined|) input =
        match input with
        | { Entity.X = x; Entity.Y = y; Entity.Name = name } when x > 0 && y > 0 -> Quadrant1
        | { Entity.X = x; Entity.Y = y; Entity.Name = name } when x < 0 && y > 0 -> Quadrant2
        | { Entity.X = x; Entity.Y = y; Entity.Name = name } when x < 0 && y < 0 -> Quadrant3
        | { Entity.X = x; Entity.Y = y; Entity.Name = name } when x > 0 && y < 0 -> Quadrant4
        | { Entity.X = x; Entity.Y = y; Entity.Name = name } when x = 0 && y = 0 -> Origin
        | _ -> Undefined

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
                              Name = values.[2] } ]

            let errornames =
                seq {
                    for p in entities ->
                        match p with
                        | Undefined -> Some(p.Name)
                        | _ -> None
                }

            Console.WriteLine("The undefined entries are: ")
            for e in errornames do
                match e with
                | Some i -> Console.WriteLine(i)
                | None -> ()
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
