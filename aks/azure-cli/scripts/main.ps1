$resourceGroupName ="testrg"
$subscriptionName="Microsoft Azure Sponsorship"
$resourceGroupLocaltion ="west europe"
$clusterName="aksClusterLog"

$acrName="aksacrkhhdLog"
$sku ="Basic"
# Set Azure subscription name
Write-Host "Setting Azure subscription to $subscriptionName"  -ForegroundColor Yellow
az account set --subscription=$subscriptionName

# Create resource group name
Write-Host "Creating resource group $resourceGroupName in region $resourceGroupLocaltion" -ForegroundColor Yellow
az group create `
--name=$resourceGroupName `
--location=$resourceGroupLocaltion `
--output=jsonc

# Provisioning AKS cluster 
Write-Host "Provisioning AKS cluster with default parameters" -ForegroundColor Cyan
& ((Split-Path $MyInvocation.InvocationName) + "\createClusterAKS.ps1") -resourceGroupName   $resourceGroupName -clusterName   $clusterName


# Provisioning AKS cluster 
Write-Host "Provisioning AKS cluster with default parameters" -ForegroundColor Cyan
& ((Split-Path $MyInvocation.InvocationName) + "\createAzureContainerRegistry.ps1") -resourceGroupName   $resourceGroupName  -acrName   $acrName -sku $sku
