#!/bin/bash

source ./DB/build.env

ls -R "./flyway-${FLYWAY_VERSION}/"

"./flyway-${FLYWAY_VERSION}/flyway" migrate -X -url=jdbc:mysql://$(jq ".DBEndpoint" "${CODEBUILD_SRC_DIR_StackOutput}/StackOutput.json" -r)