param([string]$version)

if([System.String]::IsNullOrEmpty($version))
{
	Write-Warning("No version specified")
	Exit
}

Split-Path -Parent $dte.Solution.FileName | cd
C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe /t:Rebuild /p:Configuration=Release "Kinect.ReactiveV2.sln"

nuget pack ".\nugetDrop\Kinect.ReactiveV2\Kinect.ReactiveV2.nuspec" -OutputDirectory ".\nugetDrop\Kinect.ReactiveV2" -Version $version