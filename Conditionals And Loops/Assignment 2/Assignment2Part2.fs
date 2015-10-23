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

 [<EntryPoint>]
 let main argv =
   Console.Write("How many person do you want to compute? ")
   let input = Console.ReadLine()
   let numberOfPersons = checkIntValue input
   let mutable output = ""
   for personId = 1 to numberOfPersons do
      printf "Insert the name of the Person %i:" personId
      let personName = Console.ReadLine()
      printf "Insert the age of the Person %i: " personId
      let input = Console.ReadLine()
      let personAge = checkIntValue input
      output <- output + compute personName personAge + Environment.NewLine
   printf "%s" output
   Console.ReadLine |> ignore
   0
