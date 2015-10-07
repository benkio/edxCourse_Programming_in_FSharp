module Assignment2Part2

(*
 *
 * Second part of assignment of the conditional ond Loops section
 *
 *)
 open System

 let checkIntValue (input : string) : int =
   if not (String.IsNullOrEmpty input)
      then
        let canConvert, inputValue = Int32.TryParse(input)
        if canConvert || inputValue > 0
        then inputValue
        else 0
      else 0

 let rec compute name age =
   let ageToString = string age
   let outputPersonDetails = "PersonName: " + name + " PersonAge: " + ageToString + " "
   if age >= 20
      then outputPersonDetails + "Field: Person " + name + " is no longer a teenager"
      elif age < 13
         then if age > 0
                 then outputPersonDetails + "Field: Person " + name + " is a kid"
                 else outputPersonDetails + "Input Invalid - cannot compute correctly"
         else outputPersonDetails + "Field: Person " + name + " is a teenager"
