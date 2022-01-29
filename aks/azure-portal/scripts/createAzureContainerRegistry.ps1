Param(
    [parameter(Mandatory=$false)]
    [string]$resourceGroupName,
    [parameter(Mandatory=$true)]
    [string]$acrName,
    [parameter(Mandatory=$true)]
    [string]$sku
    )

# Create AKS cluster
Write-Host "Creating azure container registry $acrName with resource group $resourceGroupName in region $resourceGroupLocaltion" -ForegroundColor Yellow
az acr create `
--resource-group=$resourceGroupName `
--name=$acrName `
--sku $sku `
--output=jsonc

Write-Host "Successfully created azure container registry $acrName with kubernetes version $kubernetesVersion and $workerNodeCount node(s)" -ForegroundColor Green