#!/bin/bash

set -e
ipaddr="`ifconfig | grep "inet " | grep -Fv 127.0.0.1 | awk '{print $2}' | head -n 1`"
export AZURE_COSMOS_EMULATOR_IP_ADDRESS_OVERRIDE=$ipaddr
docker-compose -p cosmos up --detach

echo "Waiting for Cosmos to start..."
until [ `docker logs --tail 1 cosmosdb | tr -d '\r\n' | grep '^Started$'` ]; do printf '.'; sleep 2; done
echo "It's up !"
