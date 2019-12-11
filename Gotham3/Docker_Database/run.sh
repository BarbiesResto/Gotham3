#!/bin/bash
    ( /opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P '1Secure*Password1' -i /Gotham3/Docker_Database/script.sql
/bin/bash
