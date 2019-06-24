@echo off

:: Create User Accounts and Set User-Secrets for Intergation Tests
SET username="TestUsername"
SET secrets_id="IntergrationTestID123"

NET USER %username% /DELETE

dotnet user-secrets remove "Username" --id %secrets_id%
dotnet user-secrets remove "Password" --id %secrets_id%