Param([string] $configuration = "Release")
$workingDirectory = Get-Location
$zip = "$workingDirectory\build\7za.exe"

# Set location to the Solution directory
(Get-Item $PSScriptRoot).Parent.FullName | Push-Location

# Version
$version = "1.0.0"
$cmsUIVersion = "12.17.1"
$runtimeVersion = "5.0.12"

# CMS dependency
$cmsUIParts = $cmsUIVersion.Split(".")
$cmsUIMajor = [int]::Parse($cmsUIParts[0]) + 1
$cmsUINextMajorVersion = ($cmsUIMajor.ToString() + ".0.0")

# Runtime compilation dependency
$runtimeParts = $runtimeVersion.Split(".")
$runtimeMajor = [int]::Parse($runtimeParts[0]) + 1
$runtimeNextMajorVersion = ($runtimeMajor.ToString() + ".0.0")

#cleanup all by dtk folder which is used by tests
Get-ChildItem -Path out\ -Exclude dtk | Remove-Item -Recurse -Force

#copy assets approval reviews
Copy-Item -Path src\CodeArt.Optimizely.PropertyInheritance\modules\_protected\CodeArt.Optimizely.PropertyInheritance\ClientResources\ -Destination out\CodeArt.Optimizely.PropertyInheritance\$version\ClientResources -recurse -Force
Copy-Item src\CodeArt.Optimizely.PropertyInheritance\modules\_protected\CodeArt.Optimizely.PropertyInheritance\module.config out\CodeArt.Optimizely.PropertyInheritance
((Get-Content -Path out\CodeArt.Optimizely.PropertyInheritance\module.config -Raw).TrimEnd() -Replace '=""', "=`"$version`"" ) | Set-Content -Path out\CodeArt.Optimizely.PropertyInheritance\module.config
Set-Location $workingDirectory\out\CodeArt.Optimizely.PropertyInheritance
Start-Process -NoNewWindow -Wait -FilePath $zip -ArgumentList "a", "CodeArt.Optimizely.PropertyInheritance.zip", "$version", "module.config"
Set-Location $workingDirectory

Write-Output "Start creating package '$configuration'"

# Packaging public packages
dotnet pack -c $configuration /p:PackageVersion=$version /p:CmsUIVersion=$cmsUIVersion /p:CmsUINextMajorVersion=$cmsUINextMajorVersion /p:RuntimeVersion=$runtimeVersion /p:RuntimeNextMajorVersion=$runtimeNextMajorVersion src/PropertyInheritanceSampleSite.sln

Pop-Location
