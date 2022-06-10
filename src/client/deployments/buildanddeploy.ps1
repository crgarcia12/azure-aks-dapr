function New-ClientDeployment {
    [CmdletBinding()]
    param(
        $Version = "1.0.10"
    )
    Write-Verbose $Version
    docker build -t crgarakspublicacr.azurecr.io/daprclient:$Version .
    docker push crgarakspublicacr.azurecr.io/daprclient:$Version
    kubectl delete -f deployments/k8s-dapr.yaml
    kubectl apply -f deployments/k8s-dapr.yaml
}

New-ClientDeployment -Verbose
