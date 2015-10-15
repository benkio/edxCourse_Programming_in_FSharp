module TypePractice =

    open System

    type Person = {
        name : String
        age : int
        id : int
    }

    let GetUser username userage = {
        name = username
        age = userage
        id = username.Length * userage 
    }
                       
    [<EntryPoint>]    
    let main argv =
        Console.WriteLine("Enter the main users name: ")
        let name = Console.ReadLine()
        Console.WriteLine("Enter the main users age: ")
        let age = Console.ReadLine()
        let mainuser = GetUser name (int age)

        let people =
            [ let mutable run = true
              while run do
                  Console.WriteLine("Do you want to add more people (y/n)? ")
                  if Console.ReadLine().ToLower() = "y" then
                      Console.WriteLine("Enter the users name: ")
                      let name = Console.ReadLine()
                      Console.WriteLine("Enter the users age: ")
                      let age = Console.ReadLine()
                      yield GetUser name (int age)
                  else run <- false ]
        
        let sameid =
            seq {
                for x in people do
                    if x.id = mainuser.id then yield x
            }

        for x in sameid do
            Console.WriteLine(x.name)
        Console.ReadKey()
        0 // return an integer exit code

    