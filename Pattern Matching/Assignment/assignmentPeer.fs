open System
open System.IO

// A value for gravity
let g = 9.81
let requiredspeed distancetotravel angle = Math.Sqrt(distancetotravel * g / (Math.Sin(2.0 * angle)))
// We assume the surface is flat.
let distancetraveled speed angle = (Math.Pow(speed, 2.0) * Math.Sin(2.0 * angle)) / g
// Not needed but interesting
let timeofflight speed angle = (distancetraveled speed angle) / (speed * Math.Cos(angle))
let angleofreach (name, distance, expected, speed) = 0.5 * Math.Asin((g * distance) / Math.Pow(speed, 2.0))
let calculateangle (x, y) = Math.Atan(y / x)

let (|IsOnTarget|_|) testcase =
    match testcase with
        | (_, distance, expected, speed) when distance = expected -> Some()
        | _ -> None

let GetHitText(name, distance, expected, speed) = name + " hit its target"

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

        let inputitems =
            [ while not input.EndOfStream do
              let raw = input.ReadLine()
              let values = raw.Split(',')
              yield ((float values.[0], float values.[1]), float values.[2], float values.[3], values.[4]) ]

        let temp =
            [ for item in inputitems ->
                let point, speed, expecteddistance, name = item
                let angle = calculateangle point
                let dt = distancetraveled speed angle
                let reqspeed = requiredspeed expecteddistance angle
                (dt, reqspeed) ]

        let values =
            seq {
                for item in inputitems do
                    let point, speed, expecteddistance, name = item
                    let angle = calculateangle point
                    yield (name, distancetraveled speed angle, expecteddistance, speed)
                    }

        for newitem in values do
            match newitem with
                | IsOnTarget -> Console.WriteLine(GetHitText newitem)
                | (name, distance, expected, speed) ->
            let angle = angleofreach newitem
            Console.WriteLine(name + "needs to have an angle of " + (string angle))
    with _ -> ()
    Console.ReadKey()
    0 // return an integer exit code
