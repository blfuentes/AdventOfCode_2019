open System.IO

let filepath = __SOURCE_DIRECTORY__ + @"../../test_input_00.txt"

type reactElement = { amount: int; name: string }

let extractTransformation(reaction: string) =
    let operators = reaction.Split('=')
    let componentsPart = operators.[0].Split(',')
    let resultPart = operators.[1].Replace("> ", "").Split(' ')
    let result = { amount = int(resultPart.[0]); name = resultPart.[1] }
    let components = seq {
        for comp in componentsPart do
            yield { amount = int(comp.Trim().Split(' ').[0]); name = comp.Trim().Split(' ').[1] }
    }
    (components, result)

let transformations = File.ReadAllLines(filepath) |> Seq.map extractTransformation

