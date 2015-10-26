    

    open System
    open System.IO
     
    let gravity = 9.81
     
    let angleOfReach distance speed = 0.5 * Math.Asin ((gravity * distance) / Math.Pow(speed, 2.0))
     
    let distanceTravelled speed theta = (Math.Pow(speed, 2.0) * Math.Sin(2.0 * theta)) / gravity
     
    let calculateAngle x y = Math.Atan(y/x)
     
    let howFar x y speed = distanceTravelled speed (calculateAngle x y)
     
    let willHitExpectedDistance x y speed expectedDistance = ((howFar x y speed) = expectedDistance)
     
    type GunData =
        {
            x : float
            y : float
            speed : float
            expectedDistance : float
            name : string
        }
     
    let (|Hit|Miss|Invalid|) input =
            match input with
            | { x = a:float; y = b:float; speed = c:float; expectedDistance = d:float; name = e:string; } when a > 0.0 && willHitExpectedDistance a b c d -> Hit
            | { x = a:float; y = b:float; speed = c:float; expectedDistance = d:float; name = e:string; } when a > 0.0 && not (willHitExpectedDistance a b c d) -> Miss
            | _ -> Invalid
     
     
    let GetFile =
        Console.Write("Enter the full path to the name of the input file: ")
        Console.ReadLine()
       
    [<EntryPoint>]    
    let main argv =
        try
            use input =
                new StreamReader(match argv.Length with
                                    | 0 -> GetFile    
                                    | _ -> argv.[0])    
     
            let entities =
                [ while not input.EndOfStream do
                        let raw = input.ReadLine()
                        let values = raw.Split(',')
                        yield  {
                                x = float values.[0]
                                y = float values.[1]
                                speed = float values.[2]
                                expectedDistance = float values.[3]                        
                                name = values.[4] } ]
     
            for e in entities do
                match e with
                | Hit -> Console.WriteLine("Gun {0} hit the target!", e.name)
                | Miss ->   if Double.IsNaN(angleOfReach e.expectedDistance e.speed)                      
                            then Console.WriteLine("Gun {0} didn't hit the target. Angle adjustment failed",e.name)
                            else Console.WriteLine("Gun {0} didn't hit the target. Adjust angle to {1}", e.name, angleOfReach e.expectedDistance e.speed)                      
                | _ -> Console.WriteLine("Invalid data")          
     
            Console.ReadKey()
            0
        with
        | :? System.IO.FileNotFoundException ->
            Console.Write("File Not Found. Press a key to exit")
            Console.ReadKey()
            -1
        | _ ->
        Console.Write("Invalid data in the input file")
        Console.ReadKey()
        -1

