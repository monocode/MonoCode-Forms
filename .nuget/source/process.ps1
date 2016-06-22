Param(
	[Parameter()]
	[string]$deploy_folder = "..\definitions",
	
	[Parameter()]
	[string]$version = $null, # 2.2.000-pre02

	[string]$relativePath = "..\.."
)

New-Item $deploy_folder -ItemType Directory -ErrorAction SilentlyContinue
Remove-Item "$deploy_folder\*.*" -ErrorAction SilentlyContinue

$rootPathResolved = Resolve-Path $relativePath

gci -Recurse *.nuspec | Where-Object { $_.PSIsContainer -eq $False -and $_.Name -match ".nuspec$" } | % {
	$f = $_.FullName
	$s = $_.Name
	Write-Host "`tProcssing and Updating: $s";
	Copy-Item -Force $f $deploy_folder
	
	#Write-Host "Reading XML"
	[string]$xf = (Resolve-Path "$deploy_folder\$s").Path
	[xml]$xmlContent = Get-Content -Path $xf
	
	if ($version) {
		Write-Host "`t`tUpdating Version to $version"
		$xmlContent.package.metadata.version = "$version"
	}
	
	$xmlContent.package.files.file | % {
		$s1 = $_.src.Replace($rootPathResolved, $relativePath)
		$_.SetAttribute("src", $s1)
		#Write-Host $_.src
	}
	
	#Write-Host "Updating XML File: $xf"
	$xmlContent.Save($xf)
}

