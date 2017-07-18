#I "packages/.build/FAKE/tools"
#r "FakeLib.dll"

open Fake
open Fake.DotNetCli

let projectName = "FsUnit.xUnit.Typed"
let solutionFile = projectName |> sprintf "%s.sln"
let buildDir = "./.build"
let repoDir = __SOURCE_DIRECTORY__

Target "Clean" (fun _ -> CleanDir buildDir)

Target "Restore" (fun _ -> Restore id)

Target "Build" (fun _ -> Build id)

Target "Test" (fun _ ->
    !!"tests/**/*.??proj"
    |> Seq.iter (fun p -> Test (fun o -> { o with Project = p })))

Target "Release" (fun _ ->
    Paket.Pack (fun p ->
        { p with
            BuildPlatform = "AnyCPU"
            OutputPath = buildDir </> "nugets" }))

"Restore" ==> "Build"
"Build" ==> "Test"
"Clean" ?=> "Build"
"Test" ==> "Release"

RunTargetOrDefault "Test"