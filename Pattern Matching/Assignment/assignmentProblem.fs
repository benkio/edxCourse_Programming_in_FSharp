module assignmentProblem =
(*
 * Assignment Problem on Pattern Matching
 * Staring from the Hands on practice idea and https://en.wikipedia.org/wiki/Trajectory_of_a_projectile formulas
*)
    open System
    open System.IO

    // Usefull types for the assignment

    //(Target x coordinate, Target y coordinate, speed, expected distance, name)
    type Input =
        { x : float
          y : float
          speed : float
          expectedDistance: float
          name : String }

    type Output =
      { distance : float
        hitExpectedDistance : Boolean
        angle2ReachDistance : float
        hitTarget: Boolean
        angle2ReachTarget : float }

// Utilities to calculate stuff and manage inputs

    let gravity = 9.81

    let calculateAngleOfReach distance speed = 0.5 * Math.Asin(((gravity * distance)/Math.Pow(speed,2.0)))

    let calculateDistanceTraveled angle speed  = (Math.Pow(speed,2.0) * Math.Sin(2.0*angle)) / gravity

    let projectleHeightAtDistance distance angle speed = ((distance * (Math.Tan angle)) - (gravity * Math.Pow(distance,2.0)))/(2.0 * Math.Pow((speed * (Math.Cos angle)),2.0))

    let angleToReachTarget x y speed =
      let denominator = gravity * x
      let squareRoot = Math.Pow(speed,4.0) - ( gravity * ((gravity * Math.Pow(x,2.0)) + (2.0*y*Math.Pow(speed,2.0))))
      if (denominator > 0.0 && squareRoot > 0.0 )
        then
          let angle1 = Math.Atan ((Math.Pow(speed,2.0) + squareRoot )/denominator)
          let angle2 = Math.Atan ((Math.Pow(speed,2.0) - squareRoot )/denominator)
          if (angle1 > 0.0) then angle1 else angle2
        else
          raise (System.DivideByZeroException(""))

    let GetFile =
        Console.Write("Enter the full path to the name of the input file: ")
        Console.ReadLine()

//Active Patterns

    let (|ReachDistance|NotReachDistance|) (input:Input) =
      match input with
        | x when x.expectedDistance = calculateDistanceTraveled (calculateAngleOfReach x.expectedDistance x.speed) x.speed -> ReachDistance
        | _ -> NotReachDistance

    let (|ReachTarget|NotReachTarget|) (input:Input) =
      match input with
        | x when x.y = projectleHeightAtDistance x.expectedDistance (calculateAngleOfReach x.expectedDistance x.speed) x.speed -> ReachTarget
        | _ -> NotReachTarget

//Pattern Matching

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
                      yield { x = float values.[0]
                              y = float values.[1]
                              speed = float values.[2]
                              expectedDistance = float values.[3]
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
