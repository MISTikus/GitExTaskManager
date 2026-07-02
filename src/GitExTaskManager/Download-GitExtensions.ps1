param(
    [Parameter(Mandatory=$true)]
    [string] $ExtractRootPath,
    [Parameter(Mandatory=$true)]
    [string] $Version,
    [ValidateSet('GitHub','AppVeyor', ignorecase=$False)]
    [string] $Source = "GitHub",
    [ValidateSet('x64','arm64', ignorecase=$False)]
    [string] $Architecture = "x64"
)

$LatestVersionName = "latest";

function Test-LocalCopy 
{
    Param(
        [Parameter(Mandatory=$true, Position=0)]
        [string] $ExtractPath,
        [Parameter(Mandatory=$true, Position=1)]
        [string] $FileName
    )

    $FilePath = [System.IO.Path]::Combine($ExtractPath, $FileName);
    if (Test-Path $FilePath)
    {
        Write-Host "Download '$FileName' already exists.";
        return $true;
    }
    
    return $false;
}

function Find-ArchiveUrl
{
    param (
        [Parameter(Mandatory=$true, Position=0)]
        [string] $Version,
        [Parameter(Mandatory=$true, Position=1)]
        [ValidateSet('GitHub','AppVeyor', ignorecase=$False)]
        [string] $Source,
        [Parameter(Mandatory=$true, Position=2)]
        [ValidateSet('x64','arm64', ignorecase=$False)]
        [string] $Architecture
    )

    Write-Host "Searching for Git Extensions release '$Version' on '$Source' ($Architecture).";
    if ($Source -eq "GitHub")
    {
        return Find-ArchiveUrlFromGitHub -Version $Version -Architecture $Architecture;
    }

    if ($Source -eq "AppVeyor")
    {
        return Find-ArchiveUrlFromAppVeyor -Version $Version -Architecture $Architecture;
    }

    throw "Unable to find download URL for 'Git Extensions $Version'";
}

# Picks the portable .zip asset name matching the requested architecture.
# Preference: an asset whose name carries the '-x64-'/'-arm64-' marker.
# Fallback: when no asset carries any architecture marker (older single-build
# releases) the first portable .zip is returned.
function Select-PortableAssetName
{
    param (
        [Parameter(Mandatory=$true, Position=0)]
        [string[]] $AssetNames,
        [Parameter(Mandatory=$true, Position=1)]
        [string] $Architecture
    )

    $Portable = @($AssetNames | Where-Object {
        $_ -and $_.ToLower().Contains('portable') -and $_.ToLower().EndsWith('.zip')
    })
    if ($Portable.Count -eq 0)
    {
        return $null;
    }

    $ArchMarker = "-$($Architecture.ToLower())-";
    $Matched = @($Portable | Where-Object { $_.ToLower().Contains($ArchMarker) });
    if ($Matched.Count -gt 0)
    {
        return $Matched[0];
    }

    # No asset is tagged with the requested architecture. If none of the
    # portable assets carry any architecture marker at all, treat it as an
    # old single-build release and take the first one.
    $Tagged = @($Portable | Where-Object { $_.ToLower() -match '-(x64|x86|arm64)-' });
    if ($Tagged.Count -eq 0)
    {
        return $Portable[0];
    }

    return $null;
}

function Find-ArchiveUrlFromGitHub
{
    param (
        [Parameter(Mandatory=$true, Position=0)]
        [string] $Version,
        [Parameter(Mandatory=$true, Position=1)]
        [ValidateSet('x64','arm64', ignorecase=$False)]
        [string] $Architecture
    )

    $BaseUrl = 'https://api.github.com/repos/gitextensions/gitextensions/releases';
    $SelectedRelease = $null;
    if ($Version -eq $LatestVersionName)
    {
        $SelectedRelease = Invoke-RestMethod -Uri "$BaseUrl/latest";
        $Version = $SelectedRelease.tag_name;
        Write-Host "Selected release '$($SelectedRelease.name)'.";
    }
    else
    {
        $Releases = Invoke-RestMethod -Uri $BaseUrl;
        foreach ($Release in $Releases)
        {
            if ($Release.tag_name -eq $Version)
            {
                Write-Host "Selected release '$($Release.name)'.";
                $SelectedRelease = $Release;
                break;
            }
        }
    }

    if (!($null -eq $SelectedRelease))
    {
        $AssetNames = @($SelectedRelease.assets | ForEach-Object { $_.name });
        $SelectedName = Select-PortableAssetName -AssetNames $AssetNames -Architecture $Architecture;
        if (!([string]::IsNullOrWhiteSpace($SelectedName)))
        {
            $Asset = $SelectedRelease.assets | Where-Object { $_.name -eq $SelectedName } | Select-Object -First 1;
            Write-Host "Selected asset '$($Asset.name)'.";
            return $Version,$Asset.browser_download_url;
        }
    }

    throw "Unable to find download URL for 'Git Extensions $Version' ($Architecture) on GitHub";
}

