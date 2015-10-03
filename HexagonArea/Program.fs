module Program

  open System
(*
 * Second application Week 1 of the course
 * Hexagon area calculation.
 *)

  let hexArea t : float =
   (3.0 * Math.Sqrt(3.0) / 2.0) * Math.Pow(t,2.0)

  let rad2deg r : float = r * (180.0 / 3.14)
  let deg2rad d : float = d * (3.14 / 180.0)

  [<EntryPoint>]
  let main argv =
    Console.WriteLine("insert the legth of a hexagon side: ")
    let value = Console.ReadLine()
    let f = float value
    //Execute calc
    let calc = hexArea f
    // Display on screen - Side Effect
    printf "%f" calc
    0
