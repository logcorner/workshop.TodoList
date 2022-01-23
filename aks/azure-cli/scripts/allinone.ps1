Param(
    [parameter(Mandatory = $false)]
    [bool]$ProvisionAKSCluster = $false
)
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
& ((Split-Path $MyInvocation.InvocationName) + "\initializeAKS.ps1")
