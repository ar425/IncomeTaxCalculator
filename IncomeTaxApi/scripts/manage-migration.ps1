# manage-migration.ps1
param()

$migrationsOutput = "../Migrations"
$startupProj = "../IncomeTaxApi.csproj"

if (-not (Test-Path $startupProj)) {
    Write-Host "✖  Cannot find project: $startupProj" -ForegroundColor Red; exit 1
}

function Show-Menu {
    Write-Host "`nEF Core Migration Manager"
    Write-Host "------------------------------------------"
    Write-Host "1. Create a new migration"
    Write-Host "2. Remove the last migration"
    Write-Host "3. Update database"
    Write-Host "4. Exit`n"
}

do {
    Show-Menu
    $choice = Read-Host "Choose an option (1-4)"
    switch ($choice) {
        "1" {
            $migrationName = Read-Host "Enter migration name"
            if ([string]::IsNullOrWhiteSpace($migrationName)) {
                Write-Host "Migration name cannot be empty." -ForegroundColor Red
            } else {
                dotnet ef migrations add $migrationName --project $startupProj
                if ($LASTEXITCODE -eq 0) {
                    Write-Host "Migration '$migrationName' created successfully." -ForegroundColor Green
                } else {
                    Write-Host "Error creating migration." -ForegroundColor Red
                }
            }
        }
        "2" {
            $confirm = Read-Host "Are you sure you want to remove the last migration? (y/n)"
            if ($confirm -eq "y") {
                dotnet ef migrations remove --project $startupProj
                if ($LASTEXITCODE -eq 0) {
                    Write-Host "Last migration removed." -ForegroundColor Yellow
                } else {
                    Write-Host "Error removing migration." -ForegroundColor Red
                }
            }
        }
        "3" {
            dotnet ef database update --project $startupProj
            if ($LASTEXITCODE -eq 0) {
                Write-Host "Database updated successfully." -ForegroundColor Green
            } else {
                Write-Host "Error updating database." -ForegroundColor Red
            }
        }
        "4" { Write-Host "Good‑bye!"; break }
        default { Write-Host "Invalid option." -ForegroundColor Yellow }
    }
} while ($true)