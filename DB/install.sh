#!/bin/bash
source ./DB/build.env

wget "https://repo1.maven.org/maven2/org/flywaydb/flyway-commandline/${FLYWAY_VERSION}/flyway-commandline-${FLYWAY_VERSION}-linux-x64.tar.gz"
tar -xvzf "flyway-commandline-${FLYWAY_VERSION}-linux-x64.tar.gz"

mv ./DB/sql "./flyway-${FLYWAY_VERSION}/sql/"