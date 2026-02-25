# UTF-8 BOM 없이 작성
param(
    [string]$InputCsv = "config_analysis.csv"
)

if (-not (Test-Path $InputCsv)) {
    Write-Error "File not found: $InputCsv"
    exit 1
}

$data = Import-Csv $InputCsv -Encoding UTF8

$configOut = @()
$koOut = @()
$enOut = @()

$grouped = $data | Group-Object Category

foreach ($group in $grouped) {
    $cat = $group.Name

    $configOut += "// === $cat ===`n"
    $koOut += "// === $cat ===`n"
    $enOut += "// === $cat ===`n"

    foreach ($item in $group.Group) {
        $varName = $item.ConfigVarName
        $catName = $item.CategoryName
        $keyName = $item.ConfigKeyName
        $enText = $item.EnglishText
        $locKey = $item.LocalizationKey

        # Simple Korean translation (just copy English for now, will manually translate)
        $koText = $enText

        $configOut += "$varName = SkillTreeConfig.BindServerSync(config, `"$catName`", `"$keyName`", ..., SkillTreeConfig.GetConfigDescription(`"$locKey`"));`n"
        $koOut += "[`"$locKey`"] = `"$koText`",`n"
        $enOut += "[`"$locKey`"] = `"$enText`",`n"
    }

    $configOut += "`n"
    $koOut += "`n"
    $enOut += "`n"
}

# Save without BOM
$utf8NoBom = New-Object System.Text.UTF8Encoding($false)
[System.IO.File]::WriteAllText("$PSScriptRoot\config_changes.txt", ($configOut -join ""), $utf8NoBom)
[System.IO.File]::WriteAllText("$PSScriptRoot\korean_keys_template.txt", ($koOut -join ""), $utf8NoBom)
[System.IO.File]::WriteAllText("$PSScriptRoot\english_keys.txt", ($enOut -join ""), $utf8NoBom)

Write-Host "Done! $($data.Count) keys processed" -ForegroundColor Green
Write-Host "  config_changes.txt" -ForegroundColor Cyan
Write-Host "  korean_keys_template.txt (needs translation)" -ForegroundColor Yellow
Write-Host "  english_keys.txt" -ForegroundColor Cyan
