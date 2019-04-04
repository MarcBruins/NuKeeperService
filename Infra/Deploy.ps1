$resourceGroupName = "NuKeeper"
$functionName = "NuKeeperService"
$location = "westeurope"

Function CreateResourceGroup($rgName, $rgLocation)
{
    $resourceGroup = Get-AzureRmResourceGroup -Name $rgName -ErrorAction SilentlyContinue
    if(!$resourceGroup)
    {
        Write-Host "Resource group '$resourceGroup' does not exist. To create a new resource group, please enter a location.";
        if(!$rgLocation) {
            $rgLocation = Read-Host "resourceGroupLocation";
        }
        Write-Host "Creating resource group '$rgName' in location '$rgLocation'";
        New-AzureRmResourceGroup -Name $rgName -Location $rgLocation
    }
    else
    {
        Write-Host "Using existing resource group '$rgName'";
    }
}

Function DeployFunction($rgName, $fName)
{

    $functionApp = Get-AzureRmWebApp -ResourceGroupName $rgName -Name $fName -Erroraction SilentlyContinue 

   
    if($functionApp)
    {
        Write-Host "Updating function"
    }
    else
    {
        Write-Host "Creating new function"
    }

    [string]$root = $script:PSScriptRoot
    $template = "$root\function.json"

    New-AzureRmResourceGroupDeployment `
        -TemplateFile $template `
        -appName $fName `
        -ResourceGroupName $rgName
}



CreateResourceGroup -rgName $resourceGroupName -rgLocation $location
DeployFunction -rgName $resourceGroupName -fName $functionName