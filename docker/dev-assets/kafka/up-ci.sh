#!/bin/sh

set -e

COMPOSE_PROJECT_NAME=incoming docker-compose -f ./docker-compose.yml up --build -d