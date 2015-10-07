module Assignment2Part1

(*
 * First part of assignment of the conditional ond Loops section
 *)


let rec dosomethingrandom x =
  if x = 0 then 1
  else dosomethingrandom (x - 1) * x
