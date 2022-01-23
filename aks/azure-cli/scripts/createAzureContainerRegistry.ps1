Param(

    [parameter(Mandatory=$true)]
    [string]$resourceGroupName="aksP4ResourceGroupLog",
    [string]$acrName="logcornerAcrDakareAzure",
    [parameter(Mandatory=$false)]
    [string]$sku='Basic'
    
)

# Create AKS cluster
Write-Host "Creating azure container registry $acrName with resource group $resourceGroupName in region $resourceGroupLocaltion" -ForegroundColor Yellow
az acr create `
--resource-group=$resourceGroupName `
--name=$acrName `
--sku sku `
--output=jsonc

Write-Host "Successfully created azure container registry $acrName with kubernetes version $kubernetesVersion and $workerNodeCount node(s)" -ForegroundColor Green