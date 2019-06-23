@echo off

:: Create User Accounts and Set User-Secrets for Intergation Tests
SET username="TestUsername"
SET password="TestPassword"
SET secrets_id="IntergrationTestID123"

NET USER %username% %password% /ADD

dotnet user-secrets set "Username" %username% --id %secrets_id%
dotnet user-secrets set "Password" %password% --id %secrets_id%