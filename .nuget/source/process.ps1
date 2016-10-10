Param(
	[Parameter()]
	[string]$deploy_folder = "..\definitions",
	
	[Parameter()]
	[string]$version = $null, 
	
	[Parameter()]
	[string]$key = $null,
	
	[Parameter()]
	[string]$source = "https://www.nuget.org/api/v2/package",
	
	[Parameter()]
	[string]$publish = $false,

	[string]$relativePath = "..\.."
)

New-Item $deploy_folder -ItemType Directory -ErrorAction SilentlyContinue
Remove-Item "$deploy_folder\*.*" -ErrorAction SilentlyContinue

$rootPathResolved = Resolve-Path $relativePath

Write-Host $relativePath
Write-Host $rootPathResolved

gci -Recurse *.nuspec | Where-Object { $_.PSIsContainer -eq $False -and $_.Name -match ".nuspec$" } | % {
	$f = $_.FullName
	$s = $_.Name
	Write-Host "Procssing and Updating: $s ($f)";
	Copy-Item -Force $f $deploy_folder
	
	#Write-Host "Reading XML"
	[string]$xf = (Resolve-Path "$deploy_folder\$s").Path
	[xml]$xmlContent = Get-Content -Path $xf
	
	if ($version) {
		Write-Host "`tUpdating $xf Version to $version"
		$xmlContent.package.metadata.version = "$version"
		
		foreach($x in $xmlContent.package.metadata.dependencies.group)
		{
			foreach($g in $x.dependency)
			{
				if($g.id.StartsWith("MonoCode."))
				{
					$g.Attributes["version"].Value = $version
				}
			}
		}
		
		foreach($g in $xmlContent.package.metadata.dependencies.dependency)
		{
			if($g.id.StartsWith("MonoCode."))
			{
				$g.Attributes["version"].Value = $version
			}
		}
	}
	
	$xmlContent.package.files.file | % {
		$s1 = $_.src.Replace($rootPathResolved, $relativePath)
		$_.SetAttribute("src", $s1)
		#Write-Host $_.src
	}
	
	#Write-Host "Updating XML File: $xf"
	
	$xmlContent.Save($xf)
	
	$nugetPath = "$($rootPathResolved)\.nuget\nuget.exe "
	
	Write-Host "`tConverting nuget package." -ForegroundColor Green
	iex "$($nugetPath) pack $($xf) -OutputDirectory ..\deploy" 	
	
	if($publish -eq $true -and $key -ne $null)
	{
		$nugetPackage = (Resolve-Path "..\deploy\$($xmlContent.package.metadata.id).$($xmlContent.package.metadata.version).nupkg")
		#Write-Host $nugetPackage -ForegroundColor Cyan
		Write-Host "`tUploading $($nugetPackage) to Nuget." -ForegroundColor Cyan
		iex "$($nugetPath) push $($nugetPackage) -Source $source -ApiKey $($key)"
	}
}

