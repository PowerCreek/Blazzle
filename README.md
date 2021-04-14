# Blizzle3

Step 1
  Clone project
  
Step 2
  cd into blozor dir
  >cd [..]blozor
  
Step 3
  Run electronize start
  >electronize start
  
  This takes a while. Perhaps a few seconds. It is installing node.js at the obj directory.
  
Step 4
  
  Be sure to remove the Debug config from the project unless you really need it.
  
  Use the Run/Publish config for Blazoop to install into the blozor working directory, which is
  
  Project: Blazoop
  Target Location: E:/RiderProjects/BlazorShield/Blizzle4-test/blozor/obj/Host/bin.
  Deployment mode: Self-Contained
  Target runtime: win-x64
  
  Publish the blazoop project.
  
Step 5
  Run Blozor. It should display the blazor wasm item we just published.
  
Step 6
  cd into the solution directory above blozor.
  
  Run electronize build command:
  > electronize build /target win10-x64 /PublishSingleFile false /PublishReadyToRun false
  
  Once this happens, you should have a one click installer and an exe to check out your application.
  The installer installs into the OS by way of official installation. So the item would appear in the start menu and programs directory.
  
