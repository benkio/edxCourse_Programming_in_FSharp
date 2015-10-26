// NB. The problem statement is somewhat unclear for this assignment.
// A number of assumptions have been made:
//  1. We consider the "target" to be the expected distance,
//  2. TargetX and TargetY tare "where to aim" to reach that distance.
//  3. We output corrections for the angle to aim.

open System
open System.IO

// Gravity is a global constant
let GRAVITY = 9.81

// Float tolerance for equivalence
let EPSILON = 1e-16

// Record describing a projectile
type Projectile = 
    {   TargetX : float
        TargetY : float
        Speed : float
        ExpectedDistance : float
        Name : string   }

// Angle between two points in Cartesian space
let Angle x y = atan (y / x)

// Distance travelled given a speed and an angle
let TravelDistance speed x y =
    pown speed 2 * sin(2.0 * Angle x y) / GRAVITY

// Angle required to travel a given distance
let AngleOfReach speed distance =
    asin ( GRAVITY * distance / pown speed 2) / 2.0

// Distance travelled by a projectile record
let ActualDistance projectile =
    TravelDistance projectile.Speed projectile.TargetX projectile.TargetY

// Maximum distance travelled by a projectile record
// is found at pi / 4 = 45 degrees, i.e. atan 1.0 1.0.
let MaximumDistance projectile =
    TravelDistance projectile.Speed 1.0 1.0

// Multiple-case active pattern to determine if a target can be reached.
let (|Reach|Far|Short|) difference =
    if EPSILON > abs difference then    Reach 
    elif difference > 0.0       then    Far
    else                                Short

// Whether or not a projectile will travel its expected distance
let TravelsExpectedDistance projectile =
    match projectile.ExpectedDistance - ActualDistance projectile with
    | Reach -> true | Far -> false | Short -> false

// Parameterized active expressions to determine if a target
// can be reached at a given speed and angle.
// Actual angle is exactly correct
let (|Exact|_|) target actual =
    if abs(target - actual) < EPSILON then Some(0.0) else None

// Actual angle is too high
let (|High|_|) target actual =
    match EPSILON + target - actual with
    | adjustment when adjustment > 0.0 -> Some(adjustment)
    | _ -> None

// Actual angle is too low
let (|Low|_|) target actual =
    match EPSILON + target - actual with
    | adjustment when adjustment < 0.0 -> Some(adjustment)
    | _ -> None

// Angle to compensate to reach the expected distance
// A correction of NaN means "unreachable," i.e.,
// d^2 * sin(2 * pi / 2) / GRAVITY < expectedDistance
let CorrectionAngle projectile =
    let actual  = Angle projectile.TargetX projectile.TargetY
    let target = AngleOfReach projectile.Speed projectile.ExpectedDistance
    if Double.IsNaN(target) then 
        Double.NaN
    else
        match actual with
        | Exact target correction   -> 0.0
        | High  target correction   -> correction
        | Low   target correction   -> correction
        | _                         -> Double.NaN

// Display output of calculations and corrections.
// Steps according to the problem statement:
// 1. Compute the actual distance the projectile will travel
// 2. Compute the amount the flat distance must change to reach the expected distance
// 3. Compute the actual angle required to travel the expected distance
// 4. Compute difference between given and required angle
let AdjustmentString projectile =
    match projectile with
    | Some(p)   ->  match TravelsExpectedDistance p with
                    | true  ->  "No angular adjustment required to reach the expected distance."
                    | false ->  let correction = CorrectionAngle p
                                match Double.IsNaN(correction) with
                                | true  ->  let maxDist = MaximumDistance p
                                            "Impossible to reach expected distance of " + 
                                            string p.ExpectedDistance + " units at the given speed.\n" +
                                            "The maximum distance is " + string maxDist + " units."
                                | false ->  "Adjust angle by " + string correction + 
                                            " to reach the expected distance."
    | None      -> "Cannot compute adjustment: Invalid projectile."

// Read lines from a CSV file into Projectile records
// and calculate adjustment information for each line.
let ProcessInputLines (input : StreamReader) = seq {
    while not input.EndOfStream do
        let raw = input.ReadLine()
        let values = raw.Split(',')
        let p = 
            match values.Length with
            | 5 -> Some({   TargetX = float values.[0]
                            TargetY = float values.[1]
                            Speed   = float values.[2]
                            ExpectedDistance = float values.[3]
                            Name    = values.[4] })
            | _ -> None
        yield p, AdjustmentString p }

// Prompt the user for the name of a CSV input file
let GetInputFile () =
    Console.Write("Enter the path to the Lab 4 input file: ")
    Console.ReadLine()

// Display an error message when an exception occurs
let DisplayErrorMessage (msg : string) (exitCode : int) =
    Console.WriteLine(msg)
    exitCode

// Take input and display correction calculations
[<EntryPoint>]
let main argv = 
    printfn "Program input: %A" argv
    let mutable exitCode = 0
    try
        // NB. The input file must be opened in main when we use a string seq.
        // Otherwise, an ObjectDisposedException occurs.
        use inputFile = new StreamReader(   match argv.Length with
                                        | 0 -> GetInputFile ()
                                        | _ -> argv.[0] )
        for projectile, adjustment in ProcessInputLines inputFile do
            printfn "%A\n%s\n" projectile adjustment
    with
        | :? System.FormatException -> 
                exitCode <- DisplayErrorMessage "Cannot process further: Invalid lines in input stream." -1
        | :? System.IO.FileNotFoundException ->
                exitCode <- DisplayErrorMessage "The specified file does not exist." -2
        | :? System.IO.DirectoryNotFoundException ->
                exitCode <- DisplayErrorMessage "The specified directory does not exist." -3
        | _ ->  exitCode <- DisplayErrorMessage "An unhandled exception occured." -4
    Console.WriteLine("Press any key to exit.")
    Console.ReadKey() |> ignore
    exitCode