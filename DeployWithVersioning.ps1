$namespace = "garage"
$hostname=""
$url = "http://buildversionsapi.local:8080"
$curl = "curl.exe"
$configuration = "production"
$deploy = "deploy"
$kubeseal = "C:/Apps/kubeseal/kubeseal"

$alive = &${curl} -s "${url}/Ping" -H "accept: text/plain"
if($alive -ne "pong")
{
	"You need to do an initial deploy of BuildVersionsApi"
	"Please run InitBuildVersion.ps1"
	return
}

foreach($name in @(
	"doorworker", 
	"garageworker",
	"lightworker",
	"remoteapi"
))
{
	$buildVersion = $null
	$buildVersion = &${curl} -s "${url}/buildversions/GetVersionByName/${name}" | ConvertFrom-Json
	$semanticVersion = $buildVersion.semanticVersion

	if([string]::IsNullOrEmpty($semanticVersion)) 
	{
		"Could not connect to buildversionsapi"
		"Please check that BuildVersionsApi is working correctly in your Kubernetes"
		return
	}

	"Current deploy: ${env:REGISTRYHOST}/${name}:${semanticVersion}"

	cd ./${deploy}/${name}

	kustomize edit set image "${env:REGISTRYHOST}/${name}:${semanticVersion}"

	if(Test-Path -Path ./secrets/*)
	{
		"Creating secrets"
		kubectl create secret generic ${name}-secret --output json --dry-run=client --from-file=./secrets |
			&${kubeseal} -n "${namespace}" --controller-namespace kube-system --format yaml > "secret.yaml"
	}

	cd ../..

	kubectl apply -k ./${deploy}/${name}
	
	#Restore secret.yaml and kustomization.yaml since this script alters them temporary
	#if([string]::IsNullOrEmpty($env:AGENT_NAME) -and [string]::IsNullOrEmpty($hostname))
	if([string]::IsNullOrEmpty($env:AGENT_NAME))
	{
		if(Test-Path -Path ${deploy}/${name}/secret.yaml)
		{
			git checkout ${deploy}/${name}/secret.yaml
		}
		git checkout ${deploy}/${name}/kustomization.yaml
	}
}
