open System
open System.IO

type Target =
    {
        x : float
        y : float
        velocity : float
        expectedDistance : float
        name : String
    }

let G = 9.81

let AngleOfReach (tgt: Target) : float = 0.5 * Math.Asin(G * tgt.expectedDistance / Math.Pow(tgt.velocity, 2.0))

let CalculateAngle (tgt: Target) : float = Math.Atan(tgt.y / tgt.x)

let DistanceTravelled (tgt: Target) : float = Math.Pow(tgt.velocity, 2.0) * Math.Sin(2.0 * (CalculateAngle tgt))/G

let MaximumDistance (tgt: Target) : float = Math.Pow(tgt.velocity, 2.0) / G

let RadiansToDegrees (r: float) : float = r * 180.0 / Math.PI

let GetFile : String = 
    Console.Write("Enter the input file name: ")
    Console.ReadLine();

let (|Hit|CannotHit|Missed|) tgt = if (DistanceTravelled tgt) = tgt.expectedDistance then Hit elif Double.IsNaN(AngleOfReach tgt) then CannotHit else Missed

[<EntryPoint>]
let main argv = 
    try
        use sr = 
            new StreamReader(match argv.Length with
                            | 0 -> GetFile
                            | _ -> argv.[0])

        let targets = [
            while not sr.EndOfStream do
                let t = sr.ReadLine()
                let vals = t.Split(',')
                yield {x = float vals.[0];
                       y = float vals.[1];
                       velocity = float vals.[2];
                       expectedDistance = float vals.[3];
                       name = vals.[4];
                }
        ]

        List.iter (fun t -> 
                        match t with
                        | Hit -> printfn "%A HIT its target" t.name
                        | CannotHit -> printfn "%A CANNOT REACH its target with velocity %A. It is trying to hit a target at range %A, but can only reach as far as %A." t.name t.velocity t.expectedDistance (MaximumDistance t)
                        | Missed -> printfn "%A MISSED its target. It would require an angle of %A radians (%A degrees) to hit its target" t.name (AngleOfReach t) (RadiansToDegrees (AngleOfReach t))
        ) targets

        Console.ReadKey() |> ignore
        0
    with
    | :? FileNotFoundException -> 
        Console.WriteLine("File not found! Press any key to continue.")
        Console.ReadKey() |> ignore
        -1
    | _ ->
        Console.WriteLine("Unhandled, fatal exception occurred! Press any key to continue.")
        Console.ReadKey() |> ignore
        -1
