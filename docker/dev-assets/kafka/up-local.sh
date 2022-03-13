#!/bin/sh

set -e

export COMPOSE_PROJECT_NAME=efacture-kafka
export DEFAULT_REPLICATION_FACTOR=1
export DEFAULT_NUM_PARTITIONS=1

export OUT_PAYLOAD_TOPIC_NAME="PayloadOutgoing"
export INC_PAYLOAD_TOPIC_NAME="PayloadIncoming"

# path to julie templates
outgoingKafkaPath="" 
incomingKafkaPath=""

rm -rf ./outgoing-julie
rm -rf ./incoming-julie
rm -rf ./outgoing-schemas
rm -rf ./incoming-schemas

mkdir ./outgoing-julie
cp ./local.properties ./outgoing-julie
sed -E 's/__(([^_]|_[^_])*)__/${\1}/g' "${outgoingKafkaPath}/julie/topology-template.yml" | envsubst > ./outgoing-julie/topology-local.yml
mkdir  ./outgoing-schemas
cp -R "${outgoingKafkaPath}/schemas/." ./outgoing-schemas/

mkdir ./incoming-julie
cp ./local.properties ./incoming-julie
sed -E 's/__(([^_]|_[^_])*)__/${\1}/g' "${incomingKafkaPath}/julie/topology-template.yml" | envsubst > ./incoming-julie/topology-local.yml
mkdir  ./incoming-schemas
cp -R "${incomingKafkaPath}/schemas/." ./incoming-schemas/

docker-compose -f ./docker-compose.yml -f ./docker-compose.local.yml -f ./docker-compose.julie.yml up --build -d