function Find-ArchiveUrlFromAppVeyor
{
    param (
        [Parameter(Mandatory=$true, Position=0)]
        [string] $Version,
        [Parameter(Mandatory=$true, Position=1)]
        [ValidateSet('x64','arm64', ignorecase=$False)]
        [string] $Architecture
    )

    $UrlVersion = $Version;
    if ($UrlVersion.StartsWith("v"))
    {
        $UrlVersion = $UrlVersion.Substring(1);
    }

    $UrlBase = "https://ci.appveyor.com/api";

    try
    {
        if ($Version -eq $LatestVersionName)
        {
            $Url = "$UrlBase/projects/gitextensions/gitextensions/branch/master";
        }
        else
        {
            $Url = "$UrlBase/projects/gitextensions/gitextensions/build/$UrlVersion";
        }

        $BuildInfo = Invoke-RestMethod -Uri $Url;
        $Version = "v$($BuildInfo.build.version)";
        $Job = $BuildInfo.build.jobs[0];
        if ($Job.Status -eq "success")
        {
            $JobId = $Job.jobId;
            Write-Host "Selected build job '$JobId'.";

            $AssetsUrl = "$UrlBase/buildjobs/$JobId/artifacts";
            $Assets = Invoke-RestMethod -Method Get -Uri $AssetsUrl;
            $FileNames = @($Assets | Where-Object { $_.type -and $_.type.ToLower() -eq 'zip' } | ForEach-Object { $_.FileName });
            $SelectedFileName = Select-PortableAssetName -AssetNames $FileNames -Architecture $Architecture;
            if (!([string]::IsNullOrWhiteSpace($SelectedFileName)))
            {
                Write-Host "Selected asset '$SelectedFileName'.";
                return $Version,($AssetsUrl + "/" + $SelectedFileName);
            }
        }
    }
    catch
    {
        if (!($_.Exception.Response.StatusCode -eq 404))
        {
            throw;
        }
    }

    throw "Unable to find download URL for 'Git Extensions $Version' ($Architecture) on AppVeyor";
}

function Get-Application 
{
    param (
        [Parameter(Mandatory=$true, Position=0)]
        [string] $ArchiveUrl,
        [Parameter(Mandatory=$true, Position=1)]
        [string] $ExtractPath,
        [Parameter(Mandatory=$true, Position=2)]
        [string] $FileName,
        [Parameter(Mandatory=$true, Position=3)]
        [ValidateSet('GitHub','AppVeyor', ignorecase=$False)]
        [string] $Source
    )
    
    if (!(Test-Path $ExtractPath))
    {
        New-Item -ItemType directory -Path $ExtractPath | Out-Null;
    }

    $FilePath = [System.IO.Path]::Combine($ExtractPath, $FileName);

    if ($Source -eq "AppVeyor") 
    {
        $ExtractPath = [System.IO.Path]::Combine($ExtractPath, "GitExtensions");
    }

    Write-Host "Downloading '$ArchiveUrl'...";

    Invoke-WebRequest -Uri $ArchiveUrl -OutFile $FilePath;
    Expand-Archive $FilePath -DestinationPath $ExtractPath -Force;
    
    Write-Host "Application extracted to '$ExtractPath'.";
}

function Get-ZipFileName {
    param (
        [Parameter(Mandatory=$true, Position=0)]
        [string] $Version
    )
    
    return "GitExtensions-$Version.zip";
}


Push-Location $PSScriptRoot;
try 
{
    $ExtractRootPath = Resolve-Path $ExtractRootPath;
    Write-Host "Extraction root path is '$ExtractRootPath'.";

    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12;

    if (!($Version -eq $LatestVersionName)) 
    {
        $FileName = Get-ZipFileName -Version $Version;
        if (Test-LocalCopy -ExtractPath $ExtractRootPath -FileName $FileName)
        {
            exit 0;
        }
    }
    
    $SelectedVersion,$DownloadUrl = Find-ArchiveUrl -Version $Version -Source $Source -Architecture $Architecture;
    if ($Version -eq $LatestVersionName) 
    {
        $FileName = Get-ZipFileName -Version $SelectedVersion;
        if (Test-LocalCopy -ExtractPath $ExtractRootPath -FileName $FileName)
        {
            exit 0;
        }
    }

    Get-Application -ArchiveUrl $DownloadUrl -ExtractPath $ExtractRootPath -FileName $FileName -Source $Source;
}
catch 
{
    Write-Host $_.Exception -ForegroundColor Red;
    exit -1;
}
finally 
{
    Pop-Location;
}