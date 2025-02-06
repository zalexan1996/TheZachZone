$server = "https://localhost:8001"
$swaggerJsonUri = "swagger/v1/swagger.json"

$uri = "$server/$swaggerJsonUri"

$openApi = Invoke-RestMethod -Method GET -Uri $uri