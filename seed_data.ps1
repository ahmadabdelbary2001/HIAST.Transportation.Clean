# Seed Database Script
$baseUrl = "http://localhost:5066/api"
$jsonPath = "./seed_data.json"

if (!(Test-Path $jsonPath)) {
    Write-Error "seed_data.json not found!"
    exit
}

# Ensure we read the file with UTF-8 encoding
$jsonContent = Get-Content $jsonPath -Raw -Encoding UTF8
$data = $jsonContent | ConvertFrom-Json

# 1. Seed Buses
Write-Host "Seeding Buses..."
foreach ($bus in $data.buses) {
    $body = $bus | ConvertTo-Json -Compress
    $bodyBytes = [System.Text.Encoding]::UTF8.GetBytes($body)
    Invoke-RestMethod -Uri "$baseUrl/Bus" -Method Post -Body $bodyBytes -ContentType "application/json; charset=utf-8"
}

# 2. Seed Drivers
Write-Host "Seeding Drivers..."
foreach ($driver in $data.drivers) {
    $body = $driver | ConvertTo-Json -Compress
    $bodyBytes = [System.Text.Encoding]::UTF8.GetBytes($body)
    Invoke-RestMethod -Uri "$baseUrl/Driver" -Method Post -Body $bodyBytes -ContentType "application/json; charset=utf-8"
}

# 3. Seed Employees
Write-Host "Seeding Employees..."
foreach ($employee in $data.employees) {
    $body = $employee | ConvertTo-Json -Compress
    $bodyBytes = [System.Text.Encoding]::UTF8.GetBytes($body)
    Invoke-RestMethod -Uri "$baseUrl/Employee" -Method Post -Body $bodyBytes -ContentType "application/json; charset=utf-8"
}

# 4. Seed Lines
Write-Host "Seeding Lines..."
foreach ($line in $data.lines) {
    $body = $line | ConvertTo-Json -Compress
    $bodyBytes = [System.Text.Encoding]::UTF8.GetBytes($body)
    Invoke-RestMethod -Uri "$baseUrl/Line" -Method Post -Body $bodyBytes -ContentType "application/json; charset=utf-8"
}

# 5. Seed Stops
Write-Host "Seeding Stops..."
foreach ($stop in $data.stops) {
    $body = $stop | ConvertTo-Json -Compress
    $bodyBytes = [System.Text.Encoding]::UTF8.GetBytes($body)
    Invoke-RestMethod -Uri "$baseUrl/Stop" -Method Post -Body $bodyBytes -ContentType "application/json; charset=utf-8"
}

# 6. Seed Subscriptions
Write-Host "Seeding Subscriptions..."
foreach ($sub in $data.subscriptions) {
    $body = $sub | ConvertTo-Json -Compress
    $bodyBytes = [System.Text.Encoding]::UTF8.GetBytes($body)
    Invoke-RestMethod -Uri "$baseUrl/LineSubscription" -Method Post -Body $bodyBytes -ContentType "application/json; charset=utf-8"
}

Write-Host "Seeding Complete!"
