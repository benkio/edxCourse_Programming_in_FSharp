#r "Assignment2Part2.dll"

open System
open Assignment2Part2



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
