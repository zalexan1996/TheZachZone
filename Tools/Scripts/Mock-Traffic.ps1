
while ($true)
{
    Invoke-WebRequest -Method GET -Uri "http://localhost:8010" -Verbose | Out-Null
    $sleep = (Get-Random -Minimum 1 -Maximum 50) / 1000
    Write-Host "Sleeping for: $sleep"
    Start-Sleep -Seconds $sleep -Verbose
